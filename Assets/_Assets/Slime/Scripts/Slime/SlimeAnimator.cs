using System.Collections;
using UnityEngine;

namespace Slime.Scripts.Slime
{
    public class SlimeAnimator : MonoBehaviour
    {
        private Vector2 _offset;
        private bool _isJumping;
        private bool _idleSwitch;
        private float _timer;

        private SlimeData _data;
        private SpriteRenderer _renderer;

        private void Start()
        {
            _offset = _renderer.transform.localPosition;
        }

        private void Update()
        {
            if (_isJumping) return;
            _timer += Time.deltaTime;
            if (_timer > _data.IdleAnimationTime) {
                _renderer.sprite = _idleSwitch ? _data.Idle : _data.Falling;
                _idleSwitch = !_idleSwitch;
                _timer = 0;
            }
        }

        public void Setup(SlimeData data, SpriteRenderer spriteRenderer)
        {
            _data = data;
            _renderer = spriteRenderer;
        }

        public void Offset(float up)
        {
            Vector2 pos = _offset;
            pos.y = _offset.y + up;
            _renderer.transform.localPosition = pos;
        }

        public void Jump()
        {
            if (_isJumping) return;
            _isJumping = true;
            StartCoroutine(OnJump());
        }

        private IEnumerator OnJump()
        {
            _renderer.sprite = _data.AnticipateJump;
            yield return new WaitForSecondsRealtime(_data.JumpAnticipationTime);
            _renderer.sprite = _data.Jumping;
        }

        public void Land()
        {
            _isJumping = false;
            StartCoroutine(OnLand());
        }

        private IEnumerator OnLand()
        {
            _renderer.sprite = _data.Landing;
            yield return new WaitForSecondsRealtime(0.1f);
            _renderer.sprite = _data.AnticipateJump;
            yield return new WaitForSecondsRealtime(0.15f);
            _renderer.sprite = _data.Idle;
            _timer = 0;
        }
    }
}