using UnityEngine;

namespace SuperHexagon.Scripts
{
    public class SH_Hexagon : MonoBehaviour
    {
        [SerializeField] private float _shrinkSpeed = 3f;

        private void Update()
        {
            transform.localScale -= Vector3.one * _shrinkSpeed * Time.deltaTime;
            if (transform.localScale.magnitude < 0.25f) Destroy(gameObject);
        }
    }
}