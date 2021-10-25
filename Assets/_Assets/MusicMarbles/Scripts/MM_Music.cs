using UnityEngine;

namespace MusicMarbles.Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    public class MmMusic : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private MmController _source;
        private float _dist;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _dist = Vector3.Distance(_lineRenderer.GetPosition(0), _lineRenderer.GetPosition(1));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _source.PlayTone(_dist);
        }

        public void SetSource(MmController source)
        {
            _source = source;
        }
    }
}