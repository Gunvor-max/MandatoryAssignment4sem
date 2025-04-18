using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Strategy
{
    public class AttackItemLootStrategy : ILootStrategy
    {
        public void Loot(WorldObject worldObject, Creature creature)
        {
            if (worldObject is AttackItem attackItem)
            {
                creature.AttackItems.Add(attackItem);
                creature.WeaponEquipped = creature.AttackItems[0];
            }
            worldObject.Lootable = false;
            worldObject.Removeable = true;
        }
    }
}
