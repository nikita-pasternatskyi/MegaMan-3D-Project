using System;
using UnityEngine;
using Mirror;
namespace Assets.Scripts.Player.MegaMan
{
    [AddComponentMenu("Player/Mega Man/Slide")]
    class Slide : PlayerSpecialAbility
    {
        [SerializeField] private float _slideForce;
        [SerializeField] private CharacterController _characterController;

        protected override void OnSpecialAbility()
        {
            throw new NotImplementedException();
        }
    }
}
