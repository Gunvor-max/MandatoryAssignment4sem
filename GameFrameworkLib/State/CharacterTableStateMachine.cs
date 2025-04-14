using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.State
{
    class StateAction
    {
        public CharacterHeadingStatesType HeadingState { get; set; } // next state
        public CharacterHeadingStatesType Action { get; set; } // direction the snake should move

    }

    public class CharacterTableStateMachine : Istate
    {
        // internal table as StateMachine
        private StateAction[,] _stateMachine;
        private CharacterHeadingStatesType _currentHeadingState;

        public CharacterTableStateMachine()
        {
            _currentHeadingState = CharacterHeadingStatesType.NORTH;

            // initialize table
            _stateMachine = new StateAction[4, 3];
            _stateMachine[(int)CharacterHeadingStatesType.NORTH, (int)InputType.LEFT] = new StateAction() { HeadingState = CharacterHeadingStatesType.WEST, Action = CharacterHeadingStatesType.WEST };
            _stateMachine[(int)CharacterHeadingStatesType.NORTH, (int)InputType.RIGHT] = new StateAction() { HeadingState = CharacterHeadingStatesType.EAST, Action = CharacterHeadingStatesType.EAST };
            _stateMachine[(int)CharacterHeadingStatesType.NORTH, (int)InputType.FORWARD] = new StateAction() { HeadingState = CharacterHeadingStatesType.NORTH, Action = CharacterHeadingStatesType.NORTH };

            _stateMachine[(int)CharacterHeadingStatesType.EAST, (int)InputType.LEFT] = new StateAction() { HeadingState = CharacterHeadingStatesType.NORTH, Action = CharacterHeadingStatesType.NORTH };
            _stateMachine[(int)CharacterHeadingStatesType.EAST, (int)InputType.RIGHT] = new StateAction() { HeadingState = CharacterHeadingStatesType.SOUTH, Action = CharacterHeadingStatesType.SOUTH };
            _stateMachine[(int)CharacterHeadingStatesType.EAST, (int)InputType.FORWARD] = new StateAction() { HeadingState = CharacterHeadingStatesType.EAST, Action = CharacterHeadingStatesType.EAST };

            _stateMachine[(int)CharacterHeadingStatesType.WEST, (int)InputType.LEFT] = new StateAction() { HeadingState = CharacterHeadingStatesType.SOUTH, Action = CharacterHeadingStatesType.SOUTH };
            _stateMachine[(int)CharacterHeadingStatesType.WEST, (int)InputType.RIGHT] = new StateAction() { HeadingState = CharacterHeadingStatesType.NORTH, Action = CharacterHeadingStatesType.NORTH };
            _stateMachine[(int)CharacterHeadingStatesType.WEST, (int)InputType.FORWARD] = new StateAction() { HeadingState = CharacterHeadingStatesType.WEST, Action = CharacterHeadingStatesType.WEST };

            _stateMachine[(int)CharacterHeadingStatesType.SOUTH, (int)InputType.LEFT] = new StateAction() { HeadingState = CharacterHeadingStatesType.EAST, Action = CharacterHeadingStatesType.EAST };
            _stateMachine[(int)CharacterHeadingStatesType.SOUTH, (int)InputType.RIGHT] = new StateAction() { HeadingState = CharacterHeadingStatesType.WEST, Action = CharacterHeadingStatesType.WEST };
            _stateMachine[(int)CharacterHeadingStatesType.SOUTH, (int)InputType.FORWARD] = new StateAction() { HeadingState = CharacterHeadingStatesType.SOUTH, Action = CharacterHeadingStatesType.SOUTH };
        }


        public Move NextMove(InputType input)
        {
            // Find next move from current state and input
            CharacterHeadingStatesType nextMove = _stateMachine[(int)_currentHeadingState, (int)input].Action;

            // Find next state from current state and input
            _currentHeadingState = _stateMachine[(int)_currentHeadingState, (int)input].HeadingState;
            return ConvertDirection2Move(nextMove);
        }

        private Move ConvertDirection2Move(CharacterHeadingStatesType nextMove)
        {
            switch (nextMove)
            {
                case CharacterHeadingStatesType.NORTH: return MoveObjects.GoNorth;
                case CharacterHeadingStatesType.EAST: return MoveObjects.GoEast;
                case CharacterHeadingStatesType.SOUTH: return MoveObjects.GoSouth;

                default: return MoveObjects.GoWest;
            }
        }
    }
}
