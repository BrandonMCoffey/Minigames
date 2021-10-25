using UnityEngine;
using Utility_Scripts.FloatRef;

namespace MusicMarbles.Scripts
{
    public class MmSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnObject = null;
        [SerializeField] private FloatReference _spawnRate = null;
        private float _previousTimeSpawned;

        private void Update()
        {
            if (Time.time < _previousTimeSpawned + _spawnRate.Value) return;
            Spawn();
            _previousTimeSpawned = Time.time;
        }

        private void Spawn()
        {
            if (_spawnObject == null) return;
            Transform obj = Instantiate(_spawnObject).transform;
            obj.SetParent(transform);
            obj.localPosition = Vector3.zero;
        }
    }
}