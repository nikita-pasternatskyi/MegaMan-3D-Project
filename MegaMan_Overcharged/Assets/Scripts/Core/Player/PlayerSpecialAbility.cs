using Core.Levels;
using System;
using UnityEngine;

namespace Core.Player
{
    public abstract class PlayerSpecialAbility
    {
        public virtual void UseSpecialAbility()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                throw new NotImplementedException();
            }
        }
    }
}
