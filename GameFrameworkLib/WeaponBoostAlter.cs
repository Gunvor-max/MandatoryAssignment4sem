using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Decorator;
using GameFrameworkLib.Playground;
using GameFrameworkLib.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib
{
    public class WeaponBoostAlter : WorldObject
    {
        #region Properties
        public string Name { get; set; }
        #endregion

        #region Constructors
        public WeaponBoostAlter()
        {
            
        }

        public WeaponBoostAlter(string worldObjectName, bool lootable, bool removeable, Position position, string name ) : base(worldObjectName, lootable,removeable, position)
        {
            Name = name;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method for adding 5 attackpower to an attackweapons baseattack using a decorator pattern
        /// </summary>
        /// <param name="attackItem">Item to be boosted</param>
        /// <returns>a decorator class of the item with weaponbost</returns>
        public IAttackItem Boost(IAttackItem attackItem)
        {
            return new BoostedAttackItemDecorator(attackItem);
        }
        #endregion
    }
}
