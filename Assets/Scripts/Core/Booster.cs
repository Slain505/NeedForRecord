using System.Collections;
using UnityEngine;

namespace Core
{
    /// 
    /// Depricated for further use
    /// 
    //public enum BoosterType
    //{
    //    None,
    //    Shield,
    //    Nitro,
    //    Magnet,
    //}
    
    public class Booster : MonoBehaviour
    {
        public GameObject Visuals;
        //public BoosterType Type;
        public float Duration = 15f;
        protected virtual void ActivateEffect() {Visuals.SetActive(true);}
        protected virtual void DeactivateEffect() {Visuals.SetActive(false); }
        
        public static Booster Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        //public void Activate() 
        //{
        //    ActivateEffect();
        //    StartCoroutine(DeactivateAfterTime(Duration));
        //}

        //private IEnumerator DeactivateAfterTime(float time) 
        //{
        //    yield return new WaitForSeconds(time);
        //    DeactivateEffect();
        //}
    }
}