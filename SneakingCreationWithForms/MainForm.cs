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
using SneakingCreationWithForms.MVP;
using Canvas_Window_Template;


namespace SneakingCreationWithForms
{
    public partial class MainForm : Form,IView
    {
        static int width = 20, height = 20;//width -> Z axis, length -> X axis,in number of tiles
        static int tileSize = 20;
        static Common myDrawer = new Common();

        Presenter presenter;
        simpleOpenGlView myView;
        public Presenter MyPresenter
        {
            get
            {
                return presenter;
            }
            set
            {
                presenter=value;
            }
        }
        public simpleOpenGlView MyOpenGlView
        {
            get
            {
                return myView;
            }
            set
            {
                return;
            }
        }

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
        /// Starts Map Creation form, hookss up presenter and map's view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createMapButton_Click(object sender, EventArgs e)
        {
            CreateMapForm mapWindow = new CreateMapForm() { MyPresenter=this.MyPresenter };
            this.myView = mapWindow.MyView;
            mapWindow.ShowDialog(this);
        }

        private void createPatrolButton_Click(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Starts Guards Creation form, hookss up presenter and map's view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addGuardsButton_Click(object sender, EventArgs e)
        {
            CreateGuardsForm guardsWindow = new CreateGuardsForm() { MyPresenter=this.MyPresenter };
            this.myView = guardsWindow.MyView;
            guardsWindow.ShowDialog(this);
        }

        private void createPlayer_Click(object sender, EventArgs e)
        {
        }

        

        

        public void start()
        {
            throw new NotImplementedException();
        }

        public void startMapCreation()
        {
            throw new NotImplementedException();
        }

        public void startGuardCreation()
        {
            throw new NotImplementedException();
        }
    }
}
