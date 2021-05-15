using Core.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    [AddComponentMenu("Player/Base/Camera Control")]

    class PlayerCamera : MonoBehaviour
    {
        public enum Axis { 
            X,Y,Z,
        }
        [SerializeField] private List<Transform> _bodyToXRotate;
        [SerializeField] private List<Transform> _bodyToYRotate;
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
        private bool _isRotating;

        public void ChangeCameraMode(CameraModes _modeToSwitchTo)
        {
            _currentCameraMode = _modeToSwitchTo;
            switch (_currentCameraMode)
            {
                case CameraModes.AroundPoint:
                    _rotateWithPivot = true;
                    _camera.transform.localPosition = new Vector3(0, 0, _pivotModeZOffset);                   
                    foreach(var item in _bodyToXRotate)
                        item.rotation = Quaternion.Euler(0, 0, 0);
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
                            foreach(var item in _bodyToYRotate)
                                item.transform.localRotation = Quaternion.Euler(_cameraXRotation, 0, 0);
                        }
                        foreach (var item in _bodyToXRotate)
                            item.rotation *= Quaternion.Euler(0, mouseInput.x, 0);
                    }

                }
            }
        }

        private void OnMouseLook(InputValue value)
        {
            MouseLook(value.Get<Vector2>());         
        }

        public void RotateCamera(Axis axis,float rotateTime, float rotateValue, bool rightSide)
        {
            StartCoroutine(RotateCameraByAnAxis(axis, rotateTime, rotateValue, rightSide));
        }

        private IEnumerator RotateCameraByAnAxis(Axis axis,float rotateTime, float rotateValue, bool rightSide)
        {
            switch (axis)
            {
                case Axis.X:
                    yield break;
                case Axis.Y:
                    yield break;
                case Axis.Z:
                    var difference = 1f;
                    _cameraPivot.eulerAngles = new Vector3(_cameraPivot.eulerAngles.x, _cameraPivot.eulerAngles.y, rotateValue);
                    //while (_cameraPivot.eulerAngles.z != rotateValue)
                    ////{
                    ////    difference = _cameraPivot.eulerAngles.z * _cameraPivot.eulerAngles.z - rotateValue * rotateValue;
                    //    var targetZ = rotateValue;
                    //    Debug.Log(targetZ);
                    //    //var targetZ = Mathf.Lerp(_cameraPivot.eulerAngles.z, rotateValue, Time.deltaTime * rotateTime);
                    //    _cameraPivot.eulerAngles = new Vector3(_cameraPivot.eulerAngles.x, _cameraPivot.eulerAngles.y, targetZ);
                    //    yield return null;
                    //}
                    yield break;
            }
            yield return null;
        }

        public void EnableCameraLook() => _mouseCanLookAround = true;
    }
}
