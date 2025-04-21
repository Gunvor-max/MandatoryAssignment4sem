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
        #region Instance Fields
        private readonly string _element = "Frost";
        private readonly int _frostBreath = 1;
        private readonly int _frostfur = 2;
        #endregion

        #region Properties
        public string Element { get => _element; }
        public int FrostBreath { get => _frostBreath; }
        public int Frostfur { get => _frostfur; }
        #endregion

        #region Constructors
        public Wolf()
        {

        }

        public Wolf(string name, int hitpoint) : base(name, hitpoint)
        {
            Name = name;
            Hitpoint = hitpoint;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method for adding frostbreath attackpower
        /// </summary>
        /// <returns>Integer of 1</returns>
        protected override int CreatureSpecificAttack()
        {
            return _frostBreath;
        }

        /// <summary>
        /// Method for adding frostfur defencepower
        /// </summary>
        /// <returns>Integer of 2</returns>
        protected override int CreatureSpecificDefense()
        {
            return _frostfur;
        }

        public override string ToString()
        {
            return $"{{{nameof(Element)}={Element}, {nameof(FrostBreath)}={FrostBreath.ToString()}, {nameof(Frostfur)}={Frostfur.ToString()}, {nameof(Name)}={Name}, {nameof(Hitpoint)}={Hitpoint.ToString()}, {nameof(AttackItems)}={AttackItems}, {nameof(DefenceItems)}={DefenceItems}}}";
        }
        #endregion
    }
}
