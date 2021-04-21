using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.General;
using Mirror;

namespace Assets.Scripts.Items
{
    [AddComponentMenu("Items/Healing Item")]
    class Healing_Item : Item
    {
        [SerializeField] private int _healthToHeal;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.GetComponent<Health>() != null)
            {
                other.GetComponent<Health>().Heal(_healthToHeal);
                Destroy(this.gameObject);
            }
        }
    }
}
