using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Creatures;
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
        public Position CharacterOnMap { get; set; }
        public string Name { get; set; }
        public int Hitpoint { get; set; }
        private int _headRow = 2;
        private int _headCol = 2;
        public Random random = new Random();

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<AttackItem> AttackItems { get; set; } = new List<AttackItem>();
        public List<DefenceItem> DefenceItems { get; set; } = new List<DefenceItem>();

        public Creature()
        {
        }

        public Creature(string name, int hitpoint, List<AttackItem> attackItems, List<DefenceItem> defenceItems)
        {
            CharacterOnMap = new Position(_headRow, _headCol);
            Name = name;
            Hitpoint = hitpoint;
            AttackItems = attackItems;
            DefenceItems = defenceItems;
            PropertyChanged += MyObserver;
        }

        #region Methods 

        public void Move(Move direction)
        {
            SetCharacterOnMapNewPosition(direction);
        }

        private void SetCharacterOnMapNewPosition(Move move)
        {
            CharacterOnMap.Row += move.row;
            CharacterOnMap.Col += move.col;
        }

        public bool CheckInside(in int maxWidth, in int maxHeight)
        {
            return !(CharacterOnMap.Col == maxWidth || CharacterOnMap.Col == -1 ||
                    CharacterOnMap.Row == maxHeight || CharacterOnMap.Row == -1);

        }

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

        protected abstract int CreatureSpecificDefense();
        protected abstract int CreatureSpecificAttack();

        public void Loot(WorldObject worldObject)
        {
            if (worldObject.Lootable)
            {
                worldObject.LootStrategy?.Loot(worldObject, this);
            }
        }

        public bool HasWeapon()
        {
            if (AttackItems.Count != 0)
            {
                return true;
            }
            return false;
        }

        public AttackItem WeaponEquipped()
        {
            if (HasWeapon())
            {
                return AttackItems[0];
            }
            return null;
        }

        public int CalculatePower()
        {
            int basedmg = 10;
            AttackItem attackItem = WeaponEquipped();
            return attackItem == null ? basedmg : basedmg + attackItem.Hit;
        }

        public void Notify(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

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
                    Console.WriteLine($"{Name} gets hit now has {Hitpoint} hp");
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
