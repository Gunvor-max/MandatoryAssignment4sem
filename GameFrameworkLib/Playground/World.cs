using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Creatures;
using GameFrameworkLib.Decorator;
using GameFrameworkLib.Logging;
using GameFrameworkLib.State;
using GameFrameworkLib.Template;
using System.Diagnostics;

namespace GameFrameworkLib.Playground
{
    public class World
    {
        #region Instance Fields
        private char[,] _playground;
        private readonly string horizontalLine = "";
        private int LogKillCounter = 0;
        private Creature _mainCreature;
        private readonly ConcurrentQueue<char> _keyStrokes = new ConcurrentQueue<char>();
        private readonly StreamWriter _w = new StreamWriter("KeyLog.txt");
        private ManualResetEventSlim _pauseEvent = new ManualResetEventSlim(true);
        private static Random rnd = new Random(DateTime.Now.Millisecond);
        #endregion

        #region Properties
        public int MaxY { get; set; }
        public int MaxX { get; set; }
        public GameLevel Level { get; set; }
        private List<Creature> CreatureList { get; set; }
        private List<WorldObject> WorldObjects { get; set; } = new List<WorldObject>();
        #endregion

        #region Constructors
        public World()
        {
            
        }

        public World(int maxX, int maxY, GameLevel level)
        {
            Level = level;
            LogIt.Instance.LogEvent(TraceEventType.Information, $"Difficulty chosen: {level} - Creature hp: {DifficultyChosen()[0].Hitpoint}");


            CreatureList = DifficultyChosen();

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
            WorldObjects.Add(new AttackItem("Attack", true, true, new Position(rnd.Next(maxY), rnd.Next(maxX)), "Flamesword", 10, 5));
            WorldObjects.Add(new WeaponBoostAlter("Alter", false, false, new Position(rnd.Next(maxY), rnd.Next(maxX)), "FireAlter"));

        }
        #endregion

        /// <summary>
        /// Method for populating the list of opponent creatures based on difficulty
        /// </summary>
        /// <returns>A list of creatures</returns>
        public List<Creature> DifficultyChosen()
        {
            switch (Level)
            {
                case GameLevel.novice:
                    return
                        new List<Creature>
        {
            new Lizard("Commander - FireBreatingLizzard", 100),
            new Lizard("Recruit - FireBreatingLizzard", 100),
            new Lizard("Soldier - FireBreatingLizzard", 100),
            new Wolf("Commander - FrostWolf", 100),
            new Wolf("Recruit - FrostWolf", 100),
            new Wolf("Soldier - FrostWolf", 100),

        };
                case GameLevel.normal:
                    return
                        new List<Creature>
        {
            new Lizard("Commander - FireBreatingLizzard", 150),
            new Lizard("Recruit - FireBreatingLizzard", 150),
            new Lizard("Soldier - FireBreatingLizzard", 150),
            new Wolf("Commander - FrostWolf", 150),
            new Wolf("Recruit - FrostWolf", 150),
            new Wolf("Soldier - FrostWolf", 150),

        };
                case GameLevel.trained:
                    return
                        new List<Creature>
        {
            new Lizard("Commander - FireBreatingLizzard", 200),
            new Lizard("Recruit - FireBreatingLizzard", 200),
            new Lizard("Soldier - FireBreatingLizzard", 200),
            new Wolf("Commander - FrostWolf", 200),
            new Wolf("Recruit - FrostWolf", 200),
            new Wolf("Soldier - FrostWolf", 200),

        };
                default:
                    return new List<Creature>();
            }
        }

        /// <summary>
        /// Method for starting the game by combining multiple functions from the World class and adding a while loop
        /// </summary>
        public void StartGame()
        {
            //Log gamestart
            LogIt.Instance.LogEvent(TraceEventType.Information, $"Game started: {DateTime.Now}");

            var mainChosen = ShowOptions(["FrostWolf", "FireLizard"], "Choose your maincharacter: ");
            _mainCreature = (mainChosen == 0) ? new Wolf("Main", 100) :
                                                new Lizard("Main", 100);

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
            //Log gameend
            LogIt.Instance.LogEvent(TraceEventType.Information, $"Main killed: {LogKillCounter} enemies");
            LogIt.Instance.LogEvent(TraceEventType.Information, $"Game ended: {DateTime.Now}");

        }

        /// <summary>
        /// Method for registering a keypress from the user and adding it to a que that the ReadNextEvent() can use to update the state
        /// </summary>
        private void StartKeyReader()
        {
            while (true)
            {
                if (!_pauseEvent.Wait(0)) // Check if the key reader is paused (non-blocking)
                {
                    Thread.Sleep(10); // Avoid CPU overloading
                    continue; // Skip processing input and move to the next iteration
                }
                // Only read input when the key reader is active
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo info = Console.ReadKey(true);
                char c = info.KeyChar;
                switch (c)
                {
                    case 'a':
                    case 'd':
                    case 'w':
                        _keyStrokes.Enqueue(c); // Process valid keys
                        break;
                    default:
                        break; // Ignore unrecognized input
                }

                _w.WriteLine("Input Q: " + string.Join(":", _keyStrokes));
                _w.Flush();
                }
                else
                {
                Thread.Sleep(10); // Reduce CPU usage
                }

            }
        }




