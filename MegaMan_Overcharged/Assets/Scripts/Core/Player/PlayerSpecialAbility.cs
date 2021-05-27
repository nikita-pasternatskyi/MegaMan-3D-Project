using Core.Levels;
using System;
using UnityEngine;

namespace Core.Player
{
    public abstract class PlayerSpecialAbility : RequiresInput
    {
        protected abstract override void OnSpecialAbility();
    }
}
