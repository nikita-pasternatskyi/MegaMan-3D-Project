using Core.Player;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    public class MegaArmProjectile : Projectile
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxFlyTime;
        [SerializeField] private float _returnSpeed;
        [SerializeField] private bool _isReturning;
        [SerializeField] private Transform _positionToReturnTo;
        private float _timer;

        public void Initialize(Transform positionToReturnTo)
        {
            _positionToReturnTo = positionToReturnTo;
        }

        private void FixedUpdate()
        {
            if (FinishedFlight() && !_isReturning)
            {
                StartCoroutine(ReturnToArm());
            }
        }
        private IEnumerator ReturnToArm()
        {
            _isReturning = true;
            var difference = 1f;
            while (difference > 0.001f || difference < -0.001f)
            {              
                difference = transform.position.sqrMagnitude - _positionToReturnTo.position.sqrMagnitude;
                transform.position = Vector3.MoveTowards(transform.position, _positionToReturnTo.position, _returnSpeed);
                yield return new WaitForFixedUpdate();
            }
            _isReturning = false;
            Destroy(this.gameObject);
            yield break;
            
        }

        private bool FinishedFlight()
        {
            if (_timer < _maxFlyTime)
            {
                _timer += Time.fixedDeltaTime;
                FlyForward();
                return false;
            }
            return true;
        }

        private void FlyForward()
        {
            transform.position += transform.forward * _speed * Time.fixedDeltaTime;     
        }
    }
}
