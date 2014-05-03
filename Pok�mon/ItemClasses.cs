using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;
using PokémonGame;
using PokémonGame.Properties;

# region inventory classes
public class Inventory
{
    public static Dictionary<string, inventoryItem> items = new Dictionary<string, inventoryItem>();

    # region images
    public static Dictionary<int, Image> gallery = new Dictionary<int, Image>();
    //TODO rewrite image List. needs ID, state, image/path
    public static Dictionary<string, ImageBool> imageList = new Dictionary<string, ImageBool>();
    public static int GalleryIndex = 0;
    # endregion

    public static void remove(string id, int quantity)
    {
        if (items.ContainsKey(id)) items[id].decreaseCount(quantity);
    }

    public static void add(string id, int quantity)
    {
        if (items.ContainsKey(id))
        {
            items[id].increaseCount(quantity);
        }
        else
        {
            items.Add(id, ItemList.getItem(id, quantity));
        }
    }

    public static void output()
    {
        foreach (KeyValuePair<string, inventoryItem> i in items)
        {
            Debug.WriteLine("returned {0}", i.Value.getNameNet());
        }
    }

    public static void LoadImages()
    {
        XmlReader reader = new XmlTextReader(PokémonGame.Properties.Resources.ImageList);

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "img")
            {
                ImageBool img = new ImageBool(Image.FromFile(reader.ReadString()), false);
                imageList.Add(reader.GetAttribute("ID"), img);
            }
        }

        reader.Close();
    }
}
# endregion

public class ItemList
{
    /// <summary>
    /// static class that stores all type of item used in game.
    /// </summary>

    public static Dictionary<string, inventoryItem> items = new Dictionary<string, inventoryItem>();

    public ItemList() { }

    public ItemList(XmlReader reader)
    {
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "inventoryItem")
            {
                int type = int.Parse(reader.GetAttribute("Type"));
                inventoryItem item; //can be overwritten by the switch

                # region switch (type)
                switch (type)
                {
                    case 1: //medicine
                        item = new RecoveryItem();
                        break;

                    case 2: //pokeball
                        item = new Pokeball();
                        item.Power = double.Parse(reader.GetAttribute("Power"));
                        break;

                    case 3: //TM
                        item = new TMItem();
                        break;

                    case 4: //berry
                        item = new Berry();
                        break;

                    case 5: //holdable item
                        //item = new MainItem();
                        item = new HoldItem(); //still returns "MainItem"";
                        break;

                    case 6: //key item
                        item = new KeyItem();
                        break;

                    default:
                        item = new inventoryItem();
                        break;
                }
                # endregion

                item.ID = reader.GetAttribute("ID");
                item.Name = reader.GetAttribute("Name");
                item.type = type;
                item.Description = reader.ReadElementString();

                items.Add(item.ID, item);
            }
        }
        reader.Close();

        Serialise();
    }

    public void Serialise()
    {
        List<inventoryItem> listItem = items.Values.ToList();
        System.Xml.Serialization.XmlSerializer serial = new System.Xml.Serialization.XmlSerializer(typeof(List<inventoryItem>));

        string path = Settings.Default.AppDir + @"\itemList.xml";

        using (Stream stream = File.Create(path))
        {
            serial.Serialize(stream, listItem);
        }
    }

    public static inventoryItem getItem(string ID)
    {
        //add checks that ID exists
        if (items.ContainsKey(ID))
        {
            return items[ID];
        }
        else
        {
            return null;
        }
    }

    public static inventoryItem getItem(string ID, int quantity)
    {
        inventoryItem result;

        if (items.ContainsKey(ID))
        {
            result = items[ID];
            result.count = quantity;

            return result;
        }
        else
        {
            return null;
        }
    }
}

# region Pokemon declaration and definition
public class PokemonLookup : Pokemon
{
    public static Dictionary<string, Pokemon> pokeList = new Dictionary<string, Pokemon>();

    public PokemonLookup() { }

    public static void add(Pokemon p)
    {
        if (!pokeList.ContainsKey(p.ID.ToString()))
        {
            pokeList.Add(p.ID.ToString(), p);
        }
    }

