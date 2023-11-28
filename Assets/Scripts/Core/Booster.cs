using System.Collections;
using UnityEngine;

namespace Core
{
    public enum BoosterType
    {
        Shield,
        Nitro,
        Magnet
    }
    
    public class Booster : MonoBehaviour
    {
        public GameObject Visuals;
        public BoosterType Type;
        public float Duration = 15f;
        protected virtual void ActivateEffect() {Visuals.SetActive(true);}
        public virtual void DeactivateEffect() {Visuals.SetActive(false);}

        public void Activate() 
        {
            ActivateEffect();
            StartCoroutine(DeactivateAfterTime(Duration));
        }

        private IEnumerator DeactivateAfterTime(float time) 
        {
            yield return new WaitForSeconds(time);
            DeactivateEffect();
        }
    }
}