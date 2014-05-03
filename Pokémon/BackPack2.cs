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
    public partial class BackPack2 : UserControl
    {
        public BackPack2()
        {
            InitializeComponent();
        }

        void BackPack2_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                //updateListBox1();
            }
        }

        public void updateListBox1()
        {
            if (Inventory.items.Count == 0)
            {
                Inventory.add("AT001", 1);
            }

            foreach (inventoryItem p in Inventory.items.Values)
            {
                string s = p.ItemType();

                ListViewItem item = new ListViewItem();
                item.Text = p.Name;
                item.SubItems.Add("x");
                item.SubItems.Add(p.count.ToString());
                item.SubItems.Add(p.Description);

                # region switch (s)
                switch (s)
                {
                    case "MainItem":
                        listView1.Items.Add(item);
                        break;

                    default:
                        System.Diagnostics.Debug.WriteLine(s);
                        break;
                }
                # endregion
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
