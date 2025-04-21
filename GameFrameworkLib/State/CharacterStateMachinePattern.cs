using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.State
{
    public class CharacterStateMachinePattern : Istate
    {
        #region Instance Fields
        // internal StateMachine as pattern i.e. objects
        private IStateMachinePattern _currentState;
        #endregion

        #region Constructor
        public CharacterStateMachinePattern()
        {
            _currentState = StateObjects.East;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method for setting the state and action from a given user input
        /// </summary>
        /// <param name="input">Enum input</param>
        /// <returns>Move object with the next move for the main character</returns>
        public Move NextMove(InputType input)
        {
            // Find next move from current state and input
            Move nextMove = _currentState.NextAction(input);

            // Find next state from current state and input

            _currentState = _currentState.NextState(input);

            return nextMove;
        }
        #endregion
    }
}
