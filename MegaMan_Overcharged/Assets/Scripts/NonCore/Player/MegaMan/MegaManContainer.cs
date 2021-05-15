using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NonCore.Player.MegaMan
{
    public class MegaManContainer : PlayerContainer
    {
        [SerializeField] private Slide _slide;
        [SerializeField] private WallRun _wallRun;

        protected override void Start()
        {
            base.Start();
            _wallRun.Start( this, this.transform, _referenceTransform, in _playerPhysics);
            _slide.Start(in _referenceTransform, in _playerPhysics, this, ref _characterController);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _wallRun.FixedUpdate();
        }

        protected override void OnSpecialAbility()
        {
            _slide.UseSpecialAbility();
        }

        protected override void OnMovement(InputValue value)
        {
            base.OnMovement(value);
            _wallRun.OnMovement(value.Get<Vector2>());
        }

        protected override void OnJump()
        {
            base.OnJump();
            _wallRun.Jump();

        }
    }
}
