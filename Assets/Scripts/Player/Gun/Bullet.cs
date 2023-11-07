using Interfaces;
using Unity.Netcode;
using UnityEngine;

namespace Player.Gun
{
    public class Bullet : NetworkBehaviour
    {
        [SerializeField] private float _speed = 8;
        [SerializeField] private ushort _damage = 1;

        private void FixedUpdate() =>  transform.Translate(Vector2.right * _speed * Time.fixedDeltaTime);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IHealthSystem enemy)) enemy.TakeDamage(_damage);
            
            gameObject.SetActive(false);
        }
    }
}
