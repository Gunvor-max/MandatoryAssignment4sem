using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.State
{
    public class StateMachinePatternNorth : IStateMachinePattern
    {
        private static readonly IStateMachinePattern WEST = StateObjects.West;
        private static readonly IStateMachinePattern EAST = StateObjects.East;


        public IStateMachinePattern NextState(InputType input)
        {
            switch (input)
            {
                case InputType.FORWARD: return this;
                case InputType.LEFT: return WEST;
                case InputType.RIGHT: return EAST;
            }

            return this;
        }

        public Move NextAction(InputType input)
        {
            switch (input)
            {
                case InputType.FORWARD: return MoveObjects.GoNorth;
                case InputType.LEFT: return MoveObjects.GoWest;
                case InputType.RIGHT: return MoveObjects.GoEast;
            }


            return MoveObjects.GoNorth;
        }

        public string Retning => "North";
    }
}
