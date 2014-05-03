namespace PokémonGame
{
    partial class BattleForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleForm));
            this.labelEnemyName = new System.Windows.Forms.Label();
            this.move4But = new System.Windows.Forms.Button();
            this.move3But = new System.Windows.Forms.Button();
            this.move2But = new System.Windows.Forms.Button();
            this.move1But = new System.Windows.Forms.Button();
            this.sprite2Pic = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelFriendlyName = new System.Windows.Forms.Label();
            this.healthmax1Label = new System.Windows.Forms.Label();
            this.healthcurrent1Label = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.sprite1Pic = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.pokedexDataSet = new PokémonGame.PokedexDataSet();
            this.pokedex1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pokedex1TableAdapter = new PokémonGame.PokedexDataSetTableAdapters.pokedex1TableAdapter();
            this.tableAdapterManager = new PokémonGame.PokedexDataSetTableAdapters.TableAdapterManager();
            ((System.ComponentModel.ISupportInitialize)(this.sprite2Pic)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sprite1Pic)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pokedexDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pokedex1BindingSource)).BeginInit();
            this.SuspendLayout();
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
            // move4But
            // 
            this.move4But.Location = new System.Drawing.Point(93, 42);
            this.move4But.Name = "move4But";
            this.move4But.Size = new System.Drawing.Size(80, 37);
            this.move4But.TabIndex = 5;
            this.move4But.Text = "button4";
            this.move4But.UseVisualStyleBackColor = true;
            this.move4But.Click += new System.EventHandler(this.move4But_Click);
            // 
            // move3But
            // 
            this.move3But.Location = new System.Drawing.Point(4, 42);
            this.move3But.Name = "move3But";
            this.move3But.Size = new System.Drawing.Size(80, 37);
            this.move3But.TabIndex = 4;
            this.move3But.Text = "button3";
            this.move3But.UseVisualStyleBackColor = true;
            this.move3But.Click += new System.EventHandler(this.move3But_Click);
            // 
            // move2But
            // 
            this.move2But.Location = new System.Drawing.Point(93, 3);
            this.move2But.Name = "move2But";
            this.move2But.Size = new System.Drawing.Size(80, 37);
            this.move2But.TabIndex = 3;
            this.move2But.Text = "Button2";
            this.move2But.UseVisualStyleBackColor = true;
            this.move2But.Click += new System.EventHandler(this.move2But_Click);
            // 
            // move1But
            // 
            this.move1But.Location = new System.Drawing.Point(3, 3);
            this.move1But.Name = "move1But";
            this.move1But.Size = new System.Drawing.Size(80, 37);
            this.move1But.TabIndex = 2;
            this.move1But.Text = "button1";
            this.move1But.UseVisualStyleBackColor = true;
            this.move1But.Click += new System.EventHandler(this.move1But_Click);
            // 
            // sprite2Pic
            // 
            this.sprite2Pic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sprite2Pic.Image = ((System.Drawing.Image)(resources.GetObject("sprite2Pic.Image")));
            this.sprite2Pic.Location = new System.Drawing.Point(253, -2);
            this.sprite2Pic.Name = "sprite2Pic";
            this.sprite2Pic.Size = new System.Drawing.Size(96, 96);
            this.sprite2Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.sprite2Pic.TabIndex = 1;
            this.sprite2Pic.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.BackgroundImage = global::PokémonGame.Properties.Resources.PlayerBar;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.labelFriendlyName);
            this.panel3.Controls.Add(this.healthmax1Label);
            this.panel3.Controls.Add(this.healthcurrent1Label);
            this.panel3.Location = new System.Drawing.Point(181, 214);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(169, 49);
            this.panel3.TabIndex = 8;
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
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(188, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(162, 49);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            this.richTextBox1.Click += new System.EventHandler(this.richTextBox1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 77);
            this.button1.TabIndex = 1;
            this.button1.Text = "Fight";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.sprite2Pic);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.sprite1Pic);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 263);
            this.panel1.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Location = new System.Drawing.Point(0, 268);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(352, 83);
            this.panel4.TabIndex = 8;
            this.panel4.VisibleChanged += new System.EventHandler(this.panel4_VisibleChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(15, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(303, 58);
            this.button2.TabIndex = 0;
            this.button2.TabStop = false;
            this.button2.Text = "button2";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.TextChanged += new System.EventHandler(this.button2_TextChanged);
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.AutoSize = true;
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(15, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(0, 13);
            this.textBox1.TabIndex = 0;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::PokémonGame.Properties.Resources.EnemyBar;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.labelEnemyName);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(166, 41);
            this.panel5.TabIndex = 9;
            // 
            // sprite1Pic
            // 
            this.sprite1Pic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sprite1Pic.Image = ((System.Drawing.Image)(resources.GetObject("sprite1Pic.Image")));
            this.sprite1Pic.Location = new System.Drawing.Point(3, 162);
            this.sprite1Pic.Name = "sprite1Pic";
            this.sprite1Pic.Size = new System.Drawing.Size(96, 96);
            this.sprite1Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.sprite1Pic.TabIndex = 0;
            this.sprite1Pic.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.richTextBox1);
            this.panel2.Controls.Add(this.move4But);
            this.panel2.Controls.Add(this.move3But);
            this.panel2.Controls.Add(this.move2But);
            this.panel2.Controls.Add(this.move1But);
            this.panel2.Location = new System.Drawing.Point(0, 357);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(352, 83);
            this.panel2.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(188, 56);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Backpack";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pokedexDataSet
            // 
            this.pokedexDataSet.DataSetName = "PokedexDataSet";
            this.pokedexDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pokedex1BindingSource
            // 
            this.pokedex1BindingSource.DataMember = "pokedex1";
            this.pokedex1BindingSource.DataSource = this.pokedexDataSet;
            // 
            // pokedex1TableAdapter
            // 
            this.pokedex1TableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.pokedex1TableAdapter = this.pokedex1TableAdapter;
            this.tableAdapterManager.pokedex2TableAdapter = null;
            this.tableAdapterManager.UpdateOrder = PokémonGame.PokedexDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // BattleForm
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 440);
            this.ControlBox = false;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "BattleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.sprite2Pic)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sprite1Pic)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pokedexDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pokedex1BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //double healthMax1 = new double();
        //double healthMax2 = new double();
        //double healthCurrent1 = new double();
        //double healthCurrent2 = new double();
        string victoryResult = System.String.Empty;

        private System.Windows.Forms.Label labelEnemyName;
        private System.Windows.Forms.Button move4But;
        private System.Windows.Forms.Button move3But;
        private System.Windows.Forms.Button move2But;
        private System.Windows.Forms.Button move1But;
        private PokedexDataSet pokedexDataSet;
        private System.Windows.Forms.BindingSource pokedex1BindingSource;
        private PokedexDataSetTableAdapters.pokedex1TableAdapter pokedex1TableAdapter;
        private System.Windows.Forms.PictureBox sprite2Pic;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelFriendlyName;
        private System.Windows.Forms.Label healthmax1Label;
        private System.Windows.Forms.Label healthcurrent1Label;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox sprite1Pic;
        private System.Windows.Forms.Panel panel2;
        private PokedexDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label textBox1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}