using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PokémonGame
{
    public partial class Shop : UserControl
    {
        private List<inventoryItem> shopItems = new List<inventoryItem>();

        public Shop()
        {
            InitializeComponent();
        }

        public Shop(Size s)
        {
            InitializeComponent();
            Size = s;
        }

        public void LoadShop(List<inventoryItem> items)
        {
            shopItems.Clear();

            foreach (inventoryItem iv in items)
            {
                shopItems.Add(iv.Clone() as inventoryItem);
            }

            refreshShop();
            Show();
        }

        private void refreshShop()
        {
            listView1.Items.Clear();

            foreach (inventoryItem iv in shopItems)
            {
                ListViewItem lvi = new ListViewItem(iv.Name);
                lvi.SubItems.Add(iv.count.ToString());

                listView1.Items.Add(lvi);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //clear lists
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //get value of items
            //get quantity of items
            //check if there is enough money to buy them
            //remove money
            //add items
        }
    }
}
