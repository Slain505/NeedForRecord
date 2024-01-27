using System;
using System.Collections.Generic;
using Collectables;
using Core;
using DG.Tweening;
using Obstacles;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class PickableItems : MonoBehaviour
    {
        [SerializeField] private Sprite[] _policeSprites; 
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        [SerializeField] private float _speedyPoliceSpeed;
        [SerializeField] private float _regularPoliceSpeed;
        private Vector3 _startPositionOffset = new Vector3(0, -6, 0);
        private float _appearingDuration = 3f;
        private float _moveDuration = 3f;
        public int Damage;

        //Appearing animation + chase logic
        private void PoliceAnimation()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(transform.position - _startPositionOffset, _appearingDuration).SetEase(Ease.OutCubic))
                .Append(transform.DOMove(transform.position + new Vector3(0, 15, 0), CalculateDurationOfMove())).SetEase(Ease.Linear)
                .AppendCallback(() => Destroy(gameObject));
        }

        //Bad naming - this method is calculating which Police sprite to use and how fast it should move 
        private float CalculateDurationOfMove()
        {
            _SpriteRenderer.sprite = _policeSprites[UnityEngine.Random.Range(0, _policeSprites.Length)];
            if (_SpriteRenderer.sprite == _policeSprites[0])
            {
                //Speedy version
                return _speedyPoliceSpeed;
            }
            else if (_SpriteRenderer.sprite == _policeSprites[1])
            {
                //Regular one
                return _regularPoliceSpeed;
            }
            //Default
            return _regularPoliceSpeed;
        }
        
        private void Start()
        {
            Vector3 startPostion = transform.position;
            transform.position = startPostion;
            transform.DOMoveY(-transform.position.y - 100f, 100f).SetEase(Ease.OutCubic);

        //    PoliceAnimation();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            //if (other.TryGetComponent<BoosterPickUp>(out var boosterPickUp))
            //{
            //    Debug.Log($"{boosterPickUp.Type} was destroyed by Police.");
            //    Destroy(other.gameObject);
            //}
            if(other.TryGetComponent<Coin>(out var coinPickUp))
            {
                Debug.Log("Coin was destroyed by Police.");
                Destroy(other.gameObject);
            }
            else if(other.TryGetComponent<Obstacle>(out var obstacle))
            {
                Debug.Log("Obstacle was destroyed by Police.");
                Destroy(other.gameObject);
            }
            //else if(other.TryGetComponent<Police> (out var police))
            //{
            //    Debug.LogError("FIX POLICE SPAWN!");
            //}
            //else if(other.TryGetComponent<Lifes> (out var lifes))
            //{
            //    Debug.Log("Life was destroyed by Police.");
            //    Destroy(other.gameObject);
            //}
        }
    }
}