using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player.Multiplayer.Client_SidePrediction
{
    public class InputMessage
    {
        public uint Tick;
        public uint StartTickNumber;
        public InputsStamp InputsStamp;

        public InputMessage(uint tick, InputsStamp inputsStamp)
        {
            Tick = tick;
            InputsStamp = inputsStamp;
        }

        public InputMessage()
        {
            Tick = 0;
            InputsStamp = null;
        }
    }
}
