using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.General;
using Mirror;

namespace Assets.Scripts.Player
{
    public class UIPlayerHealth : NetworkBehaviour
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
