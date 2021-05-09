using Assets.Scripts.Levels;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerSpecialAbility : MonoBehaviour
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
