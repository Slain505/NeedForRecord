using Player;
using UI;
using UnityEngine;

namespace Core
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject enemyPrefab; 
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private BoxCollider2D policeSpawnArea;
        [SerializeField] private LayerMask policeSpawnAreaLayerMask;
        [SerializeField] private int attemptsToSpawnPolice = 10;
        [SerializeField] private float spawnRate;
        public static GameplayController Instance { get; private set; }
        private Player.Player player;

        private int _score;
        private int _playerHealth = 100;
        
        public GameplayController()
        {
            Instance = this;
        }
        
        private void SpawnPlayer()
        {
            var playerStart = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity, transform);
            player = playerStart.GetComponent<Player.Player>();
            player.Setup(playerConfig);
        }
        
        private void SpawnEnemy()
        {
            if (Level.Level.Instance.IsGamePaused)
            {
                return; 
            }
            
            Debug.Log("Spawn enemy called.");
            var spawned = false;

            for (int i = 0; i < attemptsToSpawnPolice; i++)
            {
                var spawnPosition = GetRandomPositionInSpawnArea();

                if (!IsPositionOccupied(spawnPosition))
                {
                    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    spawned = true;
                    break;
                }
            }

            if (!spawned)
            {
                Debug.Log("Failed to find an unoccupied position for police spawn after " + attemptsToSpawnPolice + " attempts.");
            }
        }

        private bool IsPositionOccupied(Vector3 spawnPosition)
        {
            var size = new Vector2(1.2000000f, 1.20000005f);
            var angle = 0f;

            Collider2D[] collidersInArea = Physics2D.OverlapCapsuleAll(spawnPosition, size, CapsuleDirection2D.Vertical, angle, policeSpawnAreaLayerMask);

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
            InvokeRepeating("SpawnEnemy", spawnRate + 4f, spawnRate);
        }

        Vector2 GetRandomPositionInSpawnArea()
        {
            Bounds bounds = policeSpawnArea.bounds;
            var x = Random.Range(bounds.min.x, bounds.max.x);
            var y = Random.Range(bounds.min.y, bounds.max.y);

            return new Vector2(x, y);
        }
    }
}
