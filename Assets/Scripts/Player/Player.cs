using System.Collections;
using System.Collections.Generic;
using Code.Game;
using Game.Player;
using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        private PedalsController _pedalsController;
        private PlayerController _playerController;
        private PlayerModel _model;

        private Rigidbody2D rb;

        public PlayerModel Model => _model;

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

        private void MoveLeft()
        {
            Vector2 pos = transform.position;
            pos.x -= _model.TurnSpeed * Time.deltaTime;
            transform.position = pos;
        }

        private void MoveRight()
        {
            Vector2 pos = transform.position;
            pos.x += _model.TurnSpeed * Time.deltaTime;
            transform.position = pos;
        }
        
        private void MoveForward()
        {
            Vector2 pos = transform.position;
            pos.y += _model.Acceleration * Time.deltaTime;
            transform.position = pos;
        }
        
        private void MoveBackward()
        {
            Vector2 pos = transform.position;
            pos.y -= _model.Acceleration * Time.deltaTime;
            transform.position = pos;
        }

        public void Setup(PlayerModel model)
        {
            _model = model;
            //model.crash += OnModelCrash;
        }
    }
}