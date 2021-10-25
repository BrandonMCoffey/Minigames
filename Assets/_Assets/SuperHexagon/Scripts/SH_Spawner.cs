using System.Collections.Generic;
using UnityEngine;

namespace SuperHexagon.Scripts
{
    public class SH_Spawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRate = 1f;
        [SerializeField] private List<GameObject> _hexagonPrefabs = new List<GameObject>();
        private float _nextTimeToSpawn;
        private static float[] _rotationIndex = new[] { 0f, 60f, 120f, 180f, 240f, 300f };

        private void Update()
        {
            if (Time.time < _nextTimeToSpawn) return;

            if (_hexagonPrefabs.Count == 0) {
                Debug.LogError("No objects to spawn");
                return;
            }

            int index = Random.Range(0, _hexagonPrefabs.Count);
            Transform obj = Instantiate(_hexagonPrefabs[index], Vector3.zero, Quaternion.identity).transform;

            index = Random.Range(0, _rotationIndex.Length);
            obj.rotation = Quaternion.Euler(0, 0, _rotationIndex[index]);
            obj.localScale = Vector3.one * 10;

            _nextTimeToSpawn = Time.time + _spawnRate;
        }
    }
}