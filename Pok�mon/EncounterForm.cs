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
    public partial class EncounterForm : Form
    {
        # region variables etc.
        # region system variables
        ScreenBox screenBox1;

        public int stage = 0;

        public int Cescapes = 0;
        # endregion

        # region contestant variables
        public Pokemon currentPokemon;
        public Move meMove;
        public Move enMove;
        public int speed = 0; //1 for first, 2 for second in battle order

        public List<Pokemon> EnemyGroup = new List<Pokemon>();
        public int EI = 0;
        public List<Pokemon> FriendlyGroup = new List<Pokemon>(); //replaced by static PokeBox.Party.PokeList
        public int FI = 0;
        public List<Pokemon> bOrder = new List<Pokemon>(2);
        # endregion
        # endregion

        # region Constructors
        public EncounterForm()
        {
            InitializeComponent();
            initialise();

            # region enter pokemon into battle groups
            FriendlyGroup.Add(new Pokemon());
            EnemyGroup.Add(new Pokemon());
            # endregion
        }

        public EncounterForm(List<Pokemon> _Enemy)
        {
            InitializeComponent();

            EnemyGroup = _Enemy;

            # region needs to be re-written to directly reference Party pokemon
            foreach (KeyValuePair<int, Pokemon> t in PokeBox.Party.PokeList)
            {
                FriendlyGroup.Add(t.Value);
            }
            # endregion

            initialise();
        }

        public EncounterForm(List<Pokemon> enemy, List<Pokemon> team)
        {
            InitializeComponent();

            EnemyGroup = enemy;
            FriendlyGroup = team;

            initialise();
        }

        private void initialise()
        {
            screenBox1 = new ScreenBox(this);
            addText("A wild " + EnemyGroup[EI].Name + " appeared.");

            drawPictures();
            Pokemon_Enters();
            drawMoves();
            drawHP();
        }
        # endregion

        # region battle methods
        # region Main Battle Method
        private void BattleTurn()
        {
            # region start of turn
            double damage = 0;
            double SecondaryDamage = 0;

            # region choose enemy Move
            chooseMoveEnemy();
            # endregion

            # region calcspeed
            //addText("calcSpeed");
            calcSpeed(); //calculate speed
            # endregion
            # endregion

            //////////////////////////////////////////////////////

            # region first combatent
            
            # region calcHit() + exceptions
            if (meMove.ID == "Item") Debug.WriteLine("Player used an item"); //TODO expand item used check.
            else if (meMove.ID == "PokemonSwitch") Debug.WriteLine("Player switched pokemon"); //TODO expand pokemon switch check.
            else if (meMove.ID == "Flee") Debug.WriteLine("Player tried to flee"); //TODO expand player flee check.
            else
            {
                //use move
                addText(bOrder[0].Name + " used " + getMove(0).Name);

                if (calcHit()) //check hit 1
                {
                    # region primary damage

                    # region calcDamage
                    damage = calcDamage(0, getMove(0)); //TODO getMoves sorted, calculate damage 1.
                    # endregion

                    # region TakeDamage()
                    //ShowText();//apply damage 1
                    TakeDamage(bOrder[1], damage, false);
                    # endregion

                    drawHP(); //draw damage 1
                    # endregion

                    # region secondary damage
                    # region checkKO
                    if (!checkKO()) //pokemon hasn't been knocked out yet.
                    {
                        # region calcDamageSecondary()
                        SecondaryDamage = calcDamageSecondary(); //calculate secondary damage 1
                        # endregion

                        # region TakeDamage()
                        TakeDamage(bOrder[1], SecondaryDamage, true); //apply secondary damage 1
                        # endregion

                        drawHP();
                    }
                    # endregion
                    
                    //TODO move this.
                    # region checkKO()
                    checkKO();
                    # endregion

                    # endregion
                }
                else { addText(bOrder[0].Name + " missed."); ShowText(); }
            }
            # endregion

            drawHP(); //draw secondary damage 1

            # endregion

            //////////////////////////////////////////////////////
            damage = 0; //reset damage calculation
            SecondaryDamage = 0;
            //////////////////////////////////////////////////////

            if (!checkKO()) //if neither pokemon knocked out, then continue.
            {
                # region second combatent

                //use move
                addText(bOrder[1].Name + " used " + getMove(1).Name);

                # region calcHit()
                if (calcHit()) //check hit 2
                {
                    # region primary damage

                    # region calcDamage()
                    damage = calcDamage(1, getMove(1)); //calculate damage 2
                    # endregion

                    # region TakeDamage()
                    TakeDamage(bOrder[0], damage, false);
                    # endregion

                    drawHP(); //draw damage 2

                    # region checkKO()
                    checkKO(); //check ko/health
                    # endregion
                    # endregion

                    if (!checkKO())
                    {
                        # region secondary damage

                        # region calcDamageSecondary()
                        SecondaryDamage = calcDamageSecondary(); //calculate secondary damage 2
                        # endregion

                        # region TakeDamage()
                        TakeDamage(bOrder[0], SecondaryDamage, true); //apply secondary damage 2
                        # endregion

                        drawHP(); //draw secondary damage 2

                        # region checkKO()
                        checkKO(); //check ko/health
                        # endregion
                        # endregion
                    }
                }
                else { addText(bOrder[1].Name + " missed."); ShowText(); }
                # endregion
                # endregion
            }
            //////////////////////////////////////////////////////

            # region check if pokemon KO'd
            if (checkKO())
            {
                if (FriendlyGroup[FI].HPCurrent <= 0)
                {
                    addText("Your pokemon fainted.");
                    friendlyFainted();
                }
                else
                {
                    addText("Enemy pokemon knocked out.");
                    enemyFainted();
                }
                ShowText();
            }
            # endregion

            # region end of turn
            procTurnEnd(null, null);
            # endregion         
        }
        # endregion

        # region pokemon methods
        private void Pokemon_Enters()
        {
            # region assign moves to buttons
            buttonmove1.Text = FriendlyGroup[FI].moves[0].Name;
            buttonmove2.Text = FriendlyGroup[FI].moves[1].Name;
            buttonmove3.Text = FriendlyGroup[FI].moves[2].Name;
            buttonmove4.Text = FriendlyGroup[FI].moves[3].Name;
            # endregion

            labelEnemyName.Text = EnemyGroup[EI].Name;
            labelFriendlyName.Text = FriendlyGroup[FI].Name;
        }

        private void TakeDamage(Pokemon target, double amount, bool quiet)
        {
            if (amount > 0)
            {
                target.HPCurrent -= amount;
                addText(target.Name + " took " + amount.ToString() + " damage.");
            }
            else if (!quiet)
            {
                addText(target.Name + " didn't take any damage.");
            }
            
            ShowText();
        }

        private void chooseMoveEnemy()
        {
            enMove = EnemyGroup[EI].chooseMove();
            //addText(EnemyGroup[EI].Name + " chose to use " + enMove.Name);
            //ShowText();
        }

        private Move getMove(int c) //c for combatent
        {
            if (c == speed) { Debug.WriteLine("meMove"); return meMove; } //player 
            else { Debug.WriteLine("enMove"); return enMove; } //enemy
        }
        # endregion

        # region battle events
        private void fleeBattle()
        {
            addText("Trying to flee from enemy");
            ShowText();

            if (calcFlee()) { this.DialogResult = DialogResult.Abort; this.Close(); }
            else
            {
                meMove = new Move { ID = "Flee", Speed = 10, Name = "Flee", Power = 0 };
                BattleTurn();
            }
        }

        private void enemyFainted()
        {
            bool cont = false;
            //TODO check to see if there are any eligable pokemon to send out

            //TODO remove this skip
            if (cont) { }
            else { battleEnd(); }
        }

        private void friendlyFainted()
        {
            int poke = -1;
            for (int i = 0; i < FriendlyGroup.Count; i++)
            {
                if (FriendlyGroup[i].HPCurrent > 0)
                {
                    poke = i;
                    break;
                }
            }
            if (poke == -1)
            {
                this.Close();
            }
            else
            {
                FI = poke;
                Pokemon_Enters();
            }
        }

        private void battleEnd()
        {
            //TODO fill with conversations etc.
            this.Close();
        }
        # endregion

        # region screentext processes
        private void addText(string s)
        {
            screenBox1.addText(s);
        }

        private void ShowText() { screenBox1.ShowDialog(); }

        private void procMainDamage(object sender, EventArgs e)
        {
            screenBox1.VisibleChanged -= procMainDamage;
            
            drawHP();

            addText("secondary damage");
            screenBox1.VisibleChanged += new EventHandler(procSecondaryDamage);
        }

        private void procSecondaryDamage(object sender, EventArgs e)
        {
            screenBox1.VisibleChanged -= procSecondaryDamage;
            
            addText("Check if Pokemon is KO'd");
            //screenBox1.VisibleChanged += new EventHandler(procKOCheck);
        }

        //private void procKOCheck(object sender, EventArgs e)
        //{
        //    screenBox1.VisibleChanged -= procKOCheck;

        //    # region first/second pokemon check
        //    if (stage == 0) //first pokemon
        //    {
        //        stage = 1;
        //        addText(bOrder[1].Name + " attacked " + bOrder[0].Name);
        //        screenBox1.VisibleChanged += new EventHandler(procMainDamage);
        //    }
        //    else if (stage == 1) //second pokemon
        //    {
        //        addText("turn over");
        //        screenBox1.VisibleChanged += new EventHandler(procTurnEnd);
        //    }
        //    # endregion
        //}

        private void procTurnEnd(object sender, EventArgs e)
        {
            screenBox1.VisibleChanged -= new EventHandler(procTurnEnd);

            stage = 0;
            panel1.Enabled = false;
            panel2.Enabled = true;
        }

        private void procBattleEnd(object sender, EventArgs e)
        {
            screenBox1.VisibleChanged -= procBattleEnd;
            this.Close();
        }
        # endregion

        # region calculations
        private bool calcFlee()
        {
            Cescapes++;

            bool result = false;
            Random r = new Random();

            int A = FriendlyGroup[FI].Speed.NetStat * 32;
            int B = (int)(Math.Floor((double)(EnemyGroup[EI].Speed.NetStat / 4)));
            int C = Cescapes;
            int F = (A / B) + (30 * C);

            if (F > 255) result = true;
            else if (r.Next(255) < F) result = true;

            Debug.WriteLine("CalcFlee {0} {1}", result, F);

            return result;
        }

        private void calcSpeed()
        {
            double fSpeed = FriendlyGroup[FI].Speed.NetStat;
            double eSpeed = EnemyGroup[EI].Speed.NetStat;
            int order = 0;

            //work out how their moves affect them
            if (meMove.Speed > enMove.Speed) order = 1; //player has faster move
            if (enMove.Speed > meMove.Speed) order = 2; //enemy has faster move

            //both moves have same speed, base it on combatent speed
            if (order == 0)
            {
                if (fSpeed > eSpeed)  order = 1;  //player is faster
                else  order =2;   //enemy is faster
            }

            if (order == 1) { bOrder.Add(FriendlyGroup[FI]); bOrder.Add(EnemyGroup[EI]); speed = 0; }
            else { bOrder.Add(EnemyGroup[EI]); bOrder.Add(FriendlyGroup[FI]); speed = 1; }

            //addText(bOrder[0].Name + " attacks first.");
            //ShowText();
        }

        private bool calcHit()
        {
            bool hit = true;
            //addText("calcHit()");

            if (hit) { } //addText("Attack hit."); }
            else { addText("Attack missed."); }

            ShowText();

            return hit;
        }

        # region damage
        private double STAB(Pokemon p, Move m)
        {
            double result = 1;
                        
            if (p.Type1 == m.Type || p.Type2 == m.Type) result = 1.5;

            return result;
        }

        public double calcResist(Pokemon P, string Type)
        {
            double result = 1;

            //TODO this needs writing

            return result;
        }

        private double calcDamage(int order, Move m)
        {
            # region assign pokemon to variables
            int e = 0;
            if (order == 0) e = 1;
            Pokemon p = bOrder[order];
            Pokemon eP = bOrder[e];
            # endregion

            # region set up damage variable and random number
            double damage = 0; //damage variable
            Random critChance = new Random();
            int crit = critChance.Next(85, 100);
            # endregion

            # region Calculate which stat to use to attack with
            double Attack = 0;
            double Defense = 0;
            if (m.Category == "Physical") { Attack = p.Attack.NetStat; Defense = eP.Defense.NetStat; }
            else { Attack = p.SpAtt.NetStat; Defense = eP.SpDef.NetStat; }
            # endregion

            damage = (((((2 * p.Level / 5) + 2) * Attack * m.Power / Defense) / 50) + 2) * STAB(p, m) * calcResist(eP, m.Type) * crit / 100;

            # region calculate crit
            if (critChance.Next() > 80) { damage = damage * 1.5; } //TODO calculate crit chances etc.
            damage = Math.Ceiling(damage);
            # endregion

            //addText("calcDamage()");
            //ShowText();

            return damage;
        }

        private double calcDamageSecondary() { return 0; }
        # endregion

        private bool checkKO()
        {
            bool KO = false;

            if (FriendlyGroup[FI].HPCurrent <= 0) //Pokemon fainted
            {
                //addText(FriendlyGroup[FI].Name + " fainted"); 
                KO = true;

            }
            if (EnemyGroup[EI].HPCurrent <= 0) //second pokemon fainted.
            {
                //addText(EnemyGroup[EI].Name + " fainted");
                KO = true;
            }
            
            ShowText();

            return KO;
        }
        # endregion
        # endregion

        # region updating methods
        # region screen/display methods
        private void drawHP()
        {
            # region generic loaders
            double xratio = (panel5.Width / 120);
            double yratio = (panel5.Height / 30);
            int EnWidth = 0; //
            int PWidth = 0;
            //sprites imageCropper = new sprites(); //new sprite for it's image cropping method
            Rectangle hpCrop = new Rectangle(0, 18, 48, 7); //
            Image HPbarNet = SpriteLoader.cropSprite(Properties.Resources.HP_bars, hpCrop);
            Pokemon Enemy = EnemyGroup[EI];
            Pokemon Friendly = FriendlyGroup[FI];
            # endregion

            # region Enemy image calculation and drawing
            Bitmap bmpEnemy = new Bitmap(Properties.Resources.EnemyBar);            //Enemy background info image
            Graphics g = Graphics.FromImage(bmpEnemy);
            EnWidth = (int)(xratio * 48 * Enemy.HPCurrent / Enemy.HP.NetStat);    //ratio X imagesize X HP percentage
            if (EnWidth < 0)
            {
                EnWidth = 0;
            }
            Rectangle hpSize = new Rectangle((int)(xratio * 50), (int)(yratio * 20), EnWidth, 4);
            g.DrawImage(HPbarNet, hpSize);
            # endregion

            # region Player image calculation and drawing
            yratio = (panel3.Height / 41);
            Bitmap bmpFriend = new Bitmap(Properties.Resources.PlayerBar);  //Player background info image
            Graphics p = Graphics.FromImage(bmpFriend);                     //create Graphics to edit image
            PWidth = (int)(xratio * 48 * Friendly.HPCurrent / Friendly.HP.NetStat); //
            if (PWidth < 0)
            {
                PWidth = 0;
            }
            hpSize = new Rectangle((int)(xratio * 62), (int)(yratio * 20), PWidth, 4);
            p.DrawImage(HPbarNet, hpSize);
            # endregion

            Debug.WriteLine("{0}/{1}, {2}/{3}", Friendly.HPCurrent, Friendly.HP.NetStat, Enemy.HPCurrent, Enemy.HP.NetStat);

            panel3.BackgroundImage = bmpFriend;
            panel5.BackgroundImage = bmpEnemy;

            healthcurrent1Label.Text = Friendly.HPCurrent.ToString();
            healthmax1Label.Text = Friendly.HP.NetStat.ToString();
        }

        private void drawPictures()
        {
            pictureBoxE.Image = images.get(EnemyGroup[EI].ID, EnemyGroup[EI].variant);
            pictureBoxT.Image = images.get(FriendlyGroup[FI].ID, FriendlyGroup[FI].variant);
        }

        private void drawMoves()
        {
            if (FriendlyGroup[FI].moves[0] != null) buttonmove1.Text = FriendlyGroup[FI].moves[0].Name;
            if (FriendlyGroup[FI].moves[1] != null) buttonmove2.Text = FriendlyGroup[FI].moves[1].Name;
            if (FriendlyGroup[FI].moves[2] != null) buttonmove3.Text = FriendlyGroup[FI].moves[2].Name;
            if (FriendlyGroup[FI].moves[3] != null) buttonmove4.Text = FriendlyGroup[FI].moves[3].Name;
        }
        # endregion


        # endregion

        # region activators
        # region buttons
        private void buttonFight_click(object sender, EventArgs e)
        {
            panel1.Enabled = true; //enable move buttons
            //panel2.Enabled = false; //disable fight buttons etc.
        }

        private void buttonMove_click(object sender, EventArgs e)
        {
            # region buttonMove? switches
            Button s = sender as Button;
            switch (s.Name)
            {
                case "buttonmoves[0]":
                    meMove = FriendlyGroup[FI].moves[0];
                    break;

                case "buttonmoves[1]":
                    meMove = FriendlyGroup[FI].moves[1];
                    break;

                case "buttonmoves[2]":
                    meMove = FriendlyGroup[FI].moves[2];
                    break;

                case "buttonmoves[3]":
                    meMove = FriendlyGroup[FI].moves[3];
                    break;
            }
            # endregion

            # region disable move buttons
            panel1.Enabled = false;
            # endregion

            # region set order of combatents
            bOrder.Clear();
            //if (FriendlyGroup[FI].Speed.NetStat > EnemyGroup[EI].Speed.NetStat)
            //{
            //    bOrder.Add(FriendlyGroup[FI]);
            //    bOrder.Add(EnemyGroup[EI]);
            //}
            //else
            //{
            //    bOrder.Add(EnemyGroup[EI]);
            //    bOrder.Add(FriendlyGroup[FI]);
            //}
            # endregion

            //addText(FriendlyGroup[FI].Name + " chose to use " + meMove.Name);

            BattleTurn();
        }

        private void buttonBag_click(object sender, EventArgs e)
        {
            Backpack bag = new Backpack();
            bag.ShowDialog();
        }

        private void buttonPokemon_click(object sender, EventArgs e) { }

        private void buttonRun_click(object sender, EventArgs e) { fleeBattle(); }
        # endregion

        # region Form events
        private void EncounterForm_Shown(object sender, EventArgs e)
        {
            ShowText();
            procTurnEnd(null, null);
        }
        # endregion

        # endregion
    }
}
