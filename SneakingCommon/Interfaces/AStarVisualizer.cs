using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SneakingClasses.Data_Classes;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public partial class AStarVisualizer : Form
    {
        static AStarVisualizer myInstance;
        private List<valuePoint> points;
        static public AStarVisualizer getInstance(List<valuePoint> points, int width)
        {
            if (myInstance == null)
            {
                myInstance = new AStarVisualizer(points, width);
                return myInstance;
            }
            else
            {
                myInstance.visualize(points, width);
                return myInstance;
            }

        }

        private AStarVisualizer()
        {
            InitializeComponent();
        }

        public AStarVisualizer(List<valuePoint> points, int width)
        {
            InitializeComponent();
            visualize(points, width);
        }

        public void visualize(List<valuePoint> points, int width)
        {
            textBox1.Text = "";
            String current;
            String[] lines = new String[points.Count / width];
            int j = 0;
            //points.Reverse();
            for (int i = 0; i < points.Count; i++)
            {
                current = points[i].value.ToString();
                if (points[i].value > 9 || points[i].value < 0)
                    lines[j] = lines[j] + " " + current + " ";
                else
                {
                    current = String.Format("{0,-3}", current);
                    lines[j] = lines[j] + " " + current + " ";
                }
                if ((i + 1) % width == 0)
                    j++;
            }

            for (int i = j - 1; i >= 0; i--)
            {
                textBox1.Text = textBox1.Text + lines[i] + "\r\n";
            }
        }
    }
}
