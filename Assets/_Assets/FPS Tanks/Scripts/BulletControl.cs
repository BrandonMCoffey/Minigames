using System.Collections;
using UnityEngine;

namespace FPS_Tanks.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletControl : MonoBehaviour
    {
        Rigidbody _rb = null;
        //int bounces = 0;
        //int maxBounces = 1;

        void Awake()
        {
            _rb = this.gameObject.GetComponent<Rigidbody>();
            StartCoroutine(Lifespan(4f));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Environment")) {
                // if (bounces++ >= maxBounces)
                // {
                this.transform.Find("Art").gameObject.SetActive(false);
                this.gameObject.GetComponent<Collider>().enabled = false;
                this.transform.Find("Particles").gameObject.GetComponent<ParticleSystem>().Stop();
                // }
                // Vector3 closestPoint = other.ClosestPoint(this.transform.position);
                // Debug.Log(Vector3.Angle(this.transform.position, closestPoint));
            }
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                Destroy(other.gameObject);
                this.transform.Find("Art").gameObject.SetActive(false);
                this.gameObject.GetComponent<Collider>().enabled = false;
                this.transform.Find("Particles").gameObject.GetComponent<ParticleSystem>().Stop();
            }
        }

        public void SetSpeed(float speed)
        {
            if (_rb != null) {
                _rb.velocity = _rb.transform.forward * speed;
            }
        }

        IEnumerator Lifespan(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(this.gameObject);
        }
    }
}