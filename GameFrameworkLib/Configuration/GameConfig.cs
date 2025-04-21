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
        #region Instance Fields
        private int maxY = 100;
        private int maxX = 100;
        private GameLevel level = GameLevel.novice;
        #endregion

        #region Properties
        public int MaxY { get => maxY; set => maxY = value; }
        public int MaxX { get => maxX; set => maxX = value; }
        public GameLevel Level { get => level; set => level = value; }
        #endregion

        #region Methods
        /// <summary>
        /// Helper method for Generating XML structure of this class 
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


        /// <summary>
        /// Method for reading a single node from a given XML file
        /// </summary>
        /// <param name="NodeFromXML">The name of the node</param>
        /// <param name="path">The path to the XML file</param>
        /// <returns>The value of the node read</returns>
        public string? ReadFromConfigFile(string NodeFromXML, string path)
        {
            XmlDocument configDoc = new XmlDocument();
            //string path = Environment.GetEnvironmentVariable("AbstractServerConfig");
            path = "C:\\Users\\mini_\\source\\repos\\MandatoryAssignment4sem\\GameFrameworkLib\\Configuration\\ConfigFile.xml";
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

        /// <summary>
        /// Method for Configuring a World object with properties from a XML file
        /// </summary>
        /// <returns>The World object</returns>
        public World CreateGame()
        {
            XmlDocument configDoc = new XmlDocument();
            string path = "C:\\Users\\mini_\\source\\repos\\MandatoryAssignment4sem\\GameFrameworkLib\\Configuration\\ConfigFile.xml";
            configDoc.Load(path);

            XmlNode rootNode = configDoc.DocumentElement; 

            int maxY = 0;
            int maxX = 0;
            GameLevel level = GameLevel.novice; 

            if (rootNode != null)
            {
                foreach (XmlNode node in rootNode.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "MaxY":
                            int.TryParse(node.InnerText.Trim(), out maxY);
                            break;

                        case "MaxX":
                            int.TryParse(node.InnerText.Trim(), out maxX);
                            break;

                        case "Level":
                            string levelStr = node.InnerText.Trim();
                            switch (levelStr.ToLower())
                            {
                                case "novice":
                                    level = GameLevel.novice;
                                    break;
                                case "normal":
                                    level = GameLevel.normal;
                                    break;
                                case "trained":
                                    level = GameLevel.trained;
                                    break;
                                default:
                                    Trace.WriteLine($"Unknown level: {levelStr}");
                                    break;
                            }
                            break;

                        default:
                            Trace.WriteLine($"Unknown config: {node.Name}");
                            break;
                    }
                }
            }
            return new World(maxY, maxX, level);
        }
        #endregion

    }
}
