using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.State
{
    public enum CharacterHeadingStatesType
    {
        NORTH,
        EAST,
        WEST,
        SOUTH
    };

    public enum InputType
    {
        LEFT,
        RIGHT,
        FORWARD,
        STAND,
    };

    public record Move(int row, int col);
    public interface Istate
    {
        /// <summary>
        /// Method for setting the state and action from a given user input
        /// </summary>
        /// <param name="input">Enum input</param>
        /// <returns>Move object with the next move for the main character</returns>
        Move NextMove(InputType input);
    }
}
