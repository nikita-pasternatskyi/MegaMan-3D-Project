using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player.Multiplayer.Client_SidePrediction
{
    public class InputsStamp
    {
        public bool Forward;
        public bool Backward;
        public bool Left;
        public bool Right;
        public bool Jump;

        public InputsStamp(bool forward, bool backward, bool left, bool right, bool jump)
        {
            Forward = forward;
            Backward = backward;
            Left = left;
            Right = right;
            Jump = jump;
        }

        public InputsStamp()
        {
            Forward = false;
            Backward = false;
            Left = false;
            Right = false;
            Jump = false;
        }

    }
}
