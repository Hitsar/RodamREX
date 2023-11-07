using Pool;
using UnityEngine;
using Unity.Netcode;

namespace Player.Gun
{
    public class Gun : NetworkBehaviour
    {
        [SerializeField] private NetworkObject _bullet;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _recharge;
        
        [SerializeField] private int _quantity = 1;

        private SpriteRenderer _spriteRenderer;
        private Camera _mainCamera;
        private InputSystem _input;
        private ObjectPool _objectPool;
        private float _time;
        
        private void Start()
        {
            if (!IsOwner) return;
            _mainCamera = Camera.main;
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            _objectPool = new ObjectPool(_bullet, _spawnPoint, _quantity, OwnerClientId);
            
            _input = new InputSystem();
            _input.Enable();
            _input.Player.Shoot.performed += _ => Shoot();
        }

        private void FixedUpdate()
        {
            if (!IsOwner) return;
            Vector3 diference = _mainCamera.ScreenToWorldPoint(_input.Player.GunDirection.ReadValue<Vector2>()) - transform.position;
            float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        
            _time -= Time.fixedDeltaTime;
            _spriteRenderer.flipY = rotateZ >= 90 || rotateZ <= -90;
        }

        private void Shoot()
        {
            if (IsOwner && _time < 0 && _objectPool.TryGetObject(out NetworkObject bullet))
            {
                bullet.transform.position = _spawnPoint.position;
                bullet.transform.rotation = _spawnPoint.rotation;
                bullet.gameObject.SetActive(true);
                _time = _recharge;
            }
        }
    }
}