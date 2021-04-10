using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [AddComponentMenu("Player/Base/Movement")]
    [RequireComponent(typeof(CharacterController))]

    public class PlayerMove : MonoBehaviour
    {
        [Header("Movement Parameters")]
        [SerializeField] private float _speed;
        [SerializeField] private float _sprintSpeedMultiplier;
        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpHeight;
        private Vector3 _velocity;

        [Header("Class dependencies")]
        [SerializeField] private PlayerCamera _playerCamera;
        [SerializeField] private CharacterController _characterController;

        [Header("Physics Check")]
        [SerializeField] private float _groundDistance;
        [SerializeField] private LayerMask _whatIsGround;
        private bool _isGrounded;

        private void OnEnable()
        {
            Input.MovementPressed += Move;
            Input.SprintHeld += Sprint;
            Input.JumpPressed += Jump;
        }

        private void FixedUpdate()
        {
            ApplyPhysics();
            _characterController.Move(_velocity * Time.fixedDeltaTime);
        }

        private void OnDisable()
        {
            Input.MovementPressed -= Move;
            Input.SprintHeld -= Sprint;
            Input.JumpPressed -= Jump;
        }

        private void Jump()
        {
            if (_isGrounded)
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }

        public void Jump(float jumpHeight)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2 * _gravity);
        }

        //Здесь копирование кода, нужно изменить. А нахера нам это вообще????
        private void Sprint(bool Held)
        {
            if (Held)
            {
                _speed *= _sprintSpeedMultiplier;
                //shakeSpeed *= sprintSpeedMultiplier;
            }
            else if (!Held)
            {
                _speed /= _sprintSpeedMultiplier;
                //playerCamera.shakeSpeed /= sprintSpeedMultiplier;
            }
        }

        private void Move(Vector2 input)
        {
            var direction = transform.forward * input.y + transform.right * input.x;
            _velocity = new Vector3(direction.x * _speed, _velocity.y, direction.z * _speed);
        }

        //Можно сделать при помощи машины состояний
        private void ApplyPhysics()
        {
            Vector3 groundCheckPosition = new Vector3
                (_characterController.bounds.center.x,
                _characterController.bounds.center.y - _characterController.height / 2,
                _characterController.bounds.center.z);

            _isGrounded = Physics.CheckSphere(groundCheckPosition, _groundDistance, _whatIsGround);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            else if (!_isGrounded)
            {
                _velocity.y += _gravity * Time.fixedDeltaTime;
            }
        }
    }


}
