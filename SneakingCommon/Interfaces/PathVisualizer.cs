using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SneakingClasses.Data_Classes;
using Sneaking_Classes.System_Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public partial class PathVisualizer : Form
    {
        static PathVisualizer myInstance;
        static public PathVisualizer getInstance(PatrolPath path, int width,int height,int tileSize)
        {
            if (myInstance == null)
            {
                myInstance = new PathVisualizer(path, width,height,tileSize);
                return myInstance;
            }
            else
            {
                myInstance.visualize(path, width, height, tileSize);
                return myInstance;
            }

        }
        private TextBox textBox1;

        private PathVisualizer(PatrolPath path, int width,int height,int tileSize)
        {
            InitializeComponent();
            
            visualize(path, width,height,tileSize);
        }
        private PathVisualizer()
        {
            InitializeComponent();
        }

        List<valuePoint> wrapPath(PatrolPath path)
        {
            int c = 0;
            List<valuePoint> points = new List<valuePoint>();
            foreach (pointObj p in path.MyWaypoints)
            {
                points.Add(new valuePoint(p, c++));
            }
            return points;
        }
        public void visualize(PatrolPath path, int width,int height,int tileSize)
        {
            List<valuePoint> points = wrapPath(path);
            textBox1.Text = "";
            String current;
            valuePoint point;
            for (int i = 0 - width/2; i < width - width / 2; i++)
            {
                for (int j = 0-height/2; j < height/2; j++)
                {
                    point = points.Find(
                                delegate(valuePoint _p)
                                {
                                    return _p.p.X == i * tileSize && _p.p.Y == j * tileSize;
                                });
                    if (point == null)//point not in list
                        current = " 0 ";
                    else
                    {
                        current = point.value.ToString();
                        if (point.value <= 9 && point.value >= 0)
                            current = String.Format("{0,-3}", current);
                    }    
                    textBox1.Text = textBox1.Text +current;
                }
                textBox1.Text = textBox1.Text + "\r\n";
            }
        }
    }
}
