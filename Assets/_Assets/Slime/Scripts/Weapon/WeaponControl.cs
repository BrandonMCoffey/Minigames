using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Slime.Scripts.Weapon
{
    public class WeaponControl : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _weapons = new List<Weapon>();

        private int _activeWeapon;

        private void Start()
        {
            foreach (var obj in _weapons.Select(weapon => weapon.gameObject)) {
                obj.SetActive(false);
            }
            EnableWeapon();
        }

        public void ActivateWeapon()
        {
            _weapons[_activeWeapon].ActivateWeapon();
        }

        public void DeactivateWeapon()
        {
            _weapons[_activeWeapon].DeactivateWeapon();
        }


        public void SwitchWeapon(bool forwards = true)
        {
            _weapons[_activeWeapon].DeactivateWeapon();
            DisableWeapon();
            _activeWeapon++;
            if (_activeWeapon >= _weapons.Count) {
                _activeWeapon = 0;
            }
            EnableWeapon();
            if (Input.GetMouseButton(0)) {
                _weapons[_activeWeapon].ActivateWeapon();
            }
        }

        private void DisableWeapon()
        {
            _weapons[_activeWeapon].gameObject.SetActive(false);
        }

        private void EnableWeapon()
        {
            _weapons[_activeWeapon].gameObject.SetActive(true);
        }
    }
}