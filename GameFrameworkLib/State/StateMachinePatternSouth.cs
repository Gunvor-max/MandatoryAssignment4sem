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
                case InputType.LEFT: return EAST;
                case InputType.RIGHT: return WEST;
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
                case InputType.FORWARD: return MoveObjects.GoSouth;
                case InputType.LEFT: return MoveObjects.GoEast;
                case InputType.RIGHT: return MoveObjects.GoWest;
            }

            return MoveObjects.GoSouth;
        }

        public string Retning => "South";
    }
}
