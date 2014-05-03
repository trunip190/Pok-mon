using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PokémonGame
{
    public partial class BattleForm : Form
    {
        # region declarations
        //# region Set combatents
        //public Dictionary<int, Pokemon> Party;
        //public List<Pokemon> EnemyParty;
        //public Pokemon Enemy = new Pokemon();
        //public List<Pokemon> Friendly = new List<Pokemon>();

        //public int PartyIndex = 0;
        //public int EnemyPartyIndex = 0;
        //#endregion

        //# region Variables
        //public Pokeball[] pokeBallsNet = new Pokeball[250];
        //Backpack newBackpack;

        //public GameWindow ParentGame;
        
        //public int StoryIndex = new int();
        //int onetime = 0;
        //public List<String> TextQueue = new List<string>();
        //public string[] StoryText = new string[10];
        //# endregion

        //# region Attack variables
        //public int pFirst = 0;
        //public double firstDamage = 0;
        //public double secondDamage = 0;
        //public double firstPoisonDamage = 0;
        //public double secondPoisonDamage = 0;
        //# endregion

        //# region Battle variables
        //public int battleStage = 0;
        public int caught = 0;
        public List<Pokemon> caughtPokemon = new List<Pokemon>(1);
        //# endregion
        # endregion

        # region Methods
        public BattleForm() { }

        public BattleForm(List<Pokemon> NEnemy, Pokeball[] pokeballs)
        {
        //    InitializeComponent();
        //    this.KeyUp += new KeyEventHandler(Buttonclick);

        //    # region pokeballs
        //    if (pokeballs != null)
        //    {
        //        for (int i = 0; pokeballs[i] != null; i++)
        //        {
        //            pokeBallsNet[i] = pokeballs[i];
        //        }
        //    }
        //    else
        //    {
        //        Pokeball pokeball1 = new Pokeball();
        //        pokeball1.Name = "Pokeball";
        //        pokeball1.increaseCount(0);
        //        pokeBallsNet[0] = pokeball1;

        //    }
        //    # endregion

        //    this.pokedex1TableAdapter.Fill(this.pokedexDataSet.pokedex1);

        //    # region pokemon loading
        //    EnemyParty = NEnemy;
        //    if (EnemyParty[0] == null)
        //    {
        //        EnemyParty[0] = new Pokemon();
        //        EnemyParty[0].ID = 50;
        //        EnemyParty[0].Level = 10;
        //        EnemyParty[0].Name = "Random";
        //        EnemyParty[0].CalcStats();
        //    }
        //    Enemy = NEnemy[0];
        //    Party = PokeBox.Party.PokeList;
        //    Friendly.Add(Party[0]);
        //    # endregion

        //    #region fill richtextbox1 with the names of the pokemon Red is carrying
        //    foreach ( KeyValuePair<int, Pokemon> p in Party)
        //    {
        //        richTextBox1.AppendText(p.Value.Name);
        //        richTextBox1.AppendText(Environment.NewLine);
        //    }
        //    #endregion

        //    LoadPokemon(); //convert base stats into netstats.

        //    # region final loading
        //    StoryText[0] = "A wild " + Enemy.Name + " appears";
        //    TextQueue.Add("A wild " + Enemy.Name + " appears");
        //    StoryText[9] = "";
        //    updateSheet();
        //    button2.Text = StoryText[0];
        //    cycletext(0);
        //    this.ActiveControl = button2;
        //    # endregion
        }

        public BattleForm(List<Pokemon> NEnemy)
        {
        //    // TODO (everything)
        //    InitializeComponent();
        //    this.KeyUp += new KeyEventHandler(Buttonclick);

        //    this.pokedex1TableAdapter.Fill(this.pokedexDataSet.pokedex1);

        //    # region pokemon loading
        //    if (NEnemy.Count < 1) NEnemy.Add(PokemonLookup.get(25));
        //    else EnemyParty = NEnemy; Enemy = EnemyParty[0];

        //    Party = PokeBox.Party.PokeList;
        //    Friendly.Add(Party[0]);
        //    # endregion

        //    #region fill richtextbox1 with the names of the pokemon Red is carrying
        //    foreach (KeyValuePair<int, Pokemon> p in Party)
        //    {
        //        richTextBox1.AppendText(p.Value.Name);
        //        richTextBox1.AppendText(Environment.NewLine);
        //    }
        //    #endregion

        //    LoadPokemon();

        //    # region final loading
        //    StoryText[0] = "A wild " + Enemy.Name + " appears";
        //    TextQueue.Add("A wild " + Enemy.Name + " appears");
        //    StoryText[9] = "";
        //    updateSheet();
        //    button2.Text = StoryText[0];
        //    cycletext(0);
        //    this.ActiveControl = button2;
        //    # endregion
        }

        //#region Battle
        //public void battleInput()
        //{

        //}

        //#region New battleprocess
        //private void grabMoves(int eMove, int fMove) //sets order of battle, and the damage to be done
        //{
        //    //If player is faster, then it gets it's attack in first.

        //    if (Friendly[0].Speed.NetStat > Enemy.Speed.NetStat)
        //    {
        //        firstDamage = FriendlyAttack();
        //        secondDamage = EnemyAttack();
        //        pFirst = 1;
        //    }
        //    else //if player is not faster, then enemy get it's attack in first.
        //    {
        //        firstDamage = EnemyAttack();
        //        secondDamage = FriendlyAttack();
        //        pFirst = 0;
        //    }
        //    battleStage = 1;
        //    moveBattleProcess();
        //}

        //private void moveBattleProcess()
        //{
        //    string battleText = "";

        //    switch (battleStage)
        //    {
        //        case 1:
        //            # region set text and display it
                    
        //            if (pFirst == 1) //Player is faster
        //            {
        //                battleText = (Friendly[0].Name + " did " + firstDamage + " to foe's " + Enemy.Name);
        //            }
        //            else //Enemy is faster
        //            {
        //                battleText = (Enemy.Name + " did " + firstDamage + " to your " + Friendly[0].Name);
        //            }
        //            updateText(battleText, StoryIndex);
        //            panel4.Show();
        //            advanceTextbox();

        //            goto case 2;

        //            # endregion

        //        case 2:
        //            # region first attack
        //            if (pFirst == 1)
        //            {
        //                Enemy.HPCurrent -= firstDamage;
        //            }
        //            else
        //            {
        //                Friendly[0].HPCurrent -= firstDamage;
        //            }
        //            drawHP();
        //            battleStage = 3;

        //            break;
                    
        //            # endregion

        //        case 3:
        //            # region goto victorycheck
        //            if (victoryCheck() > 0) //enemy has been knocked out
        //            {
        //                updateText("Enemy " + Enemy.Name + " has fainted", StoryIndex);
        //                panel4.Visible = true;
        //                advanceTextbox();
        //                battleStage = 20; //battle is over
        //                break;
        //            }
        //            else
        //            {
        //                goto case 4;
        //            }

        //            #endregion

        //        case 4:
        //            #region process poison damage

        //            if (firstPoisonDamage > 0)
        //            {
        //                updateText("Player took damage", StoryIndex);
        //            }
        //            else
        //            {
        //                goto case 5;
        //            }
        //            battleStage = 5;
        //            goto case 5;

        //            #endregion

        //        case 5:
        //            # region goto victorycheck
        //            if (victoryCheck() > 0)
        //            {
        //                goto case 20; //battle is over
        //            }
        //            else
        //            {
        //                battleStage = 6;
        //                goto case 6;
        //            }

        //            #endregion

        //        case 6:
        //            # region set text and display it
        //            if (pFirst == 1) //Player was faster, do Enemy damage
        //            {
        //                battleText = (Enemy.Name + " did " + secondDamage + " to your " + Friendly[0].Name);
        //            }
        //            else //Enemy was faster, do Player damage
        //            {
        //                battleText = (Friendly[0].Name + " did " + secondDamage + " to foe's " + Enemy.Name);
        //            }

        //            updateText(battleText, StoryIndex);
        //            advanceTextbox();

        //            panel4.Visible = true;

        //            battleStage = 7;
        //            goto case 7;

        //            # endregion

        //        case 7:
        //            # region second attack
        //            if (pFirst == 1)
        //            {
        //                Friendly[0].HPCurrent -= secondDamage;
        //            }
        //            else
        //            {
        //                Enemy.HPCurrent -= secondDamage;
        //            }

        //            battleStage = 8;
        //            drawHP();
        //            break;
                    
        //            # endregion

        //        case 8:
        //            # region goto victorycheck
        //            if (victoryCheck() >0 )
        //            {
        //                goto case 20; //battle is over
        //            }
        //            else
        //            {
        //                battleStage = 9;
        //                panel4.Show();
        //                advanceTextbox();
        //                break;
        //            }
        //            # endregion

        //        case 9:
        //            # region do poison damage
        //            if (secondPoisonDamage > 0)
        //            {
        //                updateText("Player took poison damage", StoryIndex);
        //                advanceTextbox();
        //                battleStage = 10;
        //            }
        //            else
        //            {
        //                goto case 10;
        //            }
        //            break;

        //            # endregion

        //        case 10:
        //            # region goto victorycheck
        //            if (victoryCheck() > 0)
        //            {
        //                updateText("Battle finished", StoryIndex);
        //                goto case 20; //battle is over
        //            }
        //            else
        //            {
        //                battleStage = 11; //neither pokemon is KO
        //                updateText("No pokemon fainted", StoryIndex);
        //                advanceTextbox();
        //                break;
        //            }
        //            # endregion

        //        case 11:
        //            break;

        //        case 20: //show final text and appoint experience
        //            # region Pokemon KO'd
        //            if (victoryCheck() == 3) // enemy caught
        //            {
        //                Enemy.HPCurrent = 0;
        //                goto case 21;
        //            }

        //            for ( int p = 5; p >= 0; p--)
        //            {
        //                if (EnemyParty[p] != null && EnemyParty[p].HPCurrent > 0)
        //                {
        //                    Enemy = EnemyParty[p];
        //                    EnemyPartyIndex = p;
        //                    Enemy.CalcStats();
        //                    drawHP();
        //                    LoadPokemon();
        //                }
        //            }

        //            if (Enemy.HPCurrent <= 0) //couldn't find another pokemon to replace KO'd one
        //            {
        //                updateText("You won the battle", StoryIndex);
        //                updateText(ParentGame.activeSprite[0].combatTextEnd, StoryIndex);
        //                panel4.Visible = true;
        //                advanceTextbox();
        //                battleStage = 21;
        //            }
        //            else //Enemy sends out next pokemon
        //            {
        //                updateText("Enemy trainer sends out " + Enemy.Name, StoryIndex);
        //                panel4.Visible = true;
        //                advanceTextbox();
        //                battleStage = 0;
        //            }

        //            break;
        //            # endregion
                        

        //        case 21: //close window
        //            this.Close();
                    
        //            break;


        //    }
        //}

        //#endregion

        //# region Old Battle Process

        //private void mainAttack(int EMove, int FMove)
        //{
        //    int state = victoryCheck();

        //    #region first move test
        //    if (state < 1)
        //    {
        //        if (Friendly[0].Speed.NetStat > Enemy.Speed.NetStat)
        //        {
        //            FriendlyAttack();
        //            state = victoryCheck();

        //            if (state < 1)
        //            {
        //                EnemyAttack();
        //            }
        //        }
        //        else
        //        {
        //            EnemyAttack();
        //            state = victoryCheck();

        //            if (state < 1)
        //            {
        //                FriendlyAttack();
        //            }
        //        }
        //    #endregion

        //        victoryCheck();
        //        updateSheet();
        //    }
        //    if (victoryCheck() == 1)
        //    {

        //    }
        //    advanceTextbox();
        //}

        //private double FriendlyAttack()
        //{
        //    double PlayerDamage = attackTurn(Friendly[0].Level, Friendly[0].Attack.NetStat, Friendly[0].Defense.NetStat, 100);
        //    //Enemy.HPCurrent = (Enemy.HPCurrent - PlayerDamage); //this applies the damage

        //    //string attackline = Friendly.Name + " did " + PlayerDamage + " damage to foe's " + Enemy.Name + ".";
        //    //updateText(attackline, StoryIndex);
        //    return PlayerDamage;
        //}

        //private double EnemyAttack()
        //{
        //    double EnemyDamage = attackTurn(Enemy.Level, Enemy.Attack.NetStat, Friendly[0].Defense.NetStat, 100);
        //    //Friendly.HPCurrent = (Friendly.HPCurrent - EnemyDamage); //this applies the damage
            
        //    //string attackline = Enemy.Name + " did " + EnemyDamage + " damage to your " + Friendly.Name + "." + "\r";
        //    //updateText(attackline,StoryIndex);
        //    return EnemyDamage;
        //}
        
        //private double attackTurn(int Level, int Attack, int Defense, int AttackPower)
        //{
        //    double netDamage;
        //    int STAB = 1;

        //    //insert random check for critical damage.
        //    Random critChance = new Random();
        //    int crit = critChance.Next(85, 100);

        //    netDamage = (((((2 * Level / 5) + 2) * Attack * AttackPower / Defense) / 50) + 2) * STAB * crit / 100;
            
        //    return netDamage;
        //}

        //#endregion

        //#region updating methods
        //private void updateText(string AddText, int index)
        //{
        //    cycletext(1); //just to check that storyIndex has no fragments
        //    if (StoryIndex < 10)
        //    {
        //        StoryText[index] = AddText;
        //        StoryIndex++;
        //    }
        //}

        //private void pokemonFainted() //empty just now
        //{

        //}

        //private void updateSheet()
        //{
        //    labelEnemyName.Text = Enemy.Name;
        //    labelFriendlyName.Text = Friendly[0].Name;

        //    #region victory results
        //    switch (victoryCheck())
        //    {
        //        case 1:
        //            panel2.Visible = false;
        //            textBox1.Visible = true;
        //            updateText(victoryResult,StoryIndex);
        //            long experienceGained = (long)((Enemy.baseExperience * Enemy.Level) * 1.5  * 1.5);
        //            Friendly[0].Experience += experienceGained;
        //            Friendly[0].Level = (int)(Friendly[0].Experience / 50) + 1;
        //            break;

        //        case 2:
        //            panel2.Visible = false;
        //            textBox1.Visible = true;
        //            updateText(victoryResult,StoryIndex);
        //            break;

        //        case 3:
        //            updateText(victoryResult, StoryIndex);
        //            break;

        //    }
        //    #endregion

        //    drawHP();
        //    button1.Visible = true;
        //    button1.Focus();
        //}

        //private void drawHP()
        //{
        //    # region generic loaders
        //    double xratio = (panel5.Width / 120);
        //    double yratio = (panel5.Height / 30);
        //    int EnWidth = 0; //
        //    int PWidth = 0;
        //    sprites imageCropper = new sprites(); //new sprite for it's image cropping method
        //    Rectangle hpCrop = new Rectangle(0, 18, 48, 7); //
        //    Image HPbarNet = imageCropper.cropSprite(Properties.Resources.HP_bars, hpCrop);
        //    # endregion

        //    # region Enemy image calculation and drawing
        //    Bitmap bmpEnemy = new Bitmap(Properties.Resources.EnemyBar);            //Enemy background info image
        //    Graphics g = Graphics.FromImage(bmpEnemy);
        //    EnWidth = (int)(xratio * 48 * Enemy.HPCurrent / Enemy.HP.NetStat);    //ratio X imagesize X HP percentage
        //    if (EnWidth < 0)
        //    {
        //        EnWidth = 0;
        //    }
        //    Rectangle hpSize = new Rectangle((int)(xratio * 50), (int)(yratio * 20), EnWidth, 4);
        //    g.DrawImage(HPbarNet, hpSize);
        //    # endregion

        //    # region Player image calculation and drawin
        //    yratio = (panel3.Height / 41);
        //    Bitmap bmpFriend = new Bitmap(Properties.Resources.PlayerBar);  //Player background info image
        //    Graphics p = Graphics.FromImage(bmpFriend);                     //create Graphics to edit image
        //    PWidth = (int)(xratio * 48 * Friendly[0].HPCurrent / Friendly[0].HP.NetStat); //
        //    if (PWidth < 0)
        //    {
        //        PWidth = 0;
        //    }
        //    hpSize = new Rectangle((int)(xratio * 62), (int)(yratio * 20), PWidth, 4);
        //    p.DrawImage(HPbarNet, hpSize);
        //    # endregion


        //    panel3.BackgroundImage = bmpFriend;
        //    panel5.BackgroundImage = bmpEnemy;

        //    healthcurrent1Label.Text = Friendly[0].HPCurrent.ToString();
        //    healthmax1Label.Text = Friendly[0].HP.NetStat.ToString();
        //}

        //private int victoryCheck()
        //{
        //    int state = 0;

        //    if (caught == 1)
        //    {
        //        victoryResult = Enemy.Name + " was caught.";
        //        if (caughtPokemon.Count < 1) { caughtPokemon.Add(Enemy);}
        //        state = 3;
        //    }
        //    else if (Friendly[0].HPCurrent <= 0)
        //    {
        //        victoryResult = "You lose.";
        //        state = 2;
        //    }
        //    else if (Enemy.HPCurrent <= 0)
        //    {
        //        state = 1;
        //        victoryResult = "Congratulations! you knocked out your opponent.";
        //    }

        //    Debug.WriteLine("State is {0}", state);

        //    return state;
        //}        
        //# endregion
        //# endregion

        //# region calculation
        //private void getLevel(Pokemon p)
        //{
        //    p.Level = (int)(p.Experience / 50) + 1;
        //}

        //private void LoadPokemon() //Loads sprites and converts BaseStats into NetStats : grabStats
        //{
        //    sprite2Pic.Image = images.get((Enemy.ID), 0);
        //    sprite1Pic.Image = images.get((Friendly[0].ID), 0);

        //    Enemy.CalcStats();
        //    getLevel(Enemy);
        //    Friendly[0].CalcStats();
        //    getLevel(Friendly[0]);

        //    moves[0]But.Text = GetMove(Friendly[0].moves[0]);
        //    moves[1]But.Text = GetMove(Friendly[0].moves[1]);
        //    moves[2]But.Text = GetMove(Friendly[0].moves[2]);
        //    moves[3]But.Text = GetMove(Friendly[0].moves[3]);

        //}

        //private String GetMove(Move Parent)
        //{
        //    if (Parent.ID == "1")
        //    {
        //        Parent.Name = "Tackle";
        //    }

        //    return Parent.Name;
        //}

        //public int grabStats(int path, int BaseStat, int level, int IV, int EV)
        //{
        //    int netStat = 0;

        //    if (path == 2)
        //    {
        //        netStat = ((((IV + 2) * BaseStat) + (EV / 4)) * level / 100) + 10 + level;
        //    }
        //    else
        //    {
        //        netStat = (((((IV + 2) * BaseStat) + (EV / 4)) * level / 100) + 5);
        //    }
        //    return netStat;
        //}

        //private double catchProcess(Pokemon target, Pokeball ball)
        //{
        //    double caughtNet = 0;

        //    double ballModifier = (double)ball.Power;
        //    int CatchRate = target.catchRate;
        //    int MaxHP = target.HP.NetStat;
        //    double HP = target.HPCurrent;
        //    int StatusModifier = 1;

        //    if (ball.Name == "Masterball")
        //    {
        //        caughtNet = 250;
        //    }
        //    else
        //    {
        //        caughtNet = ((((3 * MaxHP) - (2 * HP)) * (CatchRate * ballModifier) / (3 * MaxHP)) * StatusModifier) * 100;

        //        caughtNet = 0;
        //    }


        //    return caughtNet;
        //}

        //private double ballStrength()
        //{
        //    double strength = 2;

        //    return strength;
        //}

        //private void updateBallInventory()
        //{
        //    int[] locations = new int[250];
        //    Pokeball[] temparray = new Pokeball[250];
        //    int currentindex = 0;

        //    for (int i = 0; i < 250; i++)
        //    {
        //        if (pokeBallsNet[i] != null)
        //        {
        //            if (pokeBallsNet[i].count == 0)
        //            {
        //                pokeBallsNet[i] = null;
        //            }
        //            else
        //            {
        //                temparray[currentindex] = pokeBallsNet[i];
        //                locations[currentindex] = i;
        //                currentindex++;
        //            }
        //        }
        //    }
            
        //    pokeBallsNet = temparray;
        //}

        //# endregion

        //# region activators/buttons

        private void move1But_Click(object sender, EventArgs e) //attack
        {
        //    //mainAttack(60, 60); //Both pokemon attack with moves that have 60 power.
        //    grabMoves(60, 60);
        }

        private void move2But_Click(object sender, EventArgs e) //throw pokeball button
        {
        //    //int index = comboBox1.Items.IndexOf(comboBox1.Text);
        //    //throwPokeball(index);
        }

        private void move3But_Click(object sender, EventArgs e) //empty
        {

        }

        private void move4But_Click(object sender, EventArgs e) //empty
        {

        }

        private void button1_Click(object sender, EventArgs e) //Fight button
        {
        //    button1.Visible = false;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
        //    //moveBattleProcess();
        //    advanceTextbox();
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
        //    if (Party[1] != null)
        //    {
        //        Friendly[0] = Party[1];
        //        images sprites = new images();
        //        sprite1Pic.Image = images.get((Friendly[0].ID), 0);
        //        Friendly[0].CalcStats();
        //        getLevel(Friendly[0]);
        //        updateSheet();
        //    }
        }

        //private void keyPressed()
        //{
        //    //advanceTextbox();
            
        //}

        //# endregion

        //# region textbox stuff
        //private int cycletext(int function)
        //{
        //    //if function is 0, it will update the text. 
        //    //if function is 1 then it will just return how many lines of text there are
            
        //    string[] temp = new string[10];
        //    int empty = 0;

        //    for (int i = 0; i < 10; i++)
        //    {
        //        if (StoryText[i] != null && StoryText[i] != "" && StoryText.Length > 0)
        //        {
        //            temp[empty] = StoryText[i];
        //            empty++;
        //        }
        //    }

        //    StoryText = temp;
        //    StoryIndex = empty + 1;

        //    if (empty > 0)
        //    {
        //        if (function == 0)
        //        {
        //            #region advance text sequence
        //            StoryText[0] = StoryText[1];
        //            StoryText[1] = StoryText[2];
        //            StoryText[2] = StoryText[3];
        //            StoryText[3] = StoryText[4];
        //            StoryText[4] = StoryText[5];
        //            StoryText[5] = StoryText[6];
        //            StoryText[6] = StoryText[7];
        //            StoryText[7] = StoryText[8];
        //            StoryText[8] = StoryText[9];
        //            StoryText[9] = "";


        //            #endregion
        //        }
        //    }
        //    else
        //    {
        //        StoryIndex = 0;
        //    }

        //    return empty;
        //}

        //private void advanceTextbox()
        //{
        //    if (cycletext(1) > 0)
        //    {
        //        button2.Text = StoryText[0];
        //        cycletext(0);
        //        button2.Focus();
        //    }
        //    else
        //    {
        //        panel4.Visible = false;
        //    }
        //}
        //# endregion

        # endregion

        # region blanking out

        //# region processes

        //public void throwPokeball(int i)
        //{
        //    # region
        //    if (i < 0)
        //    {
        //        //fail
        //    }
        //    else if (pokeBallsNet[i] != null)
        //    {
        //        if (pokeBallsNet[i].count < 1)
        //        {
        //            updateText("You don't have any pokeballs", StoryIndex);
        //        }
        //        else if (pokeBallsNet[i].count > 0)
        //        {
        //            updateText("You threw a " + pokeBallsNet[i].Name, StoryIndex);

        //            panel4.Visible = true;
        //            Random CatchChance = new Random();

        //            if (CatchChance.Next(0, 250) < catchProcess(Enemy, pokeBallsNet[i]))
        //            {
        //                caught = 1;
        //                drawHP();
        //                victoryCheck();
        //                updateText(victoryResult, StoryIndex);
        //            }
        //            else
        //            {
        //                updateText("Oh no! You almost had it", StoryIndex);
        //            }

        //            pokeBallsNet[i].decreaseCount(1); //remove 1 pokeball
        //            updateBallInventory();
        //        }

        //    }
        //    else
        //    {
        //        updateText("You don't have any pokeballs", StoryIndex);
        //    }
        //    advanceTextbox();
        //    # endregion
        //}

        //# endregion

        //# region button pressing events
        //private void Buttonclick(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.C)
        //    {
        //        if (this.ActiveControl != null && onetime != 0)
        //        {
        //            pressButton(this.ActiveControl.Name);
        //        }
        //        else
        //        {
        //            onetime = 1;
        //        }
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            //pressButton(this.ActiveControl.Name);
        }

        //private void pressButton(string name)
        //{
        //    switch (name)
        //    {
        //        case "button1":
        //            button1_Click(button1, MouseEventArgs.Empty);
        //            break;

        //        case "moves[0]But":
        //            moves[0]But_Click(moves[0]But, MouseEventArgs.Empty);
        //            break;

        //        case "moves[1]But":
        //            moves[1]But_Click(moves[1]But, MouseEventArgs.Empty);
        //            break;

        //        case "moves[2]But":
        //            moves[2]But_Click(moves[2]But, MouseEventArgs.Empty);
        //            break;

        //        case "moves[3]But":
        //            moves[3]But_Click(moves[3]But, MouseEventArgs.Empty);
        //            break;

        //        case "button2":
        //            if (cycletext(1) > 0)
        //            {
        //                advanceTextbox();
        //            }
        //            else
        //            {
        //                panel4.Visible = false;
        //                moveBattleProcess();
        //            }
        //            break;

        //        default:
        //            break;
        //    }
        //}

        private void button3_Click(object sender, EventArgs e)
        {
        //    newBackpack = new Backpack();
        //    newBackpack.FormClosed += new FormClosedEventHandler(newBackpack_FormClosed);
        //    newBackpack.StartPosition = FormStartPosition.Manual;
        //    newBackpack.SetDesktopLocation(this.DesktopLocation.X + this.Width, this.DesktopLocation.Y);
        //    newBackpack.parent = this;
        //    this.Enabled = false;
        //    newBackpack.Show();
        }

        //void newBackpack_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    this.Enabled = true;
        //    updateBallInventory();
        //}
        //# endregion

        //# region battle events (new)
        //private void procBattleEnd(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        //private void procMainDamage(object sender, EventArgs e)
        //{

        //}

        //private void procSecondaryDamage(object sender, EventArgs e)
        //{

        //}

        //private void procText(object sender, EventArgs e)
        //{
            
        //}
        
        //# endregion

        private void button2_TextChanged(object sender, EventArgs e)
        {
        //    this.ActiveControl = button2;
        //    if (panel4.Visible == true)
        //    {
                
        //    }
        //    else
        //    {
        //        button1.Visible = true;
        //    }
        }
        
        private void panel4_VisibleChanged(object sender, EventArgs e)
        {
        //    if (panel4.Visible == false)
        //    {
        //        button1.Focus();
        //    }
        //    else
        //    {
        //        button2.Focus();
        //    }
        }

        # endregion
    }
}
