using System;
using UI;
using UnityEngine;

namespace Level
{
    public class Level: MonoBehaviour
    { 
        private Transform _transform;
        private Vector2 _targetPosition;
        
        public bool IsGamePaused { get; set; }
    
        public static Level Instance { get; private set; }
        
        void Start()
        {
            Instance = this;
            Countdown.Instance.OnCountdownFinished += OnGameStartState;
            //GameStart += OnGameStartState;
            Popups.Instance.GameOver += OnGameOver;
            Popups.Instance.GamePaused += OnGamePaused;
            Popups.Instance.GameResumed += OnGameResumed;
            Time.timeScale = 1f;
        }
        
        private void OnGameStartState()
        {
        }

        public void OnGameOver()
        {
            Time.timeScale = 0f;
        }

        public void OnGamePaused()
        {
            Time.timeScale = 0f;
            IsGamePaused = true;
        }

        public void OnGameResumed()
        {
            Time.timeScale = 1f;
            IsGamePaused = false;
        }

        //public event Action GameStart;
    }
}
