using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SneakingClasses.Data_Classes;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public class Map_Visualizer : Form
    {
        static Map_Visualizer myInstance;
        static public Map_Visualizer getInstance(List<valuePoint> points, int width)
        {
            if (myInstance == null)
            {
                myInstance = new Map_Visualizer(points, width);
                return myInstance;
            }
            else
            {
                myInstance.visualize(points, width);
                return myInstance;
            }

        }
        private TextBox textBox1;

        private Map_Visualizer()
        {
        }

        private Map_Visualizer(List<valuePoint> points, int width)
        {
            InitializeComponent();
            
            visualize(points, width);
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(826, 481);
            this.textBox1.TabIndex = 0;
            // 
            // Map_Visualizer
            // 
            this.ClientSize = new System.Drawing.Size(344, 343);
            this.Controls.Add(this.textBox1);
            this.Enabled = false;
            this.Name = "Map_Visualizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void visualize(List<valuePoint> points, int width)
        {
            textBox1.Text = "";
            String current;
            for (int i = 0; i < points.Count; i++)
            {
                current = points[i].value.ToString();
                if (points[i].value > 9 || points[i].value < 0)
                    textBox1.Text = textBox1.Text + " " + current + " ";
                else
                {
                    current = String.Format("{0,-3}", current);
                    textBox1.Text = textBox1.Text + " " + current + " ";
                }
                if ((i+1) % width == 0)
                    textBox1.Text = textBox1.Text + "\r\n";
            }
        }
    }
}
