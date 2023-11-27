using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level: MonoBehaviour
{ 
    private Transform _transform;
    private Vector2 _targetPosition;
    private float _lerpTime = 5.0f;
    private float _currentLerpTime;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        _currentLerpTime += Time.deltaTime;
        if (_currentLerpTime > _lerpTime)
        {
            _currentLerpTime = _lerpTime;
        }
        
        float newY = transform.position.y - 2f * Time.deltaTime;
        
        transform.position = new Vector2(transform.position.x, newY);
    }
}
