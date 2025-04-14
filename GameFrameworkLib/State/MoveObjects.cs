using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.State
{
    public class MoveObjects
    {
        public static Move GoNorth = new Move(-1, 0);
        public static Move GoSouth = new Move(+1, 0);
        public static Move GoWest = new Move(0, -1);
        public static Move GoEast = new Move(0, +1);
    }
}
