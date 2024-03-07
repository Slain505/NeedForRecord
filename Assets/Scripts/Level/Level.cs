using UI;
using UnityEngine;

namespace Level
{
    public class Level: MonoBehaviour
    { 
        private Transform _transform;
        private Vector2 _targetPosition;
        private float _lerpTime = 5.0f;
        private float _currentLerpTime;
        private float _speed = 0f;
        private float _savedSpeed;
        public static int Score {get; set;}
        
        public bool IsGamePaused { get; set; }
        private float _savedTimeScale { get; set; }

        public float Speed 
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value >= 0 ? value : 1;
            }
        }
    
        public static Level Instance { get; private set; }
        
        void Start()
        {
            Instance = this;
            Score = 0;
            Time.timeScale = 1f;
        }

        private void GameStartState()
        {
            _speed = 2f;
        }

        void Update()
        {
            //MoveTiles();
            CountScore();
        }

        public void GameOver()
        {
            Time.timeScale = 0f;
            _speed = 0;
        }
    
        public void Paused()
        {
            _savedSpeed = _speed;
            Time.timeScale = 0f;
            IsGamePaused = true;
        }
        
        public void Resumed()
        {
            Time.timeScale = 1f;
            _speed = _savedSpeed;
            IsGamePaused = false;
        }

        private void CountScore()
        {
            //_score += (int)(_speed * (Time.deltaTime * 100));
            PlayerPrefs.SetInt("Score", Score);
            //Debug.Log(_score);
        }

        private void MoveTiles()
        {
            _currentLerpTime += Time.deltaTime;
            if (_currentLerpTime > _lerpTime)
            {
                _currentLerpTime = _lerpTime;
            }
        
            float newY = transform.position.y - Speed * Time.deltaTime;
        
            transform.position = new Vector2(transform.position.x, newY);
        }
    }
}
