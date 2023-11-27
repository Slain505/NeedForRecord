using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnPos = 0;
    private float tileLength = 9.992f;

    [SerializeField] private Transform _levelObject;
    private int startTiles = 6;
    
    void Start()
    {
        for (int i = 0; i < startTiles; i++)
        {
            SpawnTile(Random.Range(0,tilePrefabs.Length));
        }
    }

    void Update()
    {
        var lastTilePosition = float.MinValue;
        if (activeTiles.Count > 0)
        {
            lastTilePosition = activeTiles[activeTiles.Count - 1].transform.position.y;
        }

        if (lastTilePosition < transform.position.y + 10)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }                             
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.up * spawnPos, transform.rotation, _levelObject);
        activeTiles.Add(nextTile);
        nextTile.transform.localPosition = transform.up * spawnPos;
        spawnPos += tileLength;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}