using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Playground
{
    public class Position
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Position() : this(0, 0)
        {
        }

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        /*
         * For checking if two prositions have the same values
         */
        protected bool Equals(Position other)
        {
            return Row == other.Row && Col == other.Col;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 397) ^ Col;
            }
        }
    }
}
