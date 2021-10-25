using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic {
    public class ButtonTrigger : MonoBehaviour {
        [SerializeField] private bool _oneTimePress = false;
        [SerializeField] private List<Gate> _gates = new List<Gate>();

        private int _objectsOnButton;

        private void OnTriggerEnter(Collider other)
        {
            if (_objectsOnButton == 0) Activate();
            _objectsOnButton++;
        }

        private void OnTriggerExit(Collider other)
        {
            if (_oneTimePress) return;
            _objectsOnButton--;
            if (_objectsOnButton == 0) Deactivate();
        }

        private void Activate()
        {
            foreach (Gate gate in _gates) {
                gate.Open();
            }
        }

        private void Deactivate()
        {
            foreach (Gate gate in _gates) {
                gate.Close();
            }
        }
    }
}