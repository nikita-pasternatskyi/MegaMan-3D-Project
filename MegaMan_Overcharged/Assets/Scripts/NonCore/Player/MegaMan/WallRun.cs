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
        [SerializeField] private LayerMask WhatIsWall;
        [SerializeField] private float ClosestWallDistance;
        [SerializeField] private float MinimumDistanceToGround;
        [SerializeField] private float _maxAngleRoll;

        [SerializeField] private float WallRunGravity;
        [SerializeField] private float WallJumpUpForce;
        [SerializeField] private float WallJumpNormalForce;
        [SerializeField] private float WallJumpForwardForce;

        private PlayerPhysics _playerPhysics;
        private Vector2 _unprocessedMovementInput;
        private Vector3 _lastWallNormal;

        private Vector3 _lastInputDirection;
        private bool isWallRunning;

        private void Start()
        {
            _unprocessedMovementInput = new Vector2();
            _playerPhysics = GetComponent<PlayerPhysics>();
        }

        private void FixedUpdate()
        {
            Debugging();

            Vector3 movementInputDirection = MovementDirection();

            if (CanWallRun(movementInputDirection, out RaycastHit wallHit))
                StartWallRun(movementInputDirection, wallHit);

            if (isWallRunning)
                WallRunning();
        }
        private new void OnMovement(InputValue value) => _unprocessedMovementInput = value.Get<Vector2>();
        private new void OnJump()
        {
            if (isWallRunning)
            {
                _playerPhysics.ResetYVelocity();
                _playerPhysics.AddVelocity(JumpFromWallForce());
            }
        }

        private void StartWallRun(in Vector3 movementInputDirection, in RaycastHit wallHit)
        {
            var targetZRotation = _maxAngleRoll;
            _speedLineEffect.EnableVFX();
            _lastInputDirection = movementInputDirection;
            _lastWallNormal = wallHit.normal;
            isWallRunning = true;

            if (_referenceRotation.InverseTransformVector(_lastWallNormal).x > 0)
            {
                targetZRotation *= -1;
            }

            //if the wall is in front of us
            if (Mathf.Abs(_referenceRotation.InverseTransformVector(_lastWallNormal).z) < .5f)
            {  
                _playerCamera.CallRotateZCamera(targetZRotation);
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
        private bool CanWallRun(in Vector3 checkDirection, out RaycastHit WallHit)
        {
            bool hitWall = Physics.Raycast(_referenceRotation.position, checkDirection, out WallHit, ClosestWallDistance, WhatIsWall);
            if (RelativeToGround() && hitWall && !isWallRunning)
            {
                if (CheckSurfaceAngle(WallHit.normal, 90))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        private bool RelativeToGround()
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
        private bool CheckSurfaceAngle(Vector3 surface, float targetAngle) => Mathf.Abs(targetAngle - Vector3.Angle(Vector3.up, surface)) < 0.1f;
        private Vector3 MovementDirection()
        {
            Vector3 movementInputDirection = new Vector3(_unprocessedMovementInput.x, _referenceRotation.position.y, _unprocessedMovementInput.y);
            movementInputDirection = _referenceRotation.TransformDirection(movementInputDirection);
            movementInputDirection.y = .5f;
            return movementInputDirection.normalized;
        }
        private Vector3 JumpFromWallForce()
        {
            var JumpForwardDirection = _referenceRotation.forward;
            JumpForwardDirection.y = 0;
            Vector3 forceModificators = (Vector3.up * WallJumpUpForce + JumpForwardDirection * WallJumpForwardForce);
            return _lastWallNormal * WallJumpNormalForce + forceModificators;
        }

    }
}
