using UnityEngine;

namespace Player
{
    public class PlayerMovement : Unity.Netcode.NetworkBehaviour
    {
        [SerializeField] private float _speed = 5;
        private Rigidbody2D _rigidbody;
        private PlayerVFX _vfx;
        private InputSystem _input;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _vfx = GetComponent<PlayerVFX>();
            if (IsOwner && IsClient)
            {
                _input = new InputSystem();
                _input.Enable();
            }
        }
        
        private void FixedUpdate()
        {
            if (!IsOwner) return;
            Vector2 direction = _input.Player.Move.ReadValue<Vector2>();
            _rigidbody.velocity = direction * _speed;
            _vfx.OnMove(direction);
        }
    }
}