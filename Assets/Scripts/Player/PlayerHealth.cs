using System.Collections;
using Interfaces;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : NetworkBehaviour, IHealthSystem
    {
        [SerializeField] private ushort _health = 5;
        [SerializeField] private byte _revival;
        [SerializeField] private bool _isRevival;
        private int _currentHealth;

        private void OnEnable() => _currentHealth = _health;

        public void TakeDamage(ushort damage = 1)
        {
            _currentHealth -= damage;
            if (_isRevival && _revival > 0) StartCoroutine(Revival());
            else if (_currentHealth <= 0) DieServerRpc();
        }
        
        [ServerRpc]
        private void DieServerRpc()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator Revival()
        {
            WaitForSeconds time = new WaitForSeconds(3);
            
            DieServerRpc();
            yield return time;
            gameObject.SetActive(true);
        }
    }
}