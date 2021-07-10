using Assets.Utility.Scripts.FloatRef;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MusicMarbles.Scripts {
    [RequireComponent(typeof(Slider))]
    public class MM_Slider : MonoBehaviour {
        [SerializeField] private FloatVariable _value = null;
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void SetNewValue()
        {
            if (_value != null) _value.SetValue(_slider.value);
        }
    }
}