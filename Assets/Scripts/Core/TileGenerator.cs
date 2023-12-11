using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class TileGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _levelObject;
        public GameObject[] tilePrefabs;
        private List<GameObject> _activeTiles = new List<GameObject>();
        private float _spawnPos = 0;
        private float _tileLength = 9.992f;
        private int _startTiles = 6;

        private void SpawnTile(int tileIndex)
        {
            GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.up * _spawnPos, transform.rotation, _levelObject);
            _activeTiles.Add(nextTile);
            nextTile.transform.localPosition = transform.up * _spawnPos;
            _spawnPos += _tileLength;
        }
        
        private void DeleteTile()
        {
            Destroy(_activeTiles[0]);
            _activeTiles.RemoveAt(0);
        }
    
        void Start()
        {
            for (int i = 0; i < _startTiles; i++)
            {
                SpawnTile(Random.Range(0,tilePrefabs.Length));
            }
        }

        void Update()
        {
            var lastTilePosition = float.MinValue;
            if (_activeTiles.Count > 0)
            {
                lastTilePosition = _activeTiles[_activeTiles.Count - 1].transform.position.y;
            }

            if (lastTilePosition < transform.position.y + 10)
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
                DeleteTile();
            }                             
        }
    }
}