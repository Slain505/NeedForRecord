using System.Collections;
using System.Collections.Generic;
using Core;
using Game.Player;
using UnityEngine;

namespace Boosters
{
    public class ShieldBooster : Booster 
    {
        [SerializeField]
        private Player _player;
        private bool _isActive;
        protected override void ActivateEffect() 
        {
            base.ActivateEffect();
            _player.Invincible = true;
            _isActive = true;
        }

        public override void DeactivateEffect() 
        {
            if (!_isActive) return;
            base.DeactivateEffect();
            _player.Invincible = false;
            _isActive = false;
        }
    }

}