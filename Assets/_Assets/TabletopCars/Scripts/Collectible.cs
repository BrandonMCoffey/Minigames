using UnityEngine;

namespace TabletopCars.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class Collectible : MonoBehaviour
    {
        public int ScoreAmount = 1;
        [SerializeField] private GameObject _art = null;

        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
                Collect();
            }
        }

        private void Collect()
        {
            GameController.Instance.GatherCollectible(ScoreAmount);
            GetComponent<Collider>().enabled = false;
            if (_art != null) {
                _art.SetActive(false);
            }
        }
    }
}