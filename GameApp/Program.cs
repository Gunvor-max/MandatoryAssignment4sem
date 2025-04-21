using GameFrameworkLib.Configuration;
using GameFrameworkLib;
using GameFrameworkLib.Creatures;
using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Decorator;
using GameFrameworkLib.Playground;

GameConfig config = new GameConfig();
World game = config.CreateGame();
game.StartGame();