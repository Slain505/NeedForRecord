using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Boosters
{
    public class NitroBooster : Booster
    {
        private bool _isActive;

        protected override void ActivateEffect()
        {
            base.ActivateEffect();
            Level.Instance.Speed += 2;
            _isActive = true;
        }

        public override void DeactivateEffect()
        {
            if (!_isActive) return;
            base.DeactivateEffect();
            Level.Instance.Speed -= 2;
            _isActive = false;
        }
    }
}
