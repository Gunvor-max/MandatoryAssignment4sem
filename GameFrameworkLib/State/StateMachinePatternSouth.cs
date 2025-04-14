using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.State
{
    public class StateMachinePatternSouth : IStateMachinePattern
    {
        private static readonly IStateMachinePattern WEST = StateObjects.West;
        private static readonly IStateMachinePattern EAST = StateObjects.East;


        public IStateMachinePattern NextState(InputType input)
        {
            switch (input)
            {
                case InputType.FORWARD: return this;
                case InputType.LEFT: return EAST;
                case InputType.RIGHT: return WEST;
            }

            return this;
        }

        public Move NextAction(InputType input)
        {
            switch (input)
            {
                case InputType.FORWARD: return MoveObjects.GoSouth;
                case InputType.LEFT: return MoveObjects.GoEast;
                case InputType.RIGHT: return MoveObjects.GoWest;
            }

            return MoveObjects.GoSouth;
        }

        public string Retning => "South";
    }
}
