namespace PokémonGame
{
    partial class ScreenBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.buttonCh2 = new System.Windows.Forms.Button();
            this.buttonCh1 = new System.Windows.Forms.Button();
            this.panelChoices = new System.Windows.Forms.TableLayoutPanel();
            this.panelChoices.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::PokémonGame.Properties.Resources.panel2;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(340, 91);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.ScreenBox_PreviewKeyDown);
            // 
            // buttonCh2
            // 
            this.buttonCh2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCh2.Location = new System.Drawing.Point(0, 35);
            this.buttonCh2.Margin = new System.Windows.Forms.Padding(0);
            this.buttonCh2.Name = "buttonCh2";
            this.buttonCh2.Size = new System.Drawing.Size(302, 35);
            this.buttonCh2.TabIndex = 1;
            this.buttonCh2.Text = "button3";
            this.buttonCh2.UseVisualStyleBackColor = true;
            this.buttonCh2.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonCh1
            // 
            this.buttonCh1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCh1.Location = new System.Drawing.Point(0, 0);
            this.buttonCh1.Margin = new System.Windows.Forms.Padding(0);
            this.buttonCh1.Name = "buttonCh1";
            this.buttonCh1.Size = new System.Drawing.Size(302, 35);
            this.buttonCh1.TabIndex = 0;
            this.buttonCh1.Text = "button2";
            this.buttonCh1.UseVisualStyleBackColor = true;
            this.buttonCh1.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // panelChoices
            // 
            this.panelChoices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelChoices.ColumnCount = 1;
            this.panelChoices.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelChoices.Controls.Add(this.buttonCh2, 0, 1);
            this.panelChoices.Controls.Add(this.buttonCh1, 0, 0);
            this.panelChoices.Location = new System.Drawing.Point(12, 10);
            this.panelChoices.Name = "panelChoices";
            this.panelChoices.RowCount = 2;
            this.panelChoices.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelChoices.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelChoices.Size = new System.Drawing.Size(302, 70);
            this.panelChoices.TabIndex = 2;
            this.panelChoices.Visible = false;
            // 
            // ScreenBox
            // 
            this.BackgroundImage = global::PokémonGame.Properties.Resources.panel2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(340, 91);
            this.ControlBox = false;
            this.Controls.Add(this.panelChoices);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ScreenBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ScreenBox_FormClosed);
            this.Load += new System.EventHandler(this.ScreenBox_PreviewKeyDown);
            this.Shown += new System.EventHandler(this.ScreenBox_Shown);
            this.panelChoices.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonCh2;
        private System.Windows.Forms.Button buttonCh1;
        private System.Windows.Forms.TableLayoutPanel panelChoices;
    }
}