using System.Collections;
using System.Collections.Generic;
using Collectables;
using Core;
using DG.Tweening;
using Enemy;
using Obstacles;
using UI;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private List<Booster> _boosters;
        private PlayerController _playerController;
        private Rigidbody2D _rb;
        
        public static Player Instance { get; private set; }
        public bool Invincible { get; set; }
        private float Acceleration { get; set; }
        private float TurnSpeed { get; set; }
        private int Score { get;  set; }

        private int Coins
        {
            get => _coins; 
            set
            {
                if (_coins != value)
                {
                    _coins = value;
                }
            }
        }

        private int Health
        {
            get => _health;
            set
            {
                _health = value;
                if (_health <= 0)
                {
                    Die();
                }
            }
        }
        private int _coins;
        private int _health;

        private int Die()
        {
            Destroy(gameObject);
            Level.Level.Instance.GameOver();
            Popups.Instance.OnPlayerDied();
            BoosterProgressBar.Instance.IsAlive = false;
            UpdateCoinBalance();
            CancelInvoke("SpawnEnemy");
            return 0;
        }
        
        public void UpdateCoinBalance()
        {
            var currentCoins = PlayerPrefs.GetInt("Coins");
            PlayerPrefs.SetInt("Coins", currentCoins + _coins);
        }

        private void MoveLeft()
        {
            if (transform.position.x >= -1.5f)
            {
                Vector2 pos = transform.position;
                pos.x -= TurnSpeed * Time.deltaTime;
                transform.position = pos;
            }
        }

        private void MoveRight()
        {
            if (transform.position.x <= 1.5f)
            {
                Vector2 pos = transform.position;
                pos.x += TurnSpeed * Time.deltaTime;
                transform.position = pos;
            }
        }
        
        private void MoveForward()
        {
            if (transform.position.y <= 3.5f)
            {
                Vector2 pos = transform.position;
                pos.y += Acceleration * Time.deltaTime;
                transform.position = pos;
            }
        }
        
        private void MoveBackward()
        {
            if (transform.position.y >= -4.5f)
            {
                Vector2 pos = transform.position;
                pos.y -= Acceleration * Time.deltaTime;
                transform.position = pos;
            }
        }

        public void Setup(PlayerConfig config)
        {
            Acceleration = config.Acceleration;
            Health = config.Health;
            TurnSpeed = config.TurnSpeed;
        }

        private void CalculateCollision(Collider2D other)
        {
            if (other.TryGetComponent<BoosterPickUp>(out var boosterPickUp))
            {
                foreach (var booster in _boosters)
                {
                    if (boosterPickUp.Type == booster.Type)
                    {
                        booster.Activate();
                    }
                    else
                    {
                        booster.DeactivateEffect();
                    }
                }
                Debug.Log($"{boosterPickUp.Type} was taken.");
                Destroy(other.gameObject);
            }
            else if(other.TryGetComponent<Coin>(out var coinPickUp))
            {
                Debug.Log("Coin was taken.");
                coinPickUp.transform.DOMove(this.transform.position, 0.5f).OnComplete(() =>
                {
                    Coins += 10;
                    Destroy(coinPickUp.gameObject);
                });
            }
            else if(other.TryGetComponent<Obstacle>(out var obstacle))
            {
                Debug.Log("Ooops... Obstacle hit");
                if (!Invincible)
                {
                    Health -= obstacle.Damage;
                    HealthProgressBar.Instance.SetHealth(Health);
                    StartCoroutine(OnSlowByObstacle(obstacle.SpeedPenalty));
                }
                Destroy(obstacle.gameObject);
            }
            else if(other.TryGetComponent<Police> (out var police))
            {
                Debug.Log("Police hit.");
                Health -= police.Damage;
            }
            else if(other.TryGetComponent<Lifes> (out var lifes))
            {
                Debug.Log("Life was taken.");
                lifes.transform.DOMove(this.transform.position, 0.5f).OnComplete(() =>
                {
                    if(Health < 100)
                    {
                        Health += 25;
                    }
                    if(Health > 100)
                    {
                        Health = 100;
                    }
                
                    HealthProgressBar.Instance.SetHealth(Health);
                    Destroy(lifes.gameObject);
                });
            }
        }
        
        private IEnumerator OnSlowByObstacle(float speedPenalty)
        {
            Level.Level.Instance.Speed -= speedPenalty;
            yield return new WaitForSeconds(15f);
            Level.Level.Instance.Speed += speedPenalty;
        }
        
        void Start()
        {
            Instance = this;
            HealthProgressBar.Instance.SetHealth(Health);
            _playerController = gameObject.AddComponent<PlayerController>();
        }
        
        void Update()
        {
            _playerController.HandelInputs();
            switch (_playerController.CurrentMoveDirection)
            {
                case MoveDirection.Left:
                    MoveLeft();
                    break;
                case MoveDirection.Right:
                    MoveRight();
                    break;
                case MoveDirection.None:
                    break;
            }

            if (PedalsController.Instance.IsGasButtonPressed)
            {
                MoveForward();
            }
            else if(PedalsController.Instance.IsBrakeButtonPressed)
            {
                MoveBackward();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            CalculateCollision(other);
        }
    }
}