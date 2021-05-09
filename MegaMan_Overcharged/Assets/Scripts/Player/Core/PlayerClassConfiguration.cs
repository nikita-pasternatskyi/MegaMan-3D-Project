using UnityEngine;

namespace Core.Player
{
    [CreateAssetMenu(fileName = "PlayerClassSettings", menuName = "ScriptableObjects/PlayerClassSettings", order = 1)]
    class PlayerClassConfiguration : ScriptableObject
    {
        public float Mass;
        public float Speed;
        public float AirDrag;
        public float GroundDrag;
        public float JumpHeight;
    }
}
