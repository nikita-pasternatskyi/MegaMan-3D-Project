using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core.General;

namespace Core.Player
{
    [System.Serializable]
    public class PlayerHealthWithUI
    {
        [SerializeField] private Image _energyMeter;
        [SerializeField] private TextMeshProUGUI _energyMeterText;
        public InternalHealth _health { get; private set; }

        public void Start(int maxHealth)
        {
            _health = new InternalHealth(maxHealth);
            _health.HealthChanged += RefreshUI;
            _health.Start();
        }
        public void OnDisable()
        {
            _health.HealthChanged -= RefreshUI;
        }

        private void RefreshUI(int health, float fillValue)
        {
            _energyMeterText.text = health.ToString();
            _energyMeter.fillAmount = fillValue;
        }

    }
}
