using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic {
    public class Gate : MonoBehaviour {
        [SerializeField] private float _animationTimer = 1;
        [SerializeField] private Vector3 _distanceToMove = Vector3.down;

        private Coroutine _animation;

        private Vector3 _closedPosition;

        private void Awake()
        {
            _closedPosition = transform.position;
        }

        public void Open()
        {
            if (_animation != null) {
                StopCoroutine(_animation);
            }
            _animation = StartCoroutine(OpenAnimation());
        }

        private IEnumerator OpenAnimation()
        {
            Vector3 start = transform.position;
            Vector3 goal = _closedPosition + _distanceToMove;
            float t = 0;
            while (t <= _animationTimer) {
                t += Time.fixedDeltaTime;
                transform.position = Vector3.Lerp(start, goal, t);
                yield return new WaitForFixedUpdate();
            }
            transform.position = goal;
        }

        public void Close()
        {
            if (_animation != null) {
                StopCoroutine(_animation);
            }
            _animation = StartCoroutine(CloseAnimation());
        }

        private IEnumerator CloseAnimation()
        {
            Vector3 start = transform.position;
            float t = 0;
            while (t <= _animationTimer) {
                t += Time.fixedDeltaTime;
                transform.position = Vector3.Lerp(start, _closedPosition, t);
                yield return new WaitForFixedUpdate();
            }
            transform.position = _closedPosition;
        }
    }
}