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


        
        private void createMapButton_Click(object sender, EventArgs e)
        {
            MyPresenter.createMapWindowStart();
        }

        private void createPatrolButton_Click(object sender, EventArgs e)
        {
            presenter.createMapWindowStart();
        }
        
        private void addGuardsButton_Click(object sender, EventArgs e)
        {
            presenter.createGuardWindowStart();
        }

        private void createPlayer_Click(object sender, EventArgs e)
        {
        }

        

        

        public void start()
        {
            this.Show();
        }

        /// <summary>
        /// Starts map window, hooks up presenter with window's openGl view
        /// </summary>
        public void startMapCreation()
        {
            CreateMapForm mapWindow = new CreateMapForm() { MyPresenter = this.MyPresenter };
            this.myView = mapWindow.MyView;
            mapWindow.ShowDialog(this);
        }

        /// <summary>
        /// Starts guard window, hooks up presenter with window's openGl view
        /// </summary>
        public void startGuardCreation()
        {
            CreateGuardsForm guardsWindow = new CreateGuardsForm(this.MyPresenter );
            this.myView = guardsWindow.MyView;
            guardsWindow.ShowDialog(this);
        }
    }
}
