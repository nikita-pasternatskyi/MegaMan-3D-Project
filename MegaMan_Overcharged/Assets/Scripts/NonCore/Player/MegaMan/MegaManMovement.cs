using Core.Player;
using Core.ScriptableObjects;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    [System.Serializable]
    class MegaManMovement : PlayerMovement
    {
        public MegaManMovement(in PlayerPhysics playerPhysics, in Transform body, PlayerClassConfiguration playerClassConfiguration, VFXCaller speedLines) : 
            base(playerPhysics, body, playerClassConfiguration) { _speedLineEffect = speedLines; }
        public void Start(in PlayerPhysics playerPhysics, in Transform body, PlayerClassConfiguration playerClassConfiguration, VFXCaller speedLines)
        {
            base.Start(playerPhysics, body, playerClassConfiguration);
            _speedLineEffect = speedLines;
        }

        private VFXCaller _speedLineEffect;


        public void Sprint()
        {
            if (!_isRunning)
            {           
                _isRunning = true;
                _speedLineEffect.EnableVFX();
            }
            else
            {
                _speedLineEffect.DisableVFX();
                _isRunning = false;
            }
        }

    }
}
