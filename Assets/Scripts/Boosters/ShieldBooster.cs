using Core;
using UI;
using UnityEngine;

namespace Boosters
{
    public class ShieldBooster : Booster 
    {
        [SerializeField] private Player.Player _player;
        private bool _isActive;
        
        protected override void ActivateEffect() 
        {
            base.ActivateEffect();
            _player.Invincible = true;
            _isActive = true;
            BoosterProgressBar.Instance.IsShieldActive = true;
            BoosterProgressBar.Instance.SetTime(15f);
        }

        public override void DeactivateEffect() 
        {
            if (!_isActive) return;
            base.DeactivateEffect();
            _player.Invincible = false;
            _isActive = false;
            BoosterProgressBar.Instance.IsShieldActive = false;
            BoosterProgressBar.Instance.TurnOffShieldProgressBar();
        }
    }
}