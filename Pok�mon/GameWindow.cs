using PokémonGame.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Windows.Input;

namespace PokémonGame
{
    public partial class GameWindow : Form
    {
        #region load classes and values
        public SaveFile save1 = new SaveFile();

        //public Pokeball[] pokeballs = new Pokeball[250]; //TODO remove pokeballs.

        public int ticks = 32; //basic unit of measurement - 32pixels
        public List<String> TextQueue = new List<string>();

        int fps = 0;
        Stopwatch swFPS = new Stopwatch();
        string mapLoc = "";

        List<double> hits = new List<double>(5);

        # region maps
        map thisMap = new map();
        Image imgFloor = new Bitmap(32, 32);
        Image imgBase = new Bitmap(32, 32);
        Image imgMid = new Bitmap(32, 32);
        Image imgTop = new Bitmap(32, 32);
        # endregion

        # region Lists
        public List<PokeSprite> activePokemon = new List<PokeSprite>(); //pokemon (plural?) to start battle with
        public List<Pokemon> wildPokemonList = new List<Pokemon>();     //pokemon that can be encountesave1.Red in that region
        public SpriteBase activeSprite = new SpriteBase();       //currently encountesave1.Red pokemon.
        public List<Pokemon> EnemyParty = new List<Pokemon>();

        # region spritetype lists
        List<SpritePerson> people = new List<SpritePerson>();
        # endregion
        # endregion

        # region Forms etc.
        public BattleForm battleWindow;
        public EncounterForm encounterForm;
        public PartyPokemon partyWindow = new PartyPokemon();
        public Backpack bPack = new Backpack();
        public ScreenBox Screen1;
        public PictureSingle pictures;
        # endregion

        # region movement variables
        int movePos = 0;    //Stores how many times player has moved in this loop
        int DirMove = 0;    //Stores current movement direction
        int nextMove = 0;   //Stores next directional input for smooth movement
        int DirLock = 0;    //Locks inputs while player moves
        int DirLook = 0;    //1Up, 2Down, 3Left, 4Right
        int moveType = 0;   //Walking, Biking, through walls, Surfing
        bool fast = false;
        bool transit = false;

        //temp
        int StepCounter = 0;

        public bool screenLock = false; //Stops player moving.
        # endregion

        # region position variables
        /// <summary>
        /// player's x position in pixels
        /// </summary>
        int xPC = 64;
        /// <summary>
        /// player's y position in pixels
        /// </summary>
        int yPC = 64;
        int zPC = 0;

        int xNet = 0;
        int yNet = 0;

        int xOffset = 0;
        int yOffset = 0;

        int xmod = 0;
        int ymod = 0;
        # endregion
        #endregion

        public GameWindow()
        {
            #region initialise form
            InitializeComponent();

            # region move to pokemon lookup list
            this.pokedex1TableAdapter1.Fill(this.pokedexDataSet1.pokedex1);
            # endregion

            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyReader_KeyDown);

            # region map changed events
            thisMap.floorChanged += new map.valueChanged(thisMap_floorChanged);
            thisMap.baseChanged += new map.valueChanged(thisMap_baseChanged);
            thisMap.midChanged += new map.valueChanged(thisMap_midChanged);
            thisMap.topChanged += new map.valueChanged(thisMap_topChanged);
            # endregion

            timerTick.Start();
            timerTick.Interval = 25;

            timerDraw.Start();
            timerDraw.Interval = 25;

            # region add wild pokemon
            wildPokemonList.Add(createPokemon(16, 4));
            wildPokemonList.Add(createPokemon(19, 3));
            # endregion

            save1.Red.xPos = xPC;
            save1.Red.yPos = yPC;

            # region fill party with Pikachus
            for (int p = 0; p < 6; p++)
            {
                Pokemon temp = PokemonLookup.get(25);
                temp.Level = 5;
                temp.CalcStats();
                temp.HPCurrent = temp.HP.NetStat;
                temp.moves[0] = MoveList.getMove("84");
                temp.moves[1] = MoveList.getMove("45");
                temp.onCatch();
                //p = 6;
            }
            # endregion

            # region player pos
            int x = (toTicks(xPC) * ticks);
            int y = (toTicks(yPC) * ticks);

            setPos(x, y);
            # endregion

            # region set up ScreenBox
            Screen1 = new ScreenBox(this);
            Screen1.Top = panel1.Bottom - Screen1.Height;
            Screen1.Width = panel1.Width;
            Screen1.VisibleChanged += new EventHandler(Screen1_VisibleChanged);
            # endregion

            pictures = new PictureSingle(this);
            #endregion
            swFPS.Start();
        }

