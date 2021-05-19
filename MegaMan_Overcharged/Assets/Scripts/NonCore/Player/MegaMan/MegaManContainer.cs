using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Player;
using Core.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NonCore.Player.MegaMan
{
    public class MegaManContainer : RequiresInput, IHasHealth
    {
        [SerializeField] private PlayerClassConfiguration _playerClassConfiguration;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MegaManMovement _megaManMovement;
        [SerializeField] private Slide _slide;
        [SerializeField] private WallRun _wallRun;
        [SerializeField] private Transform _referenceTransform;
        [SerializeField] private PlayerHealthWithUI _playerHealth;

        [Header("Check ground")]
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _whatIsGround;
        private float _groundDrag;
        private float _airDrag;

        private Vector2 _currentMovementInput;

        private void Start()
        {

            InitializeComponents();
        }

        private void FixedUpdate()
        {
            if (isGrounded())
                _rigidbody.drag = _groundDrag;
            else
                _rigidbody.drag = _airDrag;
            _wallRun.FixedUpdate();
            _megaManMovement.Move(_currentMovementInput, isGrounded());
        }

        protected override void OnSpecialAbility()
        {
            _slide.UseSpecialAbility();
        }

        private void InitializeComponents()
        {
            _groundDrag = _playerClassConfiguration.GroundDrag;
            _airDrag = _playerClassConfiguration.AirDrag;
            _wallRun.Start(this.transform, _referenceTransform, _rigidbody);
            _slide.Start(isGrounded(), _referenceTransform, _rigidbody, this, ref _capsuleCollider);
            _megaManMovement.Start(
                            _rigidbody,
                            _referenceTransform,
                            _playerClassConfiguration.JumpHeight,
                            _playerClassConfiguration.WalkSpeed,
                            _playerClassConfiguration.RunSpeed,
                            _playerClassConfiguration.AirControlMultiplier);
            _playerHealth.Initialize(_playerClassConfiguration.MaxHealth);
        }

        protected override void OnMovement(InputValue value)
        {
            _currentMovementInput = value.Get<Vector2>();
            _wallRun.OnMovement(value.Get<Vector2>());
        }

        protected override void OnJump()
        {
            if(isGrounded())
                _megaManMovement.Jump();
            _wallRun.Jump();
        }

        protected override void OnSprint()
        {
            _megaManMovement.Sprint();
        }

        private bool isGrounded()
        {
            Vector3 groundCheckPosition = new Vector3
                            (_capsuleCollider.bounds.center.x,
                            _capsuleCollider.bounds.center.y - _capsuleCollider.height / 2,
                            _capsuleCollider.bounds.center.z);
            return Physics.CheckSphere(groundCheckPosition, _groundCheckRadius, _whatIsGround);
        }

        public void TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }

        public void Heal(int healCount)
        {
            throw new NotImplementedException();
        }
    }
}
