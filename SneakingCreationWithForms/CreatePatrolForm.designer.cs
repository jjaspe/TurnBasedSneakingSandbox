namespace SneakingCreationWithForms
{
    partial class CreatePatrolForm
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
            this.myView = new Canvas_Window_Template.simpleOpenGlView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiLoadMap = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPatrolMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.smiSavePatrolMap = new System.Windows.Forms.ToolStripMenuItem();
            this.guardCreationGroup = new System.Windows.Forms.GroupBox();
            this.weapText = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.armorText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.strText = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.positionText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.SPText = new System.Windows.Forms.TextBox();
            this.FoVText = new System.Windows.Forms.TextBox();
            this.APText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dexText = new System.Windows.Forms.TextBox();
            this.perText = new System.Windows.Forms.TextBox();
            this.intText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.persuasionLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameText = new System.Windows.Forms.TextBox();
            this.entryPointGroup = new System.Windows.Forms.GroupBox();
            this.removeEntryPointButton = new System.Windows.Forms.Button();
            this.addEntryPointButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.entryPointsListBox = new System.Windows.Forms.ListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.entryXText = new System.Windows.Forms.TextBox();
            this.entryYText = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.entryPointRadioButton = new System.Windows.Forms.RadioButton();
            this.patrolRadioButton = new System.Windows.Forms.RadioButton();
            this.patrolCreationGroup = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.unnassignPatrolButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.patrolListBox = new System.Windows.Forms.ListBox();
            this.waypointTextBox = new System.Windows.Forms.TextBox();
            this.deletePatrol = new System.Windows.Forms.Button();
            this.finishPatrol = new System.Windows.Forms.Button();
            this.createPatrol = new System.Windows.Forms.Button();
            this.guardListBox = new System.Windows.Forms.ListBox();
            this.myNavigator = new Canvas_Window_Template.Navigator();
            this.menuStrip1.SuspendLayout();
            this.guardCreationGroup.SuspendLayout();
            this.entryPointGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.patrolCreationGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // myView
            // 
            this.myView.AccumBits = ((byte)(0));
            this.myView.AutoCheckErrors = false;
            this.myView.AutoFinish = false;
            this.myView.AutoMakeCurrent = true;
            this.myView.AutoSwapBuffers = true;
            this.myView.BackColor = System.Drawing.Color.Black;
            this.myView.ColorBits = ((byte)(32));
            this.myView.DepthBits = ((byte)(16));
            this.myView.EyeCustom = new double[] {
        -300D,
        0D,
        -0.00010000000000000002D};
            this.myView.Location = new System.Drawing.Point(4, 42);
            this.myView.Margin = new System.Windows.Forms.Padding(2);
            this.myView.Name = "myView";
            this.myView.PerspectiveEye = new double[] {
        300D,
        0D,
        0.0001D};
            this.myView.Size = new System.Drawing.Size(847, 545);
            this.myView.StencilBits = ((byte)(0));
            this.myView.TabIndex = 0;
            this.myView.ViewDistance = 300D;
            this.myView.ViewPhi = 0D;
            this.myView.ViewTheta = 0D;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1306, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiLoadMap,
            this.loadPatrolMapToolStripMenuItem,
            this.toolStripSeparator1,
            this.smiSavePatrolMap});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // smiLoadMap
            // 
            this.smiLoadMap.Name = "smiLoadMap";
            this.smiLoadMap.Size = new System.Drawing.Size(164, 22);
            this.smiLoadMap.Text = "Load Guard Map";
            this.smiLoadMap.Click += new System.EventHandler(this.loadGuardMapToolStripMenuItem_Click);
            // 
            // loadPatrolMapToolStripMenuItem
            // 
            this.loadPatrolMapToolStripMenuItem.Name = "loadPatrolMapToolStripMenuItem";
            this.loadPatrolMapToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.loadPatrolMapToolStripMenuItem.Text = "Load Patrol Map";
            this.loadPatrolMapToolStripMenuItem.Click += new System.EventHandler(this.loadPatrolMapToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // smiSavePatrolMap
            // 
            this.smiSavePatrolMap.Name = "smiSavePatrolMap";
            this.smiSavePatrolMap.Size = new System.Drawing.Size(164, 22);
            this.smiSavePatrolMap.Text = "Save Patrols Map";
            this.smiSavePatrolMap.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // guardCreationGroup
            // 
            this.guardCreationGroup.Controls.Add(this.weapText);
            this.guardCreationGroup.Controls.Add(this.label9);
            this.guardCreationGroup.Controls.Add(this.armorText);
            this.guardCreationGroup.Controls.Add(this.label2);
            this.guardCreationGroup.Controls.Add(this.strText);
            this.guardCreationGroup.Controls.Add(this.label8);
            this.guardCreationGroup.Controls.Add(this.positionText);
            this.guardCreationGroup.Controls.Add(this.label4);
            this.guardCreationGroup.Controls.Add(this.textBox4);
            this.guardCreationGroup.Controls.Add(this.SPText);
            this.guardCreationGroup.Controls.Add(this.FoVText);
            this.guardCreationGroup.Controls.Add(this.APText);
            this.guardCreationGroup.Controls.Add(this.label3);
            this.guardCreationGroup.Controls.Add(this.label5);
            this.guardCreationGroup.Controls.Add(this.dexText);
            this.guardCreationGroup.Controls.Add(this.perText);
            this.guardCreationGroup.Controls.Add(this.intText);
            this.guardCreationGroup.Controls.Add(this.label6);
            this.guardCreationGroup.Controls.Add(this.label7);
            this.guardCreationGroup.Controls.Add(this.persuasionLabel);
            this.guardCreationGroup.Controls.Add(this.nameLabel);
            this.guardCreationGroup.Controls.Add(this.nameText);
            this.guardCreationGroup.Location = new System.Drawing.Point(855, 42);
            this.guardCreationGroup.Margin = new System.Windows.Forms.Padding(2);
            this.guardCreationGroup.Name = "guardCreationGroup";
            this.guardCreationGroup.Padding = new System.Windows.Forms.Padding(2);
            this.guardCreationGroup.Size = new System.Drawing.Size(121, 406);
            this.guardCreationGroup.TabIndex = 24;
            this.guardCreationGroup.TabStop = false;
            this.guardCreationGroup.Text = "New Guard";
            // 
            // weapText
            // 
            this.weapText.Enabled = false;
            this.weapText.Location = new System.Drawing.Point(84, 247);
            this.weapText.Margin = new System.Windows.Forms.Padding(2);
            this.weapText.Name = "weapText";
            this.weapText.Size = new System.Drawing.Size(23, 20);
            this.weapText.TabIndex = 47;
            this.weapText.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 247);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 46;
            this.label9.Text = "Weapon Skill";
            // 
            // armorText
            // 
            this.armorText.Enabled = false;
            this.armorText.Location = new System.Drawing.Point(84, 216);
            this.armorText.Margin = new System.Windows.Forms.Padding(2);
            this.armorText.Name = "armorText";
            this.armorText.Size = new System.Drawing.Size(23, 20);
            this.armorText.TabIndex = 45;
            this.armorText.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 216);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Armor";
            // 
            // strText
            // 
            this.strText.Enabled = false;
            this.strText.Location = new System.Drawing.Point(84, 107);
            this.strText.Margin = new System.Windows.Forms.Padding(2);
            this.strText.Name = "strText";
            this.strText.Size = new System.Drawing.Size(23, 20);
            this.strText.TabIndex = 43;
            this.strText.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 107);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "Strength";
            // 
            // positionText
            // 
            this.positionText.Enabled = false;
            this.positionText.Location = new System.Drawing.Point(15, 75);
            this.positionText.Margin = new System.Windows.Forms.Padding(2);
            this.positionText.Name = "positionText";
            this.positionText.Size = new System.Drawing.Size(50, 20);
            this.positionText.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Position";
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(14, 334);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(67, 32);
            this.textBox4.TabIndex = 39;
            this.textBox4.Text = "Suspicion Propensity";
            // 
            // SPText
            // 
            this.SPText.Enabled = false;
            this.SPText.Location = new System.Drawing.Point(83, 341);
            this.SPText.Margin = new System.Windows.Forms.Padding(2);
            this.SPText.Name = "SPText";
            this.SPText.Size = new System.Drawing.Size(24, 20);
            this.SPText.TabIndex = 38;
            this.SPText.Text = "0";
            // 
            // FoVText
            // 
            this.FoVText.Enabled = false;
            this.FoVText.Location = new System.Drawing.Point(84, 313);
            this.FoVText.Margin = new System.Windows.Forms.Padding(2);
            this.FoVText.Name = "FoVText";
            this.FoVText.Size = new System.Drawing.Size(23, 20);
            this.FoVText.TabIndex = 37;
            this.FoVText.Text = "0";
            // 
            // APText
            // 
            this.APText.Enabled = false;
            this.APText.Location = new System.Drawing.Point(84, 289);
            this.APText.Margin = new System.Windows.Forms.Padding(2);
            this.APText.Name = "APText";
            this.APText.Size = new System.Drawing.Size(23, 20);
            this.APText.TabIndex = 36;
            this.APText.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 315);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Field of View";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 289);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "AP";
            // 
            // dexText
            // 
            this.dexText.Enabled = false;
            this.dexText.Location = new System.Drawing.Point(84, 186);
            this.dexText.Margin = new System.Windows.Forms.Padding(2);
            this.dexText.Name = "dexText";
            this.dexText.Size = new System.Drawing.Size(23, 20);
            this.dexText.TabIndex = 32;
            this.dexText.Text = "0";
            // 
            // perText
            // 
            this.perText.Enabled = false;
            this.perText.Location = new System.Drawing.Point(84, 159);
            this.perText.Margin = new System.Windows.Forms.Padding(2);
            this.perText.Name = "perText";
            this.perText.Size = new System.Drawing.Size(23, 20);
            this.perText.TabIndex = 31;
            this.perText.Text = "0";
            // 
            // intText
            // 
            this.intText.Enabled = false;
            this.intText.Location = new System.Drawing.Point(84, 134);
            this.intText.Margin = new System.Windows.Forms.Padding(2);
            this.intText.Name = "intText";
            this.intText.Size = new System.Drawing.Size(23, 20);
            this.intText.TabIndex = 30;
            this.intText.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 159);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Perception";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 134);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Intelligence";
            // 
            // persuasionLabel
            // 
            this.persuasionLabel.AutoSize = true;
            this.persuasionLabel.Location = new System.Drawing.Point(13, 186);
            this.persuasionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.persuasionLabel.Name = "persuasionLabel";
            this.persuasionLabel.Size = new System.Drawing.Size(48, 13);
            this.persuasionLabel.TabIndex = 27;
            this.persuasionLabel.Text = "Dexterity";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(13, 15);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(67, 13);
            this.nameLabel.TabIndex = 26;
            this.nameLabel.Text = "Guard Name";
            // 
            // nameText
            // 
            this.nameText.Enabled = false;
            this.nameText.Location = new System.Drawing.Point(13, 32);
            this.nameText.Margin = new System.Windows.Forms.Padding(2);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(66, 20);
            this.nameText.TabIndex = 25;
            this.nameText.Text = "Guard";
            // 
            // entryPointGroup
            // 
            this.entryPointGroup.Controls.Add(this.removeEntryPointButton);
            this.entryPointGroup.Controls.Add(this.addEntryPointButton);
            this.entryPointGroup.Controls.Add(this.label11);
            this.entryPointGroup.Controls.Add(this.label12);
            this.entryPointGroup.Controls.Add(this.entryPointsListBox);
            this.entryPointGroup.Controls.Add(this.label13);
            this.entryPointGroup.Controls.Add(this.entryXText);
            this.entryPointGroup.Controls.Add(this.entryYText);
            this.entryPointGroup.Enabled = false;
            this.entryPointGroup.Location = new System.Drawing.Point(981, 376);
            this.entryPointGroup.Name = "entryPointGroup";
            this.entryPointGroup.Size = new System.Drawing.Size(313, 204);
            this.entryPointGroup.TabIndex = 30;
            this.entryPointGroup.TabStop = false;
            this.entryPointGroup.Text = "Entry Point Creation";
            // 
            // removeEntryPointButton
            // 
            this.removeEntryPointButton.Location = new System.Drawing.Point(21, 130);
            this.removeEntryPointButton.Margin = new System.Windows.Forms.Padding(2);
            this.removeEntryPointButton.Name = "removeEntryPointButton";
            this.removeEntryPointButton.Size = new System.Drawing.Size(85, 24);
            this.removeEntryPointButton.TabIndex = 54;
            this.removeEntryPointButton.Text = "Remove Point";
            this.removeEntryPointButton.UseVisualStyleBackColor = true;
            this.removeEntryPointButton.Click += new System.EventHandler(this.removeEntryPointButton_Click);
            // 
            // addEntryPointButton
            // 
            this.addEntryPointButton.Location = new System.Drawing.Point(21, 89);
            this.addEntryPointButton.Margin = new System.Windows.Forms.Padding(2);
            this.addEntryPointButton.Name = "addEntryPointButton";
            this.addEntryPointButton.Size = new System.Drawing.Size(85, 24);
            this.addEntryPointButton.TabIndex = 51;
            this.addEntryPointButton.Text = "Add Point";
            this.addEntryPointButton.UseVisualStyleBackColor = true;
            this.addEntryPointButton.Click += new System.EventHandler(this.addEntryPointButton_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(186, 16);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "EntryPoints";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 33);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 53;
            this.label12.Text = "X";
            // 
            // entryPointsListBox
            // 
            this.entryPointsListBox.FormattingEnabled = true;
            this.entryPointsListBox.Location = new System.Drawing.Point(171, 34);
            this.entryPointsListBox.Margin = new System.Windows.Forms.Padding(2);
            this.entryPointsListBox.Name = "entryPointsListBox";
            this.entryPointsListBox.Size = new System.Drawing.Size(91, 160);
            this.entryPointsListBox.TabIndex = 32;
            this.entryPointsListBox.SelectedIndexChanged += new System.EventHandler(this.entryPointsListBox_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(65, 33);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 52;
            this.label13.Text = "Y";
            // 
            // entryXText
            // 
            this.entryXText.Enabled = false;
            this.entryXText.Location = new System.Drawing.Point(22, 47);
            this.entryXText.Margin = new System.Windows.Forms.Padding(2);
            this.entryXText.Name = "entryXText";
            this.entryXText.Size = new System.Drawing.Size(23, 20);
            this.entryXText.TabIndex = 49;
            this.entryXText.Text = "0";
            // 
            // entryYText
            // 
            this.entryYText.Enabled = false;
            this.entryYText.Location = new System.Drawing.Point(60, 47);
            this.entryYText.Margin = new System.Windows.Forms.Padding(2);
            this.entryYText.Name = "entryYText";
            this.entryYText.Size = new System.Drawing.Size(23, 20);
            this.entryYText.TabIndex = 50;
            this.entryYText.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.entryPointRadioButton);
            this.groupBox2.Controls.Add(this.patrolRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(856, 465);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(98, 75);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mode";
            // 
            // entryPointRadioButton
            // 
            this.entryPointRadioButton.AutoSize = true;
            this.entryPointRadioButton.Location = new System.Drawing.Point(7, 52);
            this.entryPointRadioButton.Name = "entryPointRadioButton";
            this.entryPointRadioButton.Size = new System.Drawing.Size(81, 17);
            this.entryPointRadioButton.TabIndex = 1;
            this.entryPointRadioButton.Text = "Entry Points";
            this.entryPointRadioButton.UseVisualStyleBackColor = true;
            // 
            // patrolRadioButton
            // 
            this.patrolRadioButton.AutoSize = true;
            this.patrolRadioButton.Checked = true;
            this.patrolRadioButton.Location = new System.Drawing.Point(7, 20);
            this.patrolRadioButton.Name = "patrolRadioButton";
            this.patrolRadioButton.Size = new System.Drawing.Size(57, 17);
            this.patrolRadioButton.TabIndex = 0;
            this.patrolRadioButton.TabStop = true;
            this.patrolRadioButton.Text = "Patrols";
            this.patrolRadioButton.UseVisualStyleBackColor = true;
            this.patrolRadioButton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // patrolCreationGroup
            // 
            this.patrolCreationGroup.Controls.Add(this.label10);
            this.patrolCreationGroup.Controls.Add(this.unnassignPatrolButton);
            this.patrolCreationGroup.Controls.Add(this.button3);
            this.patrolCreationGroup.Controls.Add(this.label1);
            this.patrolCreationGroup.Controls.Add(this.patrolListBox);
            this.patrolCreationGroup.Controls.Add(this.waypointTextBox);
            this.patrolCreationGroup.Controls.Add(this.deletePatrol);
            this.patrolCreationGroup.Controls.Add(this.finishPatrol);
            this.patrolCreationGroup.Controls.Add(this.createPatrol);
            this.patrolCreationGroup.Controls.Add(this.guardListBox);
            this.patrolCreationGroup.Location = new System.Drawing.Point(981, 42);
            this.patrolCreationGroup.Name = "patrolCreationGroup";
            this.patrolCreationGroup.Size = new System.Drawing.Size(313, 328);
            this.patrolCreationGroup.TabIndex = 32;
            this.patrolCreationGroup.TabStop = false;
            this.patrolCreationGroup.Text = "groupBox1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(43, 27);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "Guards";
            // 
            // unnassignPatrolButton
            // 
            this.unnassignPatrolButton.Location = new System.Drawing.Point(112, 131);
            this.unnassignPatrolButton.Margin = new System.Windows.Forms.Padding(2);
            this.unnassignPatrolButton.Name = "unnassignPatrolButton";
            this.unnassignPatrolButton.Size = new System.Drawing.Size(87, 24);
            this.unnassignPatrolButton.TabIndex = 38;
            this.unnassignPatrolButton.Text = "Unassign Patrol";
            this.unnassignPatrolButton.UseVisualStyleBackColor = true;
            this.unnassignPatrolButton.Click += new System.EventHandler(this.unnassignPatrolButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(112, 92);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 24);
            this.button3.TabIndex = 37;
            this.button3.Text = "Assign Patrol";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.assignPatrolButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(223, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Patrols";
            // 
            // patrolListBox
            // 
            this.patrolListBox.DisplayMember = "MyId";
            this.patrolListBox.FormattingEnabled = true;
            this.patrolListBox.Location = new System.Drawing.Point(201, 43);
            this.patrolListBox.Margin = new System.Windows.Forms.Padding(2);
            this.patrolListBox.Name = "patrolListBox";
            this.patrolListBox.Size = new System.Drawing.Size(91, 199);
            this.patrolListBox.TabIndex = 34;
            this.patrolListBox.SelectedIndexChanged += new System.EventHandler(this.patrolListBox_SelectedIndexChanged);
            // 
            // waypointTextBox
            // 
            this.waypointTextBox.Location = new System.Drawing.Point(18, 257);
            this.waypointTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.waypointTextBox.Multiline = true;
            this.waypointTextBox.Name = "waypointTextBox";
            this.waypointTextBox.Size = new System.Drawing.Size(277, 66);
            this.waypointTextBox.TabIndex = 33;
            // 
            // deletePatrol
            // 
            this.deletePatrol.Location = new System.Drawing.Point(112, 170);
            this.deletePatrol.Margin = new System.Windows.Forms.Padding(2);
            this.deletePatrol.Name = "deletePatrol";
            this.deletePatrol.Size = new System.Drawing.Size(87, 24);
            this.deletePatrol.TabIndex = 32;
            this.deletePatrol.Text = "Delete Patrol";
            this.deletePatrol.UseVisualStyleBackColor = true;
            this.deletePatrol.Click += new System.EventHandler(this.deletePatrol_Click);
            // 
            // finishPatrol
            // 
            this.finishPatrol.Location = new System.Drawing.Point(113, 207);
            this.finishPatrol.Margin = new System.Windows.Forms.Padding(2);
            this.finishPatrol.Name = "finishPatrol";
            this.finishPatrol.Size = new System.Drawing.Size(62, 34);
            this.finishPatrol.TabIndex = 31;
            this.finishPatrol.Text = "Finish";
            this.finishPatrol.UseVisualStyleBackColor = true;
            // 
            // createPatrol
            // 
            this.createPatrol.Location = new System.Drawing.Point(113, 43);
            this.createPatrol.Margin = new System.Windows.Forms.Padding(2);
            this.createPatrol.Name = "createPatrol";
            this.createPatrol.Size = new System.Drawing.Size(86, 34);
            this.createPatrol.TabIndex = 30;
            this.createPatrol.Text = "New Patrol Route";
            this.createPatrol.UseVisualStyleBackColor = true;
            this.createPatrol.Click += new System.EventHandler(this.createPatrol_Click);
            // 
            // guardListBox
            // 
            this.guardListBox.DisplayMember = "MyName";
            this.guardListBox.FormattingEnabled = true;
            this.guardListBox.Location = new System.Drawing.Point(18, 42);
            this.guardListBox.Margin = new System.Windows.Forms.Padding(2);
            this.guardListBox.Name = "guardListBox";
            this.guardListBox.Size = new System.Drawing.Size(91, 199);
            this.guardListBox.TabIndex = 36;
            this.guardListBox.SelectedIndexChanged += new System.EventHandler(this.guardListBox_SelectedIndexChanged);
            // 
            // myNavigator
            // 
            this.myNavigator.Location = new System.Drawing.Point(649, 57);
            this.myNavigator.MyView = null;
            this.myNavigator.MyWindowOwner = null;
            this.myNavigator.Name = "myNavigator";
            this.myNavigator.Orientation = Canvas_Window_Template.Basic_Drawing_Functions.Common.planeOrientation.None;
            this.myNavigator.Size = new System.Drawing.Size(190, 95);
            this.myNavigator.TabIndex = 33;
            // 
            // CreatePatrolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 596);
            this.Controls.Add(this.myNavigator);
            this.Controls.Add(this.patrolCreationGroup);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.entryPointGroup);
            this.Controls.Add(this.guardCreationGroup);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.myView);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CreatePatrolForm";
            this.Text = "Patrol Creation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreatePatrolForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.guardCreationGroup.ResumeLayout(false);
            this.guardCreationGroup.PerformLayout();
            this.entryPointGroup.ResumeLayout(false);
            this.entryPointGroup.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.patrolCreationGroup.ResumeLayout(false);
            this.patrolCreationGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Canvas_Window_Template.simpleOpenGlView myView;

        
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiLoadMap;
        private System.Windows.Forms.ToolStripMenuItem smiSavePatrolMap;
        private System.Windows.Forms.GroupBox guardCreationGroup;
        private System.Windows.Forms.TextBox positionText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox SPText;
        private System.Windows.Forms.TextBox FoVText;
        private System.Windows.Forms.TextBox APText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox dexText;
        private System.Windows.Forms.TextBox perText;
        private System.Windows.Forms.TextBox intText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label persuasionLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox strText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox weapText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox armorText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox entryPointGroup;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton entryPointRadioButton;
        private System.Windows.Forms.RadioButton patrolRadioButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox entryPointsListBox;
        private System.Windows.Forms.Button removeEntryPointButton;
        private System.Windows.Forms.Button addEntryPointButton;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox entryXText;
        private System.Windows.Forms.TextBox entryYText;
        private System.Windows.Forms.GroupBox patrolCreationGroup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button unnassignPatrolButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox patrolListBox;
        private System.Windows.Forms.TextBox waypointTextBox;
        private System.Windows.Forms.Button deletePatrol;
        private System.Windows.Forms.Button finishPatrol;
        private System.Windows.Forms.Button createPatrol;
        private System.Windows.Forms.ListBox guardListBox;
        private Canvas_Window_Template.Navigator myNavigator;
        private System.Windows.Forms.ToolStripMenuItem loadPatrolMapToolStripMenuItem;
    }
}