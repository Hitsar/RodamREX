using Interfaces;
using Unity.Netcode;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : NetworkBehaviour, IHealthSystem
    {
        [SerializeField] private ushort _health;
        private NetworkVariable<ushort> _currentHealth;

        private void OnEnable() => _currentHealth.Value = _health;
        
        public void TakeDamage(ushort damage = 1)
        {
            _currentHealth.Value -= damage;
            if (_currentHealth.Value <= 0)
                DieServerRpc();
        }

        [ServerRpc]
        private void DieServerRpc()
        {
            gameObject.SetActive(false);
        }
    }
}