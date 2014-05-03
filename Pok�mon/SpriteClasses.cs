using PokémonGame;
using PokémonGame.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;

# region sprites
public class sprites
{
    public int xPos = 0;
    public int yPos = 0;
    public int xNew = 0;
    public int yNew = 0;
    public string activateText = "";
    [XmlIgnore]
    public Image[,] spriteList = new Image[10, 10];
    public activationEvent item = new activationEvent();

    public void activate() { }

    public virtual activationEvent activate(int action, params Pokemon[] party)
    {
        activationEvent item = new activationEvent();
        item.animal = null;
        item.Item = null;
        item.Text = "";
        item.type = 1; //tell it to use the text.

        return item;
    }

    public PictureBox createSprite()
    {
        PictureBox newSprite = new PictureBox();
        newSprite.Width = 32;
        newSprite.Height = 32;
        return newSprite;
    }
}

# region Static sprites

public class terrain : sprites
{

}

public class tree : terrain
{
    public Image treeImage = Image.FromFile(@"C:\Documents and Settings\Chris Woollacott\My Documents\Downloads\Sprites\tree.png");

    public PictureBox createTree()
    {
        PictureBox treeOne = new PictureBox();
        treeOne.Width = 32;
        treeOne.Height = 32;
        treeOne.Image = treeImage;
        return treeOne;
    }
}

public class building : terrain
{

    public override activationEvent activate(int action, params Pokemon[] party)
    {
        item.animal = null;
        item.Item = null;
        item.Text = activateText;
        item.type = 1; //tell it to use the text.

        return item;
    }


    public void loadTiles()
    {
        Rectangle cropArea = new Rectangle(2, 10, 16, 16);
        spriteList = SpriteLoader.loadsprites(PokémonGame.Properties.Resources.tileset2, cropArea, 16, 16, 4, 5);
    }
}

# endregion

# region interactive landscape sprites

public class door : sprites
{
    public override activationEvent activate(int action, params Pokemon[] party)
    {
        item.animal = null;
        item.Item = null;
        item.Text = "The door is locked";
        item.type = 1; //tell it to use the text.

        return item;
    }
}

public class grass : sprites
{
    public Image grassImage = Resources.grass;
}

public class groundItem : sprites
{
    public override activationEvent activate(int action, params Pokemon[] party)
    {
        //item.animal = null;
        //item.Item = null;
        //item.Text = "Congratulations! You win...nothing. :) ";
        //item.type = 1; //tell it to use the text.

        return item;
    }
}

# endregion

# region living sprites
public class NPC : sprites
{
    public int dirLook = 1;
    public int moveAction = 1;

    [XmlIgnore]
    public Image[,][] walk = new Image[5, 4][]; //[direction(up, down, left, right); action(standing, walking, running, cycling, surfing)]

    public Image[,][] loadspritesA(Image Spritesheet, Rectangle cropArea, int xInterval, int yInterval, int iIter, int jIter)
    {
        Image[,][] array = new Image[5, 4][];
        int origX = cropArea.X;
        array[0, 0] = new Image[8];

        for (int j = 0; j < jIter; j++)
        {
            for (int i = 0; i < iIter; i++)
            {
                array[j, i] = SpriteLoader.loadspritesI(Spritesheet, cropArea, 32, 8);

                cropArea.X = (cropArea.X + xInterval);
            }
            cropArea.X = origX;
            cropArea.Y = cropArea.Y + yInterval;
        }

        return array;
    }

    #region event details
    public int function = 0; //0=talk, 1=move, 2=fight, 3=giveItem, 4=givePokemon, 5=restore
    public activationEvent activeEvent = new activationEvent();

    public override activationEvent activate(int action, params Pokemon[] party)
    {
        item.animal = null;
        item.Item = null;

        item.type = function; //tell it to use the text.

        switch (function)
        {
            case 0: //talk
                item.Text = activateText;
                break;

            case 1: //move
                //unimplemented
                break;

            case 2: //fight npc
                //unimplemented
                break;

            case 3: //receive item
                //unimplemented
                break;

            case 4: //receive pokemon
                //unimplemented
                break;

            case 5: //restore health
                item.healthEffect = 50;
                break;

            default:
                break;
        }

        return item;
    }
    #endregion
}

public class Player : NPC
{
    public Pokemon[] pokeGroup = new Pokemon[6];
    public int ticker = 0;
    public int tickMax = 16;
    public int anim = 0;
    public int action = 0; // 0/1/2 is walking, 3/4/5 is running, 6/7 tightrope, 8/9 jumping ledge

    public Player()
    {
        loadPlayer();
    }

    public void loadPlayer()
    {
        Rectangle cropArea = new Rectangle(0, 2, 32, 32);

        walk = loadspritesA(PokémonGame.Properties.Resources.heromfoverworld, cropArea, 32, 32, 4, 5);
    }

