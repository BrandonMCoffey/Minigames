using UnityEngine;

namespace Slime.Scripts.Weapon
{
    public class CaptureGun : Weapon
    {
        [SerializeField] private ParticleSystem _particles = null;

        public override void ActivateWeapon()
        {
            if (_particles != null) _particles.Play();
        }

        public override void DeactivateWeapon()
        {
            if (_particles != null) _particles.Stop();
        }
    }
}