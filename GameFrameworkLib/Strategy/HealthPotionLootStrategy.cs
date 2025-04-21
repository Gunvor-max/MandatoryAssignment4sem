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
        /// <summary>
        /// Method for looting a worldobject using the strategy pattern with health potion
        /// </summary>
        /// <param name="worldObject">The object to be looted</param>
        /// <param name="creature">The creature looting the object</param>
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
