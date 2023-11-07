using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private PlayerMovement _player;
        private EnemyVFX _vfx;

        private void Start()
        {
            _player = FindObjectOfType<PlayerMovement>();
            _vfx = GetComponent<EnemyVFX>();
        }

        private void FixedUpdate()
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                _player.gameObject.transform.position, 
                _speed * Time.fixedDeltaTime);
            _vfx.Flip(_player);
        }
    }
}