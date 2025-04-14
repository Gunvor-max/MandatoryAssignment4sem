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
        void Loot(WorldObject worldObject, Creature creature);
    }
}