        void Screen1_VisibleChanged(object sender, EventArgs e)
        {
            if (Screen1.Visible == true) { timerTick.Stop(); }
            else { timerTick.Start(); }
        }

        # region internal methods
        # region Math methods
        private int toTicks(double xTicks) //method for converting number to Ticks
        {
            xTicks = Math.Floor((xTicks / ticks));

            return int.Parse(xTicks.ToString());
        }

        private int plusTicks(double xTicks)
        {
            xTicks = Math.Ceiling((xTicks / ticks));

            return int.Parse(xTicks.ToString());
        }
        # endregion

        # region display methods
        public void addText(List<convoText> c)
        {
            Screen1.addText(c);
            Screen1.ShowDialog();
        }

        public void addText(string s)
        {
            Screen1.addText(s);
            Screen1.ShowDialog();
        }

        public void showImage(Image img)
        {
            pictures.Draw(img);
            GameLock(true);
        }

        public void cycleImages(bool forward)
        {
            int i = Inventory.GalleryIndex;

            if (forward) i++;
            else i--;

            if (i < 0) i = Inventory.gallery.Count - 1;
            else if (i >= Inventory.gallery.Count) i = 0;

            showImage(Inventory.gallery[i]);

            Inventory.GalleryIndex = i;
        }

        public void GameLock(bool locked)
        {
            if (locked)
            {
                screenLock = true;
                //timerDraw.Stop();
                //timerTick.Stop();
            }
            else
            {
                screenLock = false;
                //timerDraw.Start();
                //timerTick.Start();
            }
        }

