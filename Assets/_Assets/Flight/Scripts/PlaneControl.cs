using UnityEngine;

namespace Flight.Scripts
{
    public class PlaneControl : MonoBehaviour
    {
        [SerializeField] private int _speedMultiplier = 40;
        [SerializeField] private float _minSpeed = 20;
        [SerializeField] private float _maxSpeed = 200;
        [SerializeField] private Transform _camera = null;
        [SerializeField] private float _cameraBias = 0.5f;

        private float _speed;

        private void Awake()
        {
            if (_camera == null) {
                _camera = Camera.main.transform;
            }
        }

        private void FixedUpdate()
        {
            Vector3 moveCam = transform.position - transform.forward * 12f + transform.up * 10.0f;
            _camera.position = _camera.position * _cameraBias + moveCam * (1 - _cameraBias);
            _camera.LookAt(transform.position);

            transform.position += transform.forward * Time.deltaTime * _speed;
            _speed -= transform.forward.y * Time.deltaTime * _speedMultiplier;
            if (_speed < _minSpeed) {
                _speed = _minSpeed;
            } else if (_speed > _maxSpeed) {
                _speed = _maxSpeed;
            }
            transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
        }
    }
}