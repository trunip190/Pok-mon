﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace PokémonGame
{
    public partial class Form1 : Form
    {
        public PictureBox p = new PictureBox();

        public Form1()
        {
            InitializeComponent();

            p.Size = new Size(this.Width, this.Height - 80);
            p.Click += new EventHandler(p_Click);
            this.Controls.Add(p);

            this.pokedex1TableAdapter1.Fill(this.pokedexDataSet1.pokedex1);

            # region load items and moves
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            path = path.Remove(0, 6); //remove "file:/" from directory name

            Properties.Settings.Default.AppDir = path;

            XmlTextReader reader;

            if (File.Exists(path + @"\Resources\ItemList.xml"))
            {
                reader = new XmlTextReader(path + @"\Resources\ItemList.xml");
                ItemList Potions = new ItemList(reader);
                reader.Close();

                serialiseItems(Potions);
            }
            else { Debug.WriteLine(path + @"\Resources\ItemList.xml"); }
            
            if (File.Exists(path + @"\Resources\MoveList.xml"))
            {
                reader = new XmlTextReader(path + @"\Resources\MoveList.xml");
                MoveList.Load(reader);
                reader.Close();
            }
            else { Debug.WriteLine(path + @"\Resources\MoveList.xml"); }
            # endregion

            PokemonLookup_fill();
            spriteList_fill();
            pokeSprites_fill();
            PokeBox.init();
            testImages();

            spriteList.LoadSprites();
        }

        private void serialiseItems(ItemList items)
        {
            
        }

        # region buttons
        private void move1But_Click(object sender, EventArgs e) //First Button - Opens Game Window 
        {
            GameWindow GameWindow1 = new GameWindow();
            GameWindow1.Show();
        }

        private void move2But_Click(object sender, EventArgs e) //Second Button - Database loading/editing form 
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void move3But_Click(object sender, EventArgs e) //Third Button - Old Battle Window 
        {
            List<Pokemon> Enemy = new List<Pokemon>();
            Enemy.Add(PokemonLookup.get(int.Parse(textEnemy.Text))); //sets the id of the pokemon to battle from the textbox
            Enemy[0].CalcStats();

            PokeBox.add(PokemonLookup.get(int.Parse(textFriend.Text))); //set the id of pokemon to battle with from the other textbox
            PokeBox.Party.PokeList[0].CalcStats();

            BattleForm BattleForm2 = new BattleForm(Enemy);
            BattleForm2.Show();
        }

        private void move4But_Click(object sender, EventArgs e) //Fourth Button - New Encounter Form
        {
            List<Pokemon> enemy = new List<Pokemon>();
            List<Pokemon> team = new List<Pokemon>();

            enemy.Add(PokemonLookup.get(16));
            enemy[0].Level = 1;
            enemy[0].CalcStats();
            team.Add(PokemonLookup.get(25));
            team[0].CalcStats();

            EncounterForm Encounter = new EncounterForm(enemy, team);

            Encounter.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sName = comboBox1.Text;
            spriteClass s = spriteList.get(sName);

            if (s != null)
            {
                p.Show();

                p.Image = s.SpriteSheet();
                p.BringToFront();
            }

        }
        # endregion

        private void PokemonLookup_fill()
        {
            int c = pokedexDataSet1.Tables[0].Rows.Count;

            for (int i = 1; i <= c; i++)
            {
                PokemonLookup.add(createPokemon(i));
            }
        }

        private void spriteList_fill()
        {
            # region assemble path
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            path += @"\Resources\TileSets\TreeList.xml";
            path = path.Remove(0, 6); //remove "file:/" from directory name
            # endregion

            if (File.Exists(path))
            {
                XmlTextReader reader = new XmlTextReader(path);

                # region needs an xml file that contains sprite data [ID, Name, xAnim, yAnim, height, width, etc.]
                # region add trees
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Tree")
                    {
                        spriteClass tree = new spriteClass();

                        Dictionary<string, string> values = ParseTree(reader.ReadSubtree());

                        tree.Name = values["Name"];
                        tree.xStart = int.Parse(values["xStart"]);
                        tree.yStart = int.Parse(values["yStart"]);
                        tree.Width = int.Parse(values["xIter"]) - 1;
                        tree.Height = int.Parse(values["yIter"]) - 1;

                        Rectangle r = new Rectangle(tree.xStart * 16, tree.yStart * 16, 16, 16);
                        tree.sprites = SpriteLoader.loadsprites(Properties.Resources.trees, r, 16, 16, tree.Width, tree.Height);

                        spriteList.add(tree);
                        comboBox1.Items.Add(tree.Name);
                    }
                }
                # endregion
                # endregion
            }
        }

        private void pokeSprites_fill()
        {
            # region sprites
            images.add(001, 0, PokémonGame.Properties.Resources._001bulbasaur);
            images.add(002, 0, PokémonGame.Properties.Resources._002ivysaur);
            images.add(003, 1, PokémonGame.Properties.Resources._003venusaur_f);
            images.add(003, 0, PokémonGame.Properties.Resources._003venusaur);
            images.add(004, 0, PokémonGame.Properties.Resources._004charmander);
            images.add(005, 0, PokémonGame.Properties.Resources._005charmeleon);
            images.add(006, 0, PokémonGame.Properties.Resources._006charizard);
            images.add(007, 0, PokémonGame.Properties.Resources._007squirtle);
            images.add(008, 0, PokémonGame.Properties.Resources._008wartortle);
            images.add(009, 0, PokémonGame.Properties.Resources._009blastoise);
            images.add(010, 0, PokémonGame.Properties.Resources._010caterpie);
            images.add(011, 0, PokémonGame.Properties.Resources._011metapod);
            images.add(012, 1, PokémonGame.Properties.Resources._012butterfree_f);
            images.add(012, 0, PokémonGame.Properties.Resources._012butterfree);
            images.add(013, 0, PokémonGame.Properties.Resources._013weedle);
            images.add(014, 0, PokémonGame.Properties.Resources._014kakuna);
            images.add(015, 0, PokémonGame.Properties.Resources._015beedrill);
            images.add(016, 0, PokémonGame.Properties.Resources._016pidgey);
            images.add(017, 0, PokémonGame.Properties.Resources._017pidgeotto);
            images.add(018, 0, PokémonGame.Properties.Resources._018pidgeot);
            images.add(019, 1, PokémonGame.Properties.Resources._019rattata_f);
            images.add(019, 0, PokémonGame.Properties.Resources._019rattata);
            images.add(020, 1, PokémonGame.Properties.Resources._020raticate_f);
            images.add(020, 0, PokémonGame.Properties.Resources._020raticate);
            images.add(021, 0, PokémonGame.Properties.Resources._021spearow);
            images.add(022, 0, PokémonGame.Properties.Resources._022fearow);
            images.add(023, 0, PokémonGame.Properties.Resources._023ekans);
            images.add(024, 0, PokémonGame.Properties.Resources._024arbok);
            images.add(025, 1, PokémonGame.Properties.Resources._025pikachu_f);
            images.add(025, 0, PokémonGame.Properties.Resources._025pikachu);
            images.add(026, 1, PokémonGame.Properties.Resources._026raichu_f);
            images.add(026, 0, PokémonGame.Properties.Resources._026raichu);
            images.add(027, 0, PokémonGame.Properties.Resources._027sandshrew);
            images.add(028, 0, PokémonGame.Properties.Resources._028sandslash);
            images.add(029, 0, PokémonGame.Properties.Resources._029nidoranf);
            images.add(030, 0, PokémonGame.Properties.Resources._030nidorina);
            images.add(031, 0, PokémonGame.Properties.Resources._031nidoqueen);
            images.add(032, 0, PokémonGame.Properties.Resources._032nidoranm);
            images.add(033, 0, PokémonGame.Properties.Resources._033nidorino);
            images.add(034, 0, PokémonGame.Properties.Resources._034nidoking);
            images.add(035, 0, PokémonGame.Properties.Resources._035clefairy);
            images.add(036, 0, PokémonGame.Properties.Resources._036clefable);
            images.add(037, 0, PokémonGame.Properties.Resources._037vulpix);
            images.add(038, 0, PokémonGame.Properties.Resources._038ninetales);
            images.add(039, 0, PokémonGame.Properties.Resources._039jigglypuff);
            images.add(040, 0, PokémonGame.Properties.Resources._040wigglytuff);
            images.add(041, 1, PokémonGame.Properties.Resources._041zubat_f);
            images.add(041, 0, PokémonGame.Properties.Resources._041zubat);
            images.add(042, 1, PokémonGame.Properties.Resources._042golbat_f);
            images.add(042, 0, PokémonGame.Properties.Resources._042golbat);
            images.add(043, 0, PokémonGame.Properties.Resources._043oddish);
            images.add(044, 1, PokémonGame.Properties.Resources._044gloom_f);
            images.add(044, 0, PokémonGame.Properties.Resources._044gloom);
            images.add(045, 1, PokémonGame.Properties.Resources._045vileplume_f);
            images.add(045, 0, PokémonGame.Properties.Resources._045vileplume);
            images.add(046, 0, PokémonGame.Properties.Resources._046paras);
            images.add(047, 0, PokémonGame.Properties.Resources._047parasect);
            images.add(048, 0, PokémonGame.Properties.Resources._048venonat);
            images.add(049, 0, PokémonGame.Properties.Resources._049venomoth);
            images.add(050, 0, PokémonGame.Properties.Resources._050diglett);
            images.add(051, 0, PokémonGame.Properties.Resources._051dugtrio);
            images.add(052, 0, PokémonGame.Properties.Resources._052meowth);
            images.add(053, 0, PokémonGame.Properties.Resources._053persian);
            images.add(054, 0, PokémonGame.Properties.Resources._054psyduck);
            images.add(055, 0, PokémonGame.Properties.Resources._055golduck);
            images.add(056, 0, PokémonGame.Properties.Resources._056mankey);
            images.add(057, 0, PokémonGame.Properties.Resources._057primeape);
            images.add(058, 0, PokémonGame.Properties.Resources._058growlithe);
            images.add(059, 0, PokémonGame.Properties.Resources._059arcanine);
            images.add(060, 0, PokémonGame.Properties.Resources._060poliwag);
            images.add(061, 0, PokémonGame.Properties.Resources._061poliwhirl);
            images.add(062, 0, PokémonGame.Properties.Resources._062poliwrath);
            images.add(063, 0, PokémonGame.Properties.Resources._063abra);
            images.add(064, 1, PokémonGame.Properties.Resources._064kadabra_f);
            images.add(064, 0, PokémonGame.Properties.Resources._064kadabra);
            images.add(065, 1, PokémonGame.Properties.Resources._065alakazam_f);
            images.add(065, 0, PokémonGame.Properties.Resources._065alakazam);
            images.add(066, 0, PokémonGame.Properties.Resources._066machop);
            images.add(067, 0, PokémonGame.Properties.Resources._067machoke);
            images.add(068, 0, PokémonGame.Properties.Resources._068machamp);
            images.add(069, 0, PokémonGame.Properties.Resources._069bellsprout);
            images.add(070, 0, PokémonGame.Properties.Resources._070weepinbell);
            images.add(071, 0, PokémonGame.Properties.Resources._071victreebel);
            images.add(072, 0, PokémonGame.Properties.Resources._072tentacool);
            images.add(073, 0, PokémonGame.Properties.Resources._073tentacruel);
            images.add(074, 0, PokémonGame.Properties.Resources._074geodude);
            images.add(075, 0, PokémonGame.Properties.Resources._075graveler);
            images.add(076, 0, PokémonGame.Properties.Resources._076golem);
            images.add(077, 0, PokémonGame.Properties.Resources._077ponyta);
            images.add(078, 0, PokémonGame.Properties.Resources._078rapidash);
            images.add(079, 0, PokémonGame.Properties.Resources._079slowpoke);
            images.add(080, 0, PokémonGame.Properties.Resources._080slowbro);
            images.add(081, 0, PokémonGame.Properties.Resources._081magnemite);
            images.add(082, 0, PokémonGame.Properties.Resources._082magneton);
            images.add(083, 0, PokémonGame.Properties.Resources._083farfetchd);
            images.add(084, 1, PokémonGame.Properties.Resources._084doduo_f);
            images.add(084, 0, PokémonGame.Properties.Resources._084doduo);
            images.add(085, 1, PokémonGame.Properties.Resources._085dodrio_f);
            images.add(085, 0, PokémonGame.Properties.Resources._085dodrio);
            images.add(086, 0, PokémonGame.Properties.Resources._086seel);
            images.add(087, 0, PokémonGame.Properties.Resources._087dewgong);
            images.add(088, 0, PokémonGame.Properties.Resources._088grimer);
            images.add(089, 0, PokémonGame.Properties.Resources._089muk);
            images.add(090, 0, PokémonGame.Properties.Resources._090shellder);
            images.add(091, 0, PokémonGame.Properties.Resources._091cloyster);
            images.add(092, 0, PokémonGame.Properties.Resources._092gastly);
            images.add(093, 0, PokémonGame.Properties.Resources._093haunter);
            images.add(094, 0, PokémonGame.Properties.Resources._094gengar);
            images.add(095, 0, PokémonGame.Properties.Resources._095onix);
            images.add(096, 0, PokémonGame.Properties.Resources._096drowzee);
            images.add(097, 1, PokémonGame.Properties.Resources._097hypno_f);
            images.add(097, 0, PokémonGame.Properties.Resources._097hypno);
            images.add(098, 0, PokémonGame.Properties.Resources._098krabby);
            images.add(099, 0, PokémonGame.Properties.Resources._099kingler);
            images.add(100, 0, PokémonGame.Properties.Resources._100voltorb);
            images.add(101, 0, PokémonGame.Properties.Resources._101electrode);
            images.add(102, 0, PokémonGame.Properties.Resources._102exeggcute);
            images.add(103, 0, PokémonGame.Properties.Resources._103exeggutor);
            images.add(104, 0, PokémonGame.Properties.Resources._104cubone);
            images.add(105, 0, PokémonGame.Properties.Resources._105marowak);
            images.add(106, 0, PokémonGame.Properties.Resources._106hitmonlee);
            images.add(107, 0, PokémonGame.Properties.Resources._107hitmonchan);
            images.add(108, 0, PokémonGame.Properties.Resources._108lickitung);
            images.add(109, 0, PokémonGame.Properties.Resources._109koffing);
            images.add(110, 0, PokémonGame.Properties.Resources._110weezing);
            images.add(111, 1, PokémonGame.Properties.Resources._111rhyhorn_f);
            images.add(111, 0, PokémonGame.Properties.Resources._111rhyhorn);
            images.add(112, 1, PokémonGame.Properties.Resources._112rhydon_f);
            images.add(112, 0, PokémonGame.Properties.Resources._112rhydon);
            images.add(113, 0, PokémonGame.Properties.Resources._113chansey);
            images.add(114, 0, PokémonGame.Properties.Resources._114tangela);
            images.add(115, 0, PokémonGame.Properties.Resources._115kangaskhan);
            images.add(116, 0, PokémonGame.Properties.Resources._116horsea);
            images.add(117, 0, PokémonGame.Properties.Resources._117seadra);
            images.add(118, 1, PokémonGame.Properties.Resources._118goldeen_f);
            images.add(118, 0, PokémonGame.Properties.Resources._118goldeen);
            images.add(119, 1, PokémonGame.Properties.Resources._119seaking_f);
            images.add(119, 0, PokémonGame.Properties.Resources._119seaking);
            images.add(120, 0, PokémonGame.Properties.Resources._120staryu);
            images.add(121, 0, PokémonGame.Properties.Resources._121starmie);
            images.add(122, 0, PokémonGame.Properties.Resources._122mrmime);
            images.add(123, 1, PokémonGame.Properties.Resources._123scyther_f);
            images.add(123, 0, PokémonGame.Properties.Resources._123scyther);
            images.add(124, 0, PokémonGame.Properties.Resources._124jynx);
            images.add(125, 0, PokémonGame.Properties.Resources._125electabuzz);
            images.add(126, 0, PokémonGame.Properties.Resources._126magmar);
            images.add(127, 0, PokémonGame.Properties.Resources._127pinsir);
            images.add(128, 0, PokémonGame.Properties.Resources._128tauros);
            images.add(129, 1, PokémonGame.Properties.Resources._129magikarp_f);
            images.add(129, 0, PokémonGame.Properties.Resources._129magikarp);
            images.add(130, 1, PokémonGame.Properties.Resources._130gyarados_f);
            images.add(130, 0, PokémonGame.Properties.Resources._130gyarados);
            images.add(131, 0, PokémonGame.Properties.Resources._131lapras);
            images.add(132, 0, PokémonGame.Properties.Resources._132ditto);
            images.add(133, 0, PokémonGame.Properties.Resources._133eevee);
            images.add(134, 0, PokémonGame.Properties.Resources._134vaporeon);
            images.add(135, 0, PokémonGame.Properties.Resources._135jolteon);
            images.add(136, 0, PokémonGame.Properties.Resources._136flareon);
            images.add(137, 0, PokémonGame.Properties.Resources._137porygon);
            images.add(138, 0, PokémonGame.Properties.Resources._138omanyte);
            images.add(139, 0, PokémonGame.Properties.Resources._139omastar);
            images.add(140, 0, PokémonGame.Properties.Resources._140kabuto);
            images.add(141, 0, PokémonGame.Properties.Resources._141kabutops);
            images.add(142, 0, PokémonGame.Properties.Resources._142aerodactyl);
            images.add(143, 0, PokémonGame.Properties.Resources._143snorlax);
            images.add(144, 0, PokémonGame.Properties.Resources._144articuno);
            images.add(145, 0, PokémonGame.Properties.Resources._145zapdos);
            images.add(146, 0, PokémonGame.Properties.Resources._146moltres);
            images.add(147, 0, PokémonGame.Properties.Resources._147dratini);
            images.add(148, 0, PokémonGame.Properties.Resources._148dragonair);
            images.add(149, 0, PokémonGame.Properties.Resources._149dragonite);
            images.add(150, 0, PokémonGame.Properties.Resources._150mewtwo);
            images.add(151, 0, PokémonGame.Properties.Resources._151mew);
            images.add(152, 0, PokémonGame.Properties.Resources._152chikorita);
            images.add(153, 0, PokémonGame.Properties.Resources._153bayleef);
            images.add(154, 1, PokémonGame.Properties.Resources._154meganium_f);
            images.add(154, 0, PokémonGame.Properties.Resources._154meganium);
            images.add(155, 0, PokémonGame.Properties.Resources._155cyndaquil);
            images.add(156, 0, PokémonGame.Properties.Resources._156quilava);
            images.add(157, 0, PokémonGame.Properties.Resources._157typhlosion);
            images.add(158, 0, PokémonGame.Properties.Resources._158totodile);
            images.add(159, 0, PokémonGame.Properties.Resources._159croconaw);
            images.add(160, 0, PokémonGame.Properties.Resources._160feraligatr);
            images.add(161, 0, PokémonGame.Properties.Resources._161sentret);
            images.add(162, 0, PokémonGame.Properties.Resources._162furret);
            images.add(163, 0, PokémonGame.Properties.Resources._163hoothoot);
            images.add(164, 0, PokémonGame.Properties.Resources._164noctowl);
            images.add(165, 1, PokémonGame.Properties.Resources._165ledyba_f);
            images.add(165, 0, PokémonGame.Properties.Resources._165ledyba);
            images.add(166, 1, PokémonGame.Properties.Resources._166ledian_f);
            images.add(166, 0, PokémonGame.Properties.Resources._166ledian);
            images.add(167, 0, PokémonGame.Properties.Resources._167spinarak);
            images.add(168, 0, PokémonGame.Properties.Resources._168ariados);
            images.add(169, 0, PokémonGame.Properties.Resources._169crobat);
            images.add(170, 0, PokémonGame.Properties.Resources._170chinchou);
            images.add(171, 0, PokémonGame.Properties.Resources._171lanturn);
            images.add(172, 0, PokémonGame.Properties.Resources._172pichu);
            images.add(173, 0, PokémonGame.Properties.Resources._173cleffa);
            images.add(174, 0, PokémonGame.Properties.Resources._174igglybuff);
            images.add(175, 0, PokémonGame.Properties.Resources._175togepi);
            images.add(176, 0, PokémonGame.Properties.Resources._176togetic);
            images.add(177, 0, PokémonGame.Properties.Resources._177natu);
            images.add(178, 1, PokémonGame.Properties.Resources._178xatu_f);
            images.add(178, 0, PokémonGame.Properties.Resources._178xatu);
            images.add(179, 0, PokémonGame.Properties.Resources._179mareep);
            images.add(180, 0, PokémonGame.Properties.Resources._180flaaffy);
            images.add(181, 0, PokémonGame.Properties.Resources._181ampharos);
            images.add(182, 0, PokémonGame.Properties.Resources._182bellossom);
            images.add(183, 0, PokémonGame.Properties.Resources._183marill);
            images.add(184, 0, PokémonGame.Properties.Resources._184azumarill);
            images.add(185, 1, PokémonGame.Properties.Resources._185sudowoodo_f);
            images.add(185, 0, PokémonGame.Properties.Resources._185sudowoodo);
            images.add(186, 1, PokémonGame.Properties.Resources._186politoed_f);
            images.add(186, 0, PokémonGame.Properties.Resources._186politoed);
            images.add(187, 0, PokémonGame.Properties.Resources._187hoppip);
            images.add(188, 0, PokémonGame.Properties.Resources._188skiploom);
            images.add(189, 0, PokémonGame.Properties.Resources._189jumpluff);
            images.add(190, 1, PokémonGame.Properties.Resources._190aipom_f);
            images.add(190, 0, PokémonGame.Properties.Resources._190aipom);
            images.add(191, 0, PokémonGame.Properties.Resources._191sunkern);
            images.add(192, 0, PokémonGame.Properties.Resources._192sunflora);
            images.add(193, 0, PokémonGame.Properties.Resources._193yanma);
            images.add(194, 1, PokémonGame.Properties.Resources._194wooper_f);
            images.add(194, 0, PokémonGame.Properties.Resources._194wooper);
            images.add(195, 1, PokémonGame.Properties.Resources._195quagsire_f);
            images.add(195, 0, PokémonGame.Properties.Resources._195quagsire);
            images.add(196, 0, PokémonGame.Properties.Resources._196espeon);
            images.add(197, 0, PokémonGame.Properties.Resources._197umbreon);
            images.add(198, 1, PokémonGame.Properties.Resources._198murkrow_f);
            images.add(198, 0, PokémonGame.Properties.Resources._198murkrow);
            images.add(199, 0, PokémonGame.Properties.Resources._199slowking);
            images.add(200, 0, PokémonGame.Properties.Resources._200misdreavus);
            images.add(201, 1, PokémonGame.Properties.Resources._201unown_a);
            images.add(201, 2, PokémonGame.Properties.Resources._201unown_b);
            images.add(201, 3, PokémonGame.Properties.Resources._201unown_c);
            images.add(201, 4, PokémonGame.Properties.Resources._201unown_d);
            images.add(201, 5, PokémonGame.Properties.Resources._201unown_e);
            images.add(201, 6, PokémonGame.Properties.Resources._201unown_em);
            images.add(201, 7, PokémonGame.Properties.Resources._201unown_f);
            images.add(201, 8, PokémonGame.Properties.Resources._201unown_g);
            images.add(201, 9, PokémonGame.Properties.Resources._201unown_h);
            images.add(201, 10, PokémonGame.Properties.Resources._201unown_i);
            images.add(201, 11, PokémonGame.Properties.Resources._201unown_j);
            images.add(201, 12, PokémonGame.Properties.Resources._201unown_k);
            images.add(201, 13, PokémonGame.Properties.Resources._201unown_l);
            images.add(201, 14, PokémonGame.Properties.Resources._201unown_m);
            images.add(201, 15, PokémonGame.Properties.Resources._201unown_n);
            images.add(201, 16, PokémonGame.Properties.Resources._201unown_o);
            images.add(201, 17, PokémonGame.Properties.Resources._201unown_p);
            images.add(201, 18, PokémonGame.Properties.Resources._201unown_q);
            images.add(201, 19, PokémonGame.Properties.Resources._201unown_qm);
            images.add(201, 20, PokémonGame.Properties.Resources._201unown_r);
            images.add(201, 21, PokémonGame.Properties.Resources._201unown_s);
            images.add(201, 22, PokémonGame.Properties.Resources._201unown_t);
            images.add(201, 23, PokémonGame.Properties.Resources._201unown_u);
            images.add(201, 24, PokémonGame.Properties.Resources._201unown_v);
            images.add(201, 25, PokémonGame.Properties.Resources._201unown_w);
            images.add(201, 26, PokémonGame.Properties.Resources._201unown_x);
            images.add(201, 27, PokémonGame.Properties.Resources._201unown_y);
            images.add(201, 28, PokémonGame.Properties.Resources._201unown_z);
            images.add(201, 0, PokémonGame.Properties.Resources._201unown);
            images.add(202, 1, PokémonGame.Properties.Resources._202wobbuffet_f);
            images.add(202, 0, PokémonGame.Properties.Resources._202wobbuffet);
            images.add(203, 1, PokémonGame.Properties.Resources._203girafarig_f);
            images.add(203, 0, PokémonGame.Properties.Resources._203girafarig);
            images.add(204, 0, PokémonGame.Properties.Resources._204pineco);
            images.add(205, 0, PokémonGame.Properties.Resources._205forretress);
            images.add(206, 0, PokémonGame.Properties.Resources._206dunsparce);
            images.add(207, 1, PokémonGame.Properties.Resources._207gligar_f);
            images.add(207, 0, PokémonGame.Properties.Resources._207gligar);
            images.add(208, 1, PokémonGame.Properties.Resources._208steelix_f);
            images.add(208, 0, PokémonGame.Properties.Resources._208steelix);
            images.add(209, 0, PokémonGame.Properties.Resources._209snubbull);
            images.add(210, 0, PokémonGame.Properties.Resources._210granbull);
            images.add(211, 0, PokémonGame.Properties.Resources._211qwilfish);
            images.add(212, 1, PokémonGame.Properties.Resources._212scizor_f);
            images.add(212, 0, PokémonGame.Properties.Resources._212scizor);
            images.add(213, 0, PokémonGame.Properties.Resources._213shuckle);
            images.add(214, 1, PokémonGame.Properties.Resources._214heracross_f);
            images.add(214, 0, PokémonGame.Properties.Resources._214heracross);
            images.add(215, 1, PokémonGame.Properties.Resources._215sneasel_f);
            images.add(215, 0, PokémonGame.Properties.Resources._215sneasel);
            images.add(216, 0, PokémonGame.Properties.Resources._216teddiursa);
            images.add(217, 1, PokémonGame.Properties.Resources._217ursaring_f);
            images.add(217, 0, PokémonGame.Properties.Resources._217ursaring);
            images.add(218, 0, PokémonGame.Properties.Resources._218slugma);
            images.add(219, 0, PokémonGame.Properties.Resources._219magcargo);
            images.add(220, 0, PokémonGame.Properties.Resources._220swinub);
            images.add(221, 1, PokémonGame.Properties.Resources._221piloswine_f);
            images.add(221, 0, PokémonGame.Properties.Resources._221piloswine);
            images.add(222, 0, PokémonGame.Properties.Resources._222corsola);
            images.add(223, 0, PokémonGame.Properties.Resources._223remoraid);
            images.add(224, 1, PokémonGame.Properties.Resources._224octillery_f);
            images.add(224, 0, PokémonGame.Properties.Resources._224octillery);
            images.add(225, 0, PokémonGame.Properties.Resources._225delibird);
            images.add(226, 0, PokémonGame.Properties.Resources._226mantine);
            images.add(227, 0, PokémonGame.Properties.Resources._227skarmory);
            images.add(228, 0, PokémonGame.Properties.Resources._228houndour);
            images.add(229, 1, PokémonGame.Properties.Resources._229houndoom_f);
            images.add(229, 0, PokémonGame.Properties.Resources._229houndoom);
            images.add(230, 0, PokémonGame.Properties.Resources._230kingdra);
            images.add(231, 0, PokémonGame.Properties.Resources._231phanpy);
            images.add(232, 1, PokémonGame.Properties.Resources._232donphan_f);
            images.add(232, 0, PokémonGame.Properties.Resources._232donphan);
            images.add(233, 0, PokémonGame.Properties.Resources._233porygon2);
            images.add(234, 0, PokémonGame.Properties.Resources._234stantler);
            images.add(235, 0, PokémonGame.Properties.Resources._235smeargle);
            images.add(236, 0, PokémonGame.Properties.Resources._236tyrogue);
            images.add(237, 0, PokémonGame.Properties.Resources._237hitmontop);
            images.add(238, 0, PokémonGame.Properties.Resources._238smoochum);
            images.add(239, 0, PokémonGame.Properties.Resources._239elekid);
            images.add(240, 0, PokémonGame.Properties.Resources._240magby);
            images.add(241, 0, PokémonGame.Properties.Resources._241miltank);
            images.add(242, 0, PokémonGame.Properties.Resources._242blissey);
            images.add(243, 0, PokémonGame.Properties.Resources._243raikou);
            images.add(244, 0, PokémonGame.Properties.Resources._244entei);
            images.add(245, 0, PokémonGame.Properties.Resources._245suicune);
            images.add(246, 0, PokémonGame.Properties.Resources._246larvitar);
            images.add(247, 0, PokémonGame.Properties.Resources._247pupitar);
            images.add(248, 0, PokémonGame.Properties.Resources._248tyranitar);
            images.add(249, 0, PokémonGame.Properties.Resources._249lugia);
            images.add(250, 0, PokémonGame.Properties.Resources._250hooh);
            images.add(251, 0, PokémonGame.Properties.Resources._251celebi);
            images.add(252, 0, PokémonGame.Properties.Resources._252treecko);
            images.add(253, 0, PokémonGame.Properties.Resources._253grovyle);
            images.add(254, 0, PokémonGame.Properties.Resources._254sceptile);
            images.add(255, 0, PokémonGame.Properties.Resources._255torchic);
            images.add(256, 1, PokémonGame.Properties.Resources._256combusken_f);
            images.add(256, 0, PokémonGame.Properties.Resources._256combusken);
            images.add(257, 1, PokémonGame.Properties.Resources._257blaziken_f);
            images.add(257, 0, PokémonGame.Properties.Resources._257blaziken);
            images.add(258, 0, PokémonGame.Properties.Resources._258mudkip);
            images.add(259, 0, PokémonGame.Properties.Resources._259marshtomp);
            images.add(260, 0, PokémonGame.Properties.Resources._260swampert);
            images.add(261, 0, PokémonGame.Properties.Resources._261poochyena);
            images.add(262, 0, PokémonGame.Properties.Resources._262mightyena);
            images.add(263, 0, PokémonGame.Properties.Resources._263zigzagoon);
            images.add(264, 0, PokémonGame.Properties.Resources._264linoone);
            images.add(265, 0, PokémonGame.Properties.Resources._265wurmple);
            images.add(266, 0, PokémonGame.Properties.Resources._266silcoon);
            images.add(267, 1, PokémonGame.Properties.Resources._267beautifly_f);
            images.add(267, 0, PokémonGame.Properties.Resources._267beautifly);
            images.add(268, 0, PokémonGame.Properties.Resources._268cascoon);
            images.add(269, 1, PokémonGame.Properties.Resources._269dustox_f);
            images.add(269, 0, PokémonGame.Properties.Resources._269dustox);
            images.add(270, 0, PokémonGame.Properties.Resources._270lotad);
            images.add(271, 0, PokémonGame.Properties.Resources._271lombre);
            images.add(272, 1, PokémonGame.Properties.Resources._272ludicolo_f);
            images.add(272, 0, PokémonGame.Properties.Resources._272ludicolo);
            images.add(273, 0, PokémonGame.Properties.Resources._273seedot);
            images.add(274, 1, PokémonGame.Properties.Resources._274nuzleaf_f);
            images.add(274, 0, PokémonGame.Properties.Resources._274nuzleaf);
            images.add(275, 1, PokémonGame.Properties.Resources._275shiftry_f);
            images.add(275, 0, PokémonGame.Properties.Resources._275shiftry);
            images.add(276, 0, PokémonGame.Properties.Resources._276taillow);
            images.add(277, 0, PokémonGame.Properties.Resources._277swellow);
            images.add(278, 0, PokémonGame.Properties.Resources._278wingull);
            images.add(279, 0, PokémonGame.Properties.Resources._279pelipper);
            images.add(280, 0, PokémonGame.Properties.Resources._280ralts);
            images.add(281, 0, PokémonGame.Properties.Resources._281kirlia);
            images.add(282, 0, PokémonGame.Properties.Resources._282gardevoir);
            images.add(283, 0, PokémonGame.Properties.Resources._283surskit);
            images.add(284, 0, PokémonGame.Properties.Resources._284masquerain);
            images.add(285, 0, PokémonGame.Properties.Resources._285shroomish);
            images.add(286, 0, PokémonGame.Properties.Resources._286breloom);
            images.add(287, 0, PokémonGame.Properties.Resources._287slakoth);
            images.add(288, 0, PokémonGame.Properties.Resources._288vigoroth);
            images.add(289, 0, PokémonGame.Properties.Resources._289slaking);
            images.add(290, 0, PokémonGame.Properties.Resources._290nincada);
            images.add(291, 0, PokémonGame.Properties.Resources._291ninjask);
            images.add(292, 0, PokémonGame.Properties.Resources._292shedinja);
            images.add(293, 0, PokémonGame.Properties.Resources._293whismur);
            images.add(294, 0, PokémonGame.Properties.Resources._294loudred);
            images.add(295, 0, PokémonGame.Properties.Resources._295exploud);
            images.add(296, 0, PokémonGame.Properties.Resources._296makuhita);
            images.add(297, 0, PokémonGame.Properties.Resources._297hariyama);
            images.add(298, 0, PokémonGame.Properties.Resources._298azurill);
            images.add(299, 0, PokémonGame.Properties.Resources._299nosepass);
            images.add(300, 0, PokémonGame.Properties.Resources._300skitty);
            images.add(301, 0, PokémonGame.Properties.Resources._301delcatty);
            images.add(302, 0, PokémonGame.Properties.Resources._302sableye);
            images.add(303, 0, PokémonGame.Properties.Resources._303mawile);
            images.add(304, 0, PokémonGame.Properties.Resources._304aron);
            images.add(305, 0, PokémonGame.Properties.Resources._305lairon);
            images.add(306, 0, PokémonGame.Properties.Resources._306aggron);
            images.add(307, 1, PokémonGame.Properties.Resources._307meditite_f);
            images.add(307, 0, PokémonGame.Properties.Resources._307meditite);
            images.add(308, 1, PokémonGame.Properties.Resources._308medicham_f);
            images.add(308, 0, PokémonGame.Properties.Resources._308medicham);
            images.add(309, 0, PokémonGame.Properties.Resources._309electrike);
            images.add(310, 0, PokémonGame.Properties.Resources._310manectric);
            images.add(311, 0, PokémonGame.Properties.Resources._311plusle);
            images.add(312, 0, PokémonGame.Properties.Resources._312minun);
            images.add(313, 0, PokémonGame.Properties.Resources._313volbeat);
            images.add(314, 0, PokémonGame.Properties.Resources._314illumise);
            images.add(315, 1, PokémonGame.Properties.Resources._315roselia_f);
            images.add(315, 0, PokémonGame.Properties.Resources._315roselia);
            images.add(316, 1, PokémonGame.Properties.Resources._316gulpin_f);
            images.add(316, 0, PokémonGame.Properties.Resources._316gulpin);
            images.add(317, 1, PokémonGame.Properties.Resources._317swalot_f);
            images.add(317, 0, PokémonGame.Properties.Resources._317swalot);
            images.add(318, 0, PokémonGame.Properties.Resources._318carvanha);
            images.add(319, 0, PokémonGame.Properties.Resources._319sharpedo);
            images.add(320, 0, PokémonGame.Properties.Resources._320wailmer);
            images.add(321, 0, PokémonGame.Properties.Resources._321wailord);
            images.add(322, 1, PokémonGame.Properties.Resources._322numel_f);
            images.add(322, 0, PokémonGame.Properties.Resources._322numel);
            images.add(323, 1, PokémonGame.Properties.Resources._323camerupt_f);
            images.add(323, 0, PokémonGame.Properties.Resources._323camerupt);
            images.add(324, 0, PokémonGame.Properties.Resources._324torkoal);
            images.add(325, 0, PokémonGame.Properties.Resources._325spoink);
            images.add(326, 0, PokémonGame.Properties.Resources._326grumpig);
            images.add(327, 0, PokémonGame.Properties.Resources._327spinda);
            images.add(328, 0, PokémonGame.Properties.Resources._328trapinch);
            images.add(329, 0, PokémonGame.Properties.Resources._329vibrava);
            images.add(330, 0, PokémonGame.Properties.Resources._330flygon);
            images.add(331, 0, PokémonGame.Properties.Resources._331cacnea);
            images.add(332, 1, PokémonGame.Properties.Resources._332cacturne_f);
            images.add(332, 0, PokémonGame.Properties.Resources._332cacturne);
            images.add(333, 0, PokémonGame.Properties.Resources._333swablu);
            images.add(334, 0, PokémonGame.Properties.Resources._334altaria);
            images.add(335, 0, PokémonGame.Properties.Resources._335zangoose);
            images.add(336, 0, PokémonGame.Properties.Resources._336seviper);
            images.add(337, 0, PokémonGame.Properties.Resources._337lunatone);
            images.add(338, 0, PokémonGame.Properties.Resources._338solrock);
            images.add(339, 0, PokémonGame.Properties.Resources._339barboach);
            images.add(340, 0, PokémonGame.Properties.Resources._340whiscash);
            images.add(341, 0, PokémonGame.Properties.Resources._341corphish);
            images.add(342, 0, PokémonGame.Properties.Resources._342crawdaunt);
            images.add(343, 0, PokémonGame.Properties.Resources._343baltoy);
            images.add(344, 0, PokémonGame.Properties.Resources._344claydol);
            images.add(345, 0, PokémonGame.Properties.Resources._345lileep);
            images.add(346, 0, PokémonGame.Properties.Resources._346cradily);
            images.add(347, 0, PokémonGame.Properties.Resources._347anorith);
            images.add(348, 0, PokémonGame.Properties.Resources._348armaldo);
            images.add(349, 0, PokémonGame.Properties.Resources._349feebas);
            images.add(350, 1, PokémonGame.Properties.Resources._350milotic_f);
            images.add(350, 0, PokémonGame.Properties.Resources._350milotic);
            images.add(351, 1, PokémonGame.Properties.Resources._351castform_fire);
            images.add(351, 2, PokémonGame.Properties.Resources._351castform_ice);
            images.add(351, 3, PokémonGame.Properties.Resources._351castform_water);
            images.add(351, 0, PokémonGame.Properties.Resources._351castform);
            images.add(352, 0, PokémonGame.Properties.Resources._352kecleon);
            images.add(353, 0, PokémonGame.Properties.Resources._353shuppet);
            images.add(354, 0, PokémonGame.Properties.Resources._354banette);
            images.add(355, 0, PokémonGame.Properties.Resources._355duskull);
            images.add(356, 0, PokémonGame.Properties.Resources._356dusclops);
            images.add(357, 0, PokémonGame.Properties.Resources._357tropius);
            images.add(358, 0, PokémonGame.Properties.Resources._358chimecho);
            images.add(359, 0, PokémonGame.Properties.Resources._359absol);
            images.add(360, 0, PokémonGame.Properties.Resources._360wynaut);
            images.add(361, 0, PokémonGame.Properties.Resources._361snorunt);
            images.add(362, 0, PokémonGame.Properties.Resources._362glalie);
            images.add(363, 0, PokémonGame.Properties.Resources._363spheal);
            images.add(364, 0, PokémonGame.Properties.Resources._364sealeo);
            images.add(365, 0, PokémonGame.Properties.Resources._365walrein);
            images.add(366, 0, PokémonGame.Properties.Resources._366clamperl);
            images.add(367, 0, PokémonGame.Properties.Resources._367huntail);
            images.add(368, 0, PokémonGame.Properties.Resources._368gorebyss);
            images.add(369, 1, PokémonGame.Properties.Resources._369relicanth_f);
            images.add(369, 0, PokémonGame.Properties.Resources._369relicanth);
            images.add(370, 0, PokémonGame.Properties.Resources._370luvdisc);
            images.add(371, 0, PokémonGame.Properties.Resources._371bagon);
            images.add(372, 0, PokémonGame.Properties.Resources._372shelgon);
            images.add(373, 0, PokémonGame.Properties.Resources._373salamence);
            images.add(374, 0, PokémonGame.Properties.Resources._374beldum);
            images.add(375, 0, PokémonGame.Properties.Resources._375metang);
            images.add(376, 0, PokémonGame.Properties.Resources._376metagross);
            images.add(377, 0, PokémonGame.Properties.Resources._377regirock);
            images.add(378, 0, PokémonGame.Properties.Resources._378regice);
            images.add(379, 0, PokémonGame.Properties.Resources._379registeel);
            images.add(380, 0, PokémonGame.Properties.Resources._380latias);
            images.add(381, 0, PokémonGame.Properties.Resources._381latios);
            images.add(382, 0, PokémonGame.Properties.Resources._382kyogre);
            images.add(383, 0, PokémonGame.Properties.Resources._383groudon);
            images.add(384, 0, PokémonGame.Properties.Resources._384rayquaza);
            images.add(385, 0, PokémonGame.Properties.Resources._385jirachi);
            images.add(386, 1, PokémonGame.Properties.Resources._386deoxys_em);
            images.add(386, 2, PokémonGame.Properties.Resources._386deoxys_fr);
            images.add(386, 3, PokémonGame.Properties.Resources._386deoxys_lg);
            images.add(386, 4, PokémonGame.Properties.Resources._386deoxys_rs);
            images.add(386, 0, PokémonGame.Properties.Resources._386deoxys);
            images.add(387, 0, PokémonGame.Properties.Resources._387turtwig);
            images.add(388, 0, PokémonGame.Properties.Resources._388grotle);
            images.add(389, 0, PokémonGame.Properties.Resources._389torterra);
            images.add(390, 0, PokémonGame.Properties.Resources._390chimchar);
            images.add(391, 0, PokémonGame.Properties.Resources._391monferno);
            images.add(392, 0, PokémonGame.Properties.Resources._392infernape);
            images.add(393, 0, PokémonGame.Properties.Resources._393piplup);
            images.add(394, 0, PokémonGame.Properties.Resources._394prinplup);
            images.add(395, 0, PokémonGame.Properties.Resources._395empoleon);
            images.add(396, 1, PokémonGame.Properties.Resources._396starly_f);
            images.add(396, 0, PokémonGame.Properties.Resources._396starly);
            images.add(397, 1, PokémonGame.Properties.Resources._397staravia_f);
            images.add(397, 0, PokémonGame.Properties.Resources._397staravia);
            images.add(398, 1, PokémonGame.Properties.Resources._398staraptor_f);
            images.add(398, 0, PokémonGame.Properties.Resources._398staraptor);
            images.add(399, 1, PokémonGame.Properties.Resources._399bidoof_f);
            images.add(399, 0, PokémonGame.Properties.Resources._399bidoof);
            images.add(400, 1, PokémonGame.Properties.Resources._400bibarel_f);
            images.add(400, 0, PokémonGame.Properties.Resources._400bibarel);
            images.add(401, 1, PokémonGame.Properties.Resources._401kricketot_f);
            images.add(401, 0, PokémonGame.Properties.Resources._401kricketot);
            images.add(402, 1, PokémonGame.Properties.Resources._402kricketune_f);
            images.add(402, 0, PokémonGame.Properties.Resources._402kricketune);
            images.add(403, 1, PokémonGame.Properties.Resources._403shinx_f);
            images.add(403, 0, PokémonGame.Properties.Resources._403shinx);
            images.add(404, 1, PokémonGame.Properties.Resources._404luxio_f);
            images.add(404, 0, PokémonGame.Properties.Resources._404luxio);
            images.add(405, 1, PokémonGame.Properties.Resources._405luxray_f);
            images.add(405, 0, PokémonGame.Properties.Resources._405luxray);
            images.add(406, 0, PokémonGame.Properties.Resources._406budew);
            images.add(407, 1, PokémonGame.Properties.Resources._407roserade_f);
            images.add(407, 0, PokémonGame.Properties.Resources._407roserade);
            images.add(408, 0, PokémonGame.Properties.Resources._408cranidos);
            images.add(409, 0, PokémonGame.Properties.Resources._409rampardos);
            images.add(410, 0, PokémonGame.Properties.Resources._410shieldon);
            images.add(411, 0, PokémonGame.Properties.Resources._411bastiodon);
            images.add(412, 1, PokémonGame.Properties.Resources._412burmy_grass);
            images.add(412, 2, PokémonGame.Properties.Resources._412burmy_ground);
            images.add(412, 3, PokémonGame.Properties.Resources._412burmy_steel);
            images.add(412, 0, PokémonGame.Properties.Resources._412burmy);
            images.add(413, 1, PokémonGame.Properties.Resources._413wormadam_grass);
            images.add(413, 2, PokémonGame.Properties.Resources._413wormadam_ground);
            images.add(413, 3, PokémonGame.Properties.Resources._413wormadam_steel);
            images.add(413, 0, PokémonGame.Properties.Resources._413wormadam);
            images.add(414, 0, PokémonGame.Properties.Resources._414mothim);
            images.add(415, 1, PokémonGame.Properties.Resources._415combee_f);
            images.add(415, 0, PokémonGame.Properties.Resources._415combee);
            images.add(416, 0, PokémonGame.Properties.Resources._416vespiquen);
            images.add(417, 1, PokémonGame.Properties.Resources._417pachirisu_f);
            images.add(417, 0, PokémonGame.Properties.Resources._417pachirisu);
            images.add(418, 0, PokémonGame.Properties.Resources._418buizel);
            images.add(419, 0, PokémonGame.Properties.Resources._419floatzel);
            images.add(420, 0, PokémonGame.Properties.Resources._420cherubi);
            images.add(421, 1, PokémonGame.Properties.Resources._421cherrim_overcast);
            images.add(421, 2, PokémonGame.Properties.Resources._421cherrim_sunny);
            images.add(421, 0, PokémonGame.Properties.Resources._421cherrim);
            images.add(422, 1, PokémonGame.Properties.Resources._422shellos_east);
            images.add(422, 2, PokémonGame.Properties.Resources._422shellos_west);
            images.add(422, 0, PokémonGame.Properties.Resources._422shellos);
            images.add(423, 1, PokémonGame.Properties.Resources._423gastrodon_east);
            images.add(423, 2, PokémonGame.Properties.Resources._423gastrodon_west);
            images.add(423, 0, PokémonGame.Properties.Resources._423gastrodon);
            images.add(424, 1, PokémonGame.Properties.Resources._424ambipom_f);
            images.add(424, 0, PokémonGame.Properties.Resources._424ambipom);
            images.add(425, 0, PokémonGame.Properties.Resources._425drifloon);
            images.add(426, 0, PokémonGame.Properties.Resources._426drifblim);
            images.add(427, 0, PokémonGame.Properties.Resources._427buneary);
            images.add(428, 0, PokémonGame.Properties.Resources._428lopunny);
            images.add(429, 0, PokémonGame.Properties.Resources._429mismagius);
            images.add(430, 0, PokémonGame.Properties.Resources._430honchkrow);
            images.add(431, 0, PokémonGame.Properties.Resources._431glameow);
            images.add(432, 0, PokémonGame.Properties.Resources._432purugly);
            images.add(433, 0, PokémonGame.Properties.Resources._433chingling);
            images.add(434, 0, PokémonGame.Properties.Resources._434stunky);
            images.add(435, 0, PokémonGame.Properties.Resources._435skuntank);
            images.add(436, 0, PokémonGame.Properties.Resources._436bronzor);
            images.add(437, 0, PokémonGame.Properties.Resources._437bronzong);
            images.add(438, 0, PokémonGame.Properties.Resources._438bonsly);
            images.add(439, 0, PokémonGame.Properties.Resources._439mimejr);
            images.add(440, 0, PokémonGame.Properties.Resources._440happiny);
            images.add(441, 0, PokémonGame.Properties.Resources._441chatot);
            images.add(442, 0, PokémonGame.Properties.Resources._442spiritomb);
            images.add(443, 1, PokémonGame.Properties.Resources._443gible_f);
            images.add(443, 0, PokémonGame.Properties.Resources._443gible);
            images.add(444, 1, PokémonGame.Properties.Resources._444gabite_f);
            images.add(444, 0, PokémonGame.Properties.Resources._444gabite);
            images.add(445, 1, PokémonGame.Properties.Resources._445garchomp_f);
            images.add(445, 0, PokémonGame.Properties.Resources._445garchomp);
            images.add(446, 0, PokémonGame.Properties.Resources._446munchlax);
            images.add(447, 0, PokémonGame.Properties.Resources._447riolu);
            images.add(448, 0, PokémonGame.Properties.Resources._448lucario);
            images.add(449, 1, PokémonGame.Properties.Resources._449hippopotas_f);
            images.add(449, 0, PokémonGame.Properties.Resources._449hippopotas);
            images.add(450, 1, PokémonGame.Properties.Resources._450hippowdon_f);
            images.add(450, 0, PokémonGame.Properties.Resources._450hippowdon);
            images.add(451, 0, PokémonGame.Properties.Resources._451skorupi);
            images.add(452, 0, PokémonGame.Properties.Resources._452drapion);
            images.add(453, 1, PokémonGame.Properties.Resources._453croagunk_f);
            images.add(453, 0, PokémonGame.Properties.Resources._453croagunk);
            images.add(454, 1, PokémonGame.Properties.Resources._454toxicroak_f);
            images.add(454, 0, PokémonGame.Properties.Resources._454toxicroak);
            images.add(455, 0, PokémonGame.Properties.Resources._455carnivine);
            images.add(456, 1, PokémonGame.Properties.Resources._456finneon_f);
            images.add(456, 0, PokémonGame.Properties.Resources._456finneon);
            images.add(457, 1, PokémonGame.Properties.Resources._457lumineon_f);
            images.add(457, 0, PokémonGame.Properties.Resources._457lumineon);
            images.add(458, 0, PokémonGame.Properties.Resources._458mantyke);
            images.add(459, 1, PokémonGame.Properties.Resources._459snover_f);
            images.add(459, 0, PokémonGame.Properties.Resources._459snover);
            images.add(460, 1, PokémonGame.Properties.Resources._460abomasnow_f);
            images.add(460, 0, PokémonGame.Properties.Resources._460abomasnow);
            images.add(461, 1, PokémonGame.Properties.Resources._461weavile_f);
            images.add(461, 0, PokémonGame.Properties.Resources._461weavile);
            images.add(462, 0, PokémonGame.Properties.Resources._462magnezone);
            images.add(463, 0, PokémonGame.Properties.Resources._463lickilicky);
            images.add(464, 1, PokémonGame.Properties.Resources._464rhyperior_f);
            images.add(464, 0, PokémonGame.Properties.Resources._464rhyperior);
            images.add(465, 1, PokémonGame.Properties.Resources._465tangrowth_f);
            images.add(465, 0, PokémonGame.Properties.Resources._465tangrowth);
            images.add(466, 0, PokémonGame.Properties.Resources._466electivire);
            images.add(467, 0, PokémonGame.Properties.Resources._467magmortar);
            images.add(468, 0, PokémonGame.Properties.Resources._468togekiss);
            images.add(469, 0, PokémonGame.Properties.Resources._469yanmega);
            images.add(470, 0, PokémonGame.Properties.Resources._470leafeon);
            images.add(471, 0, PokémonGame.Properties.Resources._471glaceon);
            images.add(472, 0, PokémonGame.Properties.Resources._472gliscor);
            images.add(473, 1, PokémonGame.Properties.Resources._473mamoswine_f);
            images.add(473, 0, PokémonGame.Properties.Resources._473mamoswine);
            images.add(474, 0, PokémonGame.Properties.Resources._474porygonz);
            images.add(475, 0, PokémonGame.Properties.Resources._475gallade);
            images.add(476, 0, PokémonGame.Properties.Resources._476probopass);
            images.add(477, 0, PokémonGame.Properties.Resources._477dusknoir);
            images.add(478, 0, PokémonGame.Properties.Resources._478froslass);
            images.add(479, 1, PokémonGame.Properties.Resources._479rotom_fire);
            images.add(479, 2, PokémonGame.Properties.Resources._479rotom_flying);
            images.add(479, 3, PokémonGame.Properties.Resources._479rotom_grass);
            images.add(479, 4, PokémonGame.Properties.Resources._479rotom_ice);
            images.add(479, 5, PokémonGame.Properties.Resources._479rotom_water);
            images.add(479, 0, PokémonGame.Properties.Resources._479rotom);
            images.add(480, 0, PokémonGame.Properties.Resources._480uxie);
            images.add(481, 0, PokémonGame.Properties.Resources._481mesprit);
            images.add(482, 0, PokémonGame.Properties.Resources._482azelf);
            images.add(483, 0, PokémonGame.Properties.Resources._483dialga);
            images.add(484, 0, PokémonGame.Properties.Resources._484palkia);
            images.add(485, 0, PokémonGame.Properties.Resources._485heatran);
            images.add(486, 0, PokémonGame.Properties.Resources._486regigigas);
            images.add(487, 1, PokémonGame.Properties.Resources._487giratina_altered);
            images.add(487, 2, PokémonGame.Properties.Resources._487giratina_origin);
            images.add(487, 0, PokémonGame.Properties.Resources._487giratina);
            images.add(488, 0, PokémonGame.Properties.Resources._488cresselia);
            images.add(489, 0, PokémonGame.Properties.Resources._489phione);
            images.add(490, 0, PokémonGame.Properties.Resources._490manaphy);
            images.add(491, 0, PokémonGame.Properties.Resources._491darkrai);
            images.add(492, 1, PokémonGame.Properties.Resources._492shaymin_land);
            images.add(492, 2, PokémonGame.Properties.Resources._492shaymin_sky);
            images.add(492, 0, PokémonGame.Properties.Resources._492shaymin);
            images.add(493, 1, PokémonGame.Properties.Resources._493arceus_bug);
            images.add(493, 2, PokémonGame.Properties.Resources._493arceus_dark);
            images.add(493, 3, PokémonGame.Properties.Resources._493arceus_dragon);
            images.add(493, 4, PokémonGame.Properties.Resources._493arceus_electric);
            images.add(493, 5, PokémonGame.Properties.Resources._493arceus_fighting);
            images.add(493, 6, PokémonGame.Properties.Resources._493arceus_fire);
            images.add(493, 7, PokémonGame.Properties.Resources._493arceus_flying);
            images.add(493, 8, PokémonGame.Properties.Resources._493arceus_ghost);
            images.add(493, 9, PokémonGame.Properties.Resources._493arceus_grass);
            images.add(493, 10, PokémonGame.Properties.Resources._493arceus_ground);
            images.add(493, 11, PokémonGame.Properties.Resources._493arceus_ice);
            images.add(493, 12, PokémonGame.Properties.Resources._493arceus_normal);
            images.add(493, 13, PokémonGame.Properties.Resources._493arceus_poison);
            images.add(493, 14, PokémonGame.Properties.Resources._493arceus_psychic);
            images.add(493, 15, PokémonGame.Properties.Resources._493arceus_rock);
            images.add(493, 16, PokémonGame.Properties.Resources._493arceus_steel);
            images.add(493, 17, PokémonGame.Properties.Resources._493arceus_water);
            images.add(493, 0, PokémonGame.Properties.Resources._493arceus);
            images.add(494, 0, PokémonGame.Properties.Resources._494victini);
            images.add(495, 0, PokémonGame.Properties.Resources._495snivy);
            images.add(496, 0, PokémonGame.Properties.Resources._496servine);
            images.add(497, 0, PokémonGame.Properties.Resources._497serperior);
            images.add(498, 0, PokémonGame.Properties.Resources._498tepig);
            images.add(499, 0, PokémonGame.Properties.Resources._499pignite);
            images.add(500, 0, PokémonGame.Properties.Resources._500emboar);
            images.add(501, 0, PokémonGame.Properties.Resources._501oshawott);
            images.add(502, 0, PokémonGame.Properties.Resources._502dewott);
            images.add(503, 0, PokémonGame.Properties.Resources._503samurott);
            images.add(504, 0, PokémonGame.Properties.Resources._504patrat);
            images.add(505, 0, PokémonGame.Properties.Resources._505watchog);
            images.add(506, 0, PokémonGame.Properties.Resources._506lillipup);
            images.add(507, 0, PokémonGame.Properties.Resources._507herdier);
            images.add(508, 0, PokémonGame.Properties.Resources._508stoutland);
            images.add(509, 0, PokémonGame.Properties.Resources._509purrloin);
            images.add(510, 0, PokémonGame.Properties.Resources._510liepard);
            images.add(511, 0, PokémonGame.Properties.Resources._511pansage);
            images.add(512, 0, PokémonGame.Properties.Resources._512simisage);
            images.add(513, 0, PokémonGame.Properties.Resources._513pansear);
            images.add(514, 0, PokémonGame.Properties.Resources._514simisear);
            images.add(515, 0, PokémonGame.Properties.Resources._515panpour);
            images.add(516, 0, PokémonGame.Properties.Resources._516simipour);
            images.add(517, 0, PokémonGame.Properties.Resources._517munna);
            images.add(518, 0, PokémonGame.Properties.Resources._518musharna);
            images.add(519, 0, PokémonGame.Properties.Resources._519pidove);
            images.add(520, 0, PokémonGame.Properties.Resources._520tranquill);
            images.add(521, 1, PokémonGame.Properties.Resources._521unfezant_f);
            images.add(521, 0, PokémonGame.Properties.Resources._521unfezant);
            images.add(522, 0, PokémonGame.Properties.Resources._522blitzle);
            images.add(523, 0, PokémonGame.Properties.Resources._523zebstrika);
            images.add(524, 0, PokémonGame.Properties.Resources._524roggenrola);
            images.add(525, 0, PokémonGame.Properties.Resources._525boldore);
            images.add(526, 0, PokémonGame.Properties.Resources._526gigalith);
            images.add(527, 0, PokémonGame.Properties.Resources._527woobat);
            images.add(528, 0, PokémonGame.Properties.Resources._528swoobat);
            images.add(529, 0, PokémonGame.Properties.Resources._529drilbur);
            images.add(530, 0, PokémonGame.Properties.Resources._530excadrill);
            images.add(531, 0, PokémonGame.Properties.Resources._531audino);
            images.add(532, 0, PokémonGame.Properties.Resources._532timburr);
            images.add(533, 0, PokémonGame.Properties.Resources._533gurdurr);
            images.add(534, 0, PokémonGame.Properties.Resources._534conkeldurr);
            images.add(535, 0, PokémonGame.Properties.Resources._535tympole);
            images.add(536, 0, PokémonGame.Properties.Resources._536palpitoad);
            images.add(537, 0, PokémonGame.Properties.Resources._537seismitoad);
            images.add(538, 0, PokémonGame.Properties.Resources._538throh);
            images.add(539, 0, PokémonGame.Properties.Resources._539sawk);
            images.add(540, 0, PokémonGame.Properties.Resources._540sewaddle);
            images.add(541, 0, PokémonGame.Properties.Resources._541swadloon);
            images.add(542, 0, PokémonGame.Properties.Resources._542leavanny);
            images.add(543, 0, PokémonGame.Properties.Resources._543venipede);
            images.add(544, 0, PokémonGame.Properties.Resources._544whirlipede);
            images.add(545, 0, PokémonGame.Properties.Resources._545scolipede);
            images.add(546, 0, PokémonGame.Properties.Resources._546cottonee);
            images.add(547, 0, PokémonGame.Properties.Resources._547whimsicott);
            images.add(548, 0, PokémonGame.Properties.Resources._548petilil);
            images.add(549, 0, PokémonGame.Properties.Resources._549lilligant);
            images.add(550, 1, PokémonGame.Properties.Resources._550basculin_blue);
            images.add(550, 2, PokémonGame.Properties.Resources._550basculin_red);
            images.add(550, 0, PokémonGame.Properties.Resources._550basculin);
            images.add(551, 0, PokémonGame.Properties.Resources._551sandile);
            images.add(552, 0, PokémonGame.Properties.Resources._552krokorok);
            images.add(553, 0, PokémonGame.Properties.Resources._553krookodile);
            images.add(554, 0, PokémonGame.Properties.Resources._554darumaka);
            images.add(555, 1, PokémonGame.Properties.Resources._555darmanitan_standard);
            images.add(555, 2, PokémonGame.Properties.Resources._555darmanitan_zen);
            images.add(555, 0, PokémonGame.Properties.Resources._555darmanitan);
            images.add(556, 0, PokémonGame.Properties.Resources._556maractus);
            images.add(557, 0, PokémonGame.Properties.Resources._557dwebble);
            images.add(558, 0, PokémonGame.Properties.Resources._558crustle);
            images.add(559, 0, PokémonGame.Properties.Resources._559scraggy);
            images.add(560, 0, PokémonGame.Properties.Resources._560scrafty);
            images.add(561, 0, PokémonGame.Properties.Resources._561sigilyph);
            images.add(562, 0, PokémonGame.Properties.Resources._562yamask);
            images.add(563, 0, PokémonGame.Properties.Resources._563cofagrigus);
            images.add(564, 0, PokémonGame.Properties.Resources._564tirtouga);
            images.add(565, 0, PokémonGame.Properties.Resources._565carracosta);
            images.add(566, 0, PokémonGame.Properties.Resources._566archen);
            images.add(567, 0, PokémonGame.Properties.Resources._567archeops);
            images.add(568, 0, PokémonGame.Properties.Resources._568trubbish);
            images.add(569, 0, PokémonGame.Properties.Resources._569garbodor);
            images.add(570, 0, PokémonGame.Properties.Resources._570zorua);
            images.add(571, 0, PokémonGame.Properties.Resources._571zoroark);
            images.add(572, 0, PokémonGame.Properties.Resources._572minccino);
            images.add(573, 0, PokémonGame.Properties.Resources._573cinccino);
            images.add(574, 0, PokémonGame.Properties.Resources._574gothita);
            images.add(575, 0, PokémonGame.Properties.Resources._575gothorita);
            images.add(576, 0, PokémonGame.Properties.Resources._576gothitelle);
            images.add(577, 0, PokémonGame.Properties.Resources._577solosis);
            images.add(578, 0, PokémonGame.Properties.Resources._578duosion);
            images.add(579, 0, PokémonGame.Properties.Resources._579reuniclus);
            images.add(580, 0, PokémonGame.Properties.Resources._580ducklett);
            images.add(581, 0, PokémonGame.Properties.Resources._581swanna);
            images.add(582, 0, PokémonGame.Properties.Resources._582vanillite);
            images.add(583, 0, PokémonGame.Properties.Resources._583vanillish);
            images.add(584, 0, PokémonGame.Properties.Resources._584vanilluxe);
            images.add(585, 1, PokémonGame.Properties.Resources._585deerling_autumn);
            images.add(585, 2, PokémonGame.Properties.Resources._585deerling_spring);
            images.add(585, 3, PokémonGame.Properties.Resources._585deerling_summer);
            images.add(585, 4, PokémonGame.Properties.Resources._585deerling_winter);
            images.add(585, 0, PokémonGame.Properties.Resources._585deerling);
            images.add(586, 1, PokémonGame.Properties.Resources._586sawsbuck_autumn);
            images.add(586, 2, PokémonGame.Properties.Resources._586sawsbuck_spring);
            images.add(586, 3, PokémonGame.Properties.Resources._586sawsbuck_summer);
            images.add(586, 4, PokémonGame.Properties.Resources._586sawsbuck_winter);
            images.add(586, 0, PokémonGame.Properties.Resources._586sawsbuck);
            images.add(587, 0, PokémonGame.Properties.Resources._587emolga);
            images.add(588, 0, PokémonGame.Properties.Resources._588karrablast);
            images.add(589, 0, PokémonGame.Properties.Resources._589escavalier);
            images.add(590, 0, PokémonGame.Properties.Resources._590foongus);
            images.add(591, 0, PokémonGame.Properties.Resources._591amoonguss);
            images.add(592, 1, PokémonGame.Properties.Resources._592frillish_f);
            images.add(592, 0, PokémonGame.Properties.Resources._592frillish);
            images.add(593, 1, PokémonGame.Properties.Resources._593jellicent_f);
            images.add(593, 0, PokémonGame.Properties.Resources._593jellicent);
            images.add(594, 0, PokémonGame.Properties.Resources._594alomomola);
            images.add(595, 0, PokémonGame.Properties.Resources._595joltik);
            images.add(596, 0, PokémonGame.Properties.Resources._596galvantula);
            images.add(597, 0, PokémonGame.Properties.Resources._597ferroseed);
            images.add(598, 0, PokémonGame.Properties.Resources._598ferrothorn);
            images.add(599, 0, PokémonGame.Properties.Resources._599klink);
            images.add(600, 0, PokémonGame.Properties.Resources._600klang);
            images.add(601, 0, PokémonGame.Properties.Resources._601klinklang);
            images.add(602, 0, PokémonGame.Properties.Resources._602tynamo);
            images.add(603, 0, PokémonGame.Properties.Resources._603eelektrik);
            images.add(604, 0, PokémonGame.Properties.Resources._604eelektross);
            images.add(605, 0, PokémonGame.Properties.Resources._605elgyem);
            images.add(606, 0, PokémonGame.Properties.Resources._606beheeyem);
            images.add(607, 0, PokémonGame.Properties.Resources._607litwick);
            images.add(608, 0, PokémonGame.Properties.Resources._608lampent);
            images.add(609, 0, PokémonGame.Properties.Resources._609chandelure);
            images.add(610, 0, PokémonGame.Properties.Resources._610axew);
            images.add(611, 0, PokémonGame.Properties.Resources._611fraxure);
            images.add(612, 0, PokémonGame.Properties.Resources._612haxorus);
            images.add(613, 0, PokémonGame.Properties.Resources._613cubchoo);
            images.add(614, 0, PokémonGame.Properties.Resources._614beartic);
            images.add(615, 0, PokémonGame.Properties.Resources._615cryogonal);
            images.add(616, 0, PokémonGame.Properties.Resources._616shelmet);
            images.add(617, 0, PokémonGame.Properties.Resources._617accelgor);
            images.add(618, 0, PokémonGame.Properties.Resources._618stunfisk);
            images.add(619, 0, PokémonGame.Properties.Resources._619mienfoo);
            images.add(620, 0, PokémonGame.Properties.Resources._620mienshao);
            images.add(621, 0, PokémonGame.Properties.Resources._621druddigon);
            images.add(622, 0, PokémonGame.Properties.Resources._622golett);
            images.add(623, 0, PokémonGame.Properties.Resources._623golurk);
            images.add(624, 0, PokémonGame.Properties.Resources._624pawniard);
            images.add(625, 0, PokémonGame.Properties.Resources._625bisharp);
            images.add(626, 0, PokémonGame.Properties.Resources._626bouffalant);
            images.add(627, 0, PokémonGame.Properties.Resources._627rufflet);
            images.add(628, 0, PokémonGame.Properties.Resources._628braviary);
            images.add(629, 0, PokémonGame.Properties.Resources._629vullaby);
            images.add(630, 0, PokémonGame.Properties.Resources._630mandibuzz);
            images.add(631, 0, PokémonGame.Properties.Resources._631heatmor);
            images.add(632, 0, PokémonGame.Properties.Resources._632durant);
            images.add(633, 0, PokémonGame.Properties.Resources._633deino);
            images.add(634, 0, PokémonGame.Properties.Resources._634zweilous);
            images.add(635, 0, PokémonGame.Properties.Resources._635hydreigon);
            images.add(636, 0, PokémonGame.Properties.Resources._636larvesta);
            images.add(637, 0, PokémonGame.Properties.Resources._637volcarona);
            images.add(638, 0, PokémonGame.Properties.Resources._638cobalion);
            images.add(639, 0, PokémonGame.Properties.Resources._639terrakion);
            images.add(640, 0, PokémonGame.Properties.Resources._640virizion);
            images.add(641, 0, PokémonGame.Properties.Resources._641tornadus);
            images.add(642, 0, PokémonGame.Properties.Resources._642thundurus);
            images.add(643, 0, PokémonGame.Properties.Resources._643reshiram);
            images.add(644, 0, PokémonGame.Properties.Resources._644zekrom);
            images.add(645, 0, PokémonGame.Properties.Resources._645landorus);
            images.add(646, 0, PokémonGame.Properties.Resources._646kyurem);
            images.add(647, 0, PokémonGame.Properties.Resources._647keldeo);
            images.add(648, 1, PokémonGame.Properties.Resources._648meloetta_aria);
            images.add(648, 2, PokémonGame.Properties.Resources._648meloetta_pirouette);
            images.add(648, 0, PokémonGame.Properties.Resources._648meloetta);
            images.add(649, 1, PokémonGame.Properties.Resources._649genesect_electric);
            images.add(649, 2, PokémonGame.Properties.Resources._649genesect_fire);
            images.add(649, 3, PokémonGame.Properties.Resources._649genesect_ice);
            images.add(649, 4, PokémonGame.Properties.Resources._649genesect_water);
            images.add(649, 0, PokémonGame.Properties.Resources._649genesect);


            # endregion

        }

        private Dictionary<string, string> ParseTree(XmlReader t)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            while (t.Read())
            {
                if (t.NodeType == XmlNodeType.Element)
                {
                    switch (t.Name)
                    {
                        case "Tree":
                            break;

                        default:
                            result.Add(t.Name, t.ReadElementContentAsString());
                            break;
                    }
                }
            }

            return result;
        }

        private Pokemon createPokemon(int ID)
        {
            Pokemon newPokemon = new Pokemon();

            newPokemon.ID = (ID - 1);
            newPokemon.Name = pokedexDataSet1.Tables[0].Rows[newPokemon.ID].ItemArray[2].ToString();
            newPokemon.Level = 5;
            newPokemon.Type1 = pokedexDataSet1.Tables[0].Rows[newPokemon.ID].ItemArray[3].ToString();
            newPokemon.Type2 = pokedexDataSet1.Tables[0].Rows[newPokemon.ID].ItemArray[4].ToString();

            //TODO will be moved to another static class that gives the right moves to the pokemon at that level
            newPokemon.moves[0] = MoveList.getMove("1");

            # region grab stats from database
            newPokemon.HP.BaseStat = int.Parse(pokedexDataSet1.Tables[0].Rows[newPokemon.ID].ItemArray[5].ToString());
            newPokemon.Attack.BaseStat = int.Parse(pokedexDataSet1.Tables[0].Rows[newPokemon.ID].ItemArray[6].ToString());
            newPokemon.Defense.BaseStat = int.Parse(pokedexDataSet1.Tables[0].Rows[newPokemon.ID].ItemArray[7].ToString());
            newPokemon.SpAtt.BaseStat = int.Parse(pokedexDataSet1.Tables[0].Rows[newPokemon.ID].ItemArray[8].ToString());
            newPokemon.SpDef.BaseStat = int.Parse(pokedexDataSet1.Tables[0].Rows[newPokemon.ID].ItemArray[9].ToString());
            newPokemon.Speed.BaseStat = int.Parse(pokedexDataSet1.Tables[0].Rows[newPokemon.ID].ItemArray[10].ToString());

            newPokemon.CalcStats();
            newPokemon.Heal();
            # endregion

            return newPokemon;
        }

        private void testImages()
        {
            Inventory.gallery.Add(0, Properties.Resources._001bulbasaur);
            Inventory.gallery.Add(1, Properties.Resources._002ivysaur);
            Inventory.gallery.Add(2, Properties.Resources._003venusaur);
            Inventory.gallery.Add(3, Properties.Resources._003venusaur_f);
        }

        void p_Click(object sender, EventArgs e)
        {
            p.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MapTest mt = new MapTest();
            mt.Show();
        }
    }
}
