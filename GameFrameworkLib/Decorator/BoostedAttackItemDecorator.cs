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
        private int baseBoost = 5;

        public int BaseBoost { get => baseBoost; }
        public BoostedAttackItemDecorator(IAttackItem attackItem) : base(attackItem)
        {
        }

        public override int DecorateHIT()
        {
            return base.DecorateHIT() + baseBoost;
        }
    }
}
