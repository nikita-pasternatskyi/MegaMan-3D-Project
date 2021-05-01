using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player.Multiplayer
{
    public class NetworkEnableLocalObjects : NetworkBehaviour
    {
       
        [SerializeField] GameObject[] objectsToEnable;
        [SerializeField] MonoBehaviour[] _componentsToEnable;
        public override void OnStartLocalPlayer()
        {
            StartCoroutine(WaitForReady());
        }

        [ClientCallback]
        private void EnableLocalObjects()
        {
            foreach (var gameobject in objectsToEnable)
            {
                if (gameobject != null)
                {
                    gameobject.SetActive(true);
                }
            }
            
            foreach (var component in _componentsToEnable)
            {
                if (component != null)
                {
                    component.enabled = true;
                }
            }
        }

        private IEnumerator WaitForReady()
        {
            while (!NetworkClient.ready)
            {
                yield return new WaitForSeconds(0.25f);
            }
            EnableLocalObjects();
        }

    }
}
