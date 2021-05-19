using Core.General;
using Core.Interfaces;
using Core.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    public abstract class PlayerContainer : RequiresInput, IHasHealth
    {
        [SerializeField] protected PlayerClassConfiguration _playerClassConfiguration;
        [SerializeField] protected CharacterController _characterController;
        [SerializeField] protected Transform _referenceTransform;
        [SerializeField] protected PlayerPhysics _playerPhysics;
        [SerializeField] protected PlayerHealthWithUI _playerHealth;
        protected virtual PlayerMovement _playerMovement { get; set; }

        protected Vector2 _movementInput;
        public void TakeDamage(int damage)
        {
            _playerHealth._health.TakeDamage(damage);
        }
        public void Heal(int healCount)
        {
            _playerHealth._health.Heal(healCount);
        }

        protected virtual void Start() => InitializeComponents();

        protected virtual void FixedUpdate()
        {
            _playerPhysics.FixedUpdate(Time.fixedDeltaTime);
            _playerMovement.FixedUpdate(_movementInput, Time.fixedDeltaTime);
        }

        protected new virtual void OnMovement(InputValue value) => _movementInput = value.Get<Vector2>();

        protected new virtual void OnJump() => _playerMovement.Jump();

        protected virtual void OnDisable()
        {
            _playerHealth.OnDisable();
        }
        protected virtual void InitializeComponents()
        {
        }

    }
}
