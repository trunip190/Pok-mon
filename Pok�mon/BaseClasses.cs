using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PokémonGame
{
    public class PokemonBase
    {
        # region variables
        public bool Gender; //0 is Female, 1 is Male
        public int ID;
        public int variant = 0;
        public long Experience = 0;
        public int baseExperience = 1;
        public byte[] Sprite;
        public string Name;
        public string Type1;
        public string Type2;
        public int Level;
        public int catchRate = 1;

        //individual moves need replacing by Move[]
        public Move[] moves = new Move[4];

        # region Stats
        public Stat HP = new Stat();
        public double HPCurrent = 1;
        public Stat Attack = new Stat();
        public Stat Defense = new Stat();
        public Stat SpAtt = new Stat();
        public Stat SpDef = new Stat();
        public Stat Speed = new Stat();
        # endregion

        public event Caught meCaught;
        # endregion

        public PokemonBase()
        {
            for (int i = 0; i < moves.Length; i++)
            {
                moves[i] = new Move();
            }
        }
    }

    public class Stat
    {
        public int BaseStat = 0;
        public int IV = 0;
        public int EV = 0;
        public int NetStat = 0;

        public string ToString(string Name)
        {
            string result = "";

            result = "<Stat Name=\"" + Name + "\" " + "Base=\"" + BaseStat.ToString() + "\" IV=\"" + IV.ToString() + "\" EV=\"" + EV.ToString() + "\" >" + NetStat.ToString() + "</Stat>";

            return result;
        }

        public int ToInt()
        {
            return NetStat;
        }
    }

    public class Move
    {
        # region definition of move
        public string ID;
        public string Name = "";
        public string Type;
        public string Category;
        public int Power;
        public double Accuracy;
        public int PPMax;
        public int PPCurrent;
        public int TMHM;
        public string Effect;
        public int Probability;
        public int Speed;
        # endregion

        public override string ToString()
        {
            string result = "<Move ID=\"" + ID + "\" PPMax=\"" + PPMax.ToString() + "\" PPCurrent=\"" + PPCurrent.ToString() + "\" />";

            return result;
        }
    }

    public class ImageBool
    {
        public Image img;
        public bool unlocked = false;

        public ImageBool(Image i, bool u)
        {
            img = i;
            unlocked = u;
        }
    }

    # region inventoryItem classes
    # region xml inclusions
    [XmlInclude(typeof(TMItem))]
    [XmlInclude(typeof(Berry))]
    [XmlInclude(typeof(RecoveryItem))]
    [XmlInclude(typeof(Pokeball))]
    [XmlInclude(typeof(MainItem))]
    [XmlInclude(typeof(HoldItem))]
    [XmlInclude(typeof(EvolutionItem))]
    [XmlInclude(typeof(KeyItem))]
    [XmlInclude(typeof(Picture))]
    # endregion
    public class inventoryItem : ICloneable
    {
        # region declarations
        public string Name = "Unnamed";
        public string Description = "Enter description here";
        public string NameNet = "";
        public string ID = "0";
        public int count = 0;
        public int type = 0;
        public double Power = 1;
        # endregion

        # region events and handlers
        public event UseItem onUse;
        public event UseItem onAdd;

        protected virtual void onUsing()
        {
            UseItem handler = onUse;
            if (handler != null)
            {
                handler(this);
            }
        }

        protected virtual void onAdding()
        {
            UseItem handler = onAdd;
            if (handler != null)
            {
                handler(this);
            }
        }

        # endregion

        public inventoryItem() { }

        public virtual string ItemType() { return "Generic"; }
        public virtual bool isInventoryItem() { return true; }

        # region usage/clearing
        public virtual string Use()
        {
            string result = (Name + " used.");
            decreaseCount(1);

            onUsing();

            return result;
        }

        public virtual string addItem()
        {
            string result = ("Received " + getNameNet() + ".");

            onAdding();

            return result;
        }
        # endregion

        # region +/-
        public void increaseCount(int amount)
        {
            for (int i = 0; i < amount && count < 99; i++)
            {
                count++;
            }
            NameNet = Name + " x" + count.ToString();
        }

        public void decreaseCount(int amount)
        {
            for (int i = 0; i < amount && count > 0; i++)
            {
                count--;
            }
            NameNet = Name + " x" + count.ToString();
        }
        # endregion

        public string getNameNet()
        {
            return Name + " x" + count.ToString();
        }

        public override string ToString()
        {
            return getNameNet();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class TMItem : inventoryItem { public override string ItemType() { return "TMItem"; } }

    public class Berry : inventoryItem { public override string ItemType() { return "Berry"; } }

    public class RecoveryItem : inventoryItem
    {
        # region additional declarations
        bool targetAll = false;
        int PPrestore = 0;
        int HPrestore = 0;
        int StatusHealType = 0;
        bool Revive = false;
        # endregion

        public override string ItemType() { return "RecoveryItem"; }

        public override string Use()
        {
            //TODO needs writing.

            //Single or All pokemon in party.

            //HP restore
            //PP restore
            //Status restore
            //Revive

            return base.Use();
        }
    }

    public class Pokeball : inventoryItem { public override string ItemType() { return "Pokeball"; } }

    public class MainItem : inventoryItem { public override string ItemType() { return "MainItem"; } }

    public class HoldItem : MainItem { }

    public class EvolutionItem : MainItem { }

    public class KeyItem : inventoryItem { public override string ItemType() { return "KeyItem"; } }

    public class Picture : inventoryItem
    {
        public Image img = new Bitmap(32, 32);

        public Picture() { }

        public Picture(string location)
        {
            string filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            filename += location;

            if (File.Exists(filename))
            {
                img = Image.FromFile(filename);
            }
            else Debug.WriteLine(filename);
        }

        public override string ItemType() { return "Picture"; }

        public override string Use()
        {
            string result = "You look at the picture.";

            onUsing();

            return result;
        }

        public override string addItem()
        {
            string result = "You received a picture!";

            onAdding();

            return result;
        }
    }
    # endregion

    # region mapping
    public class mapPos
    {
        //public int block = 2; //0 is vacant, 1 is grass, 2 blocks passage, 3 is cuttable, 4 is swimmable, 5 is rock climb, 6 is person
        public string name = "raw";
        public List<SpriteBase> spriteClassList = new List<SpriteBase>();
    }

    [XmlRoot]
    public class map
    {
        # region declarations
        public string name = "MapName";
        public int width = 10;
        public int height = 10;

        public int StartX = 2;
        public int StartY = 2;

        public List<SpriteBase> floor = new List<SpriteBase>();     //floor of map
        public List<SpriteBase> baseList = new List<SpriteBase>();  //Underfoot items
        public List<SpriteBase> midList = new List<SpriteBase>();   //Player-Level items
        public List<SpriteBase> topList = new List<SpriteBase>();   //Overhead items

        public List<SpriteBase> changedSprites = new List<SpriteBase>();

        # endregion

        # region delegates/events
        public delegate void valueChanged();

        # region events
        # region map events
        public event valueChanged floorChanged;
        public event valueChanged baseChanged;
        public event valueChanged midChanged;
        public event valueChanged topChanged;
        # endregion

        public event transition LoadMap;

        # region SpritePerson
        public event move spMoving;
        public event talk greet;
        public event talk farewell;
        public event UseItem ItemAdd;
        public event pokeEvent Fighting;
        # endregion
        # endregion

        # region map changed
        private void onFloorChanged()
        {
            valueChanged handler = floorChanged;
            if (handler != null)
            {
                handler();
            }
        }

        private void onBaseChanged()
        {
            valueChanged handler = baseChanged;
            if (handler != null)
            {
                handler();
            }
        }

        private void onMidChanged()
        {
            valueChanged handler = midChanged;
            if (handler != null)
            {
                handler();
            }
        }

        private void onTopChanged()
        {
            valueChanged handler = topChanged;
            if (handler != null)
            {
                handler();
            }
        }
        # endregion

        # region SpritePerson
        private void onMoving(SpriteLiving sp, int x, int y)
        {
            move handler = spMoving;
            if (handler != null)
            {
                handler(sp, x, y);
            }
        }

        protected void onGreet(List<convoText> messages)
        {
            talk handler = greet;
            if (handler != null)
            {
                handler(messages);
            }
        }

        protected void onFarewell(List<convoText> messages)
        {
            talk handler = farewell;
            if (handler != null)
            {
                handler(messages);
            }
        }

        protected void onItemAdd(inventoryItem item)
        {
            UseItem handler = ItemAdd;
            if (handler != null)
            {
                handler(item);
            }
        }

        protected void onFighting()
        {
            pokeEvent handler = Fighting;
            if (handler != null)
            {
                handler();
            }
        }
        # endregion

        private void onLoadMap(string s, int x, int y)
        {
            transition handler = LoadMap;
            if (handler != null)
            {
                handler(s, x, y);
            }
        }
        # endregion

        public map()
        {

        }

        public void loadSpriteBases()
        {
            loadEdits();

            List<SpriteBase> temp = floor;
            for (int i = 0; i < 4; i++)
            {
                # region set level to work on
                switch (i)
                {
                    case 1:
                        temp = baseList;
                        break;

                    case 2:
                        temp = midList;
                        break;

                    case 3:
                        temp = topList;
                        break;

                    default:
                        break;
                }
                # endregion

                foreach (SpriteBase sb in temp)
                {
                    sb.Initialise();
                    sb.SpriteChanged += new SpriteBase.change(sb_SpriteChanged);
                    switch (sb.GetType().ToString())
                    {
                        # region SpriteBase
                        case "PokémonGame.SpriteBase":

                            break;
                        # endregion

                        # region SpriteDoor
                        case "PokémonGame.SpriteDoor":
                            SpriteDoor sd = sb as SpriteDoor;
                            if (sd.file != "")
                            {
                                sd.LoadMap += new transition(sd_LoadMap);
                            }
                            break;
                        # endregion

                        # region SpriteTerrain
                        case "PokémonGame.SpriteTerrain":
                            SpriteTerrain st = sb as SpriteTerrain;

                            break;
                        # endregion

                        # region SpritePerson
                        case "PokémonGame.SpritePerson":
                            SpritePerson sp = sb as SpritePerson;

                            # region set level to redraw
                            if (i == 0) { sp.Moving += new move(sp_MovingFloor); }
                            if (i == 1) { sp.Moving += new move(sp_MovingBase); }
                            if (i == 2) { sp.Moving += new move(sp_MovingMid); }
                            if (i == 3) { sp.Moving += new move(sp_MovingTop); }
                            # endregion

                            sp.greet += new talk(onGreet);
                            sp.farewell += new talk(onFarewell);
                            sp.ItemAdd += new UseItem(onItemAdd);
                            sp.Fighting += new pokeEvent(onFighting);
                            break;
                        # endregion
                    }
                }
            }
        }

        protected void loadEdits()
        {
            foreach (SpriteBase sb in changedSprites)
            {
                # region set level to check
                List<SpriteBase> tempList = floor;
                if (sb.ID.StartsWith("ba")) { tempList = baseList; }
                if (sb.ID.StartsWith("mi")) { tempList = midList; }
                if (sb.ID.StartsWith("to")) { tempList = topList; }
                # endregion

                int i = tempList.FindIndex(o => o.ID == sb.ID);

                tempList[i] = sb;
            }
        }

        void sb_SpriteChanged(SpriteBase sb)
        {
            int i = changedSprites.FindIndex(o => o.ID == sb.ID);

            Debug.WriteLine("Sprite at {0}.", i);

            if (i == -1)
            {
                changedSprites.Add(sb);
            }
            else
            {
                changedSprites[i] = sb.Clone() as SpriteBase;
            }
        }

        void sd_LoadMap(string f, int x, int y)
        {
            onLoadMap(f, x, y);
        }

        # region Person moved triggers
        void sp_MovingFloor(SpriteLiving sb, int x, int y)
        {
            onFloorChanged();

            onMoving(sb, x, y);
        }

        void sp_MovingBase(SpriteLiving sb, int x, int y)
        {
            onBaseChanged();

            onMoving(sb, x, y);
        }

        void sp_MovingMid(SpriteLiving sb, int x, int y)
        {
            onMidChanged();

            onMoving(sb, x, y);
        }

        void sp_MovingTop(SpriteLiving sb, int x, int y)
        {
            onTopChanged();

            onMoving(sb, x, y);
        }
        # endregion

        public int getIndex(int level, int xpos, int ypos)
        {
            # region List<SpriteBase> temp = level
            List<SpriteBase> temp;
            switch (level)
            {
                case 0:
                    temp = floor;
                    break;

                case 1:
                    temp = baseList;
                    break;

                case 2:
                    temp = midList;
                    break;

                case 3:
                    temp = topList;
                    break;

                default:
                    Debug.WriteLine("Erroneous level {0}", level);
                    return -1;
            }
            # endregion

            int result = -1;
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].xPos == xpos && temp[i].yPos == ypos)   //if there is a match
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public SpriteBase getSprite(int level, int xpos, int ypos)
        {
            # region List<SpriteBase> temp = level
            List<SpriteBase> temp;
            switch (level)
            {
                case 0:
                    temp = floor;
                    break;

                case 1:
                    temp = baseList;
                    break;

                case 2:
                    temp = midList;
                    break;

                case 3:
                    temp = topList;
                    break;

                default:
                    Debug.WriteLine("Erroneous level {0}", level);
                    return null;
            }
            # endregion

            SpriteBase sb = new SpriteBase();

            if (level >= 0)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].xPos == xpos && temp[i].yPos == ypos)   //if there is a match
                    {
                        sb = temp[i];
                        break;
                    }
                }
            }

            return sb;
        }
    }
    # endregion

    # region spriteBase
    public class SpriteFloor
    {
        [XmlIgnore]
        public Image img = null;

        public int xPos = 0;
        public int yPos = 0;

        public string sprite = "";

        public Image toImage()
        {
            if (img == null)
            {
                if (File.Exists(sprite))
                {
                    img = Image.FromFile(sprite);
                }
                else
                {
                    img = new Bitmap(32, 32);
                }
            }

            return img;
        }
    }

    # region xml parts
    [XmlInclude(typeof(SpritePerson))]
    [XmlInclude(typeof(SpriteItem))]
    [XmlInclude(typeof(SpriteDoor))]
    [XmlInclude(typeof(SpritePokemon))]
    [XmlInclude(typeof(SpriteTerrain))]
    # endregion
    public class SpriteBase : ICloneable
    {
        # region base attributes
        [XmlIgnore]
        public Image img = null;

        public string ID = "ID0001";

        public int Type = 0;
        public int xPos
        {
            get
            { return xNet / 32; }
            set
            { xNet = value * 32; }
        }
        public int yPos
        {
            get
            { return yNet / 32; }
            set
            { yNet = value * 32; }
        }
        public int xStart = 1;  //initial xPos
        public int yStart = 1;  //initial yPos
        public string spriteSheet = "";
        public bool visible = true;
        public int block = 0;   //walk, bike, impassable, surf
        public string Name = "";

        public int width = 32;
        public int height = 32;
        public bool flip = false;
        public List<variable> requirements = new List<variable>();

        public bool save = false;

        public int xTarg = 0;
        public int xNet = 0;

        public int yTarg = 0;
        public int yNet = 0;

        # region delegates, events, handlers
        public delegate void change(SpriteBase sb);
        public event change SpriteChanged;

        public delegate void gameEvent();

        protected void onChanged(SpriteBase sb)
        {
            change handler = SpriteChanged;
            if (handler != null)
            {
                handler(sb);
            }
        }
        # endregion
        # endregion

        public SpriteBase()
        {
            xNet = xPos * 32;
            yNet = yPos * 32;
        }

        public virtual void Initialise() { }

        # region player events
        public virtual void onTick()
        {

        }

        public virtual void onActivate() { }

        public virtual void onStep() { }
        # endregion

        # region innate events
        public virtual Image toImage()
        {
            if (img == null)
            {
                string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string path = spriteSheet.Replace("%PATH%", dir);
                img = ImageLoad.FromSheet(path, xStart, yStart, width, height);
            }

            Image temp = img.Clone() as Image;

            if (flip)
            {
                temp.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }

            return temp;
        }
        # endregion

        public virtual void setImage(string s)
        {
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = s.Replace(dir, "%PATH%");

            spriteSheet = path;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SpriteLiving : SpriteBase
    {
        public List<string> movement = new List<string>();

        public int TargX = 0;
        public int TargY = 0;

        # region movement variables
        protected int tick = 0;
        protected bool moving = false;
        public int xOri = 0;
        public int yOri = 0;
        public int dist = 0;
        public int transit = 0;    //0 walking, 1 biking, 2 walls, 3 surfing/swimming.
        # endregion

        # region events

        public event talk greet;
        public event talk farewell;
        public event UseItem ItemAdd;
        public event pokeEvent Fighting;
        public event pokeEvent Defeated;

        public event move Moving;

        protected void onGreet(List<convoText> messages)
        {
            talk handler = greet;
            if (handler != null)
            {
                handler(messages);
            }
        }

        protected void onFarewell(List<convoText> messages)
        {
            talk handler = farewell;
            if (handler != null)
            {
                handler(messages);
            }
        }

        protected void onItemAdd(inventoryItem item)
        {
            UseItem handler = ItemAdd;
            if (handler != null)
            {
                handler(item);
            }
        }

        protected void onFighting()
        {
            pokeEvent handler = Fighting;
            if (handler != null)
            {
                handler();
            }
        }

        protected void onMoving(int x, int y)
        {
            move handler = Moving;
            if (handler != null)
            {
                handler(this, x, y);
            }
        }
        # endregion

        public virtual void onDefeat()
        {
            pokeEvent handler = Defeated;
            if (handler != null)
            {
                handler();
            }

            onChanged(this);
        }
    }

    public class SpritePerson : SpriteLiving
    {
        # region variables
        public List<convoText> ConvoGreet = new List<convoText>();
        public List<convoText> ConvoBattleStart = new List<convoText>();
        public List<convoText> ConvoBattleEnd = new List<convoText>();
        public List<convoText> ConvoFarewell = new List<convoText>();

        public List<inventoryItem> Items = new List<inventoryItem>();

        # region battle variables
        public List<PokemonBase> pokeGroup = new List<PokemonBase>(6);
        public bool fight = false;
        # endregion

        # region image variables
        public int dirLook = 1;                     //up, down, left, right
        public int moveAction = 0;                  //standing, walking, running, cycling, surfing
        public int variant = 0;                     //leftstep, rightstep

        [XmlIgnore]
        public Image[,] sprites = new Image[3, 4]; //[variant(move1,move2,stop1/dirLook]

        Rectangle cropArea = new Rectangle();
        # endregion
        # endregion

        # region methods
        public SpritePerson()
        {
            //TODO need to work out how to set these on initialisation;
            xOri = 2;
            yOri = 2;
            dist = 0;

            xNet = xPos * 32;
            yNet = yPos * 32;

            //TODO make this choose the right sprite
            //cropArea = new Rectangle(0, 2, width, height);
            cropArea = new Rectangle(xStart, yStart, width, height);
        }

        public override void Initialise()
        {
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = spriteSheet.Replace("%PATH%", dir);

            if (File.Exists(path))
            {
                Image sheet = Image.FromFile(path);
                //Image sheet = Properties.Resources.heromfoverworld;

                sprites = SpriteLoader.loadsprites(sheet, cropArea, width, height, 3, 4);
                //sprites = SpriteLoader.loadspritesA(sheet, cropArea, width, height, 4, 5);
            }
            else
            {
                Debug.WriteLine(path);
            }
        }

        public override Image toImage()
        {
            Image temp;

            if (sprites[variant, dirLook] != null)
            {
                temp = sprites[variant, dirLook];
            }
            else
            {
                temp = base.toImage();
            }

            return temp;
        }

        public Image SpriteSheet()
        {
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = spriteSheet.Replace("%PATH%", dir);

            if (!File.Exists(path))
            {
                return null;
            }

            Image result = new Bitmap(3 * width, 4 * width);

            using (Graphics g = Graphics.FromImage(result))
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        g.DrawImage(sprites[x, y], x * width, y * width);
                    }
                }
            }

            return result;
        }

        public override void onActivate()
        {
            Debug.WriteLine("onActivate()");
            # region greet
            onGreet(ConvoGreet);
            # endregion

            //Heal

            # region battle
            if (fight)
            {
                onFighting();
            }
            # endregion

            # region farewell text
            onFarewell(ConvoFarewell);
            # endregion

            onChanged(this);
        }

        public override void onTick()
        {
            if (!moving)
            {
                tick++;

                //only check every 32 cycles
                if (tick > 8)
                {
                    tryMove();

                    tick = 0;
                }
            }
            else
            {
                tick++;

                UpdateMove();

                //16 ticks to get to destination
                if (tick == 16)
                {
                    moving = false;
                    tick = 0;
                }
            }
        }

        private void UpdateMove()
        {
            //x distance to move
            int x = xTarg - (xNet);
            int y = yTarg - (yNet);

            int nTick = (16 - tick) + 1;

            //Move player 1 tick.
            x = x / nTick;
            y = y / nTick;

            xNet += x;
            yNet += y;

            # region image updating
            switch (tick)
            {
                case 0:     //First step.
                    variant = 1;
                    break;

                case 4:     //Foot down.
                    variant = 0;
                    break;


                case 8:     //Second step.
                    variant = 2;
                    break;

                case 12:    //Foot down.
                    variant = 0;
                    break;

                case 16:    //Stop walking.
                    variant = 0;
                    moveAction = 0;
                    break;
            }
            # endregion
        }

        private void tryMove()
        {
            Random r = new Random();
            int c = r.Next(0, 100);
            if (c > 50) //50% chance of moving
            {
                # region choose next position
                int dir = r.Next(0, 4);
                int x = 0;
                int y = 0;

                # region switch direction
                switch (dir)
                {
                    case 0:
                        y = -1;
                        break;

                    case 1:
                        y = 1;
                        break;

                    case 2:
                        x = -1;
                        break;

                    case 3:
                        x = 1;
                        break;

                    default:

                        break;
                }
                # endregion

                int i = x;
                int j = y;

                x += xPos;
                y += yPos;
                # endregion

                # region calc distance
                int xDis = x - xOri;
                int yDis = y - yOri;

                if (xDis < 0) { xDis *= -1; }
                if (yDis < 0) { yDis *= -1; }
                # endregion

                if (xDis <= dist && yDis <= dist)
                {
                    onMoving(x, y);

                    dirLook = dir;

                    variant = 1;
                    moving = true;
                }
            }
        }

        private void addItem()
        {
            foreach (inventoryItem i in Items)
            {
                i.addItem();
            }

            Items.Clear();
        }

        public void MoveNext()
        {
            if (movement.Count > 0)
            {
                # region direction switch
                switch (movement[0])
                {
                    case "N":
                        TargY = yPos - 1;
                        TargX = xPos;
                        break;

                    case "S":
                        TargY = yPos + 1;
                        TargX = xPos;
                        break;

                    case "E":
                        TargY = yPos;
                        TargX = xPos + 1;
                        break;

                    case "W":
                        TargY = yPos;
                        TargX = xPos - 1;
                        break;
                }
                # endregion

                //remove action from list
                movement.RemoveAt(0);
            }
        }
        # endregion
    }

    public class SpriteItem : SpriteBase
    {
        public List<inventoryItem> Items = new List<inventoryItem>();

        public override void onActivate()
        {
            if (Items.Count > 0)
            {
                addItem();
            }
        }

        private void addItem()
        {
            foreach (inventoryItem i in Items)
            {
                i.addItem();
            }

            Items.Clear();
        }
    }

    public class SpriteDoor : SpriteBase
    {
        public int newX = 0;
        public int newY = 0;

        public string file = "";

        public event transition LoadMap;

        protected void onLoadMap()
        {
            transition handler = LoadMap;
            if (handler != null)
            {
                handler(file, newX, newY);
            }
        }

        public override void onStep()
        {
            Debug.WriteLine("onStep()");
            if (file != "")
            {
                onLoadMap();
            }
        }

        public override void onActivate()
        {
            Debug.WriteLine("onActive()");
            if (file != "")
            {
                onLoadMap();
            }
        }
    }

    public class SpritePokemon : SpriteLiving { }

    public class SpriteTerrain : SpriteBase
    {
        public int xAnim = 0;
        public int yAnim = 0;

        public override Image toImage()
        {
            //return PokemonMapEditor.Properties.Resources.N;

            return base.toImage();
        }

        public override void onStep()
        {
        }
    }

    public class SpriteShop : SpriteBase
    {
        public event gameEvent Shopping;
        public List<inventoryItem> items = new List<inventoryItem>();

        protected void onShop()
        {
            gameEvent handler = Shopping;
            if (handler != null)
            {
                handler();
            }
        }

        public override void onActivate()
        {

        }
    }
    # endregion

    public class xy
    {
        int _x = 0;
        int _y = 0;

        public xy(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }

    public class GMVB
    {
        //TODO may be redundant and should move into savegame.

        private static Dictionary<string, int> variables = new Dictionary<string, int>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Returns the value of the ame variable. -1 for error</returns>
        public static int get(string s)
        {
            if (s == null)  //doesn't have any requirements.
                return 0;

            else if (variables.ContainsKey(s))  //variable in the system.
                return variables[s];

            else return -1; //variable not registered yet.
        }

        public static bool set(string s, int i)
        {
            bool contains = false;

            if (variables.ContainsKey(s))
            {
                contains = true;
                variables[s] = i;
            }
            else variables.Add(s, i);

            return contains;
        }

        public static bool check(List<variable> vars)
        {
            bool result = true;

            foreach (variable v in vars)
            {
                if (get(v.name) < v.val)
                {
                    result = false;
                }
            }

            return result;
        }

        public static void display()
        {
            foreach (KeyValuePair<string, int> k in variables)
                Debug.WriteLine("{0} = {1}", k.Key, k.Value);
        }

        public static List<variable> export()
        {
            List<variable> result = new List<variable>();

            foreach (KeyValuePair<string, int> k in variables)
            {
                variable var = new variable
                {
                    name = k.Key,
                    val = k.Value
                };
            }

            return result;
        }

        public static void import(List<variable> list)
        {
            variables.Clear();
            foreach (variable v in list)
            {
                variables.Add(v.name, v.val);
            }
        }
    }

    public class convoText
    {
        //TODO add in choices.
        # region attributes
        public string Text = "";    //Text to display.

        public List<variable> requirements = new List<variable>();
        public List<variable> consequences = new List<variable>();

        public List<convoText> choices = new List<convoText>();

        public string type = "Greeting";
        public inventoryItem item = null;

        # region old method of variables
        [XmlIgnore]
        public string req = "";     //variable to check.
        [XmlIgnore]
        public int val = -1;        //necessary value.

        [XmlIgnore]
        public string ConReq = "null"; //the variable to update.
        [XmlIgnore]
        public int ConVal = -1;    //what to update to.
        # endregion
        # endregion

        public convoText()
        {
            Text = "blank";

            req = "null";
            val = -1;

            ConReq = "null";
            ConVal = -1;
        }

        public convoText(string t, string r, string v, string cR, string cV)
        {
            Text = t;

            if (r == null) req = "null"; else req = r;
            int.TryParse(v, out val);

            ConReq = cR;
            int.TryParse(cV, out ConVal);
        }

        public convoText(string t, List<variable> r, List<variable> c)
        {
            Text = t;
            requirements = r;
            consequences = c;
        }

        public bool Valid()
        {
            bool result = true;

            foreach (variable v in requirements)
            {
                if (GMVB.get(v.name) != v.val)
                {
                    result = false;
                }
            }

            return result;
        }

        public string getText()
        {
            if (ConReq != "null" && ConReq != null)
            {
                GMVB.set(ConReq, ConVal);
            }

            return Text;
        }

        public override string ToString()
        {
            bool valid = true;

            foreach (variable v in requirements)
            {
                if (GMVB.get(v.name) != v.val)
                {
                    valid = false;
                }
            }

            if (!valid)
            {
                return null;
            }

            foreach (variable v in consequences)
            {
                GMVB.set(v.name, v.val);
            }

            return Text;
        }

        /// <summary>
        /// Debug.Writeline convoText details
        /// </summary>
        public void output()
        {
            Debug.WriteLine("{0};req:{1};{5}/{2};conreq:{3};conval:{4}", Text, req, val, ConReq, ConVal, GMVB.get(req));
        }
    }

    public class variable
    {
        public string name = "";
        public int val = -1;
        public string operand = "==";

        public bool check(int gv)
        {
            bool result = false;

            switch (operand)
            {
                case "==":
                    if (gv == val) { result = true; }
                    break;

                case ">":
                    if (gv > val) { result = true; }
                    break;

                case "<":
                    if (gv < val) { result = true; }
                    break;

                case "!=":
                    if (gv != val) { result = true; }
                    break;

                case ">=":
                    if (gv >= val) { result = true; }
                    break;

                case "<=":
                    if (gv <= val) { result = true; }
                    break;

                default:
                    Debug.WriteLine("variable[{0} {1}].check() - unrecognised operand {2}", name, val, operand);
                    break;
            }

            return result;
        }
    }

    public static class ImageLoad
    {
        /// <summary>
        /// stores spritesheets for quicker loading
        /// </summary>
        private static Dictionary<string, Image> images = new Dictionary<string, Image>();

        /// <summary>
        /// stores individual sprites for quicker loading
        /// </summary>
        private static Dictionary<string, Image> sprites = new Dictionary<string, Image>();

        public static Image FromSheet(string sheet, int x, int y, int width, int height)
        {
            string sName = sheet + "," + x + "," + y + "," + width + "," + height;

            if (sprites.ContainsKey(sName))
            {
                return sprites[sName];
            }
            else
            {
                if (!File.Exists(sheet))
                {
                    return new Bitmap(width, height);
                }

                Bitmap bmp = get(sheet) as Bitmap;

                Rectangle rect = new Rectangle(x, y, width, height);
                bmp = bmp.Clone(rect, System.Drawing.Imaging.PixelFormat.DontCare);

                sprites.Add(sName, bmp);

                return bmp;
            }
        }

        public static Image get(string sheet)
        {
            string s = sheet;
            if (!File.Exists(s))
            {
                s = "";
            }

            if (!images.ContainsKey(s))
            {
                images.Add(s, Image.FromFile(s));
            }


            return images[sheet];
        }
    }

    public class SpriteLoader
    {
        public static Image cropSprite(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            //Debug.WriteLine("   {0}, {1}", cropArea.X + cropArea.Width, cropArea.Y + cropArea.Height);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);

            return (Image)(bmpCrop);
        }

        public static Image[,] loadsprites(Image Spritesheet, Rectangle cropArea, int xInterval, int yInterval, int xIter, int yIter)
        {
            //Set size of array ( TODO - gives WHOLE IMAGE array)
            int SizeX = Spritesheet.Width / xInterval;
            int SizeY = Spritesheet.Height / yInterval;
            //Image[,] array = new Image[SizeX, SizeY]; //create 10x10 (160px by 160px)
            Image[,] array = new Image[xIter, yIter]; //create 10x10 (160px by 160px)

            int origX = cropArea.X;

            //load areas
            for (int y = 0; y < yIter; y++)
            {
                for (int x = 0; x < xIter; x++)
                {
                    array[x, y] = cropSprite(Spritesheet, cropArea);
                    cropArea.X = (cropArea.X + xInterval);
                }
                cropArea.X = origX;
                cropArea.Y = cropArea.Y + yInterval;
            }
            return array;
        }

        public static Image[,][] loadspritesA(Image Spritesheet, Rectangle cropArea, int xInterval, int yInterval, int iIter, int jIter)
        {   //TODO check over this - just copied and pasted
            Image[,][] array = new Image[5, 4][];
            int origX = cropArea.X;
            array[0, 0] = new Image[8];

            for (int j = 0; j < jIter; j++)
            {
                for (int i = 0; i < iIter; i++)
                {
                    array[j, i] = loadspritesI(Spritesheet, cropArea, 32, 8);

                    cropArea.X = (cropArea.X + xInterval);
                }
                cropArea.X = origX;
                cropArea.Y = cropArea.Y + yInterval;
            }

            return array;
        }

        public static Image[] loadspritesI(Image Spritesheet, Rectangle cropArea, int xInterval, int iIter)
        {
            //TODO check over this - just copied and pasted
            //TODO Very slow!
            Image[] array = new Image[iIter];
            int origX = cropArea.X;

            for (int i = 0; i < iIter; i++)
            {
                array[i] = cropSprite(Spritesheet, cropArea);
                cropArea.X = (cropArea.X + xInterval);
            }

            return array;
        }

        public static Image singleSprite(string SheetName, int x, int y)
        {
            Rectangle cropPart = new Rectangle(x * 32, y * 32, 32, 32);

            //Image spriteSheet = Properties.Resources.tileset2;
            Image spriteSheet = Image.FromFile(SheetName);

            return cropSprite(spriteSheet, cropPart);
        }

        public static Image LoadLayer(List<SpriteBase> sprites, Rectangle rect, int tick)
        {
            Bitmap result = new Bitmap(rect.Width, rect.Height);
            
            using (Graphics g = Graphics.FromImage(result))
            {
                foreach (SpriteBase sb in sprites)
                {
                    //TODO find some way of making people the right size
                    //TODO limit based on sprites in bounding box

                    Rectangle tRect = new Rectangle(sb.xPos * tick, sb.yPos * tick, tick, tick);

                    if (sb.GetType() == typeof(SpritePerson))
                    {
                        tRect = new Rectangle((sb.xPos * tick) - (tick / 4), (sb.yPos * tick) - (tick / 4), tick, tick);
                    }

                    g.DrawImage(sb.toImage(), tRect);
                }
            }

            return result;
        }
    }

    # region delegates
    public delegate void Caught();
    public delegate void UseItem(inventoryItem item);
    public delegate void pokeEvent();
    public delegate void move(SpriteLiving sb, int x, int y);
    public delegate void transition(string f, int x, int y);
    public delegate void talk(List<convoText> messages);
    # endregion
}
