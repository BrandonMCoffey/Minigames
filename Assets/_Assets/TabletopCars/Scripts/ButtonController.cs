using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TabletopCars.Scripts
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Button _template = null;

        private List<ButtonHelper> _buttonHelpers;

        private void Awake()
        {
            _buttonHelpers = new List<ButtonHelper>();
        }

        public void AddLevel(int levelNumber, string levelKey)
        {
            if (_template == null) {
                Debug.Log("Error: No template button attached to Level Buttons (" + gameObject.name + ")");
                return;
            }
            Button newButton = Instantiate(_template.gameObject).GetComponent<Button>();
            newButton.transform.SetParent(transform);
            Vector3 pos = newButton.transform.localPosition;
            pos.z = 0;
            newButton.transform.localPosition = pos;
            newButton.transform.localScale = new Vector3(1, 1, 1);
            newButton.transform.localRotation = Quaternion.Euler(0, 0, 0);
            foreach (Transform textObject in newButton.transform) {
                TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();
                if (text != null) {
                    text.text = levelNumber.ToString();
                } else {
                    Text normalText = textObject.GetComponent<Text>();
                    if (normalText != null) {
                        normalText.text = levelNumber.ToString();
                    }
                }
            }
            if (!newButton.GetComponent<ButtonHelper>()) return;
            ButtonHelper helper = newButton.GetComponent<ButtonHelper>();
            helper.Number = levelNumber - 1;
            helper.LevelKey = levelKey;
            newButton.onClick.AddListener(ResetButtons);
            newButton.onClick.AddListener(helper.OnClick);
            _buttonHelpers.Add(helper);
        }

        public void SelectLevel(int levelNumber)
        {
            if (_buttonHelpers == null || _buttonHelpers.Count == 0 || levelNumber >= _buttonHelpers.Count) return;
            ResetButtons();
            _buttonHelpers[levelNumber].SetState(false);
        }

        public void UpdateScores()
        {
            foreach (ButtonHelper helper in _buttonHelpers) {
                helper.UpdateScores();
            }
        }

        public void DestroyTemplate()
        {
            if (_template == null) return;
            Destroy(_template.gameObject);
        }

        public void ResetButtons()
        {
            foreach (ButtonHelper helper in _buttonHelpers) {
                helper.SetState(true);
            }
        }
    }
}