using System;
using UnityEngine;
using Core.Player;
using Core.General;

namespace Core.Items
{
    public class Item : MonoBehaviour
    {
        public event Action PickedUp;
        private float _height;
        protected float _startFallSpeed = 0.05f;

        protected virtual void InvokePickUpEvent() => PickedUp?.Invoke();
    }
}
