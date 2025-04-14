using GameFrameworkLib.Configuration;
using GameFrameworkLib;
using GameFrameworkLib.Creatures;
using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Decorator;
using GameFrameworkLib.Playground;

//GameConfig config = new GameConfig();
//Console.WriteLine(config.SerializeObjectToString());
//Console.WriteLine(config.ReadFromConfigFile("MaxY"));

//IAttackItem attackItem = new AttackItem("FrostMourne",2,2);
//IAttackItem boostedAttackItem = new BoostedAttackItemDecorator(attackItem);

//Console.WriteLine($"Before Boost: {attackItem.DecorateHIT()}");
//Console.WriteLine($"After Boost: {boostedAttackItem.DecorateHIT()}");

//int LizzardWon = 0;
//int WolfWon = 0;
//Console.WriteLine("Battle");

//for (int i = 0; i < 100; i++)
//{
//    Wolf wolf = new Wolf("Fenris", 100, new List<AttackItem>(), new List<DefenceItem>());

//    Lizard lizard = new Lizard("Igor", 100, new List<AttackItem>(), new List<DefenceItem>());

//    while (wolf.Hitpoint > 0 && lizard.Hitpoint > 0)
//    {
//        if(i % 2 == 0)
//        {
//        lizard.RecieveHit(wolf.Hit());
//        wolf.RecieveHit(lizard.Hit());
//        }
//        else
//        {
//            wolf.RecieveHit(lizard.Hit());
//            lizard.RecieveHit(wolf.Hit());
//        }
//        Console.WriteLine("------------");
//        if (wolf.Hitpoint <= 0 || lizard.Hitpoint <= 0)
//        {
//            string response = wolf.Hitpoint <= 0 ? lizard.Name : wolf.Name;
//            int counter = wolf.Hitpoint <= 0 ? LizzardWon++ : WolfWon++;
//            Console.WriteLine(response + " Won!");

//        }
//        //Thread.Sleep(1000);
//    }
//}
//Console.WriteLine($"Lizard won: {LizzardWon} times");
//Console.WriteLine($"Wolf won {WolfWon} times");

World world = new World(100,20);
world.StartGame();