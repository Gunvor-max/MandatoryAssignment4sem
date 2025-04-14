using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Creatures
{
    public class Wolf : Creature
    {
        private readonly string _element = "Frost";
        private readonly int _frostBreath = 1;
        private readonly int _frostfur = 2;

        public string Element { get => _element; }
        public int FrostBreath { get => _frostBreath; }
        public int Frostfur { get => _frostfur; }
        public Wolf()
        {

        }

        public Wolf(string name, int hitpoint, List<AttackItem> attackItem, List<DefenceItem> defenceItem) : base(name, hitpoint, attackItem, defenceItem)
        {
            Name = name;
            Hitpoint = hitpoint;
            attackItem = new List<AttackItem>();
            defenceItem = new List<DefenceItem>();
        }

        protected override int CreatureSpecificAttack()
        {
            return _frostBreath;
        }

        protected override int CreatureSpecificDefense()
        {
            return _frostfur;
        }

        public override string ToString()
        {
            return $"{{{nameof(Element)}={Element}, {nameof(FrostBreath)}={FrostBreath.ToString()}, {nameof(Frostfur)}={Frostfur.ToString()}, {nameof(Name)}={Name}, {nameof(Hitpoint)}={Hitpoint.ToString()}, {nameof(AttackItems)}={AttackItems}, {nameof(DefenceItems)}={DefenceItems}}}";
        }
    }
}
