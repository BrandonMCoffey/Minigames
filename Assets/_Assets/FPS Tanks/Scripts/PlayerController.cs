using UnityEngine;

namespace FPS_Tanks.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _playerSpeed = 2f;
        [SerializeField] float _playerRotateSpeed = 10f;
        [SerializeField] float _mouseUpwardsSensitivity = 80f;
        [SerializeField] float _mouseSidewaysSensitivity = 240f;
        [SerializeField] GameObject _bullet = null;

        Transform _camera = null;
        Vector3 _cameraRotation;

        float _forward = 0f;
        float _sideways = 0f;
        float _rotation = 0f;

        Rigidbody _rb = null;

        void Start()
        {
            _rb = this.gameObject.GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
            _camera = this.transform.Find("Camera");
            if (_camera != null)
                _cameraRotation = _camera.rotation.eulerAngles;
            else
                Debug.Log("No Player Camera detected");
        }

        private void Update()
        {
            _forward = Input.GetAxis("Vertical") * _playerSpeed / 50f;
            _sideways = Input.GetAxis("Horizontal") * _playerRotateSpeed / 10f;
            if (_camera != null) {
                MouseLook();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && _bullet != null) {
                GameObject bulletObject = Instantiate(_bullet, this.transform.position + this.transform.forward * 1f + this.transform.up * 0.6f, Quaternion.Euler(this.transform.rotation.eulerAngles));
                BulletControl bulletControl = bulletObject.GetComponent<BulletControl>();
                bulletControl.SetSpeed(20f);
            }
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        void MovePlayer()
        {
            _rotation += _sideways;
            _rb.MovePosition(this.transform.position + this.transform.forward * _forward);
            _rb.MoveRotation(Quaternion.Euler(0f, _rotation, 0f));
        }

        void MouseLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSidewaysSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseUpwardsSensitivity * Time.deltaTime;

            _cameraRotation.x -= mouseY;
            _cameraRotation.y += mouseX;
            _cameraRotation.x = Mathf.Clamp(_cameraRotation.x, -12f, 12f);

            _camera.localRotation = Quaternion.Euler(_cameraRotation);
        }
    }
}