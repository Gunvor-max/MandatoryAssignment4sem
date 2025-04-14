using GameFrameworkLib.Playground;
using GameFrameworkLib.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib
{
    public class WorldObject
    {
        public ILootStrategy LootStrategy { get; set; }
        public string Name { get; set; }
        public bool Lootable { get; set; }
        public bool Removeable { get; set; }
        public Position PositionOnMap { get; set; }

        public WorldObject()
        {
            
        }

        public WorldObject(string name, bool lootable, bool removeable, Position position)
        {
            Name = name;
            Lootable = lootable;
            Removeable = removeable;
            PositionOnMap = position;
        }
    }
}
