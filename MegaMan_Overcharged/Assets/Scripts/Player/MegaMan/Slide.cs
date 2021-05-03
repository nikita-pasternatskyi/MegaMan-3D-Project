using System;
using UnityEngine;
using Mirror;
using System.Collections;

namespace Assets.Scripts.Player.MegaMan
{
    [AddComponentMenu("Player/Mega Man/Slide")]
    class Slide : PlayerSpecialAbility
    {
        [SerializeField] private float _slideForce;
        [SerializeField] private float _slideTime;
        [SerializeField] private PlayerPhysics _playerPhysics;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform referenceRotation;
        [SerializeField] private float _newHeight;
        private float _oldHeight;
        [SyncVar] private float _currentSlideTime;
        [SyncVar] private Vector3 _slideVelocity;
        [SyncVar] private bool _isSliding = false;


        private void Start()
        {
            ValidateSlideTime();
        }
        //[ClientCallback]
        protected override void OnSpecialAbility()
        {
            if (!_isSliding)
            {
                ValidateSlideTime();
                StartCoroutine(CountSlide());
            }
        }

        [Command]
        private void ValidateSlideTime()
        {
            _currentSlideTime = _slideTime;
            _slideVelocity = referenceRotation.forward * _slideForce;
        }

        private IEnumerator CountSlide()
        {
            DecreaseCollider();
            _isSliding = true;
            _currentSlideTime = _slideTime;
            while (_currentSlideTime > 0)
            {
                _currentSlideTime -= Time.fixedDeltaTime;
                _playerPhysics.AddVelocity(referenceRotation.forward * _slideForce * _slideTime);
                yield return new WaitForFixedUpdate();
            }
            _isSliding = false;
            ValidateSlideTime();
            IncreaseCollider();
            yield break;
            
        }

        [Command]
        private void DecreaseCollider()
        {
            _oldHeight = _characterController.height;
            _characterController.height = _newHeight;
            //_characterController.center = new Vector3(_characterController.center.x, _newHeight/2, _characterController.center.z);
        }

        [Command]
        private void IncreaseCollider()
        {
            _characterController.height = _oldHeight;
           // _characterController.center = new Vector3(_characterController.center.x, _oldHeight / 2, _characterController.center.z);
        }
    }
}
