using UnityEngine;

namespace Player.Gun
{
    public class RotateBullet : MonoBehaviour
    {
        private void FixedUpdate() => transform.Rotate(0, 0, 40, Space.Self);
    }
}