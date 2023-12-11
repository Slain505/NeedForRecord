﻿using Core;
using UnityEngine;

namespace Player
{
    public enum MoveDirection
    {
        None,
        Right,
        Left
    }
    
    public class PlayerController : MonoBehaviour 
    { 
        private JoystickController _joystick; 
        private void Start()
        {
            GameObject joystickObject = GameObject.Find("Canvas/Joystick");
            
            if (joystickObject != null)
            {
                _joystick = JoystickController.Instance;
            }
            
            
            if (_joystick == null)
            {
                Debug.LogError("JoystickController is not assigned!");
            }
        }

        public void HandelInputs()
        {
            HandleJoystickInput();
        }
        
        private void HandleJoystickInput()
        {
            if (_joystick == null)
            {
                return;
            }

            var moveInput = _joystick.GetInputDirection();
            //Debug.Log(moveInput.x); 
            
            if (moveInput.x < -0.01)
            { 
                CurrentMoveDirection = MoveDirection.Left;
            }
            else if (moveInput.x > 0.01)
            { 
                CurrentMoveDirection = MoveDirection.Right;
            }
            else 
            { 
                CurrentMoveDirection = MoveDirection.None;
            }
        }

        public MoveDirection CurrentMoveDirection { get; private set; }
    }
}