        /// <summary>
        /// Method for updating the state of the main character depending on the key pressed
        /// </summary>
        /// <returns>The input type known as state</returns>
        private InputType ReadNextEvent()
        {
            _pauseEvent.Wait();

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

        /// <summary>
        /// Method for moving the main character, checking his position on the map as well as interacting with worldobjects and creatures
        /// </summary>
        /// <param name="move">The movement of the main character</param>
        /// <returns>A boolean indicating if the main character is still within the map</returns>
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
                        Console.WriteLine("Main Is dead");
                        Environment.Exit(0);
                    }
                    LogKillCounter++;
                    opponent.CharacterOnMap = new Position(rnd.Next(MaxY), rnd.Next(MaxX));
                }
            }
            else if (_mainCreature.MeetWorldObject(WorldObjects))
            {
                WorldObject worldObject = WorldObjects.FirstOrDefault(p => p.PositionOnMap.Equals(_mainCreature.CharacterOnMap));
                if (worldObject != null)
                {
                    if (worldObject is WeaponBoostAlter alter)
                    {
                        _mainCreature.AttackBoost = alter.Boost(_mainCreature.WeaponEquipped);
                    }
                    _mainCreature.Loot(worldObject);
                }
            }

            return inside;
        }

        /// <summary>
        /// Method that clears the previous console and print the entire map along with its entities. Also displays info on the main character 
        /// </summary>
        public void PrintPlayground()
        {
            Console.Clear();
            Console.WriteLine($"Name: {_mainCreature.Name} | HP: {_mainCreature.Hitpoint} | AttackItem: {(_mainCreature.WeaponEquipped != null ? _mainCreature.WeaponEquipped.Name : "")} | Power: {_mainCreature.CalculatePower()}");
            Console.WriteLine(horizontalLine);
            for (int r = 0; r < MaxY; r++)
            {
                Console.Write("|");
                PrintRowString(r);
                Console.WriteLine($"|");
            }
            Console.WriteLine(horizontalLine);
        }

        /// <summary>
        /// Method for building and prints the contents of a specific row on the map.
        /// </summary>
        /// <param name="r">Index of the row to be printed.</param>
        private void PrintRowString(int r)
        {
            StringBuilder sb = new StringBuilder();
            for (int c = 0; c < MaxX; c++)
            {
                PrintColRowChar(r, c);
            }
        }

        /// <summary>
        /// Method for rendering the physical entities on the map
        /// </summary>
        /// <param name="row">row index on the map</param>
        /// <param name="col">col index on the map</param>
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
                else if (_worldobject.PositionOnMap.Equals(p) && _worldobject is WeaponBoostAlter && _mainCreature.WeaponEquipped != null)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write('#');
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            if (_mainCreature.CharacterOnMap.Equals(p))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write('¤');
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(' ');
            }

        }

        /// <summary>
        /// Method for managing the battle of 2 creatures
        /// </summary>
        /// <param name="MainCreature">Main character chosen by the user</param>
        /// <param name="OpponentCreature">opponent creature met on the map</param>
        /// <returns>a bool reflecting of main character win </returns>
        private bool CheckFight(Creature MainCreature, Creature OpponentCreature)
        {
            string title = "You have encountered a monster. Are you ready to fight?";
            var result = ShowOptions(["Yes", "No"], title);
            if (result == 0)
            {
                if (MainCreature.AttackBoost == null)
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
                }
                else
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
                            MainCreature.AttackBoost = null;
                            return IsAlive;

                        }
                        Thread.Sleep(1000);
                    }
                }
                return false;
            }
            return true;

        }

        /// <summary>
        /// Method for generating an options menu used for the player to navigate the map
        /// </summary>
        /// <param name="options">a string array of options to choose</param>
        /// <param name="title">a title for displaying text info before chosing</param>
        /// <returns>an integer with the option index chosen</returns>
        public int ShowOptions(string[] options, string title)
        {
            PauseKeyReader(); // Fully stop background input handling
            int selectedIndex = 0;
            bool optionChosen = false;
            Console.CursorVisible = false;

            while (!optionChosen)
            {
                Console.Clear();
                Console.WriteLine(title);

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
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

                var keyInfo = Console.ReadKey(true); // Wait for input

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
            ResumeKeyReader(); // Allow background input handling again
            return selectedIndex;
        }




        /// <summary>
        /// Method for pausing the keyreader task listening for movements on the map
        /// </summary>
        public void PauseKeyReader()
        {
            _pauseEvent.Reset(); // Pause key reader thread
        }

        /// <summary>
        /// Method for un-pausing the keyreader task listening for movements on the map
        /// </summary>
        public void ResumeKeyReader()
        {
            _pauseEvent.Set(); // Resume key reader thread
        }


    }
    public enum GameLevel
    {
        novice, normal, trained
    }
}
