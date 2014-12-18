using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;


using OpenGlGameCommon.Classes;
using Canvas_Window_Template.Drawables;
using SneakingCommon.System_Classes;
using OpenGlGameCommon.Interfaces;
using Open_GL_Template;
using SneakingCommon.Data_Classes;
using SneakingCommon.Drawables;
using Canvas_Window_Template;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using SneakingCommon.Data_Classes;
using SneakingCreationWithForms.MVP;
using OpenGlGameCommon.Data_Classes;
using Sneaking_Gameplay.Sneaking_Drawables;

namespace SneakingCreationWithForms
{
    public partial class CreatePatrolForm : BasicOpenGlTemplate,ICanvasWindow
    {
        public static float[] tileColor=Common.colorGreen;
        public static float[] outlineColor=Common.colorBlack;
        public static float[] blockColor = Common.colorRed;
        public static float[] wallColor = Common.colorGreen;

        String filepath = (System.Reflection.Assembly.GetExecutingAssembly().Location).
            Replace("SneakingCreationWithForms\\bin\\Debug\\SneakingCreationWithForms.exe", "TBSneaking Data\\"),
            statToSkillsFilename = "Stat To Skill Factors.txt";

        Presenter MyPresenter;
        selectorObj mySelector;
        PatrolPath selectedPatrol;
        SneakingGuard selectedGuard;
        bool patrolEditing = true,drawing=false,closing=false;

        
        
        public SneakingMap Map
        {
            get { return MyPresenter.Model.Map; }
        }
        public List<PatrolPath> patrols
        {
            get{return MyPresenter.Patrols;}
        }
        public List<IPoint> EntryPoints
        {
            get
            {
                return MyPresenter.Model.Map.EntryPoints;
            }
        }
        private Tile selectedTile;
        public simpleOpenGlView MyView
        {
            get { return myView; }
            set { myView = value; }
        }

        

        public CreatePatrolForm(Presenter presenter)
        {
            InitializeComponent();
            MyPresenter = presenter;

            MyPresenter.loadSystem(filepath + statToSkillsFilename);

            mySelector = new selectorObj(this.myView);

            MyView.InitializeContexts();
            this.MyView.MouseClick += new MouseEventHandler(viewClick);
            this.MyView.Dock = DockStyle.None;

            myNavigator.Orientation = Common.planeOrientation.Z;
            myNavigator.MyWindowOwner = this;
            myNavigator.MyView = this.MyView;
            myNavigator.Parent = MyView;
            myNavigator.Dock = DockStyle.Right;

            this.WindowState = FormWindowState.Maximized;
        }
        public void start()
        {
            InitializeComponent();
            MyView.InitializeContexts();
            this.MyView.Dock = DockStyle.None;
            this.MyView.MouseClick += new MouseEventHandler(viewClick); 
            mySelector = new selectorObj(myView);
            this.Show();
        }
        public void end()
        {
            if (myView != null)
                MyView.Dispose();
            this.Hide();
        }

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

        void update()
        {
            if (selectedTile != null)
            {
                positionText.Text = selectedTile.MyOrigin.X.ToString() + "," +
                    selectedTile.MyOrigin.Y.ToString();
            }
      
            updateGuardControls();

            //Fill waypoint box
            waypointTextBox.Text = "";
            if (selectedPatrol != null)
            {
                foreach (IPoint point in selectedPatrol.MyWaypoints)
                {
                    waypointTextBox.Text = waypointTextBox.Text + "(" + point.X.ToString() +
                        "," + point.Y.ToString() + ")";
                }
            }
        }

        void addGuardToList(SneakingGuard guard)
        {
            if (guard != null)
            {
                guardListBox.Items.Add(guard);
            }
        }

         void updateGuardControls()
        {
            if (selectedGuard == null)
                return;
            //Update position and name boxes
            positionText.Text = selectedGuard.Position.X.ToString() + "," + selectedGuard.Position.Y.ToString();
            nameText.Text = selectedGuard.MyCharacter.Name;

            //Update stat boxes
            strText.Text = selectedGuard.MyCharacter.getStat("Strength").Value.ToString();
            intText.Text = selectedGuard.MyCharacter.getStat("Intelligence").Value.ToString();
            perText.Text = selectedGuard.MyCharacter.getStat("Perception").Value.ToString();
            dexText.Text = selectedGuard.MyCharacter.getStat("Dexterity").Value.ToString();
            armorText.Text = selectedGuard.MyCharacter.getStat("Armor").Value.ToString();
            weapText.Text = selectedGuard.MyCharacter.getStat("Weapon Skill").Value.ToString();
            FoVText.Text = selectedGuard.MyCharacter.getStat("Field of View").Value.ToString();
            APText.Text = selectedGuard.MyCharacter.getStat("AP").Value.ToString();
            SPText.Text = selectedGuard.MyCharacter.getStat("Suspicion Propensity").Value.ToString();
        }

