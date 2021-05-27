using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core.General;
using Core.Interfaces;

namespace Core.Player
{
    [System.Serializable]
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField] private Image _energyMeter;
        [SerializeField] private TextMeshProUGUI _energyMeterText;
        public void RefreshUI(int health, float fillValue)
        {
            _energyMeterText.text = health.ToString();
            _energyMeter.fillAmount = fillValue;
        }

    }
}
