using Core.Levels;
using System;
using UnityEngine;

namespace Core.Player
{
    abstract class PlayerSpecialAbility : MonoBehaviour
    {
        protected virtual void OnSpecialAbility()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                throw new NotImplementedException();
            }
        }
    }
}
