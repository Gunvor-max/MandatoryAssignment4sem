using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.State
{
    public interface IStateMachinePattern
    {
        IStateMachinePattern NextState(InputType input);
        /// <summary>
        /// Method for setting the action from a given user input
        /// </summary>
        /// <param name="input">Enum input</param>
        /// <returns>Move object with the next move for the main character</returns>
        Move NextAction(InputType input);

        string Retning { get; }
    }
}
