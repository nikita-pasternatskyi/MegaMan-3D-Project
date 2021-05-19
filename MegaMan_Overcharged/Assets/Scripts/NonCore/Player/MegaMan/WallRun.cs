using Core.Player;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    [System.Serializable]
    public class WallRun
    {
        [SerializeField] private ParticleSystem _runSpeedLines;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private LayerMask WhatIsWall;
        [SerializeField] private float ClosestWallDistance;
        [SerializeField] private float MinimumDistanceToGround;
        [SerializeField] private float _maxAngleRoll;

        [SerializeField] private float WallRunGravity;
        [SerializeField] private float WallJumpUpForce;
        [SerializeField] private float WallJumpNormalForce;
        [SerializeField] private float WallJumpForwardForce;
        [SerializeField] private float _cameraTransitionDuration;
        [SerializeField] private CameraControl _playerCamera;

        private Transform _referenceRotation;
        private Rigidbody _rigidBody;
        private Transform _parentTransform;

        private Vector2 _unprocessedMovementInput;
        private Vector3 _lastWallNormal;

        private Vector3 _lastInputDirection;
        private bool isWallRunning;

        public void Start(in Transform parentTransform, in Transform referenceTransform, in Rigidbody rigidBody)
        {
            _rigidBody = rigidBody;
            _parentTransform = parentTransform;
            _referenceRotation = referenceTransform;
        }
        public void FixedUpdate()
        {
            Vector3 movementInputDirection = CurrentMovementInputDirection();

            if (CanWallRun(movementInputDirection, out RaycastHit wallHit))
                StartWallRun(movementInputDirection, wallHit);

            if (isWallRunning)
                WallRunning();
        }
        public void OnMovement(in Vector2 value) => _unprocessedMovementInput = value;
        public void Jump()
        {
            if (isWallRunning)
            {
                //_rigidBody.velocity.y = 0;
                _rigidBody.AddForce(WallJumpDirection());
            }
        }

        private void StartWallRun(in Vector3 movementInputDirection, in RaycastHit wallHit)
        {
            _runSpeedLines.Play();
            var targetZ = _maxAngleRoll;
            _lastInputDirection = movementInputDirection;
            _lastWallNormal = wallHit.normal;
            isWallRunning = true;
            if (_referenceRotation.TransformVector(_lastWallNormal).x > 0)
            {
                targetZ *= -1;
            }

            if (_lastWallNormal.z == 0) //if its not forward hit/backward then rotate
                _playerCamera.CallRotateZCamera(targetZ);

            if (_rigidBody.velocity.y < 0) ;
               // _rigidBody.ResetYVelocity();
        }
        private void WallRunning()
        {
            if (Physics.Raycast(_referenceRotation.position, _lastInputDirection, 1f, WhatIsWall))
            {
                Vector3 wallRunVelocity = Vector3.zero;
                if (_rigidBody.velocity.y < 0)
                    wallRunVelocity.y = WallRunGravity * Time.fixedDeltaTime;
                _rigidBody.AddForce(wallRunVelocity);
            }
            else
            {
                _runSpeedLines.Stop();
                _playerCamera.CallRotateZCamera(0);
                isWallRunning = false;
            }
            return;
        }
        private void Debugging()
        {
            Vector3 direction = new Vector3(_unprocessedMovementInput.x, _referenceRotation.position.y, _unprocessedMovementInput.y);
            direction = _referenceRotation.TransformDirection(direction);
            direction.y = 0;
            Debug.DrawRay(_referenceRotation.position, direction * ClosestWallDistance, Color.red);

            Debug.DrawRay(_parentTransform.position, -_parentTransform.up * MinimumDistanceToGround, Color.blue);
        }
        private bool CanWallRun(in Vector3 checkDirection, out RaycastHit hit)
        {
            bool hitWall = Physics.Raycast(_referenceRotation.position, checkDirection, out hit, ClosestWallDistance, WhatIsWall);
            if (CanWallRunRelativeGround() && hitWall && !isWallRunning)
            {
                if (CheckSurfaceAngle(hit.normal, 90))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        private bool CanWallRunRelativeGround()
        {
            if (Physics.Raycast(_parentTransform.position, -_parentTransform.up, out RaycastHit groundHit, Mathf.Infinity, WhatIsWall))
            {
                if (groundHit.distance >= MinimumDistanceToGround)
                    return true;
            }
            else if (!Physics.Raycast(_parentTransform.position, -_parentTransform.up, MinimumDistanceToGround, WhatIsWall))
            {
                return true;
            }
            return false;
        }
        private bool CheckSurfaceAngle(Vector3 surface, float targetAngle)
        {
            return Mathf.Abs(targetAngle - Vector3.Angle(Vector3.up, surface)) < 0.1f;
        }
        private Vector3 ReferenceForwardTransform(Transform transform)
        {
            Vector3 returnValue = transform.forward;
            returnValue.y = 0;
            return returnValue;
        }
        private Vector3 CurrentMovementInputDirection()
        {
            Vector3 movementInputDirection = new Vector3(_unprocessedMovementInput.x, _referenceRotation.position.y, _unprocessedMovementInput.y);
            movementInputDirection = _referenceRotation.TransformDirection(movementInputDirection);
            movementInputDirection.y = .5f;
            return movementInputDirection.normalized;
        }
        private Vector3 WallJumpDirection()
        {
            Vector3 forceModificators = (Vector3.up * WallJumpUpForce + ReferenceForwardTransform(_referenceRotation) * WallJumpForwardForce);
            return _lastWallNormal * WallJumpNormalForce + forceModificators * Time.fixedDeltaTime;
        }

    }
}
