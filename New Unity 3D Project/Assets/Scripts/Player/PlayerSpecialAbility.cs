

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
            throw new NotImplementedException();
        }
    }
}
