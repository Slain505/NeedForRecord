using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField] private Text _countdownText; 
        [SerializeField] private Transform _targetPosition;
        [SerializeField] private GameObject _playerAnim;
        [SerializeField] private Sprite[] _walkingSprites;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private const float CountdownTime = 3f;
        private const float WalkingTime = 2f;
        public delegate void CountdownFinished(); 
        public event CountdownFinished OnCountdownFinished;
        
        public static Countdown Instance { get; private set; }
        
        void Start()
        {
            Instance = this;

            if(_countdownText != null)
            {
                StartCoroutine(StartCountdown());
                WalkingAnim();
            }
        }

        private void WalkingAnim()
        {
            StartCoroutine(ChangeSprite());
            Vector3 adjustedTargetPosition = _targetPosition.position + new Vector3(-0.7f, 0, 0);
            _playerAnim.transform.DOMove(adjustedTargetPosition, WalkingTime)
                .SetEase(Ease.Linear).OnComplete(() => 
                {
                    _spriteRenderer.sprite = _walkingSprites[2];
                });
        }
        
        IEnumerator StartCountdown()
        {
            float currCountdownValue = CountdownTime;
            while (currCountdownValue > 0)
            {
                _countdownText.text = currCountdownValue.ToString();
                yield return new WaitForSeconds(1.0f);
                currCountdownValue--;
            }

            _countdownText.text = "Start!";
            _playerAnim.SetActive(false);
            StopCoroutine(ChangeSprite());
            yield return new WaitForSeconds(1.0f); 

            this.gameObject.SetActive(false);
            OnCountdownFinished?.Invoke();
        }
        
        //todo: refactor this to more beautiful and smooth animation
        IEnumerator ChangeSprite()
        {
            float elapsedTime = 0;

            while (elapsedTime < WalkingTime)
            {
                _spriteRenderer.sprite = _walkingSprites[0];
                yield return new WaitForSeconds(0.3f);
                elapsedTime += 0.3f;
                if (elapsedTime >= WalkingTime)
                {
                    break;
                }
                _spriteRenderer.sprite = _walkingSprites[2];
                yield return new WaitForSeconds(0.1f);
                elapsedTime += 0.1f;
                if (elapsedTime >= WalkingTime)
                {
                    break;
                }
                _spriteRenderer.sprite = _walkingSprites[1];
                yield return new WaitForSeconds(0.3f);
                elapsedTime += 0.3f;
                if (elapsedTime >= WalkingTime)
                {
                    break;
                }
                _spriteRenderer.sprite = _walkingSprites[2];
                yield return new WaitForSeconds(0.1f);
                elapsedTime += 0.1f;

                if (elapsedTime >= WalkingTime)
                {
                    break;
                }
            }
        }
    }
}

