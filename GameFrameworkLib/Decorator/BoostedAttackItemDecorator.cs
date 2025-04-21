using GameFrameworkLib.AttackItems.BaseAttackItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Decorator
{
    public class BoostedAttackItemDecorator : AttackItemDecorator
    {
        #region Instance Fields
        private int baseBoost = 5;
        #endregion

        #region Properties
        public int BaseBoost { get => baseBoost; }
        #endregion

        #region Constructor
        public BoostedAttackItemDecorator(IAttackItem attackItem) : base(attackItem)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        ///Method used with the decorator design pattern for adding additional damage to an attack
        /// </summary>
        /// <returns>The total damage with the damage boost from the decorator</returns>
        public override int DecorateHIT()
        {
            return base.DecorateHIT() + baseBoost;
        }
        #endregion
    }
}
