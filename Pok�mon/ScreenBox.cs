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
    public partial class ScreenBox : Form
    {
        public List<convoText> TextQueue = new List<convoText>();
        public Form parent;

        private List<variable> route1 = new List<variable>();
        private List<variable> route2 = new List<variable>();

        # region delegates/events
        public delegate void TextClick();
        public delegate int PathChosen(int i);

        public event TextClick Advance;
        public event PathChosen choiceMade;

        private void onAdvance()
        {
            TextClick handler = Advance;
            if (handler != null)
            {
                handler();
            }
        }

        private void onChoiceMade(int c)
        {
            PathChosen handler = choiceMade;
            if (handler != null)
            {
                handler(c);
            }

            panelChoices.Hide();
            AdvanceText();
        }
        # endregion

        public ScreenBox()
        {
            InitializeComponent();
        }

        public ScreenBox(Form p)
        {
            InitializeComponent();
            parent = p;
        }

        void ScreenBox_PreviewKeyDown(object sender, EventArgs e)
        {
            AdvanceText();
        }

        void ScreenBox_SizeChanged(object sender, EventArgs e)
        {
            button1.Size = new Size((int)(this.Width * 0.85), (int)(this.Height * 0.66));
        }

        public void addText(List<convoText> c)
        {
            foreach (convoText ct in c)
            {
                TextQueue.Add(ct);
            }
        }

        public void addText(string s)
        {
            convoText ct = new convoText();
            ct.Text = s;

            TextQueue.Add(ct);
        }

        public void AdvanceText()
        {
            int valid = 0;
            int pos = 0;

            # region check if there are any valid texts
            for (int i = 0; i < TextQueue.Count; i++ )
            {
                if (TextQueue[i].Valid())
                {
                    if (pos == 0)
                    {
                        pos = i;
                    }
                    valid++;
                    //break;
                }
            }
            Debug.WriteLine("{0}/{1} valid.", valid, TextQueue.Count);
            # endregion

            # region text choosing
            if (valid < 1)
            {
                button1.Text = "";
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (TextQueue.Count > 0)
            {
                # region display text
                convoText ct = TextQueue[pos];

                //check validity
                if (ct.Valid())
                {
                    # region check if there are choices
                    if (ct.choices.Count > 0)
                    {
                        panelChoices.Show();

                        buttonCh1.Text = ct.choices[0].Text;
                        route1 = ct.choices[0].consequences;

                        buttonCh2.Text = ct.choices[1].Text;
                        route2 = ct.choices[1].consequences;
                    }
                    else
                    {
                        button1.Text = ct.Text;
                        ct.output();
                    }
                    # endregion
                    # region item
                    if (ct.item != null)
                    {
                        ct.item.addItem();
                    }
                    # endregion
                }
                else
                {
                    TextQueue.Add(ct);
                    //add to end
                }
                TextQueue.RemoveAt(pos);
                this.Focus();
                # endregion
            }
            # endregion

        }

        private void ScreenBox_Shown(object sender, EventArgs e)
        {
            if (parent != null)
            {
                Width = parent.Width;
                Left = parent.Left;
                Top = parent.Bottom - Height;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            GMVB.set("choice", 1);
            onChoiceMade(1);
            onAdvance();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            GMVB.set("choice", 2);
            onChoiceMade(2);
            onAdvance();
        }

        private void ScreenBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            TextQueue.Clear();
            GMVB.set("choice", -1);
        }
    }

}
