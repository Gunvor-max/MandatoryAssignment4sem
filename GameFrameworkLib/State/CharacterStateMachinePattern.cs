using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.State
{
    public class CharacterStateMachinePattern : Istate
    {
        // internal StateMachine as pattern i.e. objects
        private IStateMachinePattern _currentState;

        public CharacterStateMachinePattern()
        {
            _currentState = StateObjects.East;
        }

        public Move NextMove(InputType input)
        {
            // Find next move from current state and input
            Move nextMove = _currentState.NextAction(input);

            // Find next state from current state and input

            _currentState = _currentState.NextState(input);

            //Notify.Debug(input.ToString(), _currentState.Retning);

            return nextMove;
        }
    }
}
