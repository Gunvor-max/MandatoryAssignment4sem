using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Decorator;
using GameFrameworkLib.Playground;
using GameFrameworkLib.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib
{
    public class WeaponBoostAlter : WorldObject
    {
        public string Name { get; set; }

        public WeaponBoostAlter()
        {
            
        }

        public WeaponBoostAlter(string worldObjectName, bool lootable, bool removeable, Position position, string name ) : base(worldObjectName, lootable,removeable, position)
        {
            Name = name;
        }

        public IAttackItem Boost(IAttackItem attackItem)
        {
            return new BoostedAttackItemDecorator(attackItem);
        }
    }
}
