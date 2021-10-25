using UnityEngine;

namespace Slime.Scripts.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        private void OnEnable()
        {
            DeactivateWeapon();
        }

        public abstract void ActivateWeapon();
        public abstract void DeactivateWeapon();
    }
}