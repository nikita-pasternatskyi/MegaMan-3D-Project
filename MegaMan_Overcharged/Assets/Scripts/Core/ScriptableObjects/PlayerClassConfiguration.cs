using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerClassSettings", menuName = "ScriptableObjects/Player", order = 1)]
    public class PlayerClassConfiguration : ScriptableObject
    {
        public int MaxHealth;
        public float Mass;
        public float WalkSpeed;
        public float RunSpeed;
        public float AirDrag;
        public float GroundDrag;
        public float JumpHeight;
    }
}
