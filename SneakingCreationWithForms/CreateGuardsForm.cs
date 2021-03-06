﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Open_GL_Template;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using System.IO;
using System.Xml;
using OpenGlGameCommon.Classes;
using Canvas_Window_Template.Drawables;
using SneakingCommon.System_Classes;
using OpenGlGameCommon.Interfaces;
using Canvas_Window_Template;
using Sneaking_Gameplay.Sneaking_Drawables;
using SneakingCommon.Utility;
using SneakingCreationWithForms.MVP;
using CharacterSystemLibrary.Classes;

namespace SneakingCreationWithForms
{
    public partial class CreateGuardsForm : BasicOpenGlTemplate, ICanvasWindow
    {
        String filepath = (System.Reflection.Assembly.GetExecutingAssembly().Location).
            Replace("SneakingCreationWithForms\\bin\\Debug\\SneakingCreationWithForms.exe", "TBSneaking Data\\"),
            statToSkillsFilename="Stat To Skill Factors.txt",
            guardsFilename="Guards\\saveGuardsTest.txt";

        SneakingGuard mySelectedGuard;
        selectorObj mySelector;
        Tile selectedTile;
        Presenter presenter;

        
        private bool closing, drawing;

        public Presenter MyPresenter
        {
            get { return presenter; }
            set { presenter = value; }
        }
        public simpleOpenGlView MyView
        {
            get { return myView; }
            set { myView = value; }
        }

        public static float[] tileColor = Common.colorGreen;
        public static float[] outlineColor = Common.colorBlack;
        public static float[] blockColor = Common.colorRed;
        public static float[] wallColor = Common.colorGreen;
        
        /// <summary>
        /// Stats up game system too
        /// </summary>
        public CreateGuardsForm(Presenter presenter)
        {
            InitializeComponent();
            MyPresenter = presenter;

            MyPresenter.loadSystem(filepath+statToSkillsFilename);

            mySelector = new selectorObj(this.myView);
            
            MyView.InitializeContexts();
            this.MyView.MouseClick += new MouseEventHandler(myView_Click);
            this.MyView.Dock = DockStyle.None;

            myNavigator.Orientation = Common.planeOrientation.Z;
            myNavigator.MyWindowOwner = this;
            myNavigator.MyView = this.MyView;
            myNavigator.Parent = MyView;
            myNavigator.Dock = DockStyle.Right;

            this.WindowState = FormWindowState.Maximized;
        }
       

        #region INTERFACES IMPLEMENTATIONS
        public simpleOpenGlView getView()
        {
            return MyView;
        }

        public IWorld getMap()
        {
            return MyPresenter.Model.Map;
        }

        public void refresh()
        {
            this.Refresh();
        }
        #endregion

        public void drawingLoop()
        {
            Common myDrawer = new Common();
            myView.setCameraView(simpleOpenGlView.VIEWS.Iso);
            //myWindow.MinimizeBox = true;
            //myWindow.MaximizeBox = true;
            drawing = true;
            while (!myView.isDisposed() && !closing)
            {
                MyView.setupScene();
                //DRAW SCENE HERE
                myDrawer.drawWorld(MyPresenter.Model.Map);
                //END DRAW SCENE HERE
                MyView.flushScene();
                this.Refresh();
                Application.DoEvents();
            }
            drawing = false;
        }
       
        private void update()
        {
            if (selectedTile!=null)
            {
                positionText.Text = selectedTile.MyOrigin.X.ToString() + "," +
                    selectedTile.MyOrigin.Y.ToString();
            }
        }

        private void addGuardToList(SneakingGuard guard)
        {
            if (guard != null)
            {
                guardListBox.Items.Add(new KeyValuePair<int, string>(guard.getId(), guard.MyCharacter.Name));
                guardListBox.SelectedIndex = guardListBox.Items.Count - 1;
                mySelectedGuard = guard;
            }
        }

        #region EVENTS
        /// <summary>
        /// Hadler for clicks on view. Right clicking a guard will remove it, left clicking a tile will select it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myView_Click(object sender, MouseEventArgs e)
        {
            //Check all objects, see if any was selected
            int id = mySelector.getSelectedObjectId(new int[] { e.X, e.Y }, MyPresenter.Model.Map);
            Tile tile;
            //Check type
            if (id > -1)
            {
                switch (id % GameObjects.objectTypes)
                {
                    case 0://Tile
                        if (e.Button == MouseButtons.Left)//Select tile
                        {
                            tile = MyPresenter.Model.Map.getTile(id);
                            if (tile != null)
                                selectedTile = tile;
                        }
                        update();
                        break;
                    case 5: //Guard 
                         if (e.Button == MouseButtons.Right)//Remove
                        {
                            if (MyPresenter.Model.Map.getGuard(id) != null)
                            {
                                SneakingGuard guard=MyPresenter.removeGuard(id);
                                //remove from list box
                                if(guard!=null)
                                    guardListBox.Items.Remove(new KeyValuePair<int, string>(guard.getId(), guard.MyCharacter.Name));
                            }
                        }
                        update();
                        break;
                    default:
                        break;
                }
            }


        }

