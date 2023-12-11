using Player;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _enemyPrefab; 
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private BoxCollider2D _policeSpawnArea;
        [SerializeField] private LayerMask _policeSpawnAreaLayerMask;
        [SerializeField] private int _attemptsToSpawnPolice = 10;
        [SerializeField] private float _spawnRate;
        private int _score;
        private int _playerHealth = 100;
        public static GameplayController Instance { get; private set; }
        private Player.Player player;
        
        public GameplayController()
        {
            Instance = this;
        }
        
        private void SpawnPlayer()
        {
            var playerStart = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity, transform);
            player = playerStart.GetComponent<Player.Player>();
            player.Setup(_playerConfig);
        }
        
        private void SpawnEnemy()
        {
            if (Level.Level.Instance.IsGamePaused)
            {
                return; 
            }
            
            Debug.Log("Spawn enemy called.");
            var spawned = false;

            for (int i = 0; i < _attemptsToSpawnPolice; i++)
            {
                var spawnPosition = GetRandomPositionInSpawnArea();

                if (!IsPositionOccupied(spawnPosition))
                {
                    Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
                    spawned = true;
                    break;
                }
            }

            if (!spawned)
            {
                Debug.Log("Failed to find an unoccupied position for police spawn after " + _attemptsToSpawnPolice + " attempts.");
            }
        }

        private bool IsPositionOccupied(Vector3 spawnPosition)
        {
            var size = new Vector2(1.2000000f, 1.20000005f);
            var angle = 0f;

            Collider2D[] collidersInArea = Physics2D.OverlapCapsuleAll(spawnPosition, size, CapsuleDirection2D.Vertical, angle, _policeSpawnAreaLayerMask);

            foreach (var collider in collidersInArea)
            {
                if (collider.gameObject.CompareTag("Police"))
                {
                    return true;
                }
            }

            return false;
        }

        private void Start()
        {
            SpawnPlayer();
            Countdown.Instance.OnCountdownFinished += GameStartState;
        }

        private void GameStartState()
        {
            InvokeRepeating("SpawnEnemy", _spawnRate + 4f, _spawnRate);
        }

        Vector2 GetRandomPositionInSpawnArea()
        {
            Bounds bounds = _policeSpawnArea.bounds;
            var x = Random.Range(bounds.min.x, bounds.max.x);
            var y = Random.Range(bounds.min.y, bounds.max.y);

            return new Vector2(x, y);
        }
    }
}
