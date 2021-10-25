using Slime.Scripts.Weapon;
using UnityEngine;

namespace Slime.Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private KeyCode _switchWeapon = KeyCode.C;

        [Header("References")]
        [SerializeField] private PlayerMovement _movement = null;
        [SerializeField] private WeaponControl _weaponControl = null;

        private void Update()
        {
            ProcessMovements();
            ProcessWeapons();
        }

        private void ProcessMovements()
        {
            if (_movement == null) return;
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            _movement.SetMoveDirection(new Vector2(moveX, moveY).normalized);
        }

        private void ProcessWeapons()
        {
            if (_weaponControl == null) return;
            if (Input.GetMouseButtonDown(0)) {
                _weaponControl.ActivateWeapon();
            }
            if (Input.GetMouseButtonUp(0)) {
                _weaponControl.DeactivateWeapon();
            }
            if (Input.GetKeyDown(_switchWeapon)) {
                _weaponControl.SwitchWeapon();
            }
        }
    }
}