using Core.Levels;
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
        private bool _isGrounded;
        private float _normalColliderHeight;


        private CapsuleCollider _colliderToShrink;
        private MonoBehaviour _parentMonoBehaviour;
        private Rigidbody _playerRigidbody;
        private Transform _referenceTransform;

        public void Start(in bool isGrounded,in Transform referenceTransform, in Rigidbody playerRigidbody, in MonoBehaviour parentMonoBehaviour, ref CapsuleCollider colliderToShrink)
        {
            _isGrounded = isGrounded;
            _colliderToShrink = colliderToShrink;
            _normalColliderHeight = _colliderToShrink.height;
            _referenceTransform = referenceTransform;
            _playerRigidbody = playerRigidbody;
            _parentMonoBehaviour = parentMonoBehaviour;
        }

        public override void UseSpecialAbility()
        {
            if (!_isSliding && _isGrounded && !LevelSettings.Instance.IsPaused)
                _parentMonoBehaviour.StartCoroutine(AddSlideVelocity());
        }

        private IEnumerator AddSlideVelocity()
        {
            _isSliding = true;
            _colliderToShrink.height = _slideColliderHeight;
            float currentTime = _slideTime;
            _playerRigidbody.AddForce(_referenceTransform.forward * _slideBurst, ForceMode.Impulse);
            while (currentTime > 0)
            {
                currentTime -= Time.fixedDeltaTime;
                _playerRigidbody.AddForce(_referenceTransform.forward * _slideForce);
                yield return new WaitForFixedUpdate();
            }
            _isSliding = false;
            while (_colliderToShrink.height < _normalColliderHeight)
            {
                _colliderToShrink.height += Time.fixedDeltaTime * _recoverySpeed;
                yield return new WaitForFixedUpdate();
            }
            _colliderToShrink.height = _normalColliderHeight;

            yield break;
        }

    }
}
