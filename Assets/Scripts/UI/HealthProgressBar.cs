using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthProgressBar : MonoBehaviour
    {
        public Image MaskHealth;
        public int MaximumHealth = 100;
        public int MinimumHealth = 0;
        private float _current;

        public static HealthProgressBar Instance { get; private set; }
        void Start()
        {
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            GetCurrentFill(_current, MinimumHealth, MaximumHealth, MaskHealth);
        }

        private void GetCurrentFill(float current, int minimum, int maximum, Image mask)
        {
            var currentOffset = current - minimum;
            var maximumOffset = maximum - minimum;
            var fillAmount = currentOffset / maximumOffset;
            mask.fillAmount = fillAmount;
        }

        public void SetHealth(int health)
        {
            _current = health;
        }
    }
}
