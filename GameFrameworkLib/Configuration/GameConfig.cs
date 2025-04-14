using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using GameFrameworkLib.Template;
using GameFrameworkLib.Creatures;
using GameFrameworkLib.AttackItems.BaseAttackItem;
using GameFrameworkLib.Playground;

namespace GameFrameworkLib.Configuration
{
    public class GameConfig
    {
        private int maxY = 100;
        private int maxX = 100;
        private GameLevel level = GameLevel.novice;
        private List<Creature> creatureList = new List<Creature> 
        {
            new Wolf("Fenris",100,new List<AttackItem>(), new List<DefenceItem>()),
            new Lizard("Igor",100,new List<AttackItem>(), new List<DefenceItem>())
        };
        private List<WorldObject> worldObjects = new List<WorldObject> { new AttackItem(), new DefenceItem() };

        public int MaxY { get => maxY; set => maxY = value; }
        public int MaxX { get => maxX; set => maxX = value; }
        public GameLevel Level { get => level; set => level = value; }
        public List<Creature> CreatureList { get => creatureList; set => creatureList = value; }
        public List<WorldObject> WorldObjects { get => worldObjects; set => worldObjects = value; }

        /// <summary>
        /// Helper method for Creating generating XML structure 
        /// </summary>
        /// <returns>a string of GameConfig variables in XML form</returns>
        public string SerializeObjectToString()
        {
            var xmlserializer = new XmlSerializer(typeof(GameConfig));
            using (var writer = new StringWriter())
            {
                xmlserializer.Serialize(writer, this);
                var content = writer.ToString();
                return content;
            }
        }

        public string? ReadFromConfigFile(string NodeFromXML)
        {
            XmlDocument configDoc = new XmlDocument();
            //string path = Environment.GetEnvironmentVariable("AbstractServerConfig");
            string path = "C:\\Users\\mini_\\source\\repos\\MandatoryAssignment4sem\\GameFrameworkLib\\Configuration\\ConfigFile.xml";
            configDoc.Load(path);

            XmlNode? xxNode = configDoc.DocumentElement.SelectSingleNode($"{NodeFromXML}");
            if (xxNode != null)
            {
                Trace.Listeners.Add(new ConsoleTraceListener());
                string xxStr = xxNode.InnerText.Trim();
                //Trace.WriteLine(xxStr);
                //Trace.Flush();
                return xxStr;


                //int.TryParse(xxStr, out int port);
            }
            return null;

        }
    }
}
