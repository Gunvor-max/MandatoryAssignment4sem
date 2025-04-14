using GameFrameworkLib.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Strategy
{
    internal class DefenceItemLootStrategy : ILootStrategy
    {
        public void Loot(WorldObject worldObject, Creature creature)
        {
            if (worldObject is DefenceItem defenceItem)
            {
                creature.DefenceItems.Add(defenceItem);
            }
            worldObject.Lootable = false;
            worldObject.Removeable = true;
        }
    }
}
