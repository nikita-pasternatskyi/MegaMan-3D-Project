using Assets.Scripts.Levels;
using UnityEngine;
using Mirror;

namespace Assets.Scripts.Player
{
    [AddComponentMenu("Player/Base/Camera Control")]

    class PlayerCamera : NetworkBehaviour
    {
        [SerializeField] private Transform _bodyToYRotate;
        [SerializeField] private Transform _cameraPivot;
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
            Input.MouseMoved += MouseLook;
        }

        public override void OnStopAuthority()
        {
            Input.MouseMoved -= MouseLook;
        }
        
        [ClientCallback]
        private void MouseLook(Vector2 mouseInput)
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                if (_mouseCanLookAround)
                {
                    if (_bodyToYRotate != null)
                    {
                        mouseInput *= Time.deltaTime;

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
                        RotateBodyYAxis(mouseInput);
                        RotateCameraYAxis(mouseInput);
                    }

                }
            }
        }

        [ClientCallback]
        private void RotateCameraYAxis(Vector2 mouseInput)
        {
            _cameraPivot.transform.rotation *= Quaternion.Euler(0, mouseInput.x, 0);
        }

        [Command]
        private void RotateBodyYAxis(Vector2 mouseInput)
        {
            _bodyToYRotate.rotation *= Quaternion.Euler(0, mouseInput.x, 0);
        }

        [ClientCallback]
        public void EnableCameraLook() => _mouseCanLookAround = true;


    }
}
