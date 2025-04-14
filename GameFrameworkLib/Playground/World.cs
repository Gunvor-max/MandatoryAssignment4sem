using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Creatures;
using GameFrameworkLib.State;
using GameFrameworkLib.Template;

namespace GameFrameworkLib.Playground
{
    public class World
    {
        // WorldMap
        private char[,] _playground;
        public int MaxY { get; set; }
        public int MaxX { get; set; }
        private readonly string horizontalLine = "";
        Creature _mainCreature;
        public GameLevel Level { get; set; }
        //TODO Test Creaturelist with populated data.
        List<Creature> CreatureList { get; set; } = new List<Creature> 
        { 
            new Lizard("Commander - FireBreatingLizzard", 100, new List<AttackItem>(), new List<DefenceItem>()),
            new Lizard("Recruit - FireBreatingLizzard", 100, new List<AttackItem>(), new List<DefenceItem>()),
            new Lizard("Soldier - FireBreatingLizzard", 100, new List<AttackItem>(), new List<DefenceItem>()),
            new Lizard("Commander - FrostWolf", 100, new List<AttackItem>(), new List<DefenceItem>()),
            new Lizard("Recruit - FrostWolf", 100, new List<AttackItem>(), new List<DefenceItem>()),
            new Lizard("Soldier - FrostWolf", 100, new List<AttackItem>(), new List<DefenceItem>()),

        };
        //List<Creature> CreatureList { get; set; } = new List<Creature>();
        List<WorldObject> WorldObjects { get; set; } = new List<WorldObject>();

        private readonly ConcurrentQueue<char> _keyStrokes = new ConcurrentQueue<char>();
        private readonly StreamWriter _w = new StreamWriter("KeyLog.txt");
        private ManualResetEventSlim _pauseEvent = new ManualResetEventSlim(true);

        // random
        private static Random rnd = new Random(DateTime.Now.Millisecond);

        public World(int maxX, int maxY)
        {
            _playground = new char[maxX, maxY];
            MaxY = maxY;
            MaxX = maxX;
            for (int i = 0; i < maxX + 2; i++)
            {
                horizontalLine += "-";
            }

            foreach (Creature opponent in CreatureList)
            {
                opponent.CharacterOnMap = new Position(rnd.Next(maxY), rnd.Next(maxX));
                WorldObjects.Add(new HealthPotion("Health", true, true, new Position(rnd.Next(maxY), rnd.Next(maxX)), "Greater Healthpotion", 30));
            }
            WorldObjects.Add(new AttackItem("Attack",true,true,new Position(rnd.Next(maxY), rnd.Next(maxX)), "Flamesword",10,5));
        }

        public void StartGame()
        {
            var mainChosen = ShowOptions(["FrostWolf", "FireLizard"],"Choose your maincharacter: ");
            _mainCreature = (mainChosen == 0) ? new Wolf("Main", 100, new List<AttackItem>(), new List<DefenceItem>()) :
                                                new Lizard("Main", 100, new List<AttackItem>(), new List<DefenceItem>());
            Task.Run(() => StartKeyReader());

            // using state machine pattern
            Istate stateMachine = new CharacterStateMachinePattern();

            bool gameContinue = true;


            PrintPlayground();

            while (gameContinue)
            {
                //Console.WriteLine("Point = " + pg.Point);

                InputType nextInput = ReadNextEvent();
                Move nextMove = stateMachine.NextMove(nextInput);

                gameContinue = DoNextMove(nextMove);
                if (gameContinue)
                {
                    PrintPlayground();
                }
                Thread.Sleep(10);

            }

        }

        private void StartKeyReader()
        {
            while (true)
            {
                _pauseEvent.Wait();

                ConsoleKeyInfo info = Console.ReadKey();
                char c = info.KeyChar;
                switch (c)
                {
                    case 'a':
                        _keyStrokes.Enqueue(c);
                        break;
                    case 'd':
                        _keyStrokes.Enqueue(c);
                        break;
                    case 'w':
                        _keyStrokes.Enqueue(c);
                        break;
                    default: /* nothing */ break;
                }

                _w.WriteLine("input Q: " + string.Join(":", _keyStrokes));
                _w.Flush();
                Thread.Sleep(10); // wait 10 msec
            }
        }

        private InputType ReadNextEvent()
        {
            InputType ev = InputType.FORWARD;

            if (_keyStrokes.Count > 0)
            {
                char c = 'w'; // default forward
                _keyStrokes.TryDequeue(out c);

                switch (c)
                {
                    case 'a':
                        ev = InputType.LEFT;
                        break;
                    case 'd':
                        ev = InputType.RIGHT;
                        break;
                    case 'w':
                        ev = InputType.FORWARD;
                        break;
                }
            }

            return ev;
        }

