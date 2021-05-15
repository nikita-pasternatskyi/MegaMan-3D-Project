using Core.Player;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NonCore.Player.MegaMan
{
    [System.Serializable]
    public class WallRun
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private LayerMask WhatIsWall;
        [SerializeField] private float ClosestWallDistance;
        [SerializeField] private float MinimumDistanceToGround;
        [SerializeField] private float _maxAngleRoll;

        [SerializeField] private float WallRunSpeed;
        [SerializeField] private float WallRunGravity;
        [SerializeField] private float WallJumpUpForce;
        [SerializeField] private float WallJumpNormalForce;
        [SerializeField] private float WallJumpForwardForce;
        [SerializeField] private float _cameraTransitionDuration;
        [SerializeField] private PlayerCamera _playerCamera;

        private Transform _referenceRotation;
        private PlayerPhysics _playerPhysics;
        private Transform _parentTransform;
        private MonoBehaviour _parentMonoBehaviour;

        private bool _cameraInTransition;

        private Vector2 _currentMovementInput;
        private Vector3 _lastWallPosition;
        private Vector3 _lastWallNormal;

        private Vector3 _lastInputDirection;
        private bool isWallRunning;

        public void Start(in MonoBehaviour parentMonoBehaviour,in Transform parentTransform, Transform referenceTransform, in PlayerPhysics playerPhysics)
        {
            _parentMonoBehaviour = parentMonoBehaviour;
            _playerPhysics = playerPhysics;
            _parentTransform = parentTransform;
            _referenceRotation = referenceTransform;
        }
        public void FixedUpdate()
        {
            Vector3 movementInputDirection = CurrentMovementInputDirection();

            if (CanWallRun(in movementInputDirection, out RaycastHit wallHit)) 
                StartWallRun(movementInputDirection, wallHit);

            if (isWallRunning)
                WallRunning();
        }
        public void OnMovement(in Vector2 value) => _currentMovementInput = value;
        public void Jump()
        {
            if(isWallRunning)
                _playerPhysics.AddVelocity(WallJumpDirection());
        }

        private void StartWallRun( Vector3 movementInputDirection,  RaycastHit wallHit) 
        {
            var targetZ = _maxAngleRoll;
            bool rightSide = false;
            Debug.Log(_lastWallNormal);
            if (_lastWallNormal.x > 0)
            {
                targetZ *= -1;
            }
            _playerCamera.RotateCamera(PlayerCamera.Axis.Z, _cameraTransitionDuration, targetZ, rightSide);
            if (_playerPhysics.Velocity.y < 0)
                _playerPhysics.ResetYVelocity();
            _lastInputDirection = movementInputDirection;
            _lastWallNormal = wallHit.normal;
            _lastWallPosition = wallHit.transform.position;
            isWallRunning = true;
            return;
        }
        private void WallRunning()
        {
            //_cameraTransform.eulerAngles = new Vector3(_cameraTransform.transform.rotation.eulerAngles.x, _cameraTransform.transform.rotation.eulerAngles.y, CalculateCameraRotation());
            
            Vector3 outputDirection;
            outputDirection = _lastInputDirection;
            _lastInputDirection.z = 0;
            _lastInputDirection.y = 0;         
            if (Physics.Raycast(_referenceRotation.position, outputDirection, out RaycastHit groundHit, 1f, WhatIsWall))
            {
                Vector3 wallRunVelocity = ReferenceForwardTransform(_referenceRotation) * WallRunSpeed;
                wallRunVelocity.y = WallRunGravity * Time.fixedDeltaTime;
                _playerPhysics.AddVelocity(wallRunVelocity);
            }
            else {
                _playerCamera.RotateCamera(PlayerCamera.Axis.Z, _cameraTransitionDuration, 0, true);
                isWallRunning = false;
            }
            return;
        }
        private void Debugging()
        {
            Vector3 direction = new Vector3(_currentMovementInput.x, _referenceRotation.position.y, _currentMovementInput.y);
            direction = _referenceRotation.TransformDirection(direction);
            direction.y = 0;
            Debug.DrawRay(_referenceRotation.position, direction * ClosestWallDistance, Color.red);

            Debug.DrawRay(_parentTransform.position, -_parentTransform.up * MinimumDistanceToGround, Color.blue);
        }

        private IEnumerator RotateCamera(float targetRotZ)
        {
            yield break;
            ////_cameraInTransition = true;
            //bool whenCalled = isWallRunning;
            //Debug.Log(targetRotZ);
            //float targetZ = targetRotZ;
            //if (_lastWallNormal.z < 0)
            //    targetZ *= -1;
            //var camRot = _cameraTransform.eulerAngles;
            //if (!isWallRunning)
            //{
            //    while (camRot.z != 0)
            //    {
            //        camRot = _cameraTransform.rotation.eulerAngles;
            //        _cameraTransform.eulerAngles = new Vector3(camRot.x, camRot.y, Mathf.LerpAngle(camRot.z, targetRotZ, _cameraTransitionDuration * Time.deltaTime));
            //        yield return null;
            //    }
            //    _cameraInTransition = false;
            //}
            //else if (isWallRunning)
            //{
            //    while (targetZ - camRot.z * camRot.z > 0.001f || targetZ * targetZ - camRot.z * camRot.z < 0.001f)
            //    {
            //        camRot = _cameraTransform.rotation.eulerAngles;
            //        _cameraTransform.eulerAngles = new Vector3(camRot.x, camRot.y, Mathf.LerpAngle(camRot.z, targetRotZ, _cameraTransitionDuration * Time.deltaTime));
            //        yield return null;
            //    }
            //}
            //_cameraInTransition = false;
        }

        private bool CanWallRun(in Vector3 direction, out RaycastHit hit)
        {
            bool hitWall = Physics.Raycast(_referenceRotation.position, direction, out hit, ClosestWallDistance, WhatIsWall);
            if (CanWallRunRelativeGround() && hitWall)
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
            Vector3 movementInputDirection = new Vector3(_currentMovementInput.x, _referenceRotation.position.y, _currentMovementInput.y);
            movementInputDirection = _referenceRotation.TransformDirection(movementInputDirection);
            movementInputDirection.y = .5f;
            return movementInputDirection.normalized;
        }
        private Vector3 WallJumpDirection()
        {
            _playerPhysics.ResetYVelocity();
            return _lastWallNormal * WallJumpNormalForce + (Vector3.up * WallJumpUpForce + ReferenceForwardTransform(_referenceRotation) * WallJumpForwardForce) * Time.fixedDeltaTime;
        }

    }
}
