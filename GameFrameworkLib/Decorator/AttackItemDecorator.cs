using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Decorator
{
    public class AttackItemDecorator : IAttackItem
    {
        private IAttackItem _attackItem;

        public AttackItemDecorator(IAttackItem attackItem)
        {
            _attackItem = attackItem;
        }
        public virtual int DecorateHIT()
        {
            return _attackItem.DecorateHIT();
        }
    }
}