    public static Pokemon get(int ID)
    {
        string i = (ID - 1).ToString();

        if (pokeList.ContainsKey(i))
        {
            return pokeList[i];
        }
        else
        {
            return null;
        }
    }
}

# region Party and Boxes
public class PokemonList
{
    /// <summary>
    /// Party/Boxes for pokemon storage
    /// need a way to make them global...
    /// </summary>

    public int capacity = 30;
    public Dictionary<int, Pokemon> PokeList = new Dictionary<int, Pokemon>();

    public PokemonList() { }

    public PokemonList(int cap) { capacity = cap; }

    public string Output()
    {
        string result = "<PokemonList>\r\n";

        foreach (KeyValuePair<int, Pokemon> k in PokeList)
        {
            result += "<Pokemon>" + k.Value + "</Pokemon>\r\n";
        }

        result += "</PokemonList>";

        Debug.WriteLine(result);

        return result;
    }

    public bool add(Pokemon p)
    {
        //true for pokemon add, false for no space.
        int c = PokeList.Count;

        if (c <= capacity)
        {
            PokeList.Add(c, p);
            return true;
        }
        else { return false; }
    }
}

public static class PokeBox
{
    public static PokemonList Party = new PokemonList(6);
    public static PokemonList[] Boxes = new PokemonList[15];
    public static int current = 0;

    public static void init()
    {
        for (int i = 0; i < Boxes.Length; i++)
        {
            if (Boxes[i] == null) { Boxes[i] = new PokemonList(); }
        }
    }

    public static void add(Pokemon p)
    {
        if (!Party.add(p)) //if no space in party
        {
            while (!Boxes[current].add(p))
            {
                Debug.WriteLine("current++");
                current++;

                if (current > Boxes.Length)
                {
                    Debug.WriteLine("break");
                    break;
                }
            }
        }
    }

    public static bool Space()
    {
        bool result = false;
        int space = 0;

        if (Party.PokeList.Count < Party.capacity) result = true; //check party for space

        for (int i = 0; i < Boxes.Length; i++) //check boxes for space
        {
            space = Boxes[i].capacity - Boxes[i].PokeList.Count;
            if (space > 0)
            {
                result = true;
            }
        }

        return result;
    }

    public static void Heal(int Box)
    {
        foreach (KeyValuePair<int, Pokemon> p in Boxes[Box].PokeList)
        {
            p.Value.Heal();
        }
    }

    public static void HealGroup(double amount)
    {
        foreach (KeyValuePair<int, Pokemon> p in Party.PokeList)
        {
            p.Value.HPCurrent += amount;
            if (p.Value.HPCurrent > p.Value.HP.NetStat) p.Value.HPCurrent = p.Value.HP.NetStat;
        }
    }
}


# endregion

public class Pokemon : PokemonBase
{
    //inherits from PokemonBase

    public Pokemon() { this.meCaught += new Caught(onCatch); }

    public void CalcStats()
    {
        HP.NetStat = grabStats(2, HP.BaseStat, Level, HP.IV, HP.EV);
        Attack.NetStat = grabStats(1, Attack.BaseStat, Level, Attack.IV, Attack.EV);
        Defense.NetStat = grabStats(1, Defense.BaseStat, Level, Defense.IV, Defense.EV);
        SpAtt.NetStat = grabStats(1, SpAtt.BaseStat, Level, SpAtt.IV, SpAtt.EV);
        SpDef.NetStat = grabStats(1, SpDef.BaseStat, Level, SpDef.IV, SpDef.EV);
        Speed.NetStat = grabStats(1, Speed.BaseStat, Level, Speed.IV, Speed.EV);
    }

    public int grabStats(int path, int BaseStat, int level, int IV, int EV)
    {
        int netStat = 0;

        if (path == 2)
        {
            netStat = ((((IV + 2) * BaseStat) + (EV / 4)) * level / 100) + 10 + level;
        }
        else
        {
            netStat = (((((IV + 2) * BaseStat) + (EV / 4)) * level / 100) + 5);
        }
        return netStat;
    }