    public void tickOn()
    {
        //16tick cycle
        ticker++;
        
        if (ticker >= tickMax)    //cap at 15
        {
            ticker = 0;
        }

        # region set anim (add anim to action)
        switch (ticker)
        {
            case 0:
                anim = 0;
                break;

            case 4:
                anim = 1;
                break;

            case 8:
                anim = 0;
                break;

            case 12:
                anim = 2;
                break;

            case 16:
                break;
        }
        # endregion
    }

    public void stopMove()
    {
        ticker = 0;
        anim = 0;
        moveAction = 0;
        action = 0;
    }

    public Image Sprite()
    {
        if (dirLook == -1)
        {
            dirLook = 1;
        }

        return walk[dirLook, moveAction][action + anim];
    }
}

public class PokeSprite : sprites
{
    public Pokemon thisPokemon = new Pokemon();
    public Image PokemonSprite = null;
    public Image[,] pokeSprites = new Image[1000, 10];

    public void loadPokemon()
    {
        Rectangle cropArea = new Rectangle(0, 0, 32, 32);
        Image spriteSheet = Image.FromFile(@"C:\Documents and Settings\Chris Woollacott\My Documents\Downloads\Sprites\owgen1.PNG");
        pokeSprites = SpriteLoader.loadsprites(spriteSheet, cropArea, 32, 32, 4, 4);
    }

    public override activationEvent activate(int action, params Pokemon[] party)
    {
        item.animal = null;
        item.Item = null;
        item.Text = "Bulba!";
        item.type = 1; //tell it to use the text.

        return item;
    }


}

# endregion

# endregion

public class activationEvent
{
    public int type = 0; //tells calling method what info it contains

    //effects to be passed
    public string Text = "";
    public inventoryItem Item = new inventoryItem();
    public Pokemon animal = new Pokemon();
    public int healthEffect = 0;
}

# region spriteclass
public class spriteList
{
    public static Dictionary<int, spriteClass> Listsprite = new Dictionary<int, spriteClass>();
    public static Image[,] Trees;
    public static Image[,][] npcSprites;

    public static void LoadSprites()
    {
        # region Load spritesheets into memory
        //add in npc sprites TODO add correct spriteSheet
        Rectangle cropArea = new Rectangle(0, 0, 32, 32);
        spriteList.npcSprites = SpriteLoader.loadspritesA(Resources.heromfoverworld, cropArea, 32, 32, 4, 5);

        //add in trees.
        cropArea = new Rectangle(0, 0, 16, 16);
        spriteList.Trees = SpriteLoader.loadsprites(Resources.trees, cropArea, 16, 16, 11, 6);
        # endregion
    }

    public static bool add(spriteClass s)
    {
        if (!Listsprite.ContainsValue(s))
        {
            Listsprite.Add(Listsprite.Count, s);
            return true;
        }
        else { return false; }
    }

    public static spriteClass get(int ID)
    {
        if (Listsprite.ContainsKey(ID))
        {
            return Listsprite[ID];
        }
        else { return null; }
    }

    public static spriteClass get(string name)
    {
        spriteClass result = null;
        foreach (KeyValuePair<int, spriteClass> k in Listsprite)
        {
            if (k.Value.Name == name)
            {
                result = k.Value;
            }
        }

        return result;
    }
}

public class spriteClass
{
    # region declaration
    # region values
    # region
    public int Type = 0;
    public int Terrain = -1;
    public string Name = "";
    public int xPos = 0;
    public int yPos = 0;
    public int xStart = 1;  //initial xPos
    public int yStart = 1;  //initial yPos
    # endregion

    //public inventoryItem Item = new inventoryItem(); // make redundant
    public List<inventoryItem> Items = new List<inventoryItem>(); //start implementing
    public int agression = 0; //whether NPC attacks on sight/activation

    public GameWindow parentScreen;

    # region Pokemon
    public PokeSprite[] PartyPokemon = new PokeSprite[6];
    public List<Pokemon> PokeGroup = new List<Pokemon>();
    private int pokePos = 0;
    # endregion

    # region conversation
    public List<string>[] conversations = new List<string>[4];
    public string greetingText = "";
    public string combatText = "";
    public string combatTextEnd = "";
    public string farewellText = "";
    # endregion

    private int status = 0;
    # endregion

    # region images etc.
    public Image[,] sprites = new Image[4, 2]; //direction/variant
    public PictureBox myPicturebox = new PictureBox();

    //Image values
    public int Width = 1;   //Width (in ticks)
    public int Height = 1;  //Height (in ticks)

    public int dirLook = 1; //direction the sprite is looking
    public int xAnim = 0;   //x part of image lookup (in ticks)
    public int yAnim = 0;   //y part of image lookup (in ticks)
    # endregion

    # region events
    public event activate talk;
    public event activate spotted;
    public event defeated defeat;

    public bool talkedTo = false;
    public bool defeated = false;
    # endregion
    # endregion

    # region sprite methods
    public void loadImages()
    {
        Rectangle cropArea = new Rectangle(xStart, yStart, 16, 16); //start position is 0,0 and each sprite is 16px by 16px
        sprites = SpriteLoader.loadsprites(Resources.trees, cropArea, 16, 16, Width, Height);
    }

