using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player;

namespace Assets.Scripts.Items
{
    [AddComponentMenu("Items/Weapon Refill")]
    public class WeaponRefill_Item : MonoBehaviour
    {
        [SerializeField] private float ammoToRefill;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerWeapon>() != null)
            {
                other.GetComponent<PlayerWeapon>().Refill(ammoToRefill);
                Destroy(this.gameObject);
            }
        }
    }
}