         void selectPath(PatrolPath path)
        {
            MyPresenter.selectPath(path);
        }

         void deselectPath(PatrolPath path)
        {
            MyPresenter.deselectPath(path);
        }

         void deselectTile(Tile tile)
        {
            MyPresenter.deselectTile(tile);
        }

        int findPathIndexInList(PatrolPath path)
        {
            foreach (KeyValuePair<int, string> _path in patrolListBox.Items)
            {
                if (_path.Key == path.MyId)//Found it, return index
                    return patrolListBox.Items.IndexOf(_path);
            }
            return 0;
        }

        private void selectGuard(int id)
        {
            SneakingGuard guard = MyPresenter.findGuard(id); // Get new guard                

            if (guard != null)//Dont do anything if guard not found
            {
                //If guard has a patrol assigned, find it in the list, and set it as active in the patrol listbox
                //If not, enable patrol listbox so a patrol can be assigned to the guard
                PatrolPath patrol = null;
                if (guard.MyPatrol != null)
                {
                    patrol = MyPresenter.findPatrol(guard.MyPatrol.MyId);
                    patrolListBox.SelectedIndex = findPathIndexInList(patrol);
                    patrolListBox.Enabled = false;
                }
                else
                {
                    patrolListBox.Enabled = true;
                }

                //Deselect previous selected
                if (selectedGuard != null)
                {
                    MyPresenter.deselectGuard(selectedGuard);
                }

                //Change selected guard
                selectedGuard = guard;
                MyPresenter.selectGuard(selectedGuard);
            }
        }
        

        private void resetAllEntryTiles()
        {
            foreach (IPoint entry in EntryPoints)
            {
                MyPresenter.resetEntryPointTile(entry);                    
            }
        }

        #region EVENT HANDLERS
        void viewClick(object sender, MouseEventArgs e)
        {
            //Check all objects, see if any was selected
            int id = mySelector.getSelectedObjectId(new int[] { e.X, e.Y }, Map);

            //Check type of selected object
            if (id > -1)
            {
                switch (id % GameObjects.objectTypes)
                {                    
                    case Tile.idType://Tile
                        if (e.Button == MouseButtons.Left)
                        {
                            selectedTile = Map.getTile(id);
                            if (patrolEditing)
                            {
                                if (selectedPatrol != null && selectedPatrol.GuardOwners == 0)
                                    MyPresenter.addTileToPatrol(Map.getTile(id),selectedPatrol);
                            }
                            else
                            {
                                //Put coordinates in position boxes
                                entryXText.Text = Map.getTile(id).MyOrigin.X.ToString();
                                entryYText.Text = Map.getTile(id).MyOrigin.Y.ToString();
                            }
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            if (patrolEditing)
                            {
                                if (selectedPatrol != null && selectedPatrol.GuardOwners == 0)
                                    MyPresenter.removeTileFromPatrol(Map.getTile(id),selectedPatrol);
                            }
                        }
                        update();
                        break;
                    default:
                        break;
                }
            }


        }
        private void guardListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get selected guard
            if (guardListBox.SelectedItem != null)
            {
                MyPresenter.deselectAllGuards();
                selectedGuard = (SneakingGuard)guardListBox.SelectedItem;
                MyPresenter.selectGuard(selectedGuard);
                
                //Allow patrol editing if guard doesnt' have a patrol
                patrolListBox.Enabled = selectedGuard.MyPatrol==null;
            }
        }

