using GameFrameworkLib.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib
{
    public class DefenceItem : WorldObject
    {
        #region Properties
        public string Name { get; set; }
        public int ReduceHitpoint { get; set; }
        #endregion

        #region Constructors
        public DefenceItem():this("TestShield",4)
        {
            
        }

        public DefenceItem(string name, int reduceHitpoint)
        {
            Name = name;
            ReduceHitpoint = reduceHitpoint;
            LootStrategy = new DefenceItemLootStrategy();
        }
        #endregion
    }
}
