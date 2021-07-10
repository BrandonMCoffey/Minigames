using System.Collections.Generic;
using Assets.Utility.Scripts;
using UnityEngine;

namespace Assets.MusicMarbles.Scripts {
    [RequireComponent(typeof(AudioSource))]
    public class MM_Controller : MonoBehaviour {
        [SerializeField] private float _edgeRadius = 0.04f;
        [SerializeField] private AudioClip _tone = null;
        [SerializeField] private Material _lineRendererMaterial = default;
        private LineRenderer _tempLine;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) {
                _tempLine = new GameObject("Line", typeof(LineRenderer)).GetComponent<LineRenderer>();
                _tempLine.transform.SetParent(transform);
                _tempLine.widthMultiplier = _edgeRadius;
                _tempLine.material = _lineRendererMaterial;
                _tempLine.SetPosition(0, MouseUtils.GetMousePosition2D());
            }
            if (Input.GetMouseButton(0) && _tempLine != null) {
                _tempLine.SetPosition(1, MouseUtils.GetMousePosition2D());
            }
            if (Input.GetMouseButtonUp(0) && _tempLine != null) {
                _tempLine.SetPosition(1, MouseUtils.GetMousePosition2D());
                MM_Music music = _tempLine.gameObject.AddComponent<MM_Music>();
                music.SetSource(this);
                EdgeCollider2D col = _tempLine.gameObject.AddComponent<EdgeCollider2D>();
                col.SetPoints(new List<Vector2> {_tempLine.GetPosition(0), _tempLine.GetPosition(1)});
                col.edgeRadius = _edgeRadius / 2;
                _tempLine = null;
            }
        }

        public void PlayTone(float pitch)
        {
            Debug.Log(pitch / 10);
            _audioSource.pitch = pitch / 10;
            _audioSource.PlayOneShot(_tone);
        }
    }
}