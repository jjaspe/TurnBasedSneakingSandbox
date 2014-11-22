namespace Sneaking
{
    partial class MainForm
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
            this.createMapButton = new System.Windows.Forms.Button();
            this.createPatrolButton = new System.Windows.Forms.Button();
            this.addGuardsButton = new System.Windows.Forms.Button();
            this.createPlayer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createMapButton
            // 
            this.createMapButton.Location = new System.Drawing.Point(12, 37);
            this.createMapButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.createMapButton.Name = "createMapButton";
            this.createMapButton.Size = new System.Drawing.Size(121, 46);
            this.createMapButton.TabIndex = 0;
            this.createMapButton.Text = "Create Map";
            this.createMapButton.UseVisualStyleBackColor = true;
            this.createMapButton.Click += new System.EventHandler(this.createMapButton_Click);
            // 
            // createPatrolButton
            // 
            this.createPatrolButton.Location = new System.Drawing.Point(358, 37);
            this.createPatrolButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.createPatrolButton.Name = "createPatrolButton";
            this.createPatrolButton.Size = new System.Drawing.Size(121, 47);
            this.createPatrolButton.TabIndex = 1;
            this.createPatrolButton.Text = "Create Patrol";
            this.createPatrolButton.UseVisualStyleBackColor = true;
            this.createPatrolButton.Click += new System.EventHandler(this.createPatrolButton_Click);
            // 
            // addGuardsButton
            // 
            this.addGuardsButton.Location = new System.Drawing.Point(187, 36);
            this.addGuardsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addGuardsButton.Name = "addGuardsButton";
            this.addGuardsButton.Size = new System.Drawing.Size(121, 47);
            this.addGuardsButton.TabIndex = 2;
            this.addGuardsButton.Text = "Create Guards";
            this.addGuardsButton.UseVisualStyleBackColor = true;
            this.addGuardsButton.Click += new System.EventHandler(this.addGuardsButton_Click);
            // 
            // createPlayer
            // 
            this.createPlayer.Location = new System.Drawing.Point(525, 36);
            this.createPlayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.createPlayer.Name = "createPlayer";
            this.createPlayer.Size = new System.Drawing.Size(121, 47);
            this.createPlayer.TabIndex = 3;
            this.createPlayer.Text = "Create Player";
            this.createPlayer.UseVisualStyleBackColor = true;
            this.createPlayer.Click += new System.EventHandler(this.createPlayer_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 735);
            this.Controls.Add(this.createPlayer);
            this.Controls.Add(this.addGuardsButton);
            this.Controls.Add(this.createPatrolButton);
            this.Controls.Add(this.createMapButton);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button createMapButton;
        private System.Windows.Forms.Button createPatrolButton;
        private System.Windows.Forms.Button addGuardsButton;
        private System.Windows.Forms.Button createPlayer;
    }
}