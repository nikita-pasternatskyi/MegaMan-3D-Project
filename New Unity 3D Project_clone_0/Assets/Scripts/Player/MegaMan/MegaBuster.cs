﻿using Unity;
using UnityEngine;
using Mirror;

namespace Assets.Scripts.Player.MegaMan
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
        [Command]
        protected override void Shoot()
        {
            base.CreateProjectile();
        }

        [Server]
        protected override void CreateProjectile()
        {
            base.CreateProjectile();
        }

        public override void Refill(float ammoToRefill)
        {
            base.Refill(ammoToRefill);
        }

    }
}
