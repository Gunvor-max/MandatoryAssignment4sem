using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.State
{
    public class StateMachinePatternEast : IStateMachinePattern
    {
        private static readonly IStateMachinePattern NORTH = StateObjects.North;
        private static readonly IStateMachinePattern SOUTH = StateObjects.South;


        /// <summary>
        /// Method for returning the next state from an inputtype
        /// </summary>
        /// <param name="input">Enum input</param>
        /// <returns>IStateMachinePattern object</returns>
        public IStateMachinePattern NextState(InputType input)
        {
            switch (input)
            {
                case InputType.FORWARD: return this;
                case InputType.LEFT: return NORTH;
                case InputType.RIGHT: return SOUTH;
            }

            return this;
        }
        /// <summary>
        /// Method for returning the next move from an inputtype
        /// </summary>
        /// <param name="input">Enum input</param>
        /// <returns>A move object with coordinates</returns>
        public Move NextAction(InputType input)
        {
            switch (input)
            {
                case InputType.FORWARD: return MoveObjects.GoEast;
                case InputType.LEFT: return MoveObjects.GoNorth;
                case InputType.RIGHT: return MoveObjects.GoSouth;
            }

            return MoveObjects.GoEast;
        }

        public string Retning => "East";
    }
}
