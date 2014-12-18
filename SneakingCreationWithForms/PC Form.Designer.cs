namespace SneakingCreationWithForms
{
    partial class CreatePCForm
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
            this.nameText = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.persuasionLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.intText = new System.Windows.Forms.TextBox();
            this.perText = new System.Windows.Forms.TextBox();
            this.dexText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.APText = new System.Windows.Forms.TextBox();
            this.FoVText = new System.Windows.Forms.TextBox();
            this.SPText = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.armorText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.weapText = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.strText = new System.Windows.Forms.TextBox();
            this.knowsMap = new System.Windows.Forms.CheckBox();
            this.pcCreationGroup = new System.Windows.Forms.GroupBox();
            this.FOHText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pcCreationGroup.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(17, 33);
            this.nameText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(80, 20);
            this.nameText.TabIndex = 1;
            this.nameText.Text = "Player1";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(17, 16);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(52, 13);
            this.nameLabel.TabIndex = 26;
            this.nameLabel.Text = "PC Name";
            // 
            // persuasionLabel
            // 
            this.persuasionLabel.AutoSize = true;
            this.persuasionLabel.Location = new System.Drawing.Point(17, 138);
            this.persuasionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.persuasionLabel.Name = "persuasionLabel";
            this.persuasionLabel.Size = new System.Drawing.Size(48, 13);
            this.persuasionLabel.TabIndex = 27;
            this.persuasionLabel.Text = "Dexterity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Intelligence";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 112);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Perception";
            // 
            // intText
            // 
            this.intText.Location = new System.Drawing.Point(109, 86);
            this.intText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.intText.Name = "intText";
            this.intText.Size = new System.Drawing.Size(33, 20);
            this.intText.TabIndex = 2;
            this.intText.Text = "0";
            this.intText.TextChanged += new System.EventHandler(this.intText_TextChanged);
            // 
            // perText
            // 
            this.perText.Location = new System.Drawing.Point(109, 112);
            this.perText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.perText.Name = "perText";
            this.perText.Size = new System.Drawing.Size(33, 20);
            this.perText.TabIndex = 3;
            this.perText.Text = "0";
            this.perText.TextChanged += new System.EventHandler(this.perText_TextChanged);
            // 
            // dexText
            // 
            this.dexText.Location = new System.Drawing.Point(109, 138);
            this.dexText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dexText.Name = "dexText";
            this.dexText.Size = new System.Drawing.Size(33, 20);
            this.dexText.TabIndex = 4;
            this.dexText.Text = "0";
            this.dexText.TextChanged += new System.EventHandler(this.dexText_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 236);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "AP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 265);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Field of View";
            // 
            // APText
            // 
            this.APText.Enabled = false;
            this.APText.Location = new System.Drawing.Point(109, 236);
            this.APText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.APText.Name = "APText";
            this.APText.Size = new System.Drawing.Size(33, 20);
            this.APText.TabIndex = 36;
            this.APText.Text = "0";
            // 
            // FoVText
            // 
            this.FoVText.Enabled = false;
            this.FoVText.Location = new System.Drawing.Point(109, 262);
            this.FoVText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FoVText.Name = "FoVText";
            this.FoVText.Size = new System.Drawing.Size(33, 20);
            this.FoVText.TabIndex = 37;
            this.FoVText.Text = "0";
            // 
            // SPText
            // 
            this.SPText.Enabled = false;
            this.SPText.Location = new System.Drawing.Point(109, 314);
            this.SPText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SPText.Name = "SPText";
            this.SPText.Size = new System.Drawing.Size(33, 20);
            this.SPText.TabIndex = 38;
            this.SPText.Text = "0";
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(20, 314);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(67, 32);
            this.textBox4.TabIndex = 39;
            this.textBox4.Text = "Suspicion Propensity";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 170);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Armor";
            // 
            // armorText
            // 
            this.armorText.Location = new System.Drawing.Point(109, 170);
            this.armorText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.armorText.Name = "armorText";
            this.armorText.Size = new System.Drawing.Size(33, 20);
            this.armorText.TabIndex = 5;
            this.armorText.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 201);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "Weapon Skill";
            // 
            // weapText
            // 
            this.weapText.Location = new System.Drawing.Point(109, 201);
            this.weapText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.weapText.Name = "weapText";
            this.weapText.Size = new System.Drawing.Size(33, 20);
            this.weapText.TabIndex = 6;
            this.weapText.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 63);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 46;
            this.label8.Text = "Strength";
            // 
            // strText
            // 
            this.strText.Location = new System.Drawing.Point(109, 63);
            this.strText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.strText.Name = "strText";
            this.strText.Size = new System.Drawing.Size(33, 20);
            this.strText.TabIndex = 2;
            this.strText.Text = "0";
            // 
            // knowsMap
            // 
            this.knowsMap.AutoSize = true;
            this.knowsMap.Location = new System.Drawing.Point(154, 63);
            this.knowsMap.Name = "knowsMap";
            this.knowsMap.Size = new System.Drawing.Size(82, 17);
            this.knowsMap.TabIndex = 47;
            this.knowsMap.Text = "Knows Map";
            this.knowsMap.UseVisualStyleBackColor = true;
            // 
            // pcCreationGroup
            // 
            this.pcCreationGroup.Controls.Add(this.FOHText);
            this.pcCreationGroup.Controls.Add(this.label4);
            this.pcCreationGroup.Controls.Add(this.knowsMap);
            this.pcCreationGroup.Controls.Add(this.strText);
            this.pcCreationGroup.Controls.Add(this.label8);
            this.pcCreationGroup.Controls.Add(this.weapText);
            this.pcCreationGroup.Controls.Add(this.label7);
            this.pcCreationGroup.Controls.Add(this.armorText);
            this.pcCreationGroup.Controls.Add(this.label6);
            this.pcCreationGroup.Controls.Add(this.textBox4);
            this.pcCreationGroup.Controls.Add(this.SPText);
            this.pcCreationGroup.Controls.Add(this.FoVText);
            this.pcCreationGroup.Controls.Add(this.APText);
            this.pcCreationGroup.Controls.Add(this.label1);
            this.pcCreationGroup.Controls.Add(this.label2);
            this.pcCreationGroup.Controls.Add(this.dexText);
            this.pcCreationGroup.Controls.Add(this.perText);
            this.pcCreationGroup.Controls.Add(this.intText);
            this.pcCreationGroup.Controls.Add(this.label5);
            this.pcCreationGroup.Controls.Add(this.label3);
            this.pcCreationGroup.Controls.Add(this.persuasionLabel);
            this.pcCreationGroup.Controls.Add(this.nameLabel);
            this.pcCreationGroup.Controls.Add(this.nameText);
            this.pcCreationGroup.Location = new System.Drawing.Point(9, 37);
            this.pcCreationGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pcCreationGroup.Name = "pcCreationGroup";
            this.pcCreationGroup.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pcCreationGroup.Size = new System.Drawing.Size(245, 439);
            this.pcCreationGroup.TabIndex = 24;
            this.pcCreationGroup.TabStop = false;
            this.pcCreationGroup.Text = "PC Creation";
            // 
            // FOHText
            // 
            this.FOHText.Enabled = false;
            this.FOHText.Location = new System.Drawing.Point(109, 288);
            this.FOHText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FOHText.Name = "FOHText";
            this.FOHText.Size = new System.Drawing.Size(33, 20);
            this.FOHText.TabIndex = 50;
            this.FOHText.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 288);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "Field of Hearing";
            
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(532, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem1});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.saveToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // PC_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 496);
            this.Controls.Add(this.pcCreationGroup);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PC_Form";
            this.Text = "Create_Player_Character";
            this.pcCreationGroup.ResumeLayout(false);
            this.pcCreationGroup.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label persuasionLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox intText;
        private System.Windows.Forms.TextBox perText;
        private System.Windows.Forms.TextBox dexText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox APText;
        private System.Windows.Forms.TextBox FoVText;
        private System.Windows.Forms.TextBox SPText;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox armorText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox weapText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox strText;
        private System.Windows.Forms.CheckBox knowsMap;
        private System.Windows.Forms.GroupBox pcCreationGroup;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.TextBox FOHText;
        private System.Windows.Forms.Label label4;
    }
}