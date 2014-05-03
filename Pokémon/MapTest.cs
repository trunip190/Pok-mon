using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PokémonGame
{
    public partial class MapTest : Form
    {
        map thisMap = new map();
        int ticks = 32;

        public MapTest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Choose map file to load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Mapfiles (*.xml)|*.xml";

            XmlSerializer serial = new XmlSerializer(typeof(map));

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = ofd.OpenFile())
                {
                    thisMap = serial.Deserialize(stream) as map;
                }
            }

            pictureBox1.Size = new Size(thisMap.width * ticks, thisMap.height * ticks);

            refresh_Click(sender, e);
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            List<SpriteBase> TempMap = thisMap.floor;

            using (Graphics g = Graphics.FromImage(img))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                for (int m = 0; m < 4; m++)
                {
                    switch (m)
                    {
                        # region floor
                        case 0:
                            if (checkBox1.Checked)
                            {
                                TempMap = thisMap.floor;
                            }
                            else
                            {
                                goto default;
                            }
                            break;
                        # endregion

                        # region baseList
                        case 1:
                            if (checkBox2.Checked)
                            {
                                TempMap = thisMap.baseList;
                            }
                            else
                            {
                                goto default;
                            }
                            break;
                        # endregion

                        # region midList
                        case 2:
                            if (checkBox3.Checked)
                            {
                                TempMap = thisMap.midList;
                            }
                            else
                            {
                                goto default;
                            }
                            break;
                        # endregion

                        # region topList
                        case 3:
                            if (checkBox4.Checked)
                            {
                                TempMap = thisMap.topList;
                            }
                            else
                            {
                                goto default;
                            }
                            break;
                        # endregion

                        default:
                            TempMap = new List<SpriteBase>();
                            break;
                    }

                    foreach (SpriteBase sB in TempMap)
                    {
                        g.DrawImage(sB.toImage(), (sB.xPos * ticks), (sB.yPos * ticks), ticks, ticks);

                    }

                    
                }
            }

            pictureBox1.BackgroundImage = img;
        }
    }
}
