using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Playground
{
    public class Position
    {
        #region Properties
        public int Row { get; set; }
        public int Col { get; set; }
        #endregion

        #region Constructors
        public Position() : this(0, 0)
        {
        }

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method for checking if two prositions have the same values aka is at the same place
        /// </summary>
        /// <param name="other">The position object to be compared with</param>
        /// <returns>true if the two positions are equal</returns>
        protected bool Equals(Position other)
        {
            return Row == other.Row && Col == other.Col;
        }

        /// <summary>
        /// Method for determining whether the specified object is equal to the current position.
        /// </summary>
        /// <param name="obj">The object to compare with the current position.</param>
        /// <returns>True if the specified object is a position object with the same row and column values as this position.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position)obj);
        }

        /// <summary>
        /// Method for generating a hash code for the current position based on row and column values.
        /// </summary>
        /// <returns>A hash code representing the current position.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 397) ^ Col;
            }
        }
        #endregion
    }
}
