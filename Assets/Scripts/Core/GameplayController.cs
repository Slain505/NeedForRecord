using Player;
using UnityEngine;

namespace Core
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
            player.Setup(playerConfig);
        }
        
        //todo: implement Police
        private void SpawnEnemy()
        {
            Debug.Log("Spawn enemy called.");
            
        }
        
        private void Start()
        {
            SpawnPlayer();
        }
    }
}