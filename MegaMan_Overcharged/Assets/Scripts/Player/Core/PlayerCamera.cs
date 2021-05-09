using Core.Levels;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    [AddComponentMenu("Player/Base/Camera Control")]

    class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _bodyToXRotate;
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private Camera _camera;
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
                    _bodyToXRotate.rotation = Quaternion.Euler(0, 0, 0);
                    _camera.transform.rotation = Quaternion.Euler(0, 0, 0);
                    _mouseCanLookAround = false;
                    break;
                case CameraModes.FPS:
                    _rotateWithPivot = false;
                    _camera.transform.localPosition = new Vector3(0, 0, 0);
                    break;
            }
        }

        private void OnEnable()
        {
            Invoke(nameof(EnableCameraLook), 0.5f);
            Cursor.lockState = _lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        }

        private void MouseLook(Vector2 mouseInput)
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                if (_mouseCanLookAround)
                {
                    if (_bodyToXRotate != null)
                    {
                        mouseInput *= Time.deltaTime;

                        _cameraXRotation -= mouseInput.y;
                        _cameraXRotation = Mathf.Clamp(_cameraXRotation, _minimumMouseRotation, _maximumMouseRotation);

                        if (_rotateWithPivot)
                        {
                            _cameraPivot.transform.localRotation = Quaternion.Euler(_cameraXRotation, 0, 0);
                        }
                        else {
                            _camera.transform.localRotation = Quaternion.Euler(_cameraXRotation, 0, 0);
                        }
                        _bodyToXRotate.rotation *= Quaternion.Euler(0, mouseInput.x, 0);
                    }

                }
            }
        }

        private void OnMouseLook(InputValue value)
        {
            MouseLook(value.Get<Vector2>());         
        }

        public void EnableCameraLook() => _mouseCanLookAround = true;
    }
}