    public Image ToImage()
    {
        Image temp = sprites[xAnim, yAnim];                //get image

        # region find alternate image
        if (temp == null)   //if null, return new blank image.
        {
            foreach (Image i in sprites)
            {
                if (i != null)
                {
                    temp = i;
                    Debug.WriteLine("chose alternative image");
                }
            }
        }
        # endregion

        # region if still null, return blank image
        if (temp == null)
        {
            temp = new Bitmap(32, 32);
        }
        # endregion

        return temp;
    }

    public void ArrayImageConvert(Image[,][] img)
    {
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                sprites[x, y] = img[x, y][0];
            }
        }
    }

    public Image SpriteSheet()
    {
        int x1 = sprites.GetUpperBound(0);
        int y1 = sprites.GetUpperBound(1);

        Image i = new Bitmap(x1 * 32, y1 * 32);

        Graphics g = Graphics.FromImage(i);

        for (int y = 0; y < y1; y++)
        {
            for (int x = 0; x < x1; x++)
            {
                if (sprites[x, y] != null)
                {
                    g.DrawImage(sprites[x, y], new Rectangle(x * 32, y * 32, 32, 32));
                    Debug.WriteLine("{0}, {1}", x, y);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.LightGreen), new Rectangle(x * 32, y * 32, 32, 32));
                }
            }
        }


        g.Dispose();

        return i;
    }
    # endregion

    public spriteClass(GameWindow parent)
    {
        parentScreen = parent;
        int ticks = parent.ticks;
        myPicturebox.Width = 1 * ticks;
        myPicturebox.Height = 1 * ticks;
        myPicturebox.Left = this.xPos * ticks;
        myPicturebox.Top = this.yPos * ticks;

        myPicturebox.SizeMode = PictureBoxSizeMode.StretchImage;
        myPicturebox.Image = sprites[xStart, yStart];

        # region conversation initialisation
        conversations[0] = new List<string>();
        conversations[1] = new List<string>();
        conversations[2] = new List<string>();
        conversations[3] = new List<string>();
        # endregion

        this.talk += new activate(spriteClass_talk);
        this.spotted += new activate(spriteClass_spotted);
        this.defeat += new defeated(spriteClass_defeat);

        for (int i = 0; PartyPokemon[i] != null; i++)
        {
            PokeGroup[i] = PartyPokemon[i].thisPokemon;
        }
    }

    public spriteClass() { }

    # region activation methods
    public void activate()
    {
        # region greet
        if (!talkedTo)
        {
            //new method
            foreach (string s in conversations[0]) parentScreen.addText(s);

            //old method
            if (greetingText != "")
                parentScreen.addText(greetingText);

            talkedTo = true;
        }
        # endregion

        # region Item
        if (Items.Count > 0)
        {
            addItem();

            //TODO stop from progressing while parentScreen.pictures is visible.
        }


        # endregion

        //Heal

        # region battle
        if (agression > 0)
        {
            //TODO re-write conditions and the way pokemon are stored by spriteClass
            //if ( haspokemon && !defeated)
            if (PartyPokemon[pokePos] != null && PartyPokemon[pokePos].thisPokemon.HPCurrent > 0)
            {
                StartBattle();
            }
        }
        # endregion

        # region farewell text
        foreach (string s in conversations[3])
            parentScreen.addText(s);

        if (farewellText != "")
        {
            parentScreen.addText(farewellText);
        }
        # endregion
    }

    # region Items
    private void addItem()
    {
        foreach (inventoryItem i in Items)
        {
            i.addItem();
        }

        Items.Clear();
    }
    # endregion

    # region talking
    public void talkTo()
    {
        talk();
    }

    void spriteClass_talk()
    {

    }
    # endregion

    # region fighting
    public void StartBattle()
    {
        foreach (Pokemon p in PokeGroup) { p.CalcStats(); p.Heal(); }

        DialogResult result = parentScreen.StartBattle(PokeGroup);

        # region battle results
        if (result == DialogResult.OK)
        {
            //won battle
            parentScreen.addText("You won!");
        }
        else if (result == DialogResult.Abort)
        {
            //ran away
            parentScreen.addText("You ran away.");
        }
        else if (result == DialogResult.Retry)
        {
            //lost battle
            parentScreen.addText("You lost.");
        }
        # endregion

        //TODO fill with end of battle things.
    }

    public void defeatedMe()
    {
        defeat();
    }

    void spriteClass_defeat()
    {
        if (farewellText != null && farewellText != "")
        {
            parentScreen.Screen1.addText(farewellText);
        }
        else
        {
            parentScreen.Screen1.addText("farewellText incompatible");
        }
    }
    # endregion

    # region spotted
    public void spottedPC()
    {
        spotted();
    }

    void spriteClass_spotted() //not implemented
    {
        if (agression == 1)
        {
            //Walk to PC
            //start conversation
            //wait for text to finish
            //access parentScreen fight method
        }
    }
    # endregion
    # endregion
}
# endregion



//Delegates
public delegate void activate();
public delegate void defeated();
