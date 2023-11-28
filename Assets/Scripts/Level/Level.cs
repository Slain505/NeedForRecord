using UnityEngine;

namespace Level
{
    public class Level: MonoBehaviour
    { 
        private Transform _transform;
        private Vector2 _targetPosition;
        private float _lerpTime = 5.0f;
        private float _currentLerpTime;
        private float _speed = 2f;
        private float _savedSpeed;
        private int _score;
    
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
            _score = 0;
        }
    
        void Update()
        {
            MoveTiles();
            CountScore();
        }

        public void GameOver()
        {
            _speed = 0;
        }
    
        public void Paused()
        {
            _savedSpeed = _speed;
            _speed = 0;
        }
    
        public void Resumed()
        {
            _speed = _savedSpeed;
        }

        private void CountScore()
        {
            _score += (int)(_speed * (Time.deltaTime * 100));
            PlayerPrefs.SetInt("Score", _score);
            Debug.Log(_score);
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