        private void addGuardButton_Click(object sender, EventArgs e)
        {
            List<Stat> guardStats = new List<Stat>();            
            guardStats.Add(new Stat("Strength", Int32.Parse(strText.Text)));
            guardStats.Add(new Stat("Perception", Int32.Parse(perText.Text)));
            guardStats.Add(new Stat("Intelligence", Int32.Parse(intText.Text)));
            guardStats.Add(new Stat("Dexterity", Int32.Parse(dexText.Text)));
            guardStats.Add(new Stat("Armor", Int32.Parse(armorText.Text)));
            guardStats.Add(new Stat("Weapon Skill", Int32.Parse(weapText.Text)));
            guardStats.Add(new Stat("Field of View", Int32.Parse(FoVText.Text)));
            guardStats.Add(new Stat("AP", Int32.Parse(APText.Text)));
            guardStats.Add(new Stat("Suspicion Propensity", Int32.Parse(SPText.Text)));
            guardStats.Add(new Stat("Knows Map", knowsMap.Checked ? 1 : 0));

            SneakingGuard newGuard = MyPresenter.createGuard(txtName.Text, selectedTile, guardStats);

            addGuardToList(newGuard);

            update();
        }

        private void GuardCreation_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!MyView.IsDisposed)
                MyView.Dispose();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog MapDialog = new OpenFileDialog();
            MapDialog.Filter = "Map Files (*.map)|*.map";
            MapDialog.DefaultExt = "Map";
            MapDialog.InitialDirectory = filepath;

            string filename = MapDialog.ShowDialog() == DialogResult.OK ? MapDialog.FileName : null;

            if (filename == null)
            {
                MessageBox.Show("Couldnt' Load Map");
                return;
            }

            FileStream mapFileReader;
            try
            {
                mapFileReader = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't Open Map:" + ex.Message);
                return;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(mapFileReader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't Create XmlDocument:"+ex.Message);
                return;
            }
            this.MyPresenter.loadBareMap(doc);
            guardListBox.Items.Clear();
            if (!drawing)
                this.drawingLoop();

        }

        private void loadMapWithGuardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog MapDialog = new OpenFileDialog();
            MapDialog.Filter = "Map Files (*.mgp)|*.mgp";
            MapDialog.DefaultExt = "Map";
            MapDialog.InitialDirectory = filepath;

            string filename = MapDialog.ShowDialog() == DialogResult.OK ? MapDialog.FileName : null;

            if (filename == null)
            {
                MessageBox.Show("Couldnt' Load Map");
                return;
            }

            FileStream mapFileReader;
            try
            {
                mapFileReader = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't Open Map:" + ex.Message);
                return;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(mapFileReader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't Create XmlDocument:" + ex.Message);
                return;
            }
            MyPresenter.loadGuardsMap(doc);

            //Put guards in list
            guardListBox.Items.Clear();
            mySelectedGuard = null;
            foreach (SneakingGuard g in MyPresenter.Model.Guards)
                addGuardToList(g);

            if (!drawing)
                this.drawingLoop();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardDialog = new SaveFileDialog();
            guardDialog.Filter = "Guard Files (*.mgp)|*.mgp";
            guardDialog.DefaultExt=".mgp";
            guardDialog.InitialDirectory=filepath;
            
            string filename = guardDialog.ShowDialog()==DialogResult.OK?guardDialog.FileName:null;

            if (filename == null)
            {
                MessageBox.Show("Couldnt save Guards Map");
                return;
            }

            MyPresenter.saveGuardsMap(filename);           

        }

        private void zoomInButton_Click(object sender, EventArgs e)
        {
            this.MyView.zoomIn();
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            this.MyView.zoomOut();
        }

        /// <summary>
        /// Makes previously selected not visible, makes newly selected visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guardListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get selected guard 
            int id=0;
            SneakingGuard temp;
            if (guardListBox.SelectedItem != null)
            {
                id = ((KeyValuePair<int, string>)guardListBox.SelectedItem).Key;
                temp = MyPresenter.findGuard(id); // Get new guard

                if (temp != null)//Dont do anything if guard not found
                {
                    //Make old selected not visible
                    if (mySelectedGuard != null)
                        mySelectedGuard.Visible = false;
                    //Set new selected guard as visible
                    mySelectedGuard = temp;
                    mySelectedGuard.Visible = true; ;
                }
            }
        }

        private void intText_TextChanged(object sender, EventArgs e)
        {
            SPText.Text = ((int)MyPresenter.Model.System.getSP(Int32.Parse(intText.Text))).ToString();
        }

        private void perText_TextChanged(object sender, EventArgs e)
        {
            FoVText.Text = MyPresenter.Model.System.getFoV(Int32.Parse(perText.Text)).ToString();
        }

        private void dexText_TextChanged(object sender, EventArgs e)
        {
            APText.Text = MyPresenter.Model.System.getAP(Int32.Parse(dexText.Text)).ToString();
        }

        private void armorText_TextChanged(object sender, EventArgs e)
        {

        }

        private void GuardCreation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myView != null)
                MyView.Dispose();
            closing = true;
        }
        #endregion

        

    }
}
