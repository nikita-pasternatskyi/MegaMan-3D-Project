using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.General;

namespace Assets.Scripts.Items
{
    [AddComponentMenu("Items/Healing Item")]
    public class Healing_Item : MonoBehaviour, IItem
    {

        [SerializeField] private int _healthToHeal;


        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != null)
            {
                other.GetComponent<Health>().Heal(_healthToHeal);
                Destroy(this.gameObject);
            }
        }
    }
}