        /// <summary>
        /// Changes from patrol editing to entry point editing by enabling and disabling corresponding
        /// group boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            patrolEditing = patrolRadioButton.Checked;
            if (patrolEditing)
            {
                //Disable entry point editing controls
                //resetAllEntryTiles();
                entryPointGroup.Enabled = false;
                patrolCreationGroup.Enabled = true;
                guardCreationGroup.Enabled = true;
            }
            else
            {
                //Disable entry point editing controls
                entryPointGroup.Enabled = true;
                patrolCreationGroup.Enabled = false;
                guardCreationGroup.Enabled = false;
            }

        }

        #region PATROL EVENTS
        private void createPatrol_Click(object sender, EventArgs e)
        {
            if (Map == null)
                return;
            PatrolPath newPatrol=MyPresenter.createPath();
            if(newPatrol!=null)
                this.patrolListBox.Items.Add(newPatrol);
            update();
        }
        private void patrolListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(patrolListBox.SelectedIndex>-1)
            {
                MyPresenter.deselectAllPaths();
                selectedPatrol=(PatrolPath)patrolListBox.SelectedItem;
                MyPresenter.selectPath(selectedPatrol);                
            }
            update();
        }    
        
        /// <summary>
        /// Tells presenter to assigne a patrol to selected guard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void assignPatrolButton_Click(object sender, EventArgs e)
        {
            if (selectedGuard == null || selectedPatrol == null)
            {
                MessageBox.Show("Make sure you have a patrol and a guard selected");
                return;
            }
            if (!MyPresenter.assignPatrolToGuard(selectedGuard, selectedPatrol))
                MessageBox.Show("Invalid path. Make sure the start and end tiles are adjecent to guard, and patrol " +
                    "hasn't been assigned to another guard");
            else
            {
                //Disable Patrol List while path is assigned
                patrolListBox.Enabled = false;
            }
        }
        /// <summary>
        /// Tells Presenter to unasssign patrol from selected guard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void unnassignPatrolButton_Click(object sender, EventArgs e)
        {
            if(MyPresenter.unassignePatrolToGuard(selectedGuard))//If carried out enable patrol list for adding more
                patrolListBox.Enabled = true;
            update();
        }
        
        #endregion
        
        #region ENTRY POINTS EVENTS
        private void addEntryPointButton_Click(object sender, EventArgs e)
        {
            if (entryXText.Text != "" && entryYText.Text != "")
            {
                IPoint point=MyPresenter.addEntryPoint(Int32.Parse(entryXText.Text), Int32.Parse(entryYText.Text));
                entryPointsListBox.Items.Add(point.X.ToString() + "," + point.Y.ToString());
            }
        }
        private void removeEntryPointButton_Click(object sender, EventArgs e)
        {
            if (entryPointsListBox.SelectedIndex != -1)
            {
                IPoint point=MyPresenter.removeEntryPoint(entryPointsListBox.SelectedIndex);
                entryPointsListBox.Items.RemoveAt(entryPointsListBox.SelectedIndex);
                
            }
        }

        private void entryPointsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (entryPointsListBox.SelectedIndex != -1)
            {
                //Resets previous
                MyPresenter.resetAllEntryPoints();

                //Color tile of selected entry point
                MyPresenter.selectEntryPoint(entryPointsListBox.SelectedIndex);
            }
        }
        #endregion

        #region SAVE AND LOAD
        private void loadGuardMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog MapDialog = new OpenFileDialog();
            MapDialog.Filter = "Map Files (*.mgp)|*.mgp";
            MapDialog.DefaultExt = "Map";
            MapDialog.InitialDirectory = filepath+"Maps\\";

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
            selectedGuard = null;
            foreach (SneakingGuard g in MyPresenter.Model.Guards)
                addGuardToList(g);

            if (!drawing)
                this.drawingLoop();
        }
        private void loadPatrolMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog MapDialog = new OpenFileDialog();
            MapDialog.Filter = "Patrol Map Files (*.mpt)|*.mpt";
            MapDialog.DefaultExt = "Map";
            MapDialog.InitialDirectory = filepath + "Maps\\";

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
            MyPresenter.loadPatrolMap(doc);

            //Put guards in list
            guardListBox.Items.Clear();
            selectedGuard = null;
            foreach (SneakingGuard g in MyPresenter.Model.Guards)
                addGuardToList(g);

            //Put patrols in list
            patrolListBox.Items.Clear();
            selectedPatrol = null;
            foreach (PatrolPath p in MyPresenter.Patrols)
                patrolListBox.Items.Add(p);

            if (!drawing)
                this.drawingLoop();
        }
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog mapDialog = new SaveFileDialog();
            mapDialog.Filter = "map Files (*.mpt)|*.mpt";
            mapDialog.DefaultExt = ".mpt";
            mapDialog.InitialDirectory = "C:/TBSneaking/Maps/";

            string filename = mapDialog.ShowDialog() == DialogResult.OK ? mapDialog.FileName : null;
            if (filename == null)
            {
                MessageBox.Show("Couldn't Save Map");
                return;
            }

            MyPresenter.savePatrolMap(filename);
        }
        #endregion

        #endregion
        

        #region ICanvas Stuff
        public simpleOpenGlView getView()
        {
            return MyView;
        }

        public void refresh()
        {
            this.Refresh();
        }

        public IWorld getMap()
        {
            return MyPresenter.Model.Map;
        }
        #endregion 

        private void CreatePatrolForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing = true;
        }

        private void deletePatrol_Click(object sender, EventArgs e)
        {
            if (patrolListBox.Enabled == false)
                MessageBox.Show("Can't delete assigned patrol, unassign first");
            else
            {
                if (MyPresenter.removePatrol(selectedPatrol))
                {
                    patrolListBox.Items.Remove(selectedPatrol);
                    MyPresenter.deselectPath(selectedPatrol);
                    selectedPatrol = null;
                }
            }

        }

        
    }
}
