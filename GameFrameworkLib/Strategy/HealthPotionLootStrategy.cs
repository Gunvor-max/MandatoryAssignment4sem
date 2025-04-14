using GameFrameworkLib.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Strategy
{
    public class HealthPotionLootStrategy : ILootStrategy
    {
        public void Loot(WorldObject worldObject, Creature creature)
        {
            if (worldObject is HealthPotion potion)
            {
                creature.Hitpoint += potion.GiveHealthBack;
            }
            worldObject.Lootable = false;
            worldObject.Removeable = true;
        }
    }
}
