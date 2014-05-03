using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PokémonGame
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        static string[] SplitTextIntoChunks(string textChunks)
        {
            int posA = 0; //start position
            int posB = 0; //position of delimiter
            int i = 0;
            string[] chunk = new string[20]; //return chunk

            posB = textChunks.IndexOf(",", posA);

            //--------------------------
            char[] delimit = new char[] { ',' };
            foreach (string substr in textChunks.Split(delimit))
            {
                System.Console.WriteLine(substr);
                chunk[i] = substr;
                System.Diagnostics.Debug.WriteLine("done {0} times", i);
                i++;
            }
            //--------------------------
            return chunk;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Create new open file dialog box, limit it to txt files, and show it
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = "Text files (*.txt)|*.txt";
            OpenFileDialog1.ShowDialog();

            //create a new string array and load it with all the lines from the file chosen
            string fileName = OpenFileDialog1.FileName;
            TextReader PokeList = new StreamReader(fileName);
                       
            
            string Line = "";// PokeList.ReadLine();
            //Line = PokeList.ReadLine();

            string[] attributes = new string[20]; //pulled from readline() method
            int one = 0; //ID
            string two = ""; //Name
            string three = ""; //Type1
            string four = ""; //Type2
            int five = 0; //HP
            int six = 0; //Attack
            int seven = 0; //Defense
            int eight = 0; //Sp.Att
            int nine = 0; //Sp.Def
            int ten = 0; //Speed
            
            for (int i = 0; i < 663; i++ )//663; i++)
            {
                Line = PokeList.ReadLine();
                attributes = SplitTextIntoChunks(Line);
                one = int.Parse(attributes[0]);     //ID
                two = attributes[1];                //Name
                three = attributes[2];              //Type1
                four = attributes[3];               //Type2
                five = int.Parse(attributes[4]);    //HP
                six = int.Parse(attributes[5]);     //Attack
                seven = int.Parse(attributes[6]);   //Defense
                eight = int.Parse(attributes[7]);   //Sp.Att
                nine = int.Parse(attributes[8]);    //Sp.Def
                ten = int.Parse(attributes[9]);     //Speed

                pokedex1TableAdapter.Insert(one, two, three, four, five, six, seven, eight, nine, ten, null);
            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            this.pokedex1TableAdapter.Fill(this.pokedexDataSet1.pokedex1);
        }

        private void pokedex1BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.pokedex1BindingSource.EndEdit();
            this.tableAdapterManager1.UpdateAll(this.pokedexDataSet1);

        }
    }
}
