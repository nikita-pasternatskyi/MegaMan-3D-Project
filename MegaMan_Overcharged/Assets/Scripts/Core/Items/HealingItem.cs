using UnityEngine;
using Core.Interfaces;

namespace Core.Items
{
    public class HealingItem : Item
    {
        [SerializeField] private int _healthToHeal;

        private void OnTriggerEnter(Collider other)
        {
            InvokePickUpEvent();
            other.GetComponent<IHasHealth>()?.Heal(_healthToHeal);
            Destroy(this.gameObject);
        }
    }
}
