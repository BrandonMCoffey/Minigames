using UnityEngine;

namespace Assets.SuperHexagon.Scripts {
    public class SH_Player : MonoBehaviour {
        [SerializeField] private float _moveSpeed = 500f;
        private float _movement;

        private void Update()
        {
            _movement = Input.GetAxisRaw("Horizontal");
        }

        private void FixedUpdate()
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, _movement * Time.fixedDeltaTime * -_moveSpeed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Death");
        }
    }
}