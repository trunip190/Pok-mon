namespace PokémonGame
{
    partial class Form1
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
            this.move2But = new System.Windows.Forms.Button();
            this.move1But = new System.Windows.Forms.Button();
            this.textFriend = new System.Windows.Forms.TextBox();
            this.textEnemy = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textELevel = new System.Windows.Forms.TextBox();
            this.textFLevel = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pokedexDataSet1 = new PokémonGame.PokedexDataSet();
            this.pokedex1TableAdapter1 = new PokémonGame.PokedexDataSetTableAdapters.pokedex1TableAdapter();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pokedexDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // move2But
            // 
            this.move2But.Location = new System.Drawing.Point(99, 77);
            this.move2But.Name = "move2But";
            this.move2But.Size = new System.Drawing.Size(75, 23);
            this.move2But.TabIndex = 1;
            this.move2But.Text = "DataSet Load";
            this.move2But.UseVisualStyleBackColor = true;
            this.move2But.Click += new System.EventHandler(this.move2But_Click);
            // 
            // move1But
            // 
            this.move1But.Location = new System.Drawing.Point(15, 77);
            this.move1But.Name = "move1But";
            this.move1But.Size = new System.Drawing.Size(75, 23);
            this.move1But.TabIndex = 0;
            this.move1But.Text = "GameWindow";
            this.move1But.UseVisualStyleBackColor = true;
            this.move1But.Click += new System.EventHandler(this.move1But_Click);
            // 
            // textFriend
            // 
            this.textFriend.Location = new System.Drawing.Point(15, 25);
            this.textFriend.Name = "textFriend";
            this.textFriend.Size = new System.Drawing.Size(75, 20);
            this.textFriend.TabIndex = 4;
            this.textFriend.Text = "1";
            // 
            // textEnemy
            // 
            this.textEnemy.Location = new System.Drawing.Point(99, 25);
            this.textEnemy.Name = "textEnemy";
            this.textEnemy.Size = new System.Drawing.Size(70, 20);
            this.textEnemy.TabIndex = 5;
            this.textEnemy.Text = "7";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Friendly ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Enemy ID";
            // 
            // textELevel
            // 
            this.textELevel.Location = new System.Drawing.Point(99, 51);
            this.textELevel.Name = "textELevel";
            this.textELevel.Size = new System.Drawing.Size(70, 20);
            this.textELevel.TabIndex = 9;
            this.textELevel.Text = "5";
            // 
            // textFLevel
            // 
            this.textFLevel.Location = new System.Drawing.Point(15, 51);
            this.textFLevel.Name = "textFLevel";
            this.textFLevel.Size = new System.Drawing.Size(75, 20);
            this.textFLevel.TabIndex = 8;
            this.textFLevel.Text = "5";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "BattleWindow";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.move3But_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(99, 106);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Encounter";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.move4But_Click);
            // 
            // pokedexDataSet1
            // 
            this.pokedexDataSet1.DataSetName = "PokedexDataSet";
            this.pokedexDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pokedex1TableAdapter1
            // 
            this.pokedex1TableAdapter1.ClearBeforeFill = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(15, 136);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Sprites";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(99, 136);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(75, 21);
            this.comboBox1.TabIndex = 13;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(181, 76);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "View Maps";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 173);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textELevel);
            this.Controls.Add(this.textFLevel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textEnemy);
            this.Controls.Add(this.textFriend);
            this.Controls.Add(this.move2But);
            this.Controls.Add(this.move1But);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pokedexDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button move2But;
        private System.Windows.Forms.Button move1But;
        private System.Windows.Forms.TextBox textFriend;
        private System.Windows.Forms.TextBox textEnemy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textELevel;
        private System.Windows.Forms.TextBox textFLevel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private PokedexDataSet pokedexDataSet1;
        private PokedexDataSetTableAdapters.pokedex1TableAdapter pokedex1TableAdapter1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button4;
    }
}

