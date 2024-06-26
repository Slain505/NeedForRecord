using Core;
using UI;

namespace Boosters
{
    public class NitroBooster : Booster
    {
        private bool _isActive;

        protected override void ActivateEffect()
        {
            base.ActivateEffect();
            Player.Player.Instance.Speed += 2;
            _isActive = true;
            BoosterProgressBar.Instance.IsNitroActive = true;
            BoosterProgressBar.Instance.SetTime(15f);
        }

        public override void DeactivateEffect()
        {
            if (!_isActive) return;
            base.DeactivateEffect();
            Player.Player.Instance.Speed -= 2;
            _isActive = false;
            BoosterProgressBar.Instance.IsNitroActive = false;
            BoosterProgressBar.Instance.TurnOffNitroProgressBar();
        }
    }
}