    public void onCatch()
    {
        CalcStats();
        PokeBox.add(this);
    }

    public void Heal() { this.HPCurrent = this.HP.NetStat; }

    public Move chooseMove()
    {
        List<Move> cMoves = new List<Move>();
        Random r = new Random();

        foreach (Move m in moves)
        {
            if (m != null && m.ID != "-1") cMoves.Add(m);
        }

        return cMoves[r.Next(0, cMoves.Count)];
    }
}
# endregion

# region sprite storing and assigning

public class images
{
    public static Image[,] allSprites = new Image[1000, 100];

    public static void add(int x, int y, Image i)
    {
        allSprites[x, y] = i;
    }

    public static Image get(int ID, int variant)
    {
        return allSprites[ID + 1, variant];
    }
}

# endregion

# region Move definition
public class MoveList
{
    public static Dictionary<string, Move> moves = new Dictionary<string, Move>();

    public MoveList()
    {
        Move newMove = new Move();
        newMove.ID = "1";
        newMove.Name = "Tackle";

        moves.Add(newMove.ID, newMove);
    }

    public MoveList(XmlReader reader)
    {
        Load(reader);
    }

    public static void Serialise()
    {
        System.Xml.Serialization.XmlSerializer serial = new System.Xml.Serialization.XmlSerializer(typeof(List<Move>));

        string file = Settings.Default.AppDir + @"\moveList.xml";

        using (Stream stream = File.Create(file))
        {
            serial.Serialize(stream, moves.Values.ToList());
        }
    }

    public static void Load(XmlReader reader)
    {
        Debug.Write("Moves being added...");

        string power = "";
        string accuracy = "";

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Move")
            {
                Move newMove = new Move();
                newMove.ID = reader.GetAttribute("ID");
                newMove.Name = reader.GetAttribute("Name");
                newMove.Type = reader.GetAttribute("Type").ToUpper();
                newMove.Category = reader.GetAttribute("Category");
                //newMove.Contest = reader.GetAttribute("Contest"); //TODO implement
                newMove.PPCurrent = int.Parse(reader.GetAttribute("PP"));

                power = reader.GetAttribute("Power");
                accuracy = reader.GetAttribute("Accuracy");
                //newMove.Gen = reader.GetAttribute("Gen");

                if (power != "-") { newMove.Power = int.Parse(reader.GetAttribute("Power")); } else { newMove.Power = 0; }

                if (accuracy != "-") { newMove.Accuracy = double.Parse(reader.GetAttribute("Accuracy")); } else { newMove.Accuracy = 1; }

                moves.Add(newMove.ID, newMove);
            }

        }
        reader.Close();

        Debug.WriteLine(" Completed");
    }

    public static Move getMove(string ID)
    {
        //add checks that ID exists
        if (moves.ContainsKey(ID))
        {
            return moves[ID];
        }
        else
        {
            return new Move();
        }
    }
}
# endregion

public class SaveClass
{
    inventoryItem _item = new inventoryItem();
    inventoryItem _pic = new Picture();

    public SaveClass()
    {
        _item.onUse += new UseItem(_item_onUse);
        _item.onAdd += new UseItem(_item_onAdd);

        _pic.onAdd += new UseItem(_pic_onAdd);
        _pic.onUse += new UseItem(_pic_onUse);
    }

    void _pic_onUse(inventoryItem item)
    {
        //parentScreen.pictures.Draw(img);

        Picture pic = item as Picture;

        //parentScreen.pictures.Close();
    }

    void _pic_onAdd(inventoryItem item)
    {
        Picture pic = item as Picture;

        if (!Inventory.gallery.ContainsValue(pic.img))
        {
            Inventory.gallery.Add(Inventory.gallery.Count, pic.img);
        }
    }

    void _item_onAdd(inventoryItem item)
    {
        Inventory.items.Add(Inventory.items.Count.ToString(), item);
    }

    void _item_onUse(inventoryItem item)
    {

    }
}
