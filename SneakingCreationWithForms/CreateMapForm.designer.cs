using Canvas_Window_Template.Basic_Drawing_Functions;using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template;
namespace Sneaking
{
    partial class CreateMapForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eyeFrontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eyeTopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cornerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.isometricMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myView = new Canvas_Window_Template.simpleOpenGlView();
            this.myNavigator = new Canvas_Window_Template.Navigator();
            this.applySizeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.YTextBox = new System.Windows.Forms.TextBox();
            this.XTextBox = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.addGroup = new System.Windows.Forms.GroupBox();
            this.highWallButton = new System.Windows.Forms.RadioButton();
            this.lowWallButton = new System.Windows.Forms.RadioButton();
            this.highBlockButton = new System.Windows.Forms.RadioButton();
            this.lowBlockButton = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grpMap = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.addGroup.SuspendLayout();
            this.grpMap.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.cameraMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1362, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Open";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // cameraMenuItem
            // 
            this.cameraMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eyeFrontMenuItem,
            this.eyeTopMenuItem,
            this.cornerMenuItem,
            this.isometricMenuItem});
            this.cameraMenuItem.Name = "cameraMenuItem";
            this.cameraMenuItem.Size = new System.Drawing.Size(115, 20);
            this.cameraMenuItem.Text = "CameraMenuItem";
            // 
            // eyeFrontMenuItem
            // 
            this.eyeFrontMenuItem.Name = "eyeFrontMenuItem";
            this.eyeFrontMenuItem.Size = new System.Drawing.Size(123, 22);
            this.eyeFrontMenuItem.Text = "Front";
            this.eyeFrontMenuItem.Click += new System.EventHandler(this.eyeFrontMenuItem_Click);
            // 
            // eyeTopMenuItem
            // 
            this.eyeTopMenuItem.Name = "eyeTopMenuItem";
            this.eyeTopMenuItem.Size = new System.Drawing.Size(123, 22);
            this.eyeTopMenuItem.Text = "Top";
            this.eyeTopMenuItem.Click += new System.EventHandler(this.eyeTopMenuItem_Click);
            // 
            // cornerMenuItem
            // 
            this.cornerMenuItem.Name = "cornerMenuItem";
            this.cornerMenuItem.Size = new System.Drawing.Size(123, 22);
            this.cornerMenuItem.Text = "Corner";
            this.cornerMenuItem.Click += new System.EventHandler(this.cornerMenuItem_Click);
            // 
            // isometricMenuItem
            // 
            this.isometricMenuItem.Name = "isometricMenuItem";
            this.isometricMenuItem.Size = new System.Drawing.Size(123, 22);
            this.isometricMenuItem.Text = "Isometric";
            this.isometricMenuItem.Click += new System.EventHandler(this.isometricMenuItem_Click_1);
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
            this.myView.Location = new System.Drawing.Point(9, 25);
            this.myView.Margin = new System.Windows.Forms.Padding(2);
            this.myView.Name = "myView";
            this.myView.PerspectiveEye = new double[] {
        300D,
        0D,
        0.0001D};
            this.myView.Size = new System.Drawing.Size(983, 562);
            this.myView.StencilBits = ((byte)(0));
            this.myView.TabIndex = 14;
            this.myView.ViewDistance = 300D;
            this.myView.ViewPhi = 0D;
            this.myView.ViewTheta = 0D;
            // 
            // myNavigator
            // 
            this.myNavigator.BackColor = System.Drawing.Color.Transparent;
            this.myNavigator.Location = new System.Drawing.Point(756, 37);
            this.myNavigator.MyView = null;
            this.myNavigator.MyWindowOwner = null;
            this.myNavigator.Name = "myNavigator";
            this.myNavigator.Orientation = Canvas_Window_Template.Basic_Drawing_Functions.Common.planeOrientation.None;
            this.myNavigator.Size = new System.Drawing.Size(219, 96);
            this.myNavigator.TabIndex = 15;
            // 
            // applySizeButton
            // 
            this.applySizeButton.Location = new System.Drawing.Point(112, 97);
            this.applySizeButton.Margin = new System.Windows.Forms.Padding(2);
            this.applySizeButton.Name = "applySizeButton";
            this.applySizeButton.Size = new System.Drawing.Size(47, 24);
            this.applySizeButton.TabIndex = 2;
            this.applySizeButton.Text = "Apply";
            this.applySizeButton.UseVisualStyleBackColor = true;
            this.applySizeButton.Click += new System.EventHandler(this.applySizeButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 124);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 26);
            this.label3.TabIndex = 23;
            this.label3.Text = "Height (Y)\r\n\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 26);
            this.label2.TabIndex = 22;
            this.label2.Text = "Width (X)\r\n\r\n";
            // 
            // YTextBox
            // 
            this.YTextBox.Location = new System.Drawing.Point(65, 124);
            this.YTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.YTextBox.Name = "YTextBox";
            this.YTextBox.Size = new System.Drawing.Size(36, 20);
            this.YTextBox.TabIndex = 1;
            // 
            // XTextBox
            // 
            this.XTextBox.Location = new System.Drawing.Point(65, 73);
            this.XTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.XTextBox.Name = "XTextBox";
            this.XTextBox.Size = new System.Drawing.Size(36, 20);
            this.XTextBox.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1265, 371);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(76, 20);
            this.textBox2.TabIndex = 18;
            this.textBox2.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1265, 406);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 20);
            this.textBox1.TabIndex = 17;
            this.textBox1.Visible = false;
            // 
            // addGroup
            // 
            this.addGroup.Controls.Add(this.highWallButton);
            this.addGroup.Controls.Add(this.label4);
            this.addGroup.Controls.Add(this.lowWallButton);
            this.addGroup.Controls.Add(this.highBlockButton);
            this.addGroup.Controls.Add(this.lowBlockButton);
            this.addGroup.Location = new System.Drawing.Point(1194, 25);
            this.addGroup.Margin = new System.Windows.Forms.Padding(2);
            this.addGroup.Name = "addGroup";
            this.addGroup.Padding = new System.Windows.Forms.Padding(2);
            this.addGroup.Size = new System.Drawing.Size(136, 197);
            this.addGroup.TabIndex = 16;
            this.addGroup.TabStop = false;
            this.addGroup.Text = "Elements";
            // 
            // highWallButton
            // 
            this.highWallButton.AutoSize = true;
            this.highWallButton.Location = new System.Drawing.Point(17, 118);
            this.highWallButton.Margin = new System.Windows.Forms.Padding(2);
            this.highWallButton.Name = "highWallButton";
            this.highWallButton.Size = new System.Drawing.Size(93, 17);
            this.highWallButton.TabIndex = 3;
            this.highWallButton.TabStop = true;
            this.highWallButton.Tag = "3";
            this.highWallButton.Text = "Add High Wall";
            this.highWallButton.UseVisualStyleBackColor = true;
            // 
            // lowWallButton
            // 
            this.lowWallButton.AutoSize = true;
            this.lowWallButton.Location = new System.Drawing.Point(17, 96);
            this.lowWallButton.Margin = new System.Windows.Forms.Padding(2);
            this.lowWallButton.Name = "lowWallButton";
            this.lowWallButton.Size = new System.Drawing.Size(91, 17);
            this.lowWallButton.TabIndex = 2;
            this.lowWallButton.TabStop = true;
            this.lowWallButton.Tag = "2";
            this.lowWallButton.Text = "Add Low Wall\r\n";
            this.lowWallButton.UseVisualStyleBackColor = true;
            // 
            // highBlockButton
            // 
            this.highBlockButton.AutoSize = true;
            this.highBlockButton.Location = new System.Drawing.Point(17, 75);
            this.highBlockButton.Margin = new System.Windows.Forms.Padding(2);
            this.highBlockButton.Name = "highBlockButton";
            this.highBlockButton.Size = new System.Drawing.Size(99, 17);
            this.highBlockButton.TabIndex = 1;
            this.highBlockButton.TabStop = true;
            this.highBlockButton.Tag = "1";
            this.highBlockButton.Text = "Add High Block";
            this.highBlockButton.UseVisualStyleBackColor = true;
            // 
            // lowBlockButton
            // 
            this.lowBlockButton.AutoSize = true;
            this.lowBlockButton.Location = new System.Drawing.Point(17, 53);
            this.lowBlockButton.Margin = new System.Windows.Forms.Padding(2);
            this.lowBlockButton.Name = "lowBlockButton";
            this.lowBlockButton.Size = new System.Drawing.Size(97, 17);
            this.lowBlockButton.TabIndex = 0;
            this.lowBlockButton.TabStop = true;
            this.lowBlockButton.Tag = "0";
            this.lowBlockButton.Text = "Add Low Block";
            this.lowBlockButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 26);
            this.label4.TabIndex = 24;
            this.label4.Text = "Select one then click \r\non tile";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 16);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 26);
            this.label5.TabIndex = 25;
            this.label5.Text = "Enter dimensions, click apply \r\nto generate map\r\n";
            // 
            // grpMap
            // 
            this.grpMap.Controls.Add(this.label5);
            this.grpMap.Controls.Add(this.applySizeButton);
            this.grpMap.Controls.Add(this.label3);
            this.grpMap.Controls.Add(this.XTextBox);
            this.grpMap.Controls.Add(this.label2);
            this.grpMap.Controls.Add(this.YTextBox);
            this.grpMap.Location = new System.Drawing.Point(1007, 25);
            this.grpMap.Name = "grpMap";
            this.grpMap.Size = new System.Drawing.Size(164, 197);
            this.grpMap.TabIndex = 26;
            this.grpMap.TabStop = false;
            this.grpMap.Text = "Map";
            // 
            // MapCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 597);
            this.Controls.Add(this.grpMap);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.addGroup);
            this.Controls.Add(this.myNavigator);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.myView);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MapCreation";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapCreation_FormClosing);
            this.Shown += new System.EventHandler(this.myView_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.addGroup.ResumeLayout(false);
            this.addGroup.PerformLayout();
            this.grpMap.ResumeLayout(false);
            this.grpMap.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eyeFrontMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eyeTopMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cornerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem isometricMenuItem;
        private simpleOpenGlView myView;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private Navigator myNavigator;
        private System.Windows.Forms.Button applySizeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox YTextBox;
        private System.Windows.Forms.TextBox XTextBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox addGroup;
        private System.Windows.Forms.RadioButton highWallButton;
        private System.Windows.Forms.RadioButton lowWallButton;
        private System.Windows.Forms.RadioButton highBlockButton;
        private System.Windows.Forms.RadioButton lowBlockButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpMap;

        
    }
}

