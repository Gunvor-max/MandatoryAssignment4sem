using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Decorator
{
    public class AttackItemDecorator : IAttackItem
    {
        #region Instance Fields
        private IAttackItem _attackItem;
        #endregion

        #region Constructor
        public AttackItemDecorator(IAttackItem attackItem)
        {
            _attackItem = attackItem;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Virtual Method used with the decorator design pattern for adding additional damage to an attack
        /// </summary>
        /// <returns>The total damage with the damage boost from the decorator</returns>
        public virtual int DecorateHIT()
        {
            return _attackItem.DecorateHIT();
        }
        #endregion
    }
}
