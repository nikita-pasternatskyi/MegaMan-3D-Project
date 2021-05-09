using UnityEngine;
using Core.Player;

namespace NonCore.Player.MegaMan
{
    [AddComponentMenu("Player/Mega Man/Mega Buster")]
    class MegaBuster : PlayerWeapon
    {
        protected override void OnAltFire()
        {
            base.OnAltFire();
        }

        protected override void OnMainFire()
        {
            base.OnMainFire();
        }

        public override void Refill(float ammoToRefill)
        {
            base.Refill(ammoToRefill);
        }

    }
}
