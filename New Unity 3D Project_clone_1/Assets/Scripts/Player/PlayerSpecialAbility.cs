using Assets.Scripts.Levels;
using System;
using UnityEngine;
using Mirror;

namespace Assets.Scripts.Player
{
    class PlayerSpecialAbility : NetworkBehaviour
    {
        protected virtual void OnEnable()
        {
            Input.SpecialAbilityPressed += SpecialAbility;
        }
        protected virtual void SpecialAbility()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                throw new NotImplementedException();
            }
        }
    }
}
