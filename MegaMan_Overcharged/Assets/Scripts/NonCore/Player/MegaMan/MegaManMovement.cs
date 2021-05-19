using Core.Player;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    [System.Serializable]
    class MegaManMovement : PlayerMovement
    {
        public MegaManMovement(in Rigidbody rigidbody, in Transform body, float jumpHeight, float walkingSpeed, float runSpeed, float airMultiplier) 
            : base(rigidbody, body, jumpHeight, walkingSpeed, runSpeed, airMultiplier) { }
        [SerializeField] private ParticleSystem _runSpeedLines;

        public void Sprint()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                if (!_runSpeedLines.isPlaying)
                    _runSpeedLines.Play();
            }
            else
            {
                _isRunning = false;
                if (_runSpeedLines.isPlaying)
                    _runSpeedLines.Stop();
            }
        }

    }
}
