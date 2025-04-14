using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.State
{
    public class StateMachinePatternWest : IStateMachinePattern
    {
        private static readonly IStateMachinePattern NORTH = StateObjects.North;
        private static readonly IStateMachinePattern SOUTH = StateObjects.South;


        public IStateMachinePattern NextState(InputType input)
        {
            switch (input)
            {
                case InputType.FORWARD: return this;
                case InputType.LEFT: return SOUTH;
                case InputType.RIGHT: return NORTH;
            }

            return this;
        }

        public Move NextAction(InputType input)
        {
            switch (input)
            {
                case InputType.FORWARD: return MoveObjects.GoWest;
                case InputType.LEFT: return MoveObjects.GoSouth;
                case InputType.RIGHT: return MoveObjects.GoNorth;
            }

            return MoveObjects.GoWest;
        }

        public string Retning => "West";
    }
}
