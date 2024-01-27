using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BoosterProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject nitroProgressBar;
        [SerializeField] private GameObject shieldProgressBar;
        [SerializeField] private GameObject magnetProgressBar;
        public int MaximumNitro = 15;
        public int MinimumNitro = 0;
        public int MaximumMagnet = 15;
        public int MinimumMagnet = 0;
        public int MaximumShield = 15;
        public int MinimumShield = 0;
        private float _current;
        public Image MaskNitro;
        public Image MaskMagnet;
        public Image MaskShield;
        private float _timeLeft;
        public bool IsNitroActive;
        public bool IsMagnetActive;
        public bool IsShieldActive;
        public bool IsAlive { get; set; }
    
        //public static BoosterProgressBar Instance { get; private set; }

        void Start()
        {
        //    Instance = this;
            IsAlive = true;
        }
    
        void Update()
        {
            if (IsNitroActive)
            {
                GetNitroBarFill(MinimumNitro, MaximumNitro, MaskNitro);
            }
            else if (IsMagnetActive)
            {
                GetMagnetBarFill(MinimumMagnet, MaximumMagnet, MaskMagnet);
            }
            else if (IsShieldActive)
            {
                GetShieldBarFill(MinimumShield, MaximumShield, MaskShield);
            }

            if (_timeLeft > 0 && IsAlive)
            {
                _timeLeft -= Time.deltaTime;
            }
        }

        private void GetShieldBarFill(int minimumShield, int maximumShield, Image maskShield)
        {
            if (IsShieldActive)
            {
                shieldProgressBar.SetActive(true);
                var currentOffset = _timeLeft - minimumShield;
                var maximumOffset = maximumShield - minimumShield;
                var fillAmount = currentOffset / maximumOffset;
                maskShield.fillAmount = fillAmount;
            }
        }

        private void GetMagnetBarFill(int minimumMagnet, int maximumMagnet, Image maskMagnet)
        {
            if (IsMagnetActive)
            {
                magnetProgressBar.SetActive(true);
                var currentOffset = _timeLeft - minimumMagnet;
                var maximumOffset = maximumMagnet - minimumMagnet;
                var fillAmount = currentOffset / maximumOffset;
                maskMagnet.fillAmount = fillAmount;
            }
        }

        private void GetNitroBarFill(int minimum, int maximum, Image mask)
        {
            if (IsNitroActive)
            {
                nitroProgressBar.SetActive(true);
                var currentOffset = _timeLeft - minimum;
                var maximumOffset = maximum - minimum;
                var fillAmount = currentOffset / maximumOffset;
                mask.fillAmount = fillAmount;
            }
        }
    
        public void TurnOffNitroProgressBar()
        {
            nitroProgressBar.SetActive(false);
        }
    
        public void TurnOffMagnetProgressBar()
        {
            magnetProgressBar.SetActive(false);
        }
    
        public void TurnOffShieldProgressBar()
        {
            shieldProgressBar.SetActive(false);
        }
    
        public void SetTime(float time)
        {
            _timeLeft = time;
        }
    }
}
