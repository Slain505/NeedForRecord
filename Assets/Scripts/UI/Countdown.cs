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
        [SerializeField] private Text countdownText; 
        [SerializeField] private Transform targetPosition;
        [SerializeField] private GameObject playerAnim;
        [SerializeField] private Sprite[] walkingSprites;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private const float CountdownTime = 3f;
        public delegate void CountdownFinished(); 
        public event CountdownFinished OnCountdownFinished;
        
        public static Countdown Instance { get; private set; }
        
        void Start()
        {
            Instance = this;

            if(countdownText != null)
            {
                StartCoroutine(StartCountdown());
                WalkingAnim();
            }
        }

        private void WalkingAnim()
        {
            StartCoroutine(ChangeSprite());
            playerAnim.transform.DOMove(targetPosition.position, 3f).SetEase(Ease.Linear);
        }
        
        IEnumerator StartCountdown()
        {
            float currCountdownValue = CountdownTime;
            while (currCountdownValue > 0)
            {
                countdownText.text = currCountdownValue.ToString();
                yield return new WaitForSeconds(1.0f);
                currCountdownValue--;
            }

            countdownText.text = "Go!";
            playerAnim.SetActive(false);
            StopCoroutine(ChangeSprite());
            yield return new WaitForSeconds(1.0f); 

            this.gameObject.SetActive(false);
            OnCountdownFinished?.Invoke();
        }
        
        //todo: refactor this to more beatuful and smooth animation
        IEnumerator ChangeSprite()
        {
            float elapsedTime = 0;

            while (elapsedTime < CountdownTime)
            {
                //spriteRenderer.sprite = walkingSprites[2];
                //yield return new WaitForSeconds(0.1f);
                spriteRenderer.sprite = walkingSprites[0];
                yield return new WaitForSeconds(0.3f);
                spriteRenderer.sprite = walkingSprites[2];
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.sprite = walkingSprites[1];
                yield return new WaitForSeconds(0.3f);
                spriteRenderer.sprite = walkingSprites[2];
                yield return new WaitForSeconds(0.1f);
                elapsedTime += 0.6f;

                if (elapsedTime >= CountdownTime)
                {
                    break;
                }
            }
        }
    }
}

