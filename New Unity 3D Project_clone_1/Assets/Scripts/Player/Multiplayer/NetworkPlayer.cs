using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player.Multiplayer
{
    public class NetworkPlayer : NetworkBehaviour
    {
        [SerializeField] GameObject _cameraToEnable;
        [SerializeField] PlayerInput _inputToEnable;

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            EnableLocalObjects();
        }

        [ClientCallback]
        private void EnableLocalObjects()
        {
            _cameraToEnable.SetActive(true);
            _inputToEnable.enabled = true;
        }

    }
}
