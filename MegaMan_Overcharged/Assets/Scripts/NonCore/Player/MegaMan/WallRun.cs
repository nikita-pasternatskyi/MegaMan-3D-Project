using Core.General;
using Core.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NonCore.Player.MegaMan
{
    [RequireComponent(typeof(PlayerPhysics))]
    public class WallRun : RequiresInput
    {
        [SerializeField] private CameraControl _playerCamera;
        [SerializeField] private VFXCaller _speedLineEffect;
        [SerializeField] private Transform _referenceRotation;
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

        private PlayerPhysics _playerPhysics;
        private Vector2 _unprocessedMovementInput;
        private Vector3 _lastWallNormal;

        private Vector3 _lastInputDirection;
        private bool isWallRunning;

        public void Start()
        {
            _unprocessedMovementInput = new Vector2();
            _playerPhysics = GetComponent<PlayerPhysics>();
        }

        public void FixedUpdate()
        {
            Debugging();

            Vector3 movementInputDirection = CurrentMovementInputDirection();

            if (CanWallRun(movementInputDirection, out RaycastHit wallHit))
                StartWallRun(movementInputDirection, wallHit);

            if (isWallRunning)
                WallRunning();
        }
        public new void OnMovement(InputValue value) => _unprocessedMovementInput = value.Get<Vector2>();
        public new void OnJump()
        {
            if (isWallRunning)
            {
                _playerPhysics.ResetYVelocity();
                _playerPhysics.AddVelocity(WallJumpDirection());
            }
        }

        private void StartWallRun(in Vector3 movementInputDirection, in RaycastHit wallHit)
        {
            _speedLineEffect.EnableVFX();
            var targetZ = _maxAngleRoll;
            _lastInputDirection = movementInputDirection;
            _lastWallNormal = wallHit.normal;
            isWallRunning = true;
            if (_referenceRotation.TransformVector(_lastWallNormal).x > 0)
            {
                targetZ *= -1;
            }

            if (_lastWallNormal.z == 0)
            {  //if its not forward hit/backward then rotate
                Debug.Log("CallRotateZCamera");
                _playerCamera.CallRotateZCamera(targetZ);
            }

            else {
                Debug.Log("NotCallRotateZCamera");
                _playerCamera.CallRotateZCamera(targetZ);
            }

            if (_playerPhysics.Velocity.y < 0)
                _playerPhysics.ResetYVelocity();
        }
        private void WallRunning()
        {
            if (Physics.Raycast(_referenceRotation.position, _lastInputDirection, 1f, WhatIsWall))
            {
                Vector3 wallRunVelocity = Vector3.zero;
                if (_playerPhysics.Velocity.y < 0)
                    wallRunVelocity.y = WallRunGravity * Time.fixedDeltaTime;
                _playerPhysics.AddVelocity(wallRunVelocity);
            }
            else
            {
                _speedLineEffect.DisableVFX();
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

            Debug.DrawRay(transform.position, -transform.up * MinimumDistanceToGround, Color.blue);
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
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit groundHit, Mathf.Infinity, WhatIsWall))
            {
                if (groundHit.distance >= MinimumDistanceToGround)
                    return true;
            }
            else if (!Physics.Raycast(transform.position, -transform.up, MinimumDistanceToGround, WhatIsWall))
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
