using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace Assets.Scripts.General
{
    public class FrameRateLimiter : NetworkBehaviour
    {
        public int TargetFrameRate;
        // Start is called before the first frame update
        void OnEnable()
        {
            Application.targetFrameRate = TargetFrameRate;
        }

        private void OnDisable()
        {
            Application.targetFrameRate = -1;
        }
    }
}
