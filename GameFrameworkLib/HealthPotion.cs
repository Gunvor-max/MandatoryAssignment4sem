using GameFrameworkLib.Playground;
using GameFrameworkLib.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib
{
    public class HealthPotion : WorldObject
    {
        #region Properties
        public string Name { get; set; }
        public int GiveHealthBack { get; set; }
        #endregion

        #region Constructors
        public HealthPotion():this("Health", true, true, new Position(), "Greater HealthPotion",20)
        {
            
        }

        public HealthPotion(string worldObjectName, bool lootable, bool removeable, Position position, string name, int giveHealthBack) : base(worldObjectName, removeable ,lootable, position)
        {
            Name = name;
            GiveHealthBack = giveHealthBack;
            LootStrategy = new HealthPotionLootStrategy();
        }
        #endregion
    }
}
