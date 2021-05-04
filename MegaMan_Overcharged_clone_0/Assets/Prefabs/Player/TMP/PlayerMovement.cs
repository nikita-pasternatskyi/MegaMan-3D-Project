using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

namespace Assets.Prefabs.Player.TMP
{
    public class PlayerMovement : NetworkBehaviour
    {
        public InputCollector inputCollector;
        //[SerializeField] private Transform _forwardDirectionReference;
        public PlayerPhysics playerPhysics;
        [SyncVar] private Vector3 _velocity;
        private Vector2 _currentInput;
        public float Speed;

        public void OnMove(InputValue value)
        {
            _currentInput = value.Get<Vector2>();
            inputCollector.AddMethodCallAtThisFrame(MovePlayer, _currentInput);
        }

        public void MovePlayer(Vector2 input)
        {
            playerPhysics.AddVelocity(input * Time.deltaTime * Speed);
        }
    }
}

