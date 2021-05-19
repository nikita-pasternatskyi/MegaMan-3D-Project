using Core.Player;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    class MegaManMovement : PlayerMovement
    {
        public MegaManMovement(in PlayerPhysics playerPhysics, in Transform body, float jumpHeight, float walkingSpeed, float runSpeed) : base(playerPhysics, body, jumpHeight, walkingSpeed, runSpeed) { }
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
                if (_runSpeedLines.isPlaying)
                    _runSpeedLines.Stop();
                _isRunning = false;
            }
        }

    }
}
