using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.State
{
    public static class StateObjects
    {
        public static IStateMachinePattern North => new StateMachinePatternNorth();

        public static IStateMachinePattern West = new StateMachinePatternWest();

        public static IStateMachinePattern South = new StateMachinePatternSouth();

        public static IStateMachinePattern East = new StateMachinePatternEast();
    }
}
