namespace PokémonGame
{
    partial class EncounterForm
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
            this.buttonmove1 = new System.Windows.Forms.Button();
            this.buttonmove2 = new System.Windows.Forms.Button();
            this.buttonmove3 = new System.Windows.Forms.Button();
            this.buttonmove4 = new System.Windows.Forms.Button();
            this.buttonFight = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelEnemyName = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelFriendlyName = new System.Windows.Forms.Label();
            this.healthmax1Label = new System.Windows.Forms.Label();
            this.healthcurrent1Label = new System.Windows.Forms.Label();
            this.pictureBoxE = new System.Windows.Forms.PictureBox();
            this.pictureBoxT = new System.Windows.Forms.PictureBox();
            this.buttonBag = new System.Windows.Forms.Button();
            this.buttonPokemon = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxT)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonmove1
            // 
            this.buttonmove1.Location = new System.Drawing.Point(3, 3);
            this.buttonmove1.Name = "buttonmove1";
            this.buttonmove1.Size = new System.Drawing.Size(87, 23);
            this.buttonmove1.TabIndex = 1;
            this.buttonmove1.Text = "move1";
            this.buttonmove1.UseVisualStyleBackColor = true;
            this.buttonmove1.Click += new System.EventHandler(this.buttonMove_click);
            // 
            // buttonmove2
            // 
            this.buttonmove2.Location = new System.Drawing.Point(96, 3);
            this.buttonmove2.Name = "buttonmove2";
            this.buttonmove2.Size = new System.Drawing.Size(87, 23);
            this.buttonmove2.TabIndex = 2;
            this.buttonmove2.Text = "move2";
            this.buttonmove2.UseVisualStyleBackColor = true;
            this.buttonmove2.Click += new System.EventHandler(this.buttonMove_click);
            // 
            // buttonmove3
            // 
            this.buttonmove3.Location = new System.Drawing.Point(3, 32);
            this.buttonmove3.Name = "buttonmove3";
            this.buttonmove3.Size = new System.Drawing.Size(87, 23);
            this.buttonmove3.TabIndex = 3;
            this.buttonmove3.Text = "move3";
            this.buttonmove3.UseVisualStyleBackColor = true;
            this.buttonmove3.Click += new System.EventHandler(this.buttonMove_click);
            // 
            // buttonmove4
            // 
            this.buttonmove4.Location = new System.Drawing.Point(96, 32);
            this.buttonmove4.Name = "buttonmove4";
            this.buttonmove4.Size = new System.Drawing.Size(87, 23);
            this.buttonmove4.TabIndex = 4;
            this.buttonmove4.Text = "move4";
            this.buttonmove4.UseVisualStyleBackColor = true;
            this.buttonmove4.Click += new System.EventHandler(this.buttonMove_click);
            // 
            // buttonFight
            // 
            this.buttonFight.Location = new System.Drawing.Point(3, 3);
            this.buttonFight.Name = "buttonFight";
            this.buttonFight.Size = new System.Drawing.Size(87, 23);
            this.buttonFight.TabIndex = 5;
            this.buttonFight.Text = "Fight";
            this.buttonFight.UseVisualStyleBackColor = true;
            this.buttonFight.Click += new System.EventHandler(this.buttonFight_click);
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::PokémonGame.Properties.Resources.EnemyBar;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.labelEnemyName);
            this.panel5.Location = new System.Drawing.Point(1, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(166, 41);
            this.panel5.TabIndex = 13;
            // 
            // labelEnemyName
            // 
            this.labelEnemyName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelEnemyName.AutoSize = true;
            this.labelEnemyName.BackColor = System.Drawing.Color.Transparent;
            this.labelEnemyName.Location = new System.Drawing.Point(3, 7);
            this.labelEnemyName.Name = "labelEnemyName";
            this.labelEnemyName.Size = new System.Drawing.Size(70, 13);
            this.labelEnemyName.TabIndex = 6;
            this.labelEnemyName.Text = "Enemy Name";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.BackgroundImage = global::PokémonGame.Properties.Resources.PlayerBar;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.labelFriendlyName);
            this.panel3.Controls.Add(this.healthmax1Label);
            this.panel3.Controls.Add(this.healthcurrent1Label);
            this.panel3.Location = new System.Drawing.Point(216, 130);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(169, 49);
            this.panel3.TabIndex = 12;
            // 
            // labelFriendlyName
            // 
            this.labelFriendlyName.AutoSize = true;
            this.labelFriendlyName.BackColor = System.Drawing.Color.Transparent;
            this.labelFriendlyName.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.labelFriendlyName.Location = new System.Drawing.Point(13, 2);
            this.labelFriendlyName.Name = "labelFriendlyName";
            this.labelFriendlyName.Size = new System.Drawing.Size(74, 13);
            this.labelFriendlyName.TabIndex = 7;
            this.labelFriendlyName.Text = "Friendly Name";
            this.labelFriendlyName.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // healthmax1Label
            // 
            this.healthmax1Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.healthmax1Label.AutoSize = true;
            this.healthmax1Label.BackColor = System.Drawing.Color.Transparent;
            this.healthmax1Label.Location = new System.Drawing.Point(137, 29);
            this.healthmax1Label.Name = "healthmax1Label";
            this.healthmax1Label.Size = new System.Drawing.Size(26, 13);
            this.healthmax1Label.TabIndex = 3;
            this.healthmax1Label.Text = "max";
            this.healthmax1Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // healthcurrent1Label
            // 
            this.healthcurrent1Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.healthcurrent1Label.AutoSize = true;
            this.healthcurrent1Label.BackColor = System.Drawing.Color.Transparent;
            this.healthcurrent1Label.Location = new System.Drawing.Point(98, 29);
            this.healthcurrent1Label.Name = "healthcurrent1Label";
            this.healthcurrent1Label.Size = new System.Drawing.Size(19, 13);
            this.healthcurrent1Label.TabIndex = 5;
            this.healthcurrent1Label.Text = "hp";
            this.healthcurrent1Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // pictureBoxE
            // 
            this.pictureBoxE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxE.Location = new System.Drawing.Point(277, 2);
            this.pictureBoxE.Name = "pictureBoxE";
            this.pictureBoxE.Size = new System.Drawing.Size(96, 96);
            this.pictureBoxE.TabIndex = 15;
            this.pictureBoxE.TabStop = false;
            // 
            // pictureBoxT
            // 
            this.pictureBoxT.Location = new System.Drawing.Point(12, 83);
            this.pictureBoxT.Name = "pictureBoxT";
            this.pictureBoxT.Size = new System.Drawing.Size(96, 96);
            this.pictureBoxT.TabIndex = 16;
            this.pictureBoxT.TabStop = false;
            // 
            // buttonBag
            // 
            this.buttonBag.Location = new System.Drawing.Point(3, 32);
            this.buttonBag.Name = "buttonBag";
            this.buttonBag.Size = new System.Drawing.Size(87, 23);
            this.buttonBag.TabIndex = 17;
            this.buttonBag.Text = "Bag";
            this.buttonBag.UseVisualStyleBackColor = true;
            this.buttonBag.Click += new System.EventHandler(this.buttonBag_click);
            // 
            // buttonPokemon
            // 
            this.buttonPokemon.Location = new System.Drawing.Point(96, 3);
            this.buttonPokemon.Name = "buttonPokemon";
            this.buttonPokemon.Size = new System.Drawing.Size(87, 23);
            this.buttonPokemon.TabIndex = 18;
            this.buttonPokemon.Text = "Pokemon";
            this.buttonPokemon.UseVisualStyleBackColor = true;
            this.buttonPokemon.Click += new System.EventHandler(this.buttonPokemon_click);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(96, 32);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(87, 23);
            this.buttonRun.TabIndex = 19;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonmove1);
            this.panel1.Controls.Add(this.buttonmove2);
            this.panel1.Controls.Add(this.buttonmove3);
            this.panel1.Controls.Add(this.buttonmove4);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(1, 184);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(186, 59);
            this.panel1.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonFight);
            this.panel2.Controls.Add(this.buttonBag);
            this.panel2.Controls.Add(this.buttonRun);
            this.panel2.Controls.Add(this.buttonPokemon);
            this.panel2.Location = new System.Drawing.Point(193, 184);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(186, 59);
            this.panel2.TabIndex = 21;
            // 
            // EncounterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 249);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBoxT);
            this.Controls.Add(this.pictureBoxE);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Name = "EncounterForm";
            this.Text = "EncounterForm";
            this.Shown += new System.EventHandler(this.EncounterForm_Shown);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxT)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonmove1;
        private System.Windows.Forms.Button buttonmove2;
        private System.Windows.Forms.Button buttonmove3;
        private System.Windows.Forms.Button buttonmove4;
        private System.Windows.Forms.Button buttonFight;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label labelEnemyName;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelFriendlyName;
        private System.Windows.Forms.Label healthmax1Label;
        private System.Windows.Forms.Label healthcurrent1Label;
        private System.Windows.Forms.PictureBox pictureBoxE;
        private System.Windows.Forms.PictureBox pictureBoxT;
        private System.Windows.Forms.Button buttonBag;
        private System.Windows.Forms.Button buttonPokemon;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}