using UnityEngine;

namespace Slime.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5;

        private Vector2 _moveDirection;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void SetMoveDirection(Vector2 dir)
        {
            _moveDirection = dir;
        }

        private void Move()
        {
            _rb.velocity = _moveDirection * _moveSpeed;
        }
    }
}