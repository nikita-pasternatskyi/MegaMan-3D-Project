using UnityEngine;
using System.Collections;
using Core.Player;

namespace NonCore.Player.MegaMan
{
    [AddComponentMenu("Player/Mega Man/Slide")]
    class Slide : PlayerSpecialAbility
    {
        private bool _isSliding;
        private float _normalColliderHeight;
        [SerializeField] private float _slideForce;
        [SerializeField] private float _slideTime;
        [SerializeField] private float _slideColliderHeight;
        [SerializeField] private float _recoverySpeed;
        [SerializeField] private PlayerPhysics _playerPhysics;
        [SerializeField] private CharacterController _characterControllerToShrink;
        [SerializeField] private Transform _referenceTransform;

        protected override void OnSpecialAbility()
        {
            if(!_isSliding)
            StartCoroutine(AddSlideVelocity());
        }

        private IEnumerator AddSlideVelocity()
        {
            _isSliding = true;
            _normalColliderHeight = _characterControllerToShrink.height;
            float currentTime = _slideTime;
            while (currentTime > 0)
            {
                currentTime-= Time.fixedDeltaTime;
                _characterControllerToShrink.height = _slideColliderHeight;
                _playerPhysics.AddVelocity(_referenceTransform.forward * _slideForce * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
            while (_characterControllerToShrink.height < _normalColliderHeight)
            {
                _characterControllerToShrink.height += Time.fixedDeltaTime * _recoverySpeed;
                yield return new WaitForFixedUpdate();
            }
            _characterControllerToShrink.height = _normalColliderHeight;

            _isSliding = false;
            yield break;
        }

    }
}
