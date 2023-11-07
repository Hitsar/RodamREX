using UnityEngine;

namespace Player
{
    public class PlayerVFX : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void OnMove(Vector2 direction)
        {
            _animator.SetBool("Run", direction.x != 0 || direction.y != 0);
            if (direction.x == 0) return; _spriteRenderer.flipX = direction.x < 0;
        }
    }
}