using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level: MonoBehaviour
{ 
    private Transform _transform;
    private Vector2 _targetPosition;
    private float _lerpTime = 5.0f;
    private float _currentLerpTime;
    
    private float _speed = 2f;
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
    }
    
    void Update()
    {
        _currentLerpTime += Time.deltaTime;
        if (_currentLerpTime > _lerpTime)
        {
            _currentLerpTime = _lerpTime;
        }
        
        float newY = transform.position.y - Speed * Time.deltaTime;
        
        transform.position = new Vector2(transform.position.x, newY);
    }

    public void GameOver()
    {
        _speed = 0;
    }
}
