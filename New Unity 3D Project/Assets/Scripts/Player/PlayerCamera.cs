using UnityEngine;

namespace Assets.Scripts.Player
{
    [AddComponentMenu("Player/Base/Camera Control")]



    class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _bodyToXRotate;
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
                    break;
                case CameraModes.FPS:
                    _rotateWithPivot = false;
                    _camera.transform.localPosition = new Vector3(0, 0, 0);
                    break;
            }
        }

        private void OnEnable()
        {
            Invoke("EnableCameraLook", 0.5f);
            Cursor.lockState = _lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
            Input.MouseMoved += MouseLook;
           
        }

        private void FixedUpdate()
        {
           //ShakeCam();
        }

        private void OnDisable() => Input.MouseMoved -= MouseLook;

        private void MouseLook(Vector2 mouseInput)
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

        private void ShakeCam()
        {
            float shake = Mathf.Sin(Time.time * _shakeSpeed) * _amplitude;
            _camera.transform.position = new Vector3(_camera.transform.position.x, _cameraPivot.position.y + shake, _camera.transform.position.z);
        }

        public void EnableCameraLook() => _mouseCanLookAround = true;
    }
}