        public bool DoNextMove(Move move)
        {
            _mainCreature.Move(move);

            bool inside = _mainCreature.CheckInside(MaxX, MaxY);
            if (!inside)
            {
            Console.WriteLine("You are no longer inside");                
            }

            if (_mainCreature.MeetOpponent(CreatureList))
            {
                Creature opponent = CreatureList.FirstOrDefault(c => c.CharacterOnMap.Equals(_mainCreature.CharacterOnMap));
                if (opponent != null) 
                {
                bool isAlive = CheckFight(_mainCreature, opponent);
                if (!isAlive) 
                {
                    Environment.Exit(0);
                }

                opponent.CharacterOnMap = new Position(rnd.Next(MaxY), rnd.Next(MaxX));                    
                }
            }
            else if (_mainCreature.MeetWorldObject(WorldObjects))
            {
                WorldObject worldObject = WorldObjects.FirstOrDefault(p => p.PositionOnMap.Equals(_mainCreature.CharacterOnMap));
                if (worldObject != null) 
                {
                _mainCreature.Loot(worldObject);
                }
            }

            return inside;
        }

        public void PrintPlayground()
        {
            Console.Clear();
            Console.WriteLine($"Name: {_mainCreature.Name} | HP: {_mainCreature.Hitpoint} | AttackItem: {(_mainCreature.HasWeapon() ? _mainCreature.WeaponEquipped().Name : "")} | Power: {_mainCreature.CalculatePower()}");
            Console.WriteLine(horizontalLine);
            for (int r = 0; r < MaxY; r++)
            {
                Console.Write("|");
                PrintRowString(r);
                Console.WriteLine($"|");
            }
            Console.WriteLine(horizontalLine);
        }
        private void PrintRowString(int r)
        {
            StringBuilder sb = new StringBuilder();
            for (int c = 0; c < MaxX; c++)
            {
                PrintColRowChar(r, c);
            }
        }

        private void PrintColRowChar(int row, int col)
        {
            Position p = new Position(row, col);

            foreach (Creature _opponent in CreatureList)
            {
                if (_opponent.CharacterOnMap.Equals(p))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('o');
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            foreach (WorldObject _worldobject in WorldObjects)
            {
                if (_worldobject.PositionOnMap.Equals(p) && _worldobject.Lootable)
                {
                    switch (_worldobject)
                    {
                        case AttackItem attackItem:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write('A');
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case HealthPotion healthPotion:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write('+');
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (_mainCreature.CharacterOnMap.Equals(p))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write('@');
                Console.ForegroundColor = ConsoleColor.White;
            }
            //else if (_opponentCreature.Equals(p))
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.Write('o');
            //    Console.ForegroundColor = ConsoleColor.White;
            //}
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(' ');
            }

        }

        private bool CheckFight(Creature MainCreature, Creature OpponentCreature)
        {
            string title = "You have encountered a monster. Are you ready to fight?";
            var result = ShowOptions(["Yes", "No"], title);
            if (result == 0)
            {
                while (MainCreature.Hitpoint > 0 && OpponentCreature.Hitpoint > 0)
                {
                    OpponentCreature.RecieveHit(MainCreature.Hit());
                    MainCreature.RecieveHit(OpponentCreature.Hit());

                    if (MainCreature.Hitpoint <= 0 || OpponentCreature.Hitpoint <= 0)
                    {
                        string response = MainCreature.Hitpoint <= 0 ? OpponentCreature.Name : MainCreature.Name;
                        bool IsAlive = MainCreature.Hitpoint > 0 ? true : false;
                        Console.WriteLine(response + " Won!");
                        Console.ReadLine();
                        return IsAlive;

                    }
                    Thread.Sleep(1000);
                }
                return false;
            }
            return true;

        }

        public int ShowOptions(string[] options, string title)
        {
            PauseKeyReader();
            int selectedIndex = 0;
            bool optionChosen = false;
            Console.CursorVisible = false;

            while (!optionChosen)
            {
                // Clear the entire console window.
                Console.Clear();
                Console.WriteLine(title);

                // Loop through and display each option.
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        // Highlight the currently selected option.
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(options[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(options[i]);
                    }
                }

                // Wait for a key press.
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % options.Length;
                        break;
                    case ConsoleKey.Enter:
                        optionChosen = true;
                        break;
                }
            }

            Console.CursorVisible = true;
            ResumeKeyReader();
            return selectedIndex;
        }

        public void PauseKeyReader()
        {
            _pauseEvent.Reset(); 
        }

        public void ResumeKeyReader()
        {
            _pauseEvent.Set(); 
        }

    }
    public enum GameLevel
    {
        novice, normal, trained
    }
}
