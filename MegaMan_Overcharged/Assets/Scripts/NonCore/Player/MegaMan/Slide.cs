using Core.General;
using Core.Levels;
using Core.Player;
using System.Collections;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    [RequireComponent(typeof(PlayerPhysics))]
    public class Slide : PlayerSpecialAbility
    {

        [SerializeField] private Transform _referenceTransform;
        [SerializeField] private VFXCaller _speedLineEffect;
        [SerializeField] private float _slideBurst;
        [SerializeField] private float _slideForce;
        [SerializeField] private float _slideTime;
        [SerializeField] private float _slideColliderHeight;
        [SerializeField] private float _recoverySpeed;

        private bool _isSliding;
        private float _normalColliderHeight;
        private CharacterController _characterControllerToShrink;
        private PlayerPhysics _playerPhysics;

        private void Start()
        {
            _playerPhysics = GetComponent<PlayerPhysics>();
            _characterControllerToShrink = GetComponent<CharacterController>();
        }

        protected override void OnSpecialAbility()
        {
            if (isActiveAndEnabled)
            {
                if (!_isSliding && _playerPhysics.IsGrounded && !LevelSettings.Instance.IsPaused)
                    StartCoroutine(AddSlideVelocity());
            }
        }

        private IEnumerator AddSlideVelocity()
        {
            _normalColliderHeight = _characterControllerToShrink.height;
            _speedLineEffect.EnableVFX();
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
            _speedLineEffect.DisableVFX();
            yield return ReturnNormalHeight();
        }

        private IEnumerator ReturnNormalHeight()
        {
            while (_characterControllerToShrink.height < _normalColliderHeight)
            {
                _characterControllerToShrink.height += Time.fixedDeltaTime * _recoverySpeed;
                yield return new WaitForFixedUpdate();
            }
            _characterControllerToShrink.height = _normalColliderHeight;
        }

    }
}
