using Unity.Netcode;
using UnityEngine;

namespace Pool
{
    public class Spawner : NetworkBehaviour
    {
        [SerializeField] private NetworkObject _prefabsObject;
        [SerializeField] private Transform[] _spawners;

        [SerializeField] private int _quantity = 1;
        [SerializeField] private float _minSecondForSpawn = 1, _maxSecondForSpawn = 3;
        
        private ObjectPool _objectPool;
        private float _secondForSpawn;
        private float _time;

        private void Start()
        {
            if (!IsServer) return;
            _objectPool = new ObjectPool(_prefabsObject, transform, _quantity, OwnerClientId);
            _secondForSpawn = Random.Range(_minSecondForSpawn, _maxSecondForSpawn);
        }

        private void FixedUpdate()
        {
            if (!IsServer) return;
            _time += Time.fixedDeltaTime;
            
            if (_time >= _secondForSpawn && _objectPool.TryGetObject(out NetworkObject spawnedObject))
            {
                int spawnPointNumber = Random.Range(0, _spawners.Length);

                spawnedObject.gameObject.SetActive(true);
                spawnedObject.transform.position = _spawners[spawnPointNumber].position;
                
                _secondForSpawn = Random.Range(_minSecondForSpawn, _maxSecondForSpawn);
                _time = 0;
            }
        }
    }
}