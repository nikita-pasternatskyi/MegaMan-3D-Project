using Unity;
using UnityEngine;

namespace Assets.Scripts.Player.MegaMan
{
    [AddComponentMenu("Player/Mega Man/Mega Buster")]
    class MegaBuster : PlayerWeapon
    {
        protected override void AltFire()
        {
            base.AltFire();
        }

        protected override void MainFire()
        {
            base.MainFire();
        }

        public override void Refill(float ammoToRefill)
        {
            base.Refill(ammoToRefill);
        }

    }
}
