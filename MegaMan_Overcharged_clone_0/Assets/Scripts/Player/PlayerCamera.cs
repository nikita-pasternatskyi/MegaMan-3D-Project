using Assets.Scripts.Levels;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    class PlayerCamera : NetworkBehaviour
    {
        public float speed;
        [SerializeField] private Transform _bodyToYRotate;
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private Transform _endRotation;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _shakeSpeed;
        [SerializeField] private float _amplitude;
        [SerializeField] private float _minimumMouseRotation;
        [SerializeField] private float _maximumMouseRotation;
        [SerializeField] private bool _lockCursor;
        [SerializeField] private CameraModes _currentCameraMode;
        [SerializeField] private float _pivotModeZOffset;

        private float _cameraXRotation;
        private bool _mouseCanLookAround;
        private bool _rotateWithPivot;

        public void ChangeCameraMode(CameraModes _modeToSwitchTo)
        {
            _currentCameraMode = _modeToSwitchTo;
            switch (_currentCameraMode)
            {
                case CameraModes.AroundPoint:
                    _rotateWithPivot = true;
                    _camera.transform.localPosition = new Vector3(0, 0, _pivotModeZOffset);
                    _bodyToYRotate.rotation = Quaternion.Euler(0, 0, 0);
                    _camera.transform.rotation = Quaternion.Euler(0, 0, 0);
                    _mouseCanLookAround = false;
                    break;
                case CameraModes.FPS:
                    _rotateWithPivot = false;
                    _camera.transform.localPosition = new Vector3(0, 0, 0);
                    break;
            }
        }
        public override void OnStartAuthority()
        {
            Invoke("EnableCameraLook", 0.5f);
            Cursor.lockState = _lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        }

        private void Start()
        {
            
        }

        [ClientCallback]
        private void OnLook(InputValue value)
        {
            Vector2 mouseInput = value.Get<Vector2>();

            if (_mouseCanLookAround && isLocalPlayer)
            {
                if (_bodyToYRotate != null)
                {
                    mouseInput *= Time.deltaTime * speed;

                    _cameraXRotation -= mouseInput.y;
                    _cameraXRotation = Mathf.Clamp(_cameraXRotation, _minimumMouseRotation, _maximumMouseRotation);

                    if (_rotateWithPivot)
                    {
                        _cameraPivot.transform.localRotation = Quaternion.Euler(_cameraXRotation, 0, 0);
                    }
                    else
                    {
                        _camera.transform.localRotation = Quaternion.Euler(_cameraXRotation, 0, 0);
                    }

                    RotateCameraYAxis(mouseInput);
                    Rotate(_camera.transform.rotation.eulerAngles, mouseInput);
                }

            }

        }

        [ClientCallback]
        private void RotateCameraYAxis(Vector2 mouseInput)
        {
            _cameraPivot.rotation *= Quaternion.Euler(0, mouseInput.x, 0);
        }

        [Command]
        private void Rotate(Vector3 rotation, Vector2 mouseInput)
        {
            _bodyToYRotate.rotation *= Quaternion.Euler(0, mouseInput.x, 0);
            _endRotation.rotation = Quaternion.Euler(rotation);
        }

        [ClientCallback]
        public void EnableCameraLook() => _mouseCanLookAround = true;


    }
}
