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
        Move NextMove(InputType input);
    }
}
