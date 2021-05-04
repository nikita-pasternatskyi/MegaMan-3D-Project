using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Prefabs.Player.TMP
{
    public struct InputStamp
    {
        public int TimeStamp;
        public Action<Vector2> MethodToExecute;
        public Vector2 MovementDirection;
    }
}
