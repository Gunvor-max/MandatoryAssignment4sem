using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Decorator
{
    public interface IAttackItem
    {
        /// <summary>
        /// Method used with the decorator design pattern for adding additional damage to an attack
        /// </summary>
        /// <returns>The total damage with the damage boost from the decorator</returns>
        public int DecorateHIT();
    }
}
