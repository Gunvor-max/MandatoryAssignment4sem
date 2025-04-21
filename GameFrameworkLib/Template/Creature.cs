using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Composite;
using GameFrameworkLib.Creatures;
using GameFrameworkLib.Decorator;
using GameFrameworkLib.Playground;
using GameFrameworkLib.State;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameFrameworkLib.Template
{

    [XmlInclude(typeof(Wolf))]
    [XmlInclude(typeof(Lizard))]
    public abstract class Creature : INotifyPropertyChanged
    {
        #region Instance fields
        private int _headRow = 2;
        private int _headCol = 2;
        private Random random = new Random();
        #endregion

        #region Properties
        public string Name { get; set; }
        public int Hitpoint { get; set; }
        public Position CharacterOnMap { get; set; }
        public AttackItem WeaponEquipped { get; set; }
        public IAttackItem AttackBoost { get; set; }
        public AttackItemComposite AttackItems { get; set; } = new AttackItemComposite();
        public List<DefenceItem> DefenceItems { get; set; } = new List<DefenceItem>();
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Constructors
        public Creature()
        {
        }

        public Creature(string name, int hitpoint)
        {
            Name = name;
            Hitpoint = hitpoint;
            CharacterOnMap = new Position(_headRow, _headCol);
            PropertyChanged += MyObserver;
        }
        #endregion

        #region Methods 

        /// <summary>
        /// Method for moving a creature around the map 
        /// </summary>
        /// <param name="move">An object of the Move class</param>
        public void Move(Move direction)
        {
            SetCharacterOnMapNewPosition(direction);
        }

        /// <summary>
        /// Method for moving a creature around the map 
        /// </summary>
        /// <param name="move">An object of the Move class</param>
        private void SetCharacterOnMapNewPosition(Move move)
        {
            CharacterOnMap.Row += move.row;
            CharacterOnMap.Col += move.col;

        }

        /// <summary>
        /// Method for checking if the creature is inside the map
        /// </summary>
        /// <param name="maxWidth">Max width of the map</param>
        /// <param name="maxHeight">Max height of the map</param>
        /// <returns>A boolean</returns>
        public bool CheckInside(in int maxWidth, in int maxHeight)
        {
            return !(CharacterOnMap.Col == maxWidth || CharacterOnMap.Col == -1 ||
                    CharacterOnMap.Row == maxHeight || CharacterOnMap.Row == -1);

        }

        /// <summary>
        /// Method for interacting with a opponents on the map
        /// </summary>
        /// <param name="opponents">A list of present opponents on the map</param>
        /// <returns>A boolean that is true if this object's and the opponentobject's position (row,col) is the same</returns>
        public bool MeetOpponent(List<Creature> opponents)
        {
            foreach (var opponent in opponents) 
            {
                if (CharacterOnMap.Equals(opponent.CharacterOnMap))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method for interacting with a worldobject on the map
        /// </summary>
        /// <param name="worldObjects">A list of the present worldobjects on the map</param>
        /// <returns>A boolean that is true if this object's and the worldobject's position (row,col) is the same</returns>
        public bool MeetWorldObject(List<WorldObject> worldObjects)
        {
            foreach (var worldObject in worldObjects)
            {
                if (CharacterOnMap.Equals(worldObject.PositionOnMap))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method for dealing damage to a creature opponent. Is calculated from CalculatePower() as well as a random crit 1-10
        /// </summary>
        /// <returns>Total damage for hitting a creature</returns>
        public int Hit()
        {
            if (Hitpoint > 0)
            {
            int baseHit = CalculatePower();
            int crit = random.Next(1, 10);
            int calculatedHit = baseHit + crit + CreatureSpecificAttack();
            return calculatedHit;
            }
            return 0;
        }

        /// <summary>
        /// Method for recieving damage from a creature opponent. Alters the hitpoint to reflect damage taken
        /// </summary>
        /// <param name="hit">The hit recieved from the opponent creature</param>
        public void RecieveHit(int hit)
        {
            if (Hitpoint > 0 && hit != 0)
            {
                int calculatehit = hit - CreatureSpecificDefense();
                if (calculatehit >= 0)
                {
                    Hitpoint -= calculatehit;
                    Notify("Hitpoint");
                //Console.WriteLine($"{Name} takes {calculatehit} dmg and now has {Hitpoint}");
                }
                else
                {
                    //Console.WriteLine($"{Name} blocked the dmg");
                }
            }
        }

        /// <summary>
        /// Abstract method for adding extra defence/armor based on the creature type
        /// </summary>
        /// <returns></returns>
        protected abstract int CreatureSpecificDefense();

        /// <summary>
        /// Abstract method for adding extra attack damage based on the creature type
        /// </summary>
        /// <returns></returns>
        protected abstract int CreatureSpecificAttack();

        /// <summary>
        /// Method for looting a worldObject. Uses the Strategy design pattern to loot the object
        /// </summary>
        /// <param name="worldObject">The object the needs to be looted</param>
        public void Loot(WorldObject worldObject)
        {
            if (worldObject.Lootable)
            {
                worldObject.LootStrategy?.Loot(worldObject, this);
            }
        }

        /// <summary>
        /// Method for calculating the total base attackpower of a creature. Include attackboost if any is present
        /// </summary>
        /// <returns>the total base attackpower of a creature</returns>
        public int CalculatePower()
        {
            int basedmg = 10;
            if (AttackBoost != null)
            {
                switch (AttackBoost)
                {
                    case BoostedAttackItemDecorator boostedAttackItemDecorator:
                        basedmg += boostedAttackItemDecorator.BaseBoost;
                        break;
                    default:
                        break;
                }
            }
            AttackItem attackItem = WeaponEquipped;
            return attackItem == null ? basedmg : basedmg + attackItem.Hit;
        }

        /// <summary>
        /// Method for triggering a property event change
        /// </summary>
        /// <param name="PropertyName">The name of the property that has changed</param>
        public void Notify(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        /// <summary>
        /// Method for creating an observer with property changed notifications for Hitpoints 
        /// </summary>
        /// <param name="sender">The object that triggered the property change.</param>
        /// <param name="e">Instance of propertyChangedEventArgs</param>
        public void MyObserver(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Hitpoint":
                    if (Hitpoint <= 0)
                    {
                        Console.WriteLine($"{Name} gets hit and is now dead!");
                    }
                    else
                    {
                        string response = $"{Name} gets hit now has {Hitpoint} hp";
                        Console.WriteLine(response);
                    }
                    break;
                default:
                    break;
            }
        }
        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(Hitpoint)}={Hitpoint.ToString()}, {nameof(AttackItems)}={AttackItems}, {nameof(DefenceItems)}={DefenceItems}}}";
        }
        #endregion


    }
}
