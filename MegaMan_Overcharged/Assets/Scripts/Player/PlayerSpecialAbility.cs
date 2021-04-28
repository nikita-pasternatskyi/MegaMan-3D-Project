﻿using Assets.Scripts.Levels;
using System;
using UnityEngine;
using Mirror;

namespace Assets.Scripts.Player
{
    public class PlayerSpecialAbility : NetworkBehaviour
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