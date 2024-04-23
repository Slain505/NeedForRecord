using System;
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
        [SerializeField] private Camera _camera;
        private PlayerController _playerController;
        private Rigidbody2D _rb;
        
        public static Player Instance { get; private set; }
        public bool Invincible { get; set; }
        private float Acceleration { get; set; }
        private float TurnSpeed { get; set; }

        private int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                }
            }
        }

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

        public float Speed 
        { 
            get => _speed; 
            set => _speed = value >= 0 ? value : 1;
            
        }
        
        private int _score;
        private int _coins;
        private int _health;
        private float _speed;
        private float _currentLerpTime;
        private float _savedSpeed;
        private readonly float _lerpTime = 5.0f;

        private int Die()
        {
            Destroy(gameObject);
            Level.Level.Instance.OnGameOver();
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
            Speed = config.Speed;
        }

        private void CalculateCollision(Collider2D other)
        {
            if (other.TryGetComponent<BoosterPickUp>(out var boosterPickUp))
            {
                HandleBoosterPickUp(boosterPickUp, other);
            }
            else if (other.TryGetComponent<Coin>(out var coinPickUp))
            {
                HandleCoinPickUp(coinPickUp);
            }
            else if (other.TryGetComponent<Obstacle>(out var obstacle))
            {
                HandleObstacleCollision(obstacle);
            }
            else if (other.TryGetComponent<Police>(out var police))
            {
                HandlePoliceCollision(police);
            }
            else if (other.TryGetComponent<Lifes>(out var lifes))
            {
                HandleLifePickUp(lifes, other);
            }
        }

        private void HandleLifePickUp(Lifes lifes, Collider2D other)
        {
            Debug.Log("Life was taken.");
            lifes.transform.DOMove(this.transform.position, 0.5f).OnComplete(() =>
            {
                if (Health < 100)
                {
                    Health += 25;
                }

                if (Health > 100)
                {
                    Health = 100;
                }

                HealthProgressBar.Instance.SetHealth(Health);
                Destroy(lifes.gameObject);
            });
        }

        private void HandlePoliceCollision(Police police)
        {
            Debug.Log("Police hit.");
            Health -= police.Damage;
        }

        private void HandleObstacleCollision(Obstacle obstacle)
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

        private void HandleCoinPickUp(Coin coinPickUp)
        {
            Debug.Log("Coin was taken.");
            coinPickUp.transform.DOMove(this.transform.position, 0.5f).OnComplete(() =>
            {
                Coins += 10;
                Destroy(coinPickUp.gameObject);
            });
        }

        private void HandleBoosterPickUp(BoosterPickUp boosterPickUp, Collider2D other)
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
        
        private void MovePlayer()
        {
            _currentLerpTime += Time.deltaTime;
            if (_currentLerpTime > _lerpTime)
            {
                _currentLerpTime = _lerpTime;
            }
        
            float newY = transform.position.y + Speed * Time.deltaTime;
        
            transform.position = new Vector2(transform.position.x, newY);
        }

        private IEnumerator OnSlowByObstacle(float speedPenalty)
        {
            _speed -= speedPenalty;
            yield return new WaitForSeconds(15f);
            _speed += speedPenalty;
        }
        
        void Start()
        {
            Instance = this;
            SubscribeOnGameEvents();
            HealthProgressBar.Instance.SetHealth(Health);
            _playerController = gameObject.AddComponent<PlayerController>();
            _camera = Camera.main;
            _score = 0;
        }

        private void SubscribeOnGameEvents()
        {
            Countdown.Instance.OnCountdownFinished += OnGameStart;
            Popups.Instance.GameOver += OnGameOver;
            Popups.Instance.GamePaused += OnGamePaused;
            Popups.Instance.GameResumed += OnGameResumed;
        }

        #region EventHandlers

        private void OnGameResumed()
        {
            _speed = _savedSpeed;
        }

        private void OnGamePaused()
        {
            _savedSpeed = _speed;
        }

        private void OnGameOver()
        {
            _speed = 0;
        }

        private void OnGameStart()
        {
            _speed = 2f;
        }
        
        #endregion
        
        private void CountScore()
        {
            Score += (int)(_speed * (Time.deltaTime * 100));
            PlayerPrefs.SetInt("Score", Score);
            //Debug.Log(_score);
        }
        
        void Update()
        {
            MovePlayer();
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

            CountScore();
            _camera.transform.position = new Vector3(_camera.transform.position.x, transform.position.y + 2.5f, _camera.transform.position.z);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            CalculateCollision(other);
        }
    }
}