using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Creatures
{
    public class Lizard : Creature
    {
        private readonly string _element = "Fire";
        private readonly int _fireBreath = 2;
        private readonly int _fireScales = 1;

        public string Element { get => _element; }
        public int FireBreath { get => _fireBreath; }
        public int FireScales { get => _fireScales; }
        public Lizard()
        {
            
        }

        public Lizard(string name, int hitpoint, List<AttackItem> attackItem, List<DefenceItem> defenceItem) : base(name, hitpoint,attackItem,defenceItem)
        {
            Name = name;
            Hitpoint = hitpoint;
            attackItem = new List<AttackItem>();
            defenceItem = new List<DefenceItem>();
        }

        protected override int CreatureSpecificAttack()
        {
            return _fireBreath;
        }

        protected override int CreatureSpecificDefense()
        {
            return _fireScales;
        }

        public override string ToString()
        {
            return $"{{{nameof(Element)}={Element}, {nameof(FireBreath)}={FireBreath.ToString()}, {nameof(FireScales)}={FireScales.ToString()}, {nameof(Name)}={Name}, {nameof(Hitpoint)}={Hitpoint.ToString()}, {nameof(AttackItems)}={AttackItems}, {nameof(DefenceItems)}={DefenceItems}}}";
        }
    }
}
