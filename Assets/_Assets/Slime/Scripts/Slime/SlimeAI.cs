using System.Collections;
using UnityEngine;

namespace Slime.Scripts.Slime
{
    public class SlimeAI : MonoBehaviour
    {
        [Header("Slime Data")]
        public SlimeData SlimeData = null;

        [Header("References")]
        [SerializeField] private SpriteRenderer _renderer = null;
        [SerializeField] private Transform _shadow = null;
        [SerializeField] private Collider2D _collider = null;
        private SlimeAnimator _animator;

        [Header("Other Settings")]
        [SerializeField] private LayerMask _landingMask = 0;
        private const float SlimeHitBoxRadius = 0.1f;

        private float _idleTime;
        private bool _hasDestination;
        private Vector2 _destination;
        private float _jumpRadius;
        private Coroutine _jumpEnumerator;

        private void Awake()
        {
            _animator = GetComponent<SlimeAnimator>();
            if (_animator == null) _animator = gameObject.AddComponent<SlimeAnimator>();
            _animator.Setup(SlimeData, _renderer);

            _idleTime = GetRandom(SlimeData.IdleTimeMinMax);
        }

        private void Update()
        {
            if (SlimeData == null) return;

            _idleTime -= Time.deltaTime;

            if (!_hasDestination) {
                if (_jumpEnumerator != null) StopCoroutine(_jumpEnumerator);
                _jumpEnumerator = null;
                SetDestination();
                _idleTime = GetRandom(SlimeData.IdleTimeMinMax);
            }
            if (_idleTime <= SlimeData.JumpAnticipationTime) {
                _animator.Jump();
            }
            if (_idleTime <= 0 && _jumpEnumerator == null) {
                _jumpEnumerator = StartCoroutine(Jump());
            }
        }

        private static float GetRandom(Vector2 range)
        {
            return Random.Range(range.x, range.y > range.x ? range.y : range.x);
        }

        private void SetDestination()
        {
            int maxIterations = 0;
            while (true) {
                Vector2 jumpDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                Vector2 landingPosition;
                _jumpRadius = GetRandom(SlimeData.JumpRadiusMinMax);
                landingPosition.x = transform.position.x + jumpDirection.x * _jumpRadius;
                landingPosition.y = transform.position.y + jumpDirection.y * _jumpRadius;
                if (Physics2D.OverlapCircle(landingPosition, SlimeHitBoxRadius, _landingMask)) {
                    _destination = landingPosition;
                    _hasDestination = true;
                    break;
                }
                maxIterations++;
                if (maxIterations > 100) {
                    Debug.Log("Warning: No Valid Destination Found");
                    break;
                }
            }
        }

        private IEnumerator Jump()
        {
            Vector2 startPosition = transform.position;
            float jumpTime = _jumpRadius * SlimeData.AirTimeMultiplier;
            float jumpHeight = _jumpRadius * SlimeData.JumpHeightMultiplier;
            float shadowScale = Mathf.Min(jumpHeight * SlimeData.ShadowSizeMultiplier, 1);

            if (_collider != null) _collider.gameObject.SetActive(false);
            for (float t = 0; t < jumpTime; t += Time.deltaTime) {
                float delta = t / jumpTime;
                Vector2 newPosition = transform.position;
                newPosition.x = startPosition.x + delta * (_destination.x - startPosition.x);
                newPosition.y = startPosition.y + delta * (_destination.y - startPosition.y);
                transform.position = newPosition;

                _animator.Offset(Mathf.Sin(delta * Mathf.PI) * jumpHeight);
                if (_shadow != null) {
                    float scale = 1 - Mathf.Sin(delta * Mathf.PI) * shadowScale;
                    _shadow.localScale = new Vector2(scale, scale);
                }
                yield return null;
            }
            if (_collider != null) _collider.gameObject.SetActive(true);

            transform.position = _destination;
            _animator.Offset(0);
            _animator.Land();

            if (_shadow != null) _shadow.localScale = new Vector2(1, 1);

            _hasDestination = false;
            _idleTime = SlimeData.IdleTimeMinMax.y;
        }
    }
}