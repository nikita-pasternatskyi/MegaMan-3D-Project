using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player.Multiplayer
{
    public class NetworkPlayer : NetworkBehaviour
    {
        [SerializeField] PlayerPhysics _physicsToEnable;
        [SerializeField] GameObject _cameraToEnable;
        [SerializeField] PlayerInput _inputToEnable;
        [SerializeField] MonoBehaviour[] _componentsToEnable;

        public override void OnStartAuthority()
        {
            EnableLocalObjects();
        }

        [ClientCallback]
        private void EnableLocalObjects()
        {
            _cameraToEnable.SetActive(true);
            
            foreach (var component in _componentsToEnable)
            {
                component.enabled = true;
            }
        }

    }
}
