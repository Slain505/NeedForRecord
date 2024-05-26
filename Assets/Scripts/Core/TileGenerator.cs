using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class TileGenerator : MonoBehaviour
    {
        public GameObject[] tilePrefabs;
        public GameObject startTile;
        private List<GameObject> _activeTiles = new List<GameObject>();
        private float _spawnPos = 9.992f;
        private float _tileLength = 9.992f;
        private int _startTiles = 6;
        private bool _isStartTileExist = true;
        private Transform _playerTransform;
        private float _spawnDistance = 10f;

        private void Start()
        {
            // Spawn start tiles
            for (int i = 0; i < _startTiles; i++)
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
        }

        private void Update()
        {
            try
            {
                // Found player transform
                if (_playerTransform != null);
                _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch
            {
                // If player is not found, return
                return;
            }
            
            // If player is close to the last tile, spawn new tile and delete the first one
            if (_activeTiles.Count > 0)
            {
                float lastTilePosition = _activeTiles[_activeTiles.Count - 1].transform.position.y;
                if (lastTilePosition < _playerTransform.position.y + _spawnDistance)
                {
                    SpawnTile(Random.Range(0, tilePrefabs.Length));
                    DeleteTile();
                    if (_isStartTileExist)
                    {
                        _isStartTileExist = false;
                        DeleteStartTile();
                    }
                }
            }
        }

        private void SpawnTile(int tileIndex)
        {
            GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.up * _spawnPos, transform.rotation);
            _activeTiles.Add(nextTile);
            nextTile.transform.localPosition = transform.up * _spawnPos;
            _spawnPos += _tileLength;
        }

        private void DeleteTile()
        {
            Destroy(_activeTiles[0]);
            _activeTiles.RemoveAt(0);
        }

        private void DeleteStartTile()
        {
            Destroy(startTile);
        }
    }
}
