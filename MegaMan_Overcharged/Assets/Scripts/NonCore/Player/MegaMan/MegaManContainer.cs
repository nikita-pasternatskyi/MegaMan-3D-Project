using Core.Interfaces;
using Core.Player;
using Core.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NonCore.Player.MegaMan
{
    public class MegaManContainer : PlayerContainer, IHasHealth
    {
        [SerializeField] private PlayerClassConfiguration _playerClassConfiguration;
        [SerializeField] private Transform _referenceTransform;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private MegaManMovement _playerMovement;
        [SerializeField] private PlayerPhysics _playerPhysics;
        [SerializeField] private Slide _slide;
        [SerializeField] private WallRun _wallRun;
        [SerializeField] private PlayerHealthWithUI _playerHealthWithUI;
        [SerializeField] private VFXCaller _speedLineEffect;

        private Vector2 _movementInput;

        private void Start()
        {
            InitializeComponents();
        }

        private void FixedUpdate()
        {
            _playerMovement.FixedUpdate(_movementInput);
            _playerPhysics.FixedUpdate();
            _wallRun.FixedUpdate();
        }

        private void InitializeComponents()
        {
            _playerMovement.Start(
                            in _playerPhysics,
                            in _referenceTransform,
                            _playerClassConfiguration, _speedLineEffect);

            _wallRun.Start(this.transform, _referenceTransform, _playerPhysics, _speedLineEffect);
            _slide.Start(_referenceTransform, _playerPhysics, this, ref _characterController, _speedLineEffect);
            _playerHealthWithUI.Start(_playerClassConfiguration.MaxHealth);
            _playerPhysics.Start(_playerClassConfiguration.Mass, _playerClassConfiguration.AirDrag, _playerClassConfiguration.GroundDrag, in _characterController);
        }
        protected override void OnSpecialAbility()
        {
            _slide.UseSpecialAbility();
        }
        protected override void OnMovement(InputValue value)
        {
            _movementInput = value.Get<Vector2>();
            _wallRun.OnMovement(value.Get<Vector2>());
        }

        protected override void OnJump()
        {
            _wallRun.Jump();
            _playerMovement.Jump();
        }

        protected override void OnSprint()
        {
            _playerMovement.Sprint();
        }

        public void TakeDamage(int damage)
        {
            _playerHealthWithUI._health.TakeDamage(damage);
        }

        public void Heal(int healCount)
        {
            _playerHealthWithUI._health.Heal(healCount);
        }
    }
}
