using UnityEngine;

namespace SuperHexagon.Scripts
{
    public class SH_Camera : MonoBehaviour
    {
        [SerializeField] private float _intervalTime = 3f;
        [SerializeField] private float _moveSpeedMin = 10f;
        [SerializeField] private float _moveSpeedMax = 50f;
        private float _moveSpeed;
        private float _nextInterval;

        private void Start()
        {
            ChangeSpeed();
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * _moveSpeed);

            if (Time.time >= _nextInterval) {
                ChangeSpeed();
                _nextInterval = Time.time + _intervalTime;
            }
        }

        private void ChangeSpeed()
        {
            _moveSpeed = 0f;
            while (Mathf.Abs(_moveSpeed) < _moveSpeedMin) {
                _moveSpeed = Random.Range(-_moveSpeedMax, _moveSpeedMax);
            }
        }
    }
}