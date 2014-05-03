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
    public partial class Backpack : Form
    {
        public Form parent;
        public string chosenPokeball = "";
        public List<Pokeball> chosen = new List<Pokeball>(1);

        public Backpack()
        {
            InitializeComponent();
            listView1.Items.Clear();
        }

        # region activators
        private void Backpack_Click(object sender, EventArgs e)
        {
            populateBackpack();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            updateListbox();
        }
        # endregion

        private void populateBackpack() //TODO Remove test method
        {
            foreach (KeyValuePair<string, inventoryItem> i in ItemList.items)
            {
                Inventory.add(i.Key, 1);
            }

            updateListbox();
        }

        private void addPokeballs(string ID, int count) //clear - TODO replace by direct call to static class
        {
            Inventory.add(ID, count);

            updateListbox();
        }

        private void updateListbox()
        {
            listView1.Items.Clear();

            string typeCheck = "";

            Dictionary<string, string> itemCodes = new Dictionary<string, string>();

            # region itemcodes for dictionary
            itemCodes.Add("MainItems", "MainItem");
            itemCodes.Add("Medicine", "RecoveryItem");
            itemCodes.Add("TMHM", "TMItem");
            itemCodes.Add("Berries", "Berry");
            itemCodes.Add("KeyItems", "KeyItem");
            itemCodes.Add("PokeBalls", "Pokeball");
            # endregion

            if (itemCodes.ContainsKey(comboBox1.Text))
                typeCheck = itemCodes[comboBox1.Text];
            else typeCheck = "inventoryItem";
            
            //TODO rewrite so that it can show misc. Maybe sort into lists...?
            # region foreach inventoryItem in Inventory.items            
            foreach (inventoryItem p in Inventory.items.Values)
            {
                //insert if statement to ensure that p.value.type is the right type.
                if (p.ItemType() == typeCheck)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = p.Name;
                    item.SubItems.Add(p.count.ToString());
                    item.SubItems.Add(p.Description);

                    listView1.Items.Add(item);
                }
            }
            # endregion
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            string name = listView1.SelectedItems[0].Text;
            Debug.WriteLine(name);

            List<inventoryItem> items = Inventory.items.Values.ToList<inventoryItem>();
            GameWindow gWin = null;

            if (parent.GetType() == typeof(GameWindow))
            {
                gWin = parent as GameWindow;
            }

            foreach (inventoryItem i in items)
            {
                if (i.Name == name)
                {
                    int j = items.IndexOf(i);

                    if (gWin != null)
                    {
                        gWin.addText(items[j].Use());
                    }
                    else
                    {
                        items[j].Use();
                    }
                }
            }
        }
    }
}
