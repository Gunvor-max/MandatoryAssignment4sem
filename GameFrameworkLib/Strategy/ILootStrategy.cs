using GameFrameworkLib.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Strategy
{
    public interface ILootStrategy
    {
        /// <summary>
        /// Method for looting a worldobject
        /// </summary>
        /// <param name="worldObject">the object to be looted</param>
        /// <param name="creature">the creature looting the object</param>
        void Loot(WorldObject worldObject, Creature creature);
    }
}
