using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ScriptableObjects;
using UnityEngine.InputSystem;
using System;
using UnityEngine.VFX;

namespace Core.Player
{
    enum WeaponSwitchType { 
            MainWeapon, AlternateWeapon,
    }
    public class WeaponContainer : RequiresInput
    {
        private WeaponSwitchType _weaponSwitchType;
        [SerializeField] private int _currentMainWeaponIndex;
        [SerializeField] private int _currentAlternateWeaponIndex;
        [SerializeField] private Weapon[] _mainWeapons;
        [SerializeField] private Weapon[] _alternateWeapons;
        [SerializeField] private VisualEffect _mainMuzzleFlash;
        [SerializeField] private VisualEffect _alternateMuzzleFlash;
        private Weapon _currentMainWeapon;
        private Weapon _currentAlternateWeapon;

        private void Start()
        {
            _weaponSwitchType = WeaponSwitchType.MainWeapon;
            _currentMainWeapon = _mainWeapons[0];
            _currentAlternateWeapon = _alternateWeapons[0];
        }

        protected override void OnSwitchWeaponType()
        {
            if (_weaponSwitchType == WeaponSwitchType.AlternateWeapon)
                _weaponSwitchType = WeaponSwitchType.MainWeapon;
            else if (_weaponSwitchType == WeaponSwitchType.MainWeapon)
                _weaponSwitchType = WeaponSwitchType.AlternateWeapon;
        }

        protected override void OnSwitchWeapon(InputValue value)
        {
            float switchValue = value.Get<float>();
            if (_weaponSwitchType == WeaponSwitchType.AlternateWeapon)
            {               
                DetermineWeaponSwitch(switchValue, _alternateWeapons, ref _currentAlternateWeaponIndex, ref _currentAlternateWeapon);
            }
            else
            {
                DetermineWeaponSwitch(switchValue, _mainWeapons, ref _currentMainWeaponIndex, ref _currentMainWeapon);
            }

        }

        private void DetermineWeaponSwitch(in float switchValue, in Weapon[] weapons, ref int currentWeaponIndex, ref Weapon currentWeapon)
        {
            int previousWeaponIndex = currentWeaponIndex;
            if (switchValue > 0f)
            {
                if (currentWeaponIndex >= weapons.Length - 1)
                    currentWeaponIndex = 0;
                else
                {
                    currentWeaponIndex++;
                }
            }
            if (switchValue < 0f)
            {
                if (currentWeaponIndex <= 0)
                {
                    currentWeaponIndex = weapons.Length - 1;
                }
                else if (currentWeaponIndex >= weapons.Length - 1)
                    currentWeaponIndex = 0;
                else
                {
                    currentWeaponIndex++;
                }
            }

            if (previousWeaponIndex != currentWeaponIndex)
                SwitchWeapon(weapons, ref currentWeapon, currentWeaponIndex);
        }

        private void SwitchWeapon(in Weapon[] weapons, ref Weapon currentWeapon, in int currentWeaponIndex)
        {
            byte i = 0;
            foreach (Weapon weapon in weapons)
            {
                if (i == currentWeaponIndex)
                {
                    weapon.gameObject.SetActive(true);
                    currentWeapon = weapon;
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
                i++;
            }
        }

        protected override void OnMainFire()
        {
            _mainMuzzleFlash.SendEvent("Fire");
            _mainMuzzleFlash.Play();
            _currentMainWeapon?.OnMainFire();
        }

        protected override void OnAlternateFire()
        {
            _alternateMuzzleFlash.SendEvent("Fire");
            _mainMuzzleFlash.Play();
            _currentAlternateWeapon?.OnAlternateFire();
        }



    }
}