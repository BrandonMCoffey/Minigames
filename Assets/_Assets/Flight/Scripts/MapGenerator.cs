using System.Collections.Generic;
using UnityEngine;
using Utility_Scripts.FastNoise;

namespace Flight.Scripts
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private int _viewDistance = 600;
        [SerializeField] private int _separation = 30;
        [SerializeField] private GameObject _player = null;
        [SerializeField] private List<GameObject> _buildings = new List<GameObject>();

        private FastNoise _mapNoise = new FastNoise();
        private GameObject _map;

        private void Update()
        {
            DestroyMap();
            LoadMap(Mathf.FloorToInt((_player.transform.position.x + _player.transform.forward.x * _viewDistance / 1.5f) / _separation) * _separation,
                Mathf.FloorToInt((_player.transform.position.z + _player.transform.forward.z * _viewDistance / 1.5f) / _separation) * _separation);
        }

        private void DestroyMap()
        {
            Destroy(_map);
        }

        private void LoadMap(int px, int pz)
        {
            _map = new GameObject();
            for (int x = px - _viewDistance; x <= px + _viewDistance; x += _separation) {
                for (int z = pz - _viewDistance; z <= pz + _viewDistance; z += _separation) {
                    float heightMap = _mapNoise.GetSimplex((x) * 4f, (z) * 4f);
                    if (heightMap > 0.2f && heightMap < 0.3f) {
                        GameObject newBuilding = Instantiate(_buildings[0], new Vector3(x, heightMap * 20f, z), Quaternion.identity);
                        newBuilding.transform.parent = _map.transform;
                    } else if (heightMap > -0.4f && heightMap < -0.3f) {
                        GameObject newBuilding = Instantiate(_buildings[1], new Vector3(x, heightMap * 20f, z), Quaternion.identity);
                        newBuilding.transform.parent = _map.transform;
                    }
                }
            }
        }
    }
}