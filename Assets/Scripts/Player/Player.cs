using System;
using System.Collections;
using System.Collections.Generic;
using Code.Game;
using Core;
using DG.Tweening;
using Game.Player;
using Obstacles;
using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private List<Booster> _boosters;
        private PedalsController _pedalsController;
        private PlayerController _playerController;
        private Rigidbody2D rb;
        
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
                    PlayerPrefs.SetInt("Coins", value);
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
            Level.Instance.GameOver();
            return 0;
        }

        void Start()
        {
            _playerController = gameObject.AddComponent<PlayerController>(); 
            _pedalsController = GameObject.Find("Canvas/PedalControls").GetComponent<PedalsController>();
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

            if (_pedalsController.IsGasButtonPressed)
            {
                MoveForward();
            }
            else if(_pedalsController.IsBrakeButtonPressed)
            {
                MoveBackward();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Triggered");
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
                Destroy(other.gameObject);
            }
            else if(other.TryGetComponent<Coin>(out var coinPickUp))
            {
                coinPickUp.transform.DOMove(this.transform.position, 0.5f).OnComplete(() =>
                {
                    Coins++;
                    Destroy(coinPickUp.gameObject);
                });
            }
            else if(other.TryGetComponent<Obstacle>(out var obstacle))
            {
                if (!Invincible)
                {
                    Health -= obstacle.Damage;
                    Level.Instance.Speed -= obstacle.SpeedPenalty;
                }
                Destroy(obstacle.gameObject);
            }
        }

        private void MoveLeft()
        {
            Vector2 pos = transform.position;
            pos.x -= TurnSpeed * Time.deltaTime;
            transform.position = pos;
        }

        private void MoveRight()
        {
            Vector2 pos = transform.position;
            pos.x += TurnSpeed * Time.deltaTime;
            transform.position = pos;
        }
        
        private void MoveForward()
        {
            Vector2 pos = transform.position;
            pos.y += Acceleration * Time.deltaTime;
            transform.position = pos;
        }
        
        private void MoveBackward()
        {
            Vector2 pos = transform.position;
            pos.y -= Acceleration * Time.deltaTime;
            transform.position = pos;
        }

        public void Setup(PlayerConfig config)
        {
            Acceleration = config.Acceleration;
            Health = config.Health;
            TurnSpeed = config.TurnSpeed;
        }
    }
}