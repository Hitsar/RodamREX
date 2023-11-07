using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyVFX : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Start() => _spriteRenderer = GetComponent<SpriteRenderer>();

        public void Flip(PlayerMovement player) => _spriteRenderer.flipX = player.transform.position.x < transform.position.x;
    }
}