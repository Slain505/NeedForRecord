using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Boosters
{ 
    public class MagnetBooster : Booster 
    {
        [SerializeField]
        private Collider2D _magnetCollider;
        private bool _isActive;
        
        protected override void ActivateEffect() 
        {
            base.ActivateEffect();
            _magnetCollider.enabled = true;
            _isActive = true;
        }

        public override void DeactivateEffect()
        {
            if (!_isActive) return;
            base.DeactivateEffect();
            _magnetCollider.enabled = false;
            _isActive = false;
        }
    }
}