using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player.Multiplayer
{
    public class NetworkEnableLocalObjects : NetworkBehaviour
    {
       
        [SerializeField] GameObject _cameraToEnable;
        [SerializeField] MonoBehaviour[] _componentsToEnable;
        public override void OnStartAuthority()
        {
            EnableLocalObjects();
        }

        [ClientCallback]
        private void EnableLocalObjects()
        {
            _cameraToEnable?.SetActive(true);
            
            foreach (var component in _componentsToEnable)
            {
                if (component != null)
                {
                    component.enabled = true;
                }
            }
        }

    }
}
