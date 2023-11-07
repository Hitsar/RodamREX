using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Pool
{
    public sealed class ObjectPool
    {
        private readonly List<NetworkObject> _pool = new List<NetworkObject>();
    
        public ObjectPool(NetworkObject prefab, Transform container, int quantity, ulong id)
        {
            for (int i = 0; i < quantity; i++) 
                _pool.Add(CreateObjectServerRpc(prefab, container, id));
        }

        [ServerRpc]
        private NetworkObject CreateObjectServerRpc(NetworkObject prefab, Transform container, ulong id)
        {
            NetworkObject spawned = Object.Instantiate(prefab, container);
            spawned.SpawnWithOwnership(id);
            spawned.gameObject.SetActive(false);

            return spawned;
        }
    
        public bool TryGetObject(out NetworkObject result)
        {
            result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);
            return result != null;
        }
    }
}