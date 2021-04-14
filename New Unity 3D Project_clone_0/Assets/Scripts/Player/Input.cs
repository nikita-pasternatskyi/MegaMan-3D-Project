using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirror;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    [AddComponentMenu("Player/Base/Input")]
    class Input : NetworkBehaviour
    {
        public static Input Instance;

        public delegate void OnPauseButtonPressed();
        public delegate void OnCompanionSpecialOnePressed();
        public delegate void OnCompanionSpecialTwoPressed();
        public delegate void OnMovementPressed(Vector2 MovementInput);
        public delegate void OnMouseMoved(Vector2 MouseInput);
        public delegate void OnSprintHeld(bool held);
        public delegate void OnFirePressed();
        public delegate void OnFireReleased();
        public delegate void OnJumpPressed();
        public delegate void OnSpecialAbilityPressed();

        public static event OnPauseButtonPressed PauseButtonPressed;
        public static event OnCompanionSpecialOnePressed CompanionSpecialOnePressed;
        public static event OnCompanionSpecialTwoPressed CompanionSpecialTwoPressed;
        public static event OnFirePressed FirePressed;
        public static event OnFireReleased FireReleased;
        public static event OnMouseMoved MouseMoved;
        public static event OnJumpPressed JumpPressed;
        public static event OnMovementPressed MovementPressed;
        public static event OnSprintHeld SprintHeld;
        public static event OnSpecialAbilityPressed SpecialAbilityPressed;

        [SerializeField] private Vector2 _movementInput;
        [SerializeField] private Vector2 _mouseInput;
        [SerializeField] private float _xMouseSensitivity;
        [SerializeField] private float _yMouseSensitivity;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            else if (Instance == this)
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (isLocalPlayer)
            {
                _movementInput = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
                _mouseInput = new Vector2(UnityEngine.Input.GetAxis("Mouse X") * _xMouseSensitivity, UnityEngine.Input.GetAxis("Mouse Y") * _yMouseSensitivity);
                InvokeEvents();
            }
        }

        private void InvokeEvents()
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                PauseButtonPressed?.Invoke();
            }

            if (_movementInput != Vector2.zero)
            {
                MovementPressed?.Invoke(_movementInput);
            }

            if (_mouseInput != Vector2.zero)
            {
                MouseMoved?.Invoke(_mouseInput);
            }

            if (UnityEngine.Input.GetButtonDown("Fire1"))
            {
                FirePressed?.Invoke();
            }

            if (UnityEngine.Input.GetButtonUp("Fire1"))
            {
                FireReleased?.Invoke();
            }

            if (UnityEngine.Input.GetButtonDown("Sprint"))
            {
                SprintHeld?.Invoke(true);
            }

            if (UnityEngine.Input.GetButtonUp("Sprint"))
            {
                SprintHeld?.Invoke(false);
            }

            if (UnityEngine.Input.GetButtonDown("Jump"))
            {
                JumpPressed?.Invoke();
            }

            if (UnityEngine.Input.GetButtonDown("Companion Special 1"))
            {
                CompanionSpecialOnePressed?.Invoke();
            }

            if (UnityEngine.Input.GetButtonDown("Companion Special 2"))
            {
                CompanionSpecialTwoPressed?.Invoke();
            }
        }

    }
}
