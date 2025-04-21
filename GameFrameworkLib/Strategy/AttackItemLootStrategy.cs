using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Composite;
using GameFrameworkLib.Decorator;
using GameFrameworkLib.Logging;
using GameFrameworkLib.Template;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Strategy
{
    public class AttackItemLootStrategy : ILootStrategy
    {
        /// <summary>
        /// Method for looting a worldobject using the strategy pattern with defenceItem and the composite pattern for AttackItem
        /// </summary>
        /// <param name="worldObject">The object to be looted</param>
        /// <param name="creature">The creature looting the object</param>
        public void Loot(WorldObject worldObject, Creature creature)
        {
            if (worldObject is IAttackItem attackItem)
            {
                if (attackItem is AttackItemComposite composite)
                {
                    foreach (var item in composite.GetAll())
                    {
                        if (attackItem is AttackItem specificItem)
                        {
                            LogIt.Instance.LogEvent(TraceEventType.Information, $"Main looted: {specificItem.Name}");
                        }
                        else
                        {
                            LogIt.Instance.LogEvent(TraceEventType.Information, "Main looted unknown item.");
                        }
                        creature.AttackItems.Add(item);
                    }
                }
                else
                {
                    creature.AttackItems.Add(attackItem);
                    if (attackItem is AttackItem specificItem)
                    {
                        LogIt.Instance.LogEvent(TraceEventType.Information, $"Main looted: {specificItem.Name}");
                    }
                    else
                    {
                        LogIt.Instance.LogEvent(TraceEventType.Information, "Main looted unknown item.");
                    }

                }
                creature.WeaponEquipped = creature.AttackItems.GetFirst();
            }

            worldObject.Lootable = false;
            worldObject.Removeable = true;
        }
    }
}
