using System;
using System.Collections.Generic;
using Collectables;
using Core;
using DG.Tweening;
using Obstacles;
using UI;
using UnityEngine;

namespace Enemy
{
    public class Police : MonoBehaviour
    {
        [SerializeField] private Sprite[] policeSprites; 
        [SerializeField] private SpriteRenderer SpriteRenderer;
        [SerializeField] private float speedyPoliceSpeed;
        [SerializeField] private float regularPoliceSpeed;
        public int Damage;
        private Vector3 startPositionOffset = new Vector3(0, -6, 0);
        private float appearingDuration = 3f;
        private float moveDuration = 3f;

        //Appearing animation + chase logic
        private void PoliceAnimation()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(transform.position - startPositionOffset, appearingDuration).SetEase(Ease.OutCubic))
                .Append(transform.DOMove(transform.position + new Vector3(0, 15, 0), CalculateDurationOfMove())).SetEase(Ease.Linear)
                .AppendCallback(() => Destroy(gameObject));
        }

        //Bad naming - this method is calculating which Police sprite to use and how fast it should move 
        private float CalculateDurationOfMove()
        {
            SpriteRenderer.sprite = policeSprites[UnityEngine.Random.Range(0, policeSprites.Length)];
            if (SpriteRenderer.sprite == policeSprites[0])
            {
                //Speedy version
                return speedyPoliceSpeed;
            }
            else if (SpriteRenderer.sprite == policeSprites[1])
            {
                //Regular one
                return regularPoliceSpeed;
            }
            //Default
            return regularPoliceSpeed;
        }
        
        private void Start()
        {
            Vector3 startPostion = transform.position + startPositionOffset;
            transform.position = startPostion;

            PoliceAnimation();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<BoosterPickUp>(out var boosterPickUp))
            {
                Debug.Log($"{boosterPickUp.Type} was destroyed by Police.");
                Destroy(other.gameObject);
            }
            else if(other.TryGetComponent<Coin>(out var coinPickUp))
            {
                Debug.Log("Coin was destroyed by Police.");
                Destroy(other.gameObject);
            }
            else if(other.TryGetComponent<Obstacle>(out var obstacle))
            {
                Debug.Log("Obstacle was destroyed by Police.");
                Destroy(other.gameObject);
            }
            else if(other.TryGetComponent<Police> (out var police))
            {
                Debug.LogError("FIX POLICE SPAWN!");
            }
            else if(other.TryGetComponent<Lifes> (out var lifes))
            {
                Debug.Log("Life was destroyed by Police.");
                Destroy(other.gameObject);
            }
        }
    }
}