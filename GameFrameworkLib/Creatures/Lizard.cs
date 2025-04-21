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
        #region Instance Fields
        private readonly string _element = "Fire";
        private readonly int _fireBreath = 2;
        private readonly int _fireScales = 1;
        #endregion

        #region Properties
        public string Element { get => _element; }
        public int FireBreath { get => _fireBreath; }
        public int FireScales { get => _fireScales; }
        #endregion

        #region Constructors
        public Lizard()
        {
            
        }

        public Lizard(string name, int hitpoint) : base(name, hitpoint)
        {
            Name = name;
            Hitpoint = hitpoint;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method for adding fireBreath attackpower
        /// </summary>
        /// <returns>Integer of 2</returns>
        protected override int CreatureSpecificAttack()
        {
            return _fireBreath;
        }

        /// Method for adding fireScales defencepower
        /// </summary>
        /// <returns>Integer of 1</returns>
        protected override int CreatureSpecificDefense()
        {
            return _fireScales;
        }
        #endregion
        public override string ToString()
        {
            return $"{{{nameof(Element)}={Element}, {nameof(FireBreath)}={FireBreath.ToString()}, {nameof(FireScales)}={FireScales.ToString()}, {nameof(Name)}={Name}, {nameof(Hitpoint)}={Hitpoint.ToString()}, {nameof(AttackItems)}={AttackItems}, {nameof(DefenceItems)}={DefenceItems}}}";
        }
    }
}
