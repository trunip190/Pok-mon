using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PokémonGame
{
    public partial class MainMenu : Form
    {
        PartyPokemon partyWindow;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            partyWindow = new PartyPokemon();
            partyWindow.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Backpack playerBackpack = new Backpack();
            playerBackpack.Show();
            playerBackpack.FormClosed += new FormClosedEventHandler(playerBackpack_FormClosed);
            this.Enabled = false;
        }

        void playerBackpack_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
        }
    }
}