        private void goFullScreen()
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }
        # endregion
        # endregion

        # region game methods
        # region loading methods
        private void xmlStreamReader(string path)
        {
            # region save previous map
            save1.maps.add(thisMap.changedSprites, thisMap.name);
            # endregion

            # region loading screen
            Bitmap bmp = new Bitmap(pictureBox2.Image);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                g.FillRectangle(new SolidBrush(Color.Black), rect);
            }
            pictureBox2.Image = bmp;
            pictureBox2.Refresh();
            # endregion

            # region check file exists
            if (!File.Exists(path))
            {
                return;
            }
            mapLoc = path;
            # endregion

            # region load file/assign handlers
            using (Stream xmlFile = File.Open(path, FileMode.Open))
            {

                # region map preparation
                XmlSerializer serial = new XmlSerializer(typeof(map));

                thisMap = serial.Deserialize(xmlFile) as map;

                this.Text = thisMap.name;

                save1.Red.xPos = xPC;
                save1.Red.yPos = yPC;
                # endregion

                List<SpriteBase> temp = new List<SpriteBase>();

                for (int i = 0; i < 3; i++)
                {
                    if (i == 0) { temp = thisMap.baseList; }
                    if (i == 1) { temp = thisMap.midList; }
                    if (i == 2) { temp = thisMap.topList; }

                    # region add event handlers
                    foreach (SpriteBase sb in temp)
                    {
                        switch (sb.GetType().ToString())
                        {
                            # region SpriteBase
                            case "PokémonGame.SpriteBase":

                                break;
                            # endregion

                            # region SpriteDoor
                            case "PokémonGame.SpriteDoor":
                                SpriteDoor sd = sb as SpriteDoor;

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

                                people.Add(sp);
                                break;
                            # endregion

                            default:
                                Debug.WriteLine(sb.GetType().ToString());
                                break;
                        }
                    }
                    # endregion

                }
            }
            # endregion

            thisMap.changedSprites = save1.maps.get(thisMap.name);
            thisMap.loadSpriteBases();

            thisMap.spMoving += new PokémonGame.move(thisMap_spMoving);
            thisMap.LoadMap += new transition(Door_LoadMap);
            thisMap.greet += new talk(sp_greet);
            thisMap.farewell += new talk(sp_farewell);
            thisMap.ItemAdd += new UseItem(sp_ItemAdd);
            thisMap.Fighting += new pokeEvent(sp_Fighting);

            setOffset();

            DrawLayer(0);
            DrawLayer(1);
            DrawLayer(2);
            DrawLayer(3);

            DrawScreen();

            hits.Clear();
        }

        # region TextDisplay Methods
        public void showText(string text)
        {
            Screen1.addText(text);
        }
        # endregion

        private void clearMap()
        {
            thisMap.baseList.Clear();
            thisMap.midList.Clear();
            thisMap.topList.Clear();
        }

        # region pokemon creation method
        private Pokemon createPokemon(int ID, int Level)    //can be moved to pokemon class
        {
            Pokemon newPokemon = PokemonLookup.get(ID);
            newPokemon.Level = Level;

            return newPokemon;
        }

        private Pokemon createPokemon(XmlReader reader)
        {
            # region attributes
            Pokemon p = new Pokemon();
            string nodeName = "";
            string Name = "";
            int Level = 0;
            int ID = 0;
            Move[] moves = new Move[4];
            moves[0] = new Move();
            moves[1] = new Move();
            moves[2] = new Move();
            moves[3] = new Move();
            # endregion

            # region reading from xml
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.IsEmptyElement != true)
                {
                    nodeName = reader.Name;

                    switch (nodeName)
                    {
                        case "PokeName":
                            Name = reader.ReadString();
                            break;

                        case "PokeLevel":
                            Level = reader.ReadElementContentAsInt();
                            break;

                        case "PokeID":
                            ID = reader.ReadElementContentAsInt();
                            break;

                        case "Pokemoves[0]":
                            moves[0] = MoveList.getMove(reader.ReadElementContentAsString());
                            break;

                        case "Pokemoves[1]":
                            moves[1] = MoveList.getMove(reader.ReadElementContentAsString());
                            break;

                        case "Pokemoves[2]":
                            moves[2] = MoveList.getMove(reader.ReadElementContentAsString());
                            break;

                        case "Pokemoves[3]":
                            moves[3] = MoveList.getMove(reader.ReadElementContentAsString());
                            break;
                    }
                }

            }
            # endregion

            # region assign values to p
            p = PokemonLookup.get(ID);
            p.Name = Name;
            p.Level = Level;
            p.moves[0] = moves[0];
            p.moves[1] = moves[1];
            p.moves[2] = moves[2];
            p.moves[3] = moves[3];
            # endregion

            return p;
        }
        # endregion
        # endregion

        #region tick triggers/actions
        private void updateSprite(object sender, EventArgs e) //call UpdateWorld
        {
            if (!screenLock)
            {
                GameLock(true);
                UpdateWorld();
                fps++;
            }

            if (swFPS.ElapsedMilliseconds > 500)
            {
                label4.Text = "fps - " + (fps * 2);
                fps = 0;
                swFPS.Restart();
            }
        }

        private void timerDraw_Tick(object sender, EventArgs e)
        {
            DrawScreen();
        }

        private void UpdateWorld() //lock direction and start movement cycle
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            save1.Red.xPos = xPC; //get xPos from picturebox1
            save1.Red.yPos = yPC; //get yPos from picturebox1
            moving();

            foreach (SpritePerson sp in people)
            {
                sp.onTick();
            }
            GameLock(false);

            if (hits.Count > 100)
            {
                hits.RemoveAt(0);
            }

            hits.Add(sw.Elapsed.TotalMilliseconds);

            double average = 0;

            foreach (double d in hits)
            {
                average += d;
            }

            average /= hits.Count;

            label5.Text = "speed - " + average;
        }
        #endregion

        #region battles
        public DialogResult StartBattle(List<Pokemon> enemyList)
        {
            # region BattleWindow Method
            //battleWindow = new BattleForm(EnemyParty, PokeBoxes.Boxes[0], pokeballs);
            //battleWindow.FormClosed += new FormClosedEventHandler(BattleForm1_FormClosed);
            //battleWindow.Width = this.Width - 8;
            //battleWindow.Height = this.Height - 34;
            //battleWindow.SetDesktopLocation(this.DesktopLocation.X + 4, this.DesktopLocation.Y + 30);
            //battleWindow.ParentGame = this;

            ////add these back in
            //battleWindow.Show();
            # endregion

            # region Encounterform Method
            encounterForm = new EncounterForm(enemyList);

            encounterForm.ShowDialog();
            # endregion

            this.Enabled = false;

            return encounterForm.DialogResult;
        }

        void BattleForm1_FormClosed(object sender, FormClosedEventArgs e)
        {
            # region add caught pokemon - Work Needed (Timing)
            if (battleWindow.caught == 1)
            {
                PokeBox.add(battleWindow.caughtPokemon[0]);
            }
            # endregion

            //if (activeSprite.Count > 0) activeSprite[0].onDefeat();

            this.Enabled = true;

            int[] coord = getCoords(DirLook);

            activeSprite = new SpriteBase();

            Screen1.AdvanceText();
        }
        #endregion

        #region movement cycle
        private void moving()
        {
            # region movepos 0
            if (movePos == 0)
            {
                if (transit)
                {
                    onMoveComplete();

                    if (nextMove == 0)  //no next move planned
                    {
                        transit = false;
                        save1.Red.stopMove(); //stops moving animation.
                        labelAnim.Text = "Static";
                    }
                }
            }
            # endregion
            #region dirlock 0, dirmove > 0
            if (DirLock == 0 & DirMove >= 1)
            {
                modNewPos();

                xNet = (save1.Red.xPos + xmod); //set prospective x position
                yNet = (save1.Red.yPos + ymod); //set prospective y position

                # region check for valid move
                # region if invalid move, walk on the spot
                if (!boundCheck())
                {
                    xNet = save1.Red.xPos;
                    yNet = save1.Red.yPos;
                }
                # endregion

                transit = true;
                StepCounter++;
                labelAnim.Text = "Moving";

                if (boundCheck())
                {
                    #region wild encounter
                    WildCheck();
                    #endregion

                    #region check if player can walk into new cell
                    int collision1 = getSprite(xNet, yNet); //new getsprite method
                    DirLook = DirMove;

                    # region leaving water
                    if (moveType == 3 && collision1 == 0)
                    {
                        //raise change movement type
                        moveType = 0;
                    }
                    # endregion

                    # region collision detection
                    if (collision1 == moveType) //if player can move through that square...
                    {
                        save1.Red.xNew = xNet;
                        save1.Red.yNew = yNet;
                        DirLock = 1;
                        movePos = (ticks / 2);

                        # region set up playersprite
                        save1.Red.dirLook = DirMove - 1;
                        # endregion

                        //set speed
                        if (Keyboard.IsKeyDown(Key.LeftShift))
                        {
                            movePos /= 2;
                            save1.Red.action = 3;
                            save1.Red.tickMax = 8;
                        }
                        else
                        {
                            //save1.Red.ticker = 0;
                            save1.Red.action = 0;
                            save1.Red.tickMax = 16;
                        }
                    }
                    else
                    {
                        //DrawScreen();
                        DirLock = 0;
                        DirMove = 0;

                        movePos = 0;
                    }
                    # endregion
                    #endregion

                    nextMove = 0;
                }
                # endregion
            }
            #endregion
            #region dirlock 1, dirmove > 0
            if (DirLock == 1 & DirMove >= 1)
            {
                threeXthree();

                if (movePos > 0)
                {
                    moveNPC();
                    movePos--; //save1.Reduce number of stages left
                }
                else
                {
                    DirLock = 0;
                    DirMove = nextMove;
                    nextMove = 0;
                }
            }
            #endregion

            //DrawScreen();
        }

        private void modNewPos()
        {
            switch (DirMove)
            {
                case 1: //player pressed Up key
                    xmod = 0;
                    ymod = -1;
                    break;

                case 2: //player pressed Down key
                    xmod = 0;
                    ymod = 1;
                    break;

                case 3: //player pressed Left key
                    xmod = -1;
                    ymod = 0;
                    break;

                case 4: //player pressed Right key
                    xmod = 1;
                    ymod = 0;
                    break;
            }

            xmod *= ticks; //convert tick distance into px
            ymod *= ticks; //convert tick distance into px
        }

        private void WildCheck()
        {
            if (getSprite(xNet, yNet) == 1)
            {
                Random Rand = new Random();
                double chance = 10;

                if (Rand.Next(0, 100) < (1 / (187.5 / chance)) * 100)
                {
                    # region variables
                    List<Pokemon> wild = new List<Pokemon>();

                    //TODO check this random number is random.
                    int CreaSel = Rand.Next(0, 100);
                    # endregion

                    //TODO rework this to take into account individual probabilities
                    if (CreaSel < 55)
                    {
                        wild.Add(wildPokemonList[0]);
                    }
                    else
                    {
                        wild.Add(wildPokemonList[1]);
                    }

                    StartBattle(wild);
                }
            }
        }

        private void moveNPC()
        {
            int xmove = ((save1.Red.xPos - save1.Red.xNew) * -1); //set distance to move
            int ymove = (save1.Red.yPos - save1.Red.yNew); //set distance to move

            if (movePos > 0) //catches "divide by 0" exception
            {
                xmove = (xmove / movePos); //divide distance to move by time left
                ymove = (ymove / movePos); //divide distance to move by time left
            }
            else
            {
                xmove = (xmod / (ticks / 2));
                ymove = (ymod / (ticks / -2));
            }

            if (movePos == 8) //animate player walking
            {
                //pictureBox1.Image = save1.Red.walk[DirMove, 2][0];
            }

            move(xmove, ymove);
        }

        private void move(int x, int y) //move character set distance
        {
            //TODO move into Player class
            setOffset();

            setPos(xPC + x, yPC - y);

            //set index to next image in animation cycle            
            save1.Red.tickOn();
        }

        private void setOffset()
        {
            int width = pictureBox2.Width / 2;
            int height = pictureBox2.Height / 2;

            xOffset = xPC - width;
            yOffset = yPC - height;

            //xOffset = xPC - (5 * ticks);
            //yOffset = yPC - (5 * ticks);
        }

        private void threeXthree()
        {
            int xTicks = toTicks(xPC); //player's current x position
            int yTicks = toTicks(yPC);  //player's current y position

            int xPlus = plusTicks(xPC);
            int yPlus = plusTicks(yPC);

            grass backimage = new grass();
            Image workingImage = backimage.grassImage;

        }

        private void setPos(int x, int y)
        {
            xPC = x;
            yPC = y;

            //insert event handler

            label1.Text = "xPC - " + xPC.ToString();
            label2.Text = "yPC - " + yPC.ToString();
        }

        private bool boundCheck() //check that xNet/yNet are within map bounds
        {
            if (xNet >= thisMap.width * ticks || yNet >= thisMap.height * ticks || xNet < 0 || yNet < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int getSprite(int xPosi, int yPosi)
        {
            if (xPosi == save1.Red.xPos / ticks && yPosi == save1.Red.yPos / ticks)
            {
                return 2;
            }

            int xTicks = toTicks(xPC);
            int yTicks = toTicks(yPC);

            int xTick2 = toTicks(xPosi); //prospective xposition in ticks
            int yTick2 = toTicks(yPosi); //prospective yposition in ticks

            int index = thisMap.getIndex(2, xTick2, yTick2);
            if (index < 0)
            {
                //no object stosave1.Red at that position
                return 0;
            }
            else
            {
                //return object type.
                return thisMap.midList[index].block;
            }
        }

        public int[] getCoords(int looking)
        {
            int[] coord = new int[2];
            coord[0] = toTicks(xPC);    //x co-ordinate
            coord[1] = toTicks(yPC);     //y co-ordinate

            # region set x/y modifiers

            int x = 0;
            int y = 0;
            switch (DirLook)
            {
                case 1: //player pressed Up key
                    x = 0;
                    y = -1;
                    break;

                case 2: //player pressed Down key
                    x = 0;
                    y = 1;
                    break;

                case 3: //player pressed Left key
                    x = -1;
                    y = 0;
                    break;

                case 4: //player pressed Right key
                    x = 1;
                    y = 0;
                    break;
            }
            #endregion

            coord[0] += x;
            coord[1] += y;


            return coord;
        }

        private Image paintImages(Image ImOne, Image ImTwo, int x, int y)
        {
            throw new NotImplementedException();

            //paintImages needs x/y co-ords to show it how much of the new image to paint
            Bitmap bmp = new Bitmap(ImOne);
            Graphics newIm = Graphics.FromImage(bmp);
            newIm.DrawImage(ImTwo, x, y);

            return bmp;
        }
        #endregion

        # region drawing
        private void DrawScreen()
        {
            Bitmap Screen = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            List<SpriteBase> TempMap = thisMap.baseList;

            # region calculate boundaries in ticks
            int Left = toTicks(xPC - (panel1.Width / 2));
            int Right = toTicks(xPC + (panel1.Width / 2));

            int Top = toTicks(yPC - (panel1.Height / 2));
            int Bottom = toTicks(yPC + (panel1.Height / 2));
            # endregion

            # region pre-load next sprite
            Left--;
            Right++;
            Top--;
            Bottom++;
            # endregion

            # region draw sprites
            using (Graphics e = Graphics.FromImage(Screen))
            {
                e.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                e.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                # region cycle through sprites and draw the relevant ones
                for (int m = 0; m < 4; m++)
                {
                    # region set level to draw
                    switch (m)
                    {
                        # region floor
                        case 0:
                            TempMap = thisMap.floor;
                            break;
                        # endregion

                        # region baseList
                        case 1:
                            TempMap = thisMap.baseList;
                            break;
                        # endregion

                        # region midList
                        case 2:
                            TempMap = thisMap.midList;
                            break;
                        # endregion

                        # region topList
                        case 3:
                            //Draw Red (currently sized wrong)
                            e.DrawImage(save1.Red.Sprite(), xPC - xOffset, yPC - yOffset, ticks, ticks); //first "Sprite()" call

                            pictureBox3.Image = save1.Red.Sprite(); //second "Sprite()" call

                            TempMap = thisMap.topList;
                            break;
                        # endregion

                        default:
                            TempMap = new List<SpriteBase>();
                            break;
                    }
                    # endregion

                    foreach (SpriteBase sB in TempMap)
                    {
                        //check to see if xpos && ypos are within drawing range
                        if (sB.xPos > Left && sB.xPos < Right && sB.yPos > Top && sB.yPos < Bottom)
                        {
                            bool draw = true;
                            draw = GMVB.check(sB.requirements);

                            if (draw)
                            {
                                e.DrawImage(sB.toImage(), (sB.xNet) - xOffset, (sB.yNet) - yOffset, ticks, ticks);
                            }
                        }
                    }
                }
                # endregion
            }
            # endregion

            pictureBox2.Image = Screen;
        }

        /// <summary> Redundant
        /// Not used anymore.
        /// </summary>
        private void DrawScreen2()
        {
            //TODO remove

            //int width = imgFloor.Width;
            //int height = imgFloor.Height;

            //Bitmap MapScreen = new Bitmap(width, height);

            //# region draw images
            //Rectangle rectDest = new Rectangle(0, 0, width, height);
            //Rectangle rectSrc = new Rectangle(xOffset, yOffset, width, height);

            //using (Graphics e = Graphics.FromImage(MapScreen))
            //{
            //    e.DrawImage(imgFloor, rectDest, rectSrc, GraphicsUnit.Pixel);
            //    e.DrawImage(imgBase, rectDest, rectSrc, GraphicsUnit.Pixel);
            //    e.DrawImage(imgMid, rectDest, rectSrc, GraphicsUnit.Pixel);
            //    e.DrawImage(save1.Red.Sprite(), xPC - xOffset, yPC - yOffset);
            //    e.DrawImage(imgTop, rectDest, rectSrc, GraphicsUnit.Pixel);
            //}
            //# endregion

            //pictureBox2.Image = MapScreen;
        }

        /// <summary>
        /// Re-draws the specified level
        /// </summary>
        /// <param name="l">floor, base, mid, top</param>
        private void DrawLayer(int l)
        {
            List<SpriteBase> temp = thisMap.floor;
            if (l == 1) { temp = thisMap.baseList; }
            if (l == 2) { temp = thisMap.midList; }
            if (l == 3) { temp = thisMap.topList; }

            Image result = new Bitmap(thisMap.width * ticks, thisMap.height * ticks);

            using (Graphics e = Graphics.FromImage(result))
            {
                foreach (SpriteBase sb in temp)
                {
                    e.DrawImage(sb.toImage(), (sb.xPos * ticks), (sb.yPos * ticks), ticks, ticks);
                }
            }

            if (l == 0) { imgFloor = result; }
            if (l == 1) { imgBase = result; }
            if (l == 2) { imgMid = result; }
            if (l == 3) { imgTop = result; }
        }

        # endregion

        # region savefile
        private void SaveGame()
        {
            XmlSerializer serial = new XmlSerializer(typeof(SaveFile));

            save1.vars = GMVB.export();
            save1.xPos = xPC;
            save1.yPos = yPC;
            save1.Map = mapLoc;

            string path = Settings.Default.AppDir;

            if (!Directory.Exists(path))
            {
                return;
            }

            path += "\\save1.save";

            using (Stream stream = File.Create(path))
            {
                serial.Serialize(stream, save1);
            }
        }

        private void LoadGame()
        {
            XmlSerializer serial = new XmlSerializer(typeof(SaveFile));

            string path = Settings.Default.AppDir + "\\save1.save";

            if (!File.Exists(path))
            {
                return;
            }

            using (Stream stream = File.Open(path, FileMode.Open))
            {
                save1 = serial.Deserialize(stream) as SaveFile;
            }

            GMVB.import(save1.vars);

            xmlStreamReader(save1.Map);

            xPC = save1.xPos;
            yPC = save1.yPos;
        }
        # endregion
        # endregion

        #region eventHandlers
        private void KeyReader_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            # region Load Key
            if (e.KeyCode == Keys.L) //load pallet xml file
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                path += @"\Resources\Pallet.xml";
                path = path.Remove(0, 6); //remove "file:/" from directory name

                if (File.Exists(path))
                {
                    xmlStreamReader(path);
                    DrawScreen();
                }
            }
            # endregion
            # region Open Key
            if (e.KeyCode == Keys.O)
            {
                openToolStripMenuItem_Click(sender, e);
            }
            # endregion
            # region Save Key
            if (e.KeyCode == Keys.S)
            {
                if (e.Modifiers == Keys.Control)
                {
                    SaveGame();
                }
            }
            # endregion

            if (!screenLock)
            {
                # region game actions
                if (DirLock == 0)
                {
                    # region testing
                    if (e.KeyCode == Keys.H) //Heal all pokemon
                    {
                        PokeBox.Heal(0); //Heal box '0' (party)
                    }

                    if (e.KeyCode == Keys.P) //show pictures
                    {
                        showImage(Properties.Resources._001bulbasaur);
                    }

                    if (e.KeyCode == Keys.K) //displays all variables and values.
                    {
                        GMVB.display();
                    }
                    if (e.KeyCode == Keys.Add)
                    {
                        moveType++;
                    }
                    if (e.KeyCode == Keys.Subtract)
                    {
                        moveType--;
                    }
                    if (e.KeyCode == Keys.S)
                    {
                        poke_ability(new Move { Name = "Surf" });
                    }

                    # endregion

                    #region menu button
                    if (e.KeyCode == Keys.V) //menu key(v) pressed
                    {
                        GameLock(true);

                        int ParentX = (this.Location.X + (this.Width - contextMenuStrip1.Width));
                        int ParentY = (this.Location.Y + (this.Height - contextMenuStrip1.Height));

                        contextMenuStrip1.Closed += new ToolStripDropDownClosedEventHandler(contextMenuStrip1_Closed);
                        contextMenuStrip1.Show(ParentX, ParentY);
                    }
                    #endregion

                    #region directional key bindings
                    if (e.KeyCode == Keys.Up) //move up
                    {
                        DirMove = 1;
                    }
                    else if (e.KeyCode == Keys.Down) //move down
                    {
                        DirMove = 2;
                    }
                    else if (e.KeyCode == Keys.Left) //move left
                    {
                        DirMove = 3;
                    }
                    else if (e.KeyCode == Keys.Right) //move right
                    {
                        DirMove = 4;
                    }

                    if (DirMove > 0)
                    {
                        if (e.Modifiers == Keys.Shift)
                        {
                            fast = true;
                        }
                        else
                        {
                            fast = false;
                        }
                        save1.Red.dirLook = DirMove - 1;
                        label3.Text = "Looking - " + DirLook;
                    }
                    #endregion

                    #region action buttons
                    if (e.KeyCode == Keys.C)
                    {
                        if (Screen1.Visible == false) //TODO probably save1.Redundant with this.active = false;
                        {
                            int[] target = getCoords(DirLook);//ints for storing target co-ordinates

                            SpriteBase sb = thisMap.getSprite(2, target[0], target[1]);
                            activeSprite = sb;
                            activeSprite.onActivate();
                        }
                    }
                    #endregion

                }
                # endregion
                #region waiting movement
                else
                {
                    if (e.KeyCode == Keys.Up) //move up
                    {
                        nextMove = 0;
                    }
                    else if (e.KeyCode == Keys.Down) //move down
                    {
                        nextMove = 2;
                    }
                    else if (e.KeyCode == Keys.Left) //move left
                    {
                        nextMove = 3;
                    }
                    else if (e.KeyCode == Keys.Right) //move right
                    {
                        nextMove = 4;
                    }
                }
                # endregion
            }
        }

        private string eventSwitch(activationEvent Event)
        {
            //switch (Event.type) //1-text; 2-restore; 3-???; 4-item; 5-pokemon
            //{
            //    case 1:
            //        return Event.Text;

            //    case 2:
            //        PokeBox.Boxes[0].PokeList[0].HPCurrent += Event.healthEffect;
            //        return "Health restosave1.Red by " + Event.healthEffect.ToString();

            //    case 3:
            //        return Event.Text;

            //    case 4:
            //        for (int i = 0; i < 250; i++)
            //        {
            //            if (pokeballs[i] == null)
            //            {
            //                pokeballs[i] = (Pokeball)Event.Item;
            //            }
            //        }
            //        return (Event.Item.Name + "added to inventory");

            //    case 5:
            //        fightNext = 1;
            //        activePokemon[0].thisPokemon = Event.animal;
            //        return Event.Text;

            //    default:
            //        return "";
            //}

            return "";
        }

        # region game events
        protected void onMoveComplete()
        {
            foreach (SpriteBase sb in thisMap.baseList)
            {
                if (sb.xPos == xPC / ticks && sb.yPos == yPC / ticks)
                {
                    activeSprite = sb;
                    sb.onStep();
                }
            }
        }

        protected void onShop(List<inventoryItem> items)
        {
            Shop newShop = new Shop(this.Size);

            newShop.LoadShop(items);
        }

        # region specific
        private void Door_LoadMap(string f, int x, int y)
        {
            string file = Settings.Default.AppDir + @"\Resources\Maps\" + f;

            if (File.Exists(file))
            {
                xmlStreamReader(file);
            }
            else
            {
                Debug.WriteLine("Couldn't find {0}", file);
            }

            setPos(x * ticks, y * ticks);
            setOffset();

        }

        private void Activate_LoadMap()
        {

        }

        private void sp_greet(List<convoText> messages)
        {
            addText(messages);
        }

        private void sp_farewell(List<convoText> messages)
        {
            addText(messages);
        }

        private void sp_ItemAdd(inventoryItem item)
        {
            Inventory.add(item.ID, item.count);
            addText("You recieved " + item.getNameNet() + ".");
        }

        private void sp_Fighting()
        {
            addText("Starting fight.");
            if (activeSprite.GetType() == typeof(SpritePerson))
            {
                SpritePerson sp = activeSprite as SpritePerson;

            }
        }
        # endregion

        private void poke_ability(Move m)
        {
            if (m.Name == "Surf")
            {
                //check if next square is surfable
                xNet = (save1.Red.xPos + xmod); //set prospective x position
                yNet = (save1.Red.yPos + ymod); //set prospective y position

                int collision1 = getSprite(xNet, yNet);

                if (collision1 == 3)
                {
                    moveType = 3;
                    //move direction looking.
                    DirMove = DirLook;
                }
                else
                {
                    addText("Professor Oak: There's no point surfing there!");
                }

            }
        }
        # endregion

        # region map events
        void thisMap_spMoving(SpriteLiving sb, int x, int y)
        {
            if (getSprite(x, y) == sb.transit)
            {
                sb.xTarg = x * 32;
                sb.yTarg = y * 32;
                sb.xPos = x;
                sb.yPos = y;

                //TODO insert updating methods
                //TODO write part for detecting which level they are in
            }
            else
            {
                //sb.xTarg = sb.xPos * 32;
                //sb.yTarg = sb.yPos * 32;
            }
        }

        void thisMap_floorChanged()
        {
            Debug.WriteLine("floor changed");
            DrawLayer(0);
        }

        void thisMap_baseChanged()
        {
            Debug.WriteLine("base changed");
            DrawLayer(1);
        }

        void thisMap_midChanged()
        {
            Debug.WriteLine("mid changed.");
            DrawLayer(2);
        }

        void thisMap_topChanged()
        {
            Debug.WriteLine("top changed.");
            DrawLayer(3);
        }
        # endregion
        # endregion

        private void GameWindow_Load(object sender, EventArgs e)
        {
            DrawScreen();
        }

        # region context menu
        void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            screenLock = false;
        }

        private void pokedexToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void pokemonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            partyWindow.loadpokemon();
            partyWindow.ShowDialog();
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bPack1.updateListBox1();
            bPack1.Visible = true;
            bPack1.Focus();
        }

        private void playerToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) { }
        # endregion

        private void button1_Click(object sender, EventArgs e)
        {
            LoadGame();
            pictureBox2.Focus();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameLock(true);
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "XML|*.xml";

            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                xmlStreamReader(OpenFile.FileName);
                DrawScreen();
            }

            GameLock(false);
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveGame();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    # region SaveFile
    /// <summary>
    /// contains all the information to be saved
    /// Must NOT be static for serialisation to work.
    /// </summary>
    public class SaveFile
    {
        # region declarations and values
        public Player Red = new Player();
        public DateTime mTime = new DateTime(); //harvest timer?
        public changedMaps maps = new changedMaps();
        public List<variable> vars = new List<variable>();

        public string Map = "Pallet2";
        public int xPos = 2;
        public int yPos = 2;
        # endregion

        public SaveFile()
        {

        }

        public void checkHarvest()
        {
            TimeSpan ts = DateTime.Now - mTime;

            int i = (int)ts.TotalSeconds / 60;

            for (int j = i; j > 0; j++)
            {
                //do stuff
            }
        }
    }

    public class changedMaps
    {
        //how to store maps?
        public List<MapCont> mapChanges = new List<MapCont>();

        [XmlIgnore]
        Dictionary<string, int> mapKey = new Dictionary<string, int>();

        void refresh()
        {
            mapKey.Clear();
            for (int i = 0; i < mapChanges.Count; i++)
            {
                mapKey.Add(mapChanges[i].ID, i);
            }
        }

        public bool add(List<SpriteBase> list, string ID)
        {
            refresh();

            bool result = false;
            MapCont newEdits = new MapCont();
            newEdits.ID = ID;
            newEdits.sprites = list;

            if (mapKey.ContainsKey(ID))
            {
                result = true;

                int i = mapKey[ID];
                mapChanges[i] = newEdits;
            }
            else
            {
                mapKey.Add(ID, mapChanges.Count);
                mapChanges.Add(newEdits);
            }

            return result;
        }

        public List<SpriteBase> get(string ID)
        {
            refresh();

            List<SpriteBase> result = new List<SpriteBase>();

            if (mapKey.ContainsKey(ID))
            {
                int i = mapKey[ID];

                result = mapChanges[i].sprites;
            }

            return result;
        }
    }

    public class MapCont
    {
        public List<SpriteBase> sprites = new List<SpriteBase>();
        public string ID = "";
    }
    # endregion
}
