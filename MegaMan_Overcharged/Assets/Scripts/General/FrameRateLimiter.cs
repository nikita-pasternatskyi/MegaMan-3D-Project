using UnityEngine;

namespace Core.General
{
    public class FrameRateLimiter : MonoBehaviour
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
