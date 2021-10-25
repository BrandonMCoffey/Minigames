using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int viewDistance = 600;
    public int seperation = 30;
    public GameObject player;
    public List<GameObject> buildings;
    FastNoise mapNoise = new FastNoise();
    GameObject map;

    void Update()
    {
        DestroyMap();
        LoadMap(Mathf.FloorToInt((player.transform.position.x + player.transform.forward.x * viewDistance / 1.5f) / seperation) * seperation,
        Mathf.FloorToInt((player.transform.position.z + player.transform.forward.z * viewDistance / 1.5f) / seperation) * seperation);
    }

    void DestroyMap()
    {
        Destroy(map);
    }

    void LoadMap(int px, int pz)
    {
        map = new GameObject();
        for (int x = px - viewDistance; x <= px + viewDistance; x += seperation)
        {
            for (int z = pz - viewDistance; z <= pz + viewDistance; z += seperation)
            {
                float heightMap = mapNoise.GetSimplex((x) * 4f, (z) * 4f);
                if (heightMap > 0.2f && heightMap < 0.3f)
                {
                    GameObject newBuilding = Instantiate(buildings[0], new Vector3(x, heightMap * 20f, z), Quaternion.identity);
                    newBuilding.transform.parent = map.transform;
                }
                else if (heightMap > -0.4f && heightMap < -0.3f)
                {
                    GameObject newBuilding = Instantiate(buildings[1], new Vector3(x, heightMap * 20f, z), Quaternion.identity);
                    newBuilding.transform.parent = map.transform;
                }
            }
        }
    }
}
