using GameFrameworkLib.Decorator;
using GameFrameworkLib.Playground;
using GameFrameworkLib.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.AttackItems.BaseAttackItem
{
    public class AttackItem : WorldObject, IAttackItem
    {
        public string Name { get; set; }
        public int Hit { get; set; }
        public int Range { get; set; }

        public AttackItem() : this("AttackItem", true, true, new Position(), "TestSword", 5, 2)
        {

        }

        public AttackItem(string worldobjectname, bool lootable, bool removeable, Position position, string name, int hit, int range) : base(worldobjectname, lootable,removeable,position)
        {
            Name = name;
            Hit = hit;
            Range = range;
            LootStrategy = new AttackItemLootStrategy();
        }

        public int DecorateHIT()
        {
            return Hit;
        }

        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(Hit)}={Hit.ToString()}, {nameof(Range)}={Range.ToString()}, {nameof(Name)}={Name}, {nameof(Lootable)}={Lootable.ToString()}, {nameof(Removeable)}={Removeable.ToString()}}}";
        }
    }
}
