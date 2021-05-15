using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ScriptableObjects;

namespace Core.Player
{
    public class WeaponContainer : RequiresInput
    {
        // Start is called before the first frame update
        [SerializeField] private WeaponClassConfiguration _mainWeapon;
        [SerializeField] private WeaponClassConfiguration _alternateWeapon;

        private Weapon MainWeapon;
        private Weapon AlternateWeapon;

        private void Start()
        {
            MainWeapon = _mainWeapon.Weapon;
            AlternateWeapon = _alternateWeapon.Weapon;
        }

        protected override void OnMainFire()
        {
            MainWeapon.MainFire();
        }

        protected override void OnAlternateFire()
        {
            AlternateWeapon.AlternateFire();
        }

    }
}