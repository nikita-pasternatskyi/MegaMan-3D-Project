using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace Assets.Scripts.Player.Multiplayer
{
    public class NetworkPlayer : NetworkBehaviour
    {
        [SerializeField] GameObject _cameraToDisable;
        [SerializeField] Input _inputToDisable;
        

        // Start is called before the first frame update
        private void Start()
        {
            if (!isLocalPlayer)
            {
                DisableLocalObjects();
            }
            else if (isLocalPlayer)
            {
                Debug.Log("Its a me mario");
            }
        }

        private void DisableLocalObjects()
        {
            _cameraToDisable.SetActive(false);
            _inputToDisable.enabled = false;
        }

    }
}
