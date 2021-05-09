using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core.General;

namespace Core.Player.UI
{
    public class UIPlayerHealth : MonoBehaviour
    {
        [SerializeField] private Health _playerHealth;
        [SerializeField] private Image _energyMeter;
        [SerializeField] private TextMeshProUGUI _energyMeterText;

        private void OnEnable()
        {
            _playerHealth.HealthChanged += RefreshUI;
        }

        void RefreshUI(int health, float fillValue)
        {
            _energyMeterText.text = health.ToString();
            _energyMeter.fillAmount = fillValue;
        }

        private void OnDisable()
        {
            _playerHealth.HealthChanged -= RefreshUI;
        }
    }
}
