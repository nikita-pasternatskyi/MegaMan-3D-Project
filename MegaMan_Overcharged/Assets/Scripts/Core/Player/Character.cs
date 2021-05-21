using Core.General;
using Core.Interfaces;
using Core.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    public abstract class Character : RequiresInput, IHasHealth
    {
        public abstract void Heal(int healCount);

        public abstract void TakeDamage(int damage);
    }
}
