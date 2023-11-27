using Game.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerSpawnPoint;
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
            player.Setup(new PlayerModel(playerConfig));
        }
        
        private void SpawnEnemy()
        {
            Debug.Log("Spawn enemy called.");
            
            //var g = Game.Get<ObjectPoolsController>().EnemyPool.GetObject();
            //g.transform.position = enemySpawnPoint.transform.position +
            //                       new Vector3(
            //                           Random.Range(spawnerConfig.SpawnPosition.x, spawnerConfig.SpawnPosition.y),
            //                           0, 0);
            //var enemy = g.GetComponent<Enemy>();
            //enemy.Setup(new EnemyModel(enemyConfig));
            
            //enemy.onDie -= HandleEnemyDeath;
            //enemy.onDie += HandleEnemyDeath;
        }
        
        private void Start()
        {
            SpawnPlayer();
        }
    }
}