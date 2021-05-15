using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Player;
using Core.General;

namespace Core.Items
{
    class WeaponRefill : Item
    {
        [SerializeField] private int ammoToRefill;

        private void OnTriggerEnter(Collider other)
        {         
            if (other.GetComponent<Weapon>() != null)
            {
                InvokePickUpEvent();
                other.GetComponent<Weapon>().Refill(ammoToRefill);
                Destroy(this.gameObject);
            }
        }
    }
}
