using GameFrameworkLib.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Composite
{
    public class AttackItemComposite : IAttackItem
    {
        private List<IAttackItem> _attackItems;

        public AttackItemComposite()
        {
            _attackItems = new List<IAttackItem>();
        }

        public void Add(IAttackItem attackItem) 
        {
            _attackItems.Add(attackItem);
        }

        public void Remove(IAttackItem attackItem) 
        {
            _attackItems.Remove(attackItem);
        }

        public int DecorateHIT()
        {
            throw new NotImplementedException();
        }
    }
}
