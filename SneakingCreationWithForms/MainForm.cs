using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Sneaking_Gameplay.Sneaking_Drawables;


namespace Sneaking
{
    public partial class MainForm : Form
    {
        static int width = 20, height = 20;//width -> Z axis, length -> X axis,in number of tiles
        static int tileSize = 20;
        static Common myDrawer = new Common();

        public MainForm()
        {
            InitializeComponent();
            this.Show();
        }

        

        /*
        public void startPatrol()
        {
            patrolWnd = new PatrolForm();
            patrolWnd.Name = "Patrol Creation Form";
            patrolWnd.registerObserver(this);
            myForms.Add(patrolWnd);
        }
        public void startGuard()
        {
            guardWnd = new GuardCreation();
            guardWnd.Name = "Guard Creation Form";
            guardWnd.registerObserver(this);
            myForms.Add(guardWnd);
        }
        public void startPlayer()
        {
            playerWnd = new PC_Form();
            playerWnd.Name = "Player Creation Form";
            playerWnd.registerObserver(this);
            myForms.Add(playerWnd);
        }*/


        /// <summary>
        /// Starts Map Creation form with an empty map and waits for it to close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createMapButton_Click(object sender, EventArgs e)
        {
            CreateMapForm mapWnd = new CreateMapForm() { Map = SneakingMap.createInstance(0, 0, 0, null) };
            mapWnd.ShowDialog(this);
        }

        private void createPatrolButton_Click(object sender, EventArgs e)
        {
           
        }

        private void addGuardsButton_Click(object sender, EventArgs e)
        {
        }

        private void createPlayer_Click(object sender, EventArgs e)
        {
        }
    }
}
