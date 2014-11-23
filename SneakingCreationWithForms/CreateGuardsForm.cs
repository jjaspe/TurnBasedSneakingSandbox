using System;
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
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.System_Classes;
using OpenGlGameCommon.Interfaces;
using Canvas_Window_Template;
using Sneaking_Gameplay.Sneaking_Drawables;
using SneakingCommon.Utility;

namespace SneakingCreationWithForms
{
    public partial class CreateGuardsForm : BasicOpenGlTemplate, ICanvasWindow
    {
        String filepath = (System.Reflection.Assembly.GetExecutingAssembly().Location).
            Replace("SneakingCreationWithForms\\bin\\Debug\\SneakingCreationWithForms.exe", "TBSneaking Data\\"),
            statToSkillsFilename="Stat To Skill Factors.txt",
            guardsFilename="Guards\\saveGuardsTest.txt";

        List<SneakingGuard> myGuards;
        GameSystem myGameSystem;
        SneakingGuard mySelectedGuard;
        SneakingMap myMap;
        selectorObj mySelector;
        Tile selectedTile;
        private bool closing, drawing;


        public simpleOpenGlView MyView
        {
            get { return myView; }
            set { myView = value; }
        }
        public SneakingMap Map
        {
            get { return myMap; }
            set { myMap = value; }
        }

        public static float[] tileColor = Common.colorGreen;
        public static float[] outlineColor = Common.colorBlack;
        public static float[] blockColor = Common.colorRed;
        public static float[] wallColor = Common.colorGreen;
        

        public CreateGuardsForm()
        {
            InitializeComponent();            

            myGuards = new List<SneakingGuard>();
            myGameSystem = XmlLoader.loadSystem(filepath+statToSkillsFilename);
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

        public void setMap(IWorld newMap, int width, int height, int tileSize, IPoint origin)
        {
            throw new NotImplementedException();
        }

        public IWorld getMap()
        {
            return Map;
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
                myDrawer.drawWorld(Map);
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
        SneakingGuard findGuard(int id)
        {
            foreach (SneakingGuard guard in myGuards)
                if (guard.getId() == id)
                    return guard;
            return null;
        }
        void setGuardPosition(Tile tile)
        {
            
        }
        void removeGuard(int id)
        {
            //Find guard
            SneakingGuard guard = null;
            foreach (SneakingGuard g in myGuards)
            {
                if (g.getId() == id)
                {
                    guard = g;
                    break;
                }
            }

            if (guard != null)
            {
                //If found remove from list;
                myGuards.Remove(guard);
                //remove from drawables
                Map.Drawables.Remove(guard);
                //remove from list box
                guardListBox.Items.Remove(new KeyValuePair<int, string>(guard.getId(), guard.MyCharacter.Name));
            }
        }

        /// <summary>
        /// Returns whether a tile has a guard on top
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        bool tileOcuppied(Tile tile)
        {
            foreach (SneakingGuard g in myGuards)
            {
                if (tile.MyOrigin.equals(g.Position))
                    return true;
            }
            return false;
        }
     

        #region EVENTS

        private void myView_Click(object sender, MouseEventArgs e)
        {
            //Check all objects, see if any was selected
            int id = mySelector.getSelectedObjectId(new int[] { e.X, e.Y }, Map);
            Tile tile;
            //Check type
            if (id > -1)
            {
                switch (id % GameObjects.objectTypes)
                {
                    case 0://Tile
                        if (e.Button == MouseButtons.Left)
                        {
                            tile = Map.getTile(id);
                            if (tile != null)
                                selectedTile = tile;
                        }
                        update();
                        break;
                    case 5: //Guard 
                         if (e.Button == MouseButtons.Right)
                        {
                            if (Map.getGuard(id) != null)
                                removeGuard(id);
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
            SneakingGuard newGuard = new SneakingGuard();
            if (nameText.Text != "" && selectedTile!=null && !tileOcuppied(selectedTile))
            {
                newGuard.MyCharacter.Name = nameText.Text;
                newGuard.Position = selectedTile.MyOrigin;
                newGuard.MySize =(int) selectedTile.TileSize / 2;
                //newGuard.MyCharacter.MyCurrentTile = selectedTile;
                
                newGuard.MyCharacter.setStat("Strength", Int32.Parse(strText.Text));
                newGuard.MyCharacter.setStat("Perception", Int32.Parse(perText.Text));
                newGuard.MyCharacter.setStat("Intelligence", Int32.Parse(intText.Text));
                newGuard.MyCharacter.setStat("Dexterity", Int32.Parse(dexText.Text));
                newGuard.MyCharacter.setStat("Armor", Int32.Parse(armorText.Text));
                newGuard.MyCharacter.setStat("Weapon Skill", Int32.Parse(weapText.Text));
                newGuard.MyCharacter.setStat("Field of View", Int32.Parse(FoVText.Text));
                newGuard.MyCharacter.setStat("AP", Int32.Parse(APText.Text));
                newGuard.MyCharacter.setStat("Suspicion Propensity", Int32.Parse(SPText.Text));
                newGuard.MyCharacter.setStat("Knows Map", knowsMap.Checked ? 1 : 0);
                newGuard.Visible = true;
                myGuards.Add(newGuard);
                guardListBox.Items.Add(new KeyValuePair<int, string>(newGuard.getId(), newGuard.MyCharacter.Name));
                guardListBox.SelectedIndex = guardListBox.Items.Count - 1;
                mySelectedGuard = newGuard;
            }

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
            MapDialog.DefaultExt = ".map";
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
                MessageBox.Show("Couldn't Open Map");
                return;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(mapFileReader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't Create XmlDocument");
                return;
            }
            this.Map = XmlLoader.loadBareMap(doc);
            if (!drawing)
                this.drawingLoop();

        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardDialog = new SaveFileDialog();
            guardDialog.Filter = "Guard Files (*.grd)|*.grd";
            guardDialog.DefaultExt=".grd";
            guardDialog.InitialDirectory=filepath;
            
            string fileName = guardDialog.ShowDialog()==DialogResult.OK?guardDialog.FileName:null;

            if (fileName == null)
            {
                MessageBox.Show("Couldnt save Guards");
                return;
            }

            XmlLoader.saveGuards(guardsFilename, myGuards);           

        }

        private void zoomInButton_Click(object sender, EventArgs e)
        {
            this.MyView.zoomIn();
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            this.MyView.zoomOut();
        }

        private void guardListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get selected guard 
            int id=0;
            SneakingGuard temp;
            if (guardListBox.SelectedItem != null)
            {
                id = ((KeyValuePair<int, string>)guardListBox.SelectedItem).Key;
                temp = findGuard(id); // Get new guard

                if (temp != null)//Dont do anything if guard not found
                {
                    //Clear old guard from map, if there was one
                    if (mySelectedGuard != null)
                        Map.Drawables.Remove(mySelectedGuard);
                    //Set new selected guard and add guard to map objects
                    mySelectedGuard = temp;
                    Map.Drawables.Add(mySelectedGuard);
                }
            }
        }

        private void intText_TextChanged(object sender, EventArgs e)
        {
            SPText.Text = ((int)myGameSystem.getSP(Int32.Parse(intText.Text))).ToString();
        }

        private void perText_TextChanged(object sender, EventArgs e)
        {
            FoVText.Text = myGameSystem.getFoV(Int32.Parse(perText.Text)).ToString();
        }

        private void dexText_TextChanged(object sender, EventArgs e)
        {
            APText.Text = myGameSystem.getAP(Int32.Parse(dexText.Text)).ToString();
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
