using Core.General;
using Core.Player;
using Core.ScriptableObjects;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    class MegaManMovement : PlayerMovement
    {
        [SerializeField] private VFXCaller _speedLineEffect;

        protected override void OnSprint()
        {
            if (isActiveAndEnabled)
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
}
