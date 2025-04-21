using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Composite
{
    public class AttackItemComposite : IAttackItem
    {
        #region Instance Fields
        private List<IAttackItem> _attackItems;
        #endregion

        #region Constructors
        public AttackItemComposite()
        {
            _attackItems = new List<IAttackItem>();
        }
        #endregion

        #region Methods
        public IEnumerable<IAttackItem> GetAll()
        {
            return _attackItems;
        }

        /// <summary>
        /// Method for returning the first attackItem of the IAttackItem list 
        /// </summary>
        /// <returns>an Attackitem</returns>
        public AttackItem GetFirst()
        {
            return _attackItems[0] as AttackItem;
        }

        /// <summary>
        /// Method for adding an IattackItem to the list of IAttackItems
        /// </summary>
        /// <param name="attackItem">Item to be added</param>
        public void Add(IAttackItem attackItem) 
        {
            _attackItems.Add(attackItem);
        }

        /// <summary>
        /// Method for removing an IattackItem from the list of IAttackItems
        /// </summary>
        /// <param name="attackItem">Item to be removed</param>
        public void Remove(IAttackItem attackItem) 
        {
            _attackItems.Remove(attackItem);
        }

        /// <summary>
        /// Not a functional method. Method needed to be implemented for using the Iattack interface but it has no value.
        /// </summary>
        /// <returns>Throws a not implemented exception if used</returns>
        /// <exception cref="NotImplementedException">Exception</exception>
        public int DecorateHIT()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
