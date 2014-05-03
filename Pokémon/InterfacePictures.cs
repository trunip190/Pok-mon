using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PokémonGame
{
    public class PictureSingle : PictureBox
    {
        GameWindow Parent;
        Image border;

        public delegate void action();

        public event action Closing;
        public event action Showing;

        protected void onClosing()
        {
            action handler = Closing;
            if (handler != null)
            {
                handler();
            }
        }

        protected void onShowing()
        {
            action handler = Showing;
            if (handler != null)
            {
                handler();
            }
        }

        public PictureSingle(GameWindow p)
        {
            Parent = p;
            Parent.Controls.Add(this);
            this.Visible = false;
            this.BorderStyle = BorderStyle.FixedSingle;

            # region size/location
            this.Size = new Size(300, 100);

            border = new Bitmap(this.Width, this.Height);
            
            //Centre control on screen
            int left = (p.Width - this.Width) / 2;
            int top = (p.Height - this.Height) / 2;
            this.Location = new Point(left, top);
            # endregion

            PreviewKeyDown += new PreviewKeyDownEventHandler(InterfacePictures_PreviewKeyDown); //TODO change this to have controls.
        }

        void InterfacePictures_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Visible)
            {
                switch (e.KeyCode)
                {
                    case Keys.Right:
                        Parent.cycleImages(true);
                        break;

                    case Keys.Left:
                        Parent.cycleImages(false);
                        break;

                    case Keys.X:
                        this.Close();
                        break;
                }
            }
        }

        public void Draw(Image img)
        {
            onShowing();
            using (Graphics g = Graphics.FromImage(img))
            {
                g.DrawImage(border, new Point(0, 0));

                Width = img.Width;
                Height = img.Height;

                Image = img;
            }

            Show();
            BringToFront();
            Focus();
        }

        public void Close()
        {
            Hide();
            Parent.GameLock(false);
            onClosing();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
