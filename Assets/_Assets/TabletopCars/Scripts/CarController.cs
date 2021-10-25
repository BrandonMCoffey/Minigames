using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : MonoBehaviour {
        private const float YOffset = 0.35f;

        [SerializeField] private float _moveSpeed = 5;
        [SerializeField] private float _turnSpeed = 5;

        private Rigidbody _rigidbody;
        private bool _hasStarted = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!_hasStarted) {
                if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) {
                    _hasStarted = true;
                    GameController.Instance.StartLevel();
                }
            }
            Move();
            Turn();
        }

        private void Move()
        {
            float moveAmount = Input.GetAxisRaw("Vertical") * _moveSpeed;
            Vector3 move = moveAmount * transform.forward;
            _rigidbody.AddForce(move);
        }

        private void Turn()
        {
            float turnAmount = Input.GetAxisRaw("Horizontal") * _turnSpeed;
            if (_rigidbody.velocity.magnitude < 5) {
                turnAmount *= (_rigidbody.velocity.magnitude + 0.5f) / 5.5f;
            }
            Quaternion turnOffset = Quaternion.Euler(0, turnAmount, 0);
            _rigidbody.MoveRotation(_rigidbody.rotation * turnOffset);
        }

        public void Reload(Transform startingLocation)
        {
            _hasStarted = false;
            Vector3 startingPosition = new Vector3(0, YOffset, 8);
            Quaternion startingRotation = Quaternion.Euler(0, 180, 0);
            if (startingLocation != null) {
                startingPosition = startingLocation.position;
                startingPosition.y = YOffset;
                startingRotation = startingLocation.rotation;
            }
            transform.position = startingPosition;
            transform.rotation = startingRotation;
        }
    }
}