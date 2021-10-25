using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    [RequireComponent(typeof(Image))]
    public class ButtonHelper : MonoBehaviour {
        public int Number;
        public string LevelKey;
        [SerializeField] private Color _starBase = Color.gray;
        [SerializeField] private Color _starAchieved = Color.yellow;
        [SerializeField] private List<SpriteRenderer> _stars = new List<SpriteRenderer>();

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void UpdateScores()
        {
            int score = PlayerPrefs.GetInt(LevelKey);
            if (score > 0) {
                for (int i = 0; i < score; i++) {
                    _stars[i].color = _starAchieved;
                }
            }
            for (int i = score; i < _stars.Count; i++) {
                _stars[i].color = _starBase;
            }
        }

        public void OnClick()
        {
            GameController.Instance.SelectLevel(Number);
            SetState(false);
        }

        public void SetState(bool state)
        {
            _image.raycastTarget = state;
            _image.color = state ? Color.white : Color.green;
        }
    }
}