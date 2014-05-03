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
    public partial class PartyPokemon : Form
    {
        sprites loader = new sprites();
        List<Pokemon> party = new List<Pokemon>();
        Panel selectedPanel;
        public Pokemon chosenPokemon;

        public PartyPokemon()
        {
            InitializeComponent();
            Rectangle cropArea = new Rectangle(59, 37, 256, 192);
            this.BackgroundImage = SpriteLoader.cropSprite(Properties.Resources.pokémonsummaryscreen, cropArea);
            
            loadpokemon();
        }

        public void loadpokemon()
        {
            
        }

        # region object events
        # region cancel button
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Cancel_ring;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }
        # endregion

        private void panels_click(object sender, MouseEventArgs e)
        {
            //Set initial bounds to this form.
            Point pt = this.Location;

            //Adjust position relative to the clicked Panel
            pt.X += ((Panel)sender).Right;
            pt.Y += ((Panel)sender).Bottom;

            selectedPanel = (Panel)sender;
            contextMenuStrip1.Show(pt);
        }

        private void PartyPokemon_Load(object sender, EventArgs e)
        {
            for (int p = 0; p < PokeBox.Party.PokeList.Count; p++)
            {
                party.Add(PokeBox.Party.PokeList[p]);
            }

            Debug.WriteLine("There are {0} {1}", party.Count, "pokemon");

            # region display/hide boxes for pokemon
            # region second pokemon
            if (party.Count < 2) panel2.Visible = false;
            else
            {
                panel2.Visible = true;
                panel2.BackgroundImage = Properties.Resources.inactivePokemon;
            }
            #endregion

            # region third pokemon
            if (party.Count < 3) panel3.Visible = false;
            else
            {
                panel3.Visible = true;
                panel3.BackgroundImage = Properties.Resources.inactivePokemon;
            }
            # endregion

            # region fourth pokemon
            if (party.Count < 4) panel4.Visible = false;
            else
            {
                panel4.Visible = true;
                panel4.BackgroundImage = Properties.Resources.inactivePokemon;
            }
            # endregion

            # region fifth pokemon
            if (party.Count < 5) panel5.Visible = false;
            else
            {
                panel5.Visible = true;
                panel5.BackgroundImage = Properties.Resources.inactivePokemon;
            }
            # endregion

            # region sixth pokemon
            if (party.Count < 6) panel6.Visible = false;
            else
            {
                panel6.Visible = true;
                panel6.BackgroundImage = Properties.Resources.inactivePokemon;
            }
            # endregion
            # endregion

            # region iteration
            if (party.Count > 0)
            {
                spriteImage1.Image = images.get(party[0].ID, 0);
                Name1label.Text = party[0].Name;
                Level1label.Text = party[0].Level.ToString();
                MaxHPlabel1.Text = party[0].HP.NetStat.ToString();
                CurrentHPlabel1.Text = party[0].HPCurrent.ToString();
            }

            if (party.Count > 1)
            {
                spriteImage2.Image = images.get(party[1].ID, 0);
                Name2label.Text = party[1].Name;
                Level2label.Text = party[1].Level.ToString();
                MaxHPlabel2.Text = party[1].HP.NetStat.ToString();
                CurrentHPlabel2.Text = party[1].HPCurrent.ToString();
            }

            if (party.Count > 2)
            {
                spriteImage3.Image = images.get(party[2].ID, 0);
                Name3label.Text = party[2].Name;
                Level3label.Text = party[2].Level.ToString();
                MaxHPlabel3.Text = party[2].HP.NetStat.ToString();
                CurrentHPlabel3.Text = party[2].HPCurrent.ToString();
            }

            if (party.Count > 3)
            {
                spriteImage4.Image = images.get(party[3].ID, 0);
                Name4label.Text = party[3].Name;
                Level4label.Text = party[3].Level.ToString();
                MaxHPlabel4.Text = party[3].HP.NetStat.ToString();
                CurrentHPlabel4.Text = party[3].HPCurrent.ToString();
            }

            if (party.Count > 4)
            {
                spriteImage5.Image = images.get(party[4].ID, 0);
                Name5label.Text = party[4].Name;
                Level5label.Text = party[4].Level.ToString();
                MaxHPlabel5.Text = party[4].HP.NetStat.ToString();
                CurrentHPlabel5.Text = party[4].HPCurrent.ToString();
            }

            if (party.Count > 5)
            {
                spriteImage6.Image = images.get(party[5].ID, 0);
                Name6label.Text = party[5].Name;
                Level6label.Text = party[5].Level.ToString();
                MaxHPlabel6.Text = party[5].HP.NetStat.ToString();
                CurrentHPlabel6.Text = party[5].HPCurrent.ToString();
            }
            #endregion
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Insert link to chosen Panel.

            # region switch (selectedPanel.Name)
            switch (selectedPanel.Name)
            {
                case "panel1":
                    chosenPokemon = party[0];
                    break;

                case "panel2":
                    chosenPokemon = party[1];
                    break;

                case "panel3":
                    chosenPokemon = party[2];
                    break;

                case "panel4":
                    chosenPokemon = party[3];
                    break;

                case "panel5":
                    chosenPokemon = party[4];
                    break;

                case "panel6":
                    chosenPokemon = party[5];
                    break;
            }
            # endregion

            DialogResult = DialogResult.OK;
            Close();
        }
        # endregion
    }
}
