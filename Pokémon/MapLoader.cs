using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;

namespace PokémonGame
{
    class MapLoader
    {
        public static map tMap = new map();

        public static map LoadMap(XmlReader reader, GameWindow parent)
        {
            //TODO replace

            return tMap;
        }

        # region submethods
        private static SpriteBase loadObject(XmlReader reader, GameWindow parent)
        {
            SpriteBase sC = new SpriteBase();
            //TODO remove
            return sC;
        }

        public static List<string>[] readText(XmlReader reader)
        {
            # region list initialisation
            List<string>[] conversations = new List<string>[4];
            conversations[0] = new List<string>();
            conversations[1] = new List<string>();
            conversations[2] = new List<string>();
            conversations[3] = new List<string>();
            # endregion

            # region sort text nodes into lists
            while (reader.Read())
            {
                if (reader.Name == "Text" && reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.GetAttribute("Type"))
                    {
                        case "greetingText":
                            conversations[0].Add(reader.ReadString());
                            break;

                        case "combatText":
                            conversations[1].Add(reader.ReadString());
                            break;

                        case "combatTextEnd":
                            conversations[2].Add(reader.ReadString());
                            break;

                        case "farewellText":
                            conversations[3].Add(reader.ReadString());
                            break;

                        default:
                            break;
                    }
                }
            }
            # endregion

            # region debug
            for (int i = 0; i < 4; i++)
            {
                foreach (string s in conversations[i])
                    Debug.WriteLine("{0}, {1}", i, s);
            }
            # endregion

            return conversations;
        }

        private static SpriteBase switchType(int type)
        {
            SpriteBase result = null;

            switch (type)
            {
                case 1: //Terrain
                    result = new SpriteTerrain();
                    break;

                case 2: //blank
                    break;

                case 3: //tree
                    result = new SpriteTerrain();
                    break;

                case 4: //item
                    result = new SpriteItem();
                    break;

                case 5: //blank
                    break;

                case 6: //npc
                    result = new SpritePerson();
                    break;

                case 7: //blank
                    break;

                case 8: //blank
                    break;

                case 9: //map transition
                    break;

                default:
                    result = new SpriteBase();
                    break;
            }

            return result;
        }

        private static Pokemon readPokemonNode(XmlReader reader)
        {
            Debug.WriteLine("readPokemonNode - {0}", reader.Name);

            Pokemon poke = new Pokemon();

            //TODO
            # region while (reader.Read())
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "PokeName":
                        poke.Name = reader.ReadString();
                        break;

                    case "PokeLevel":
                        poke.Level = reader.ReadElementContentAsInt();
                        break;

                    case "PokeID":
                        poke.ID = reader.ReadElementContentAsInt();
                        break;

                    case "Pokemoves[0]":
                        poke.moves[0] = MoveList.getMove(reader.ReadElementContentAsString());
                        break;

                    case "Pokemoves[1]":
                        poke.moves[1] = MoveList.getMove(reader.ReadElementContentAsString());
                        break;

                    case "Pokemoves[2]":
                        poke.moves[2] = MoveList.getMove(reader.ReadElementContentAsString());
                        break;

                    case "Pokemoves[3]":
                        poke.moves[3] = MoveList.getMove(reader.ReadElementContentAsString());
                        break;
                }
            }
            # endregion

            return poke;
        }

        private static void loadItem(int mapID, SpriteBase spCla)
        {
            //TODO replace

        }
        # endregion
    }

    class MapSaver
    {
        public static string SaveMap(map m)
        {
            //TODO replace

            return "";
        }

        public static XmlNode makeItem(inventoryItem item)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode result = doc.CreateNode(XmlNodeType.Element, "inventoryItem", null);


            return result;
        }

        public static XmlNode makePokemon(Pokemon poke)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode result = doc.CreateNode(XmlNodeType.Element, "inventoryItem", null);


            return result;
        }
    }
}
