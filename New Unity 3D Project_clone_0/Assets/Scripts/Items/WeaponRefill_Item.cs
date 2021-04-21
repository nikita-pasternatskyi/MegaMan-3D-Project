using UnityEngine;
using Assets.Scripts.Player;
using Mirror;

namespace Assets.Scripts.Items
{
    [AddComponentMenu("Items/Weapon Refill")]
    class WeaponRefill_Item : Item
    {
        [SerializeField] private float ammoToRefill;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.GetComponent<PlayerWeapon>() != null)
            {
                other.GetComponent<PlayerWeapon>().Refill(ammoToRefill);
                Destroy(this.gameObject);
            }
        }
    }
}
