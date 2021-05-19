﻿using Core.Levels;
using Core.Player;
using System.Collections;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    [System.Serializable]
    public class Slide : PlayerSpecialAbility
    {
        [SerializeField] private float _slideBurst;
        [SerializeField] private float _slideForce;
        [SerializeField] private float _slideTime;
        [SerializeField] private float _slideColliderHeight;
        [SerializeField] private float _recoverySpeed;

        private bool _isSliding;
        private float _normalColliderHeight;

        private CharacterController _characterControllerToShrink;
        private MonoBehaviour _parentMonoBehaviour;
        private PlayerPhysics _playerPhysics;
        private Transform _referenceTransform;

        public void Start(in Transform referenceTransform, in PlayerPhysics playerPhysics, in MonoBehaviour parentMonoBehaviour, ref CharacterController characterController)
        {
            _characterControllerToShrink = characterController;
            _normalColliderHeight = _characterControllerToShrink.height;
            _referenceTransform = referenceTransform;
            _playerPhysics = playerPhysics;
            _parentMonoBehaviour = parentMonoBehaviour;
        }

        public override void UseSpecialAbility()
        {
            if (!_isSliding && _playerPhysics.IsGrounded && !LevelSettings.Instance.IsPaused)
                _parentMonoBehaviour.StartCoroutine(AddSlideVelocity());
        }

        private IEnumerator AddSlideVelocity()
        {
            _isSliding = true;
            _characterControllerToShrink.height = _slideColliderHeight;
            float currentTime = _slideTime;
            _playerPhysics.AddVelocity(_referenceTransform.forward * _slideBurst);
            while (currentTime > 0)
            {
                currentTime -= Time.fixedDeltaTime;
                _playerPhysics.AddVelocity(_referenceTransform.forward * _slideForce);
                yield return new WaitForFixedUpdate();
            }
            _isSliding = false;
            while (_characterControllerToShrink.height < _normalColliderHeight)
            {
                _characterControllerToShrink.height += Time.fixedDeltaTime * _recoverySpeed;
                yield return new WaitForFixedUpdate();
            }
            _characterControllerToShrink.height = _normalColliderHeight;

            yield break;
        }

    }
}
