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
        [SerializeField] private Transform _playerSpawnPoint;
        private int _score;
        private int _playerHealth = 100;
        private Player.Player player;
        
        private void SpawnPlayer()
        {
            var playerStart = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity, transform);
            player = playerStart.GetComponent<Player.Player>();
            player.Setup(_playerConfig);
        }
        
        private void Start()
        {
            SpawnPlayer();
        }
    }
}
