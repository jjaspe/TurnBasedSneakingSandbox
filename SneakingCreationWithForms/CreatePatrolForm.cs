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
        List<IFormObserver> myObservers;
        OpenGlMap myMap;
        selectorObj mySelector;
        PatrolPath currentPath;
        SneakingGuard mySelectedGuard;
        List<IPoint> entryPoints;
        int selectedEntryPoint=-1;
        bool patrolEditing = true,drawing=false,closing=false;

        
        
        public OpenGlMap Map
        {
          get { return myMap; }
          set { myMap = value; }
        }
        public List<PatrolPath> paths;
        public List<SneakingGuard> guards;
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
            paths = new List<PatrolPath>();
            guards = new List<SneakingGuard>();
            entryPoints = new List<IPoint>();
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

        private void update()
        {
            if (selectedTile != null)
            {
                positionText.Text = selectedTile.MyOrigin.X.ToString() + "," +
                    selectedTile.MyOrigin.Y.ToString();
            }
            #region GUARD STUFF
            if (guardListBox.Items.Count > 0)
            {
                SneakingGuard tempGuard = guardListBox.SelectedItem != null ?
                    MyPresenter.findGuard(((KeyValuePair<int, string>)guardListBox.SelectedItem).Key) : null;
                if (tempGuard != null)
                {
                    //Deselect old guard if there was one
                    if (mySelectedGuard != null)
                        deselectGuard(mySelectedGuard);

                    //Select guard
                    selectGuard(tempGuard.getId());

                    //Update guard stuff
                    updateGuardControls();
                }
            }
            #endregion
            #region PATROL STUFF
            //Fill patrol list
            KeyValuePair<int, string> currentItem;
            PatrolPath lastPath,
                tempPath = patrolListBox.SelectedItem != null ?
                findPath(((KeyValuePair<int, string>)patrolListBox.SelectedItem).Key) : null;

            #region CHANGE PATHS
            //Set current path
            if (tempPath != null)
            {
                //deselect old currentPath if there was one
                if (currentPath != null)
                    deselectPath(currentPath);

                currentPath = tempPath;
                //select new currentPath
                selectPath(currentPath);
            }


            if (paths != null)
            {
                if (paths.Count > patrolListBox.Items.Count)//Add last path to list
                {
                    lastPath = paths[paths.Count - 1];
                    if (lastPath != null)
                    {
                        currentItem = new KeyValuePair<int, string>(lastPath.MyId, lastPath.name);
                        patrolListBox.Items.Add(currentItem);
                    }
                }
            }
            #endregion

            //Fill waypoint box
            waypointTextBox.Text = "";
            if (currentPath != null)
            {
                foreach (IPoint point in currentPath.MyWaypoints)
                {
                    waypointTextBox.Text = waypointTextBox.Text + "(" + point.X.ToString() +
                        "," + point.Y.ToString() + ")";
                }
            }
            #endregion
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

        private void updateGuardControls()
        {
            //Update position and name boxes
            positionText.Text = mySelectedGuard.Position.X.ToString() + "," + mySelectedGuard.Position.Y.ToString();
            nameText.Text = mySelectedGuard.MyCharacter.Name;

            //Update stat boxes
            strText.Text = mySelectedGuard.MyCharacter.getStat("Strength").Value.ToString();
            intText.Text = mySelectedGuard.MyCharacter.getStat("Intelligence").Value.ToString();
            perText.Text = mySelectedGuard.MyCharacter.getStat("Perception").Value.ToString();
            dexText.Text = mySelectedGuard.MyCharacter.getStat("Dexterity").Value.ToString();
            armorText.Text = mySelectedGuard.MyCharacter.getStat("Armor").Value.ToString();
            weapText.Text = mySelectedGuard.MyCharacter.getStat("Weapon Skill").Value.ToString();
            FoVText.Text = mySelectedGuard.MyCharacter.getStat("Field of View").Value.ToString();
            APText.Text = mySelectedGuard.MyCharacter.getStat("AP").Value.ToString();
            SPText.Text = mySelectedGuard.MyCharacter.getStat("Suspicion Propensity").Value.ToString();
        }


        private void selectPath(PatrolPath path)
        {
            MyPresenter.selectPath(path);
        }

        private void deselectPath(PatrolPath path)
        {
            MyPresenter.deselectPath(path);
        }

        private void deselectTile(Tile tile)
        {
            MyPresenter.deselectTile(tile);
        }

        private void selectGuard(int id)
        {

            SneakingGuard guard = MyPresenter.findGuard(id); // Get new guard                

            if (guard != null)//Dont do anything if guard not found
            {
                //If guard has a patrol assigned, find it in the list, and set it in the listbox
                //If not, enable patrol list so one can be assigned
                PatrolPath patrol = null;
                if (guard.MyPatrol != null)
                {
                    patrol = findPath(guard.MyPatrol.MyId);
                    patrolListBox.SelectedIndex = findIndexInList(patrol);
                    patrolListBox.Enabled = false;
                    update();
                }
                else
                {
                    patrolListBox.Enabled = true;
                    update();
                }

                //Make old selected not visible
                if (mySelectedGuard != null)
                    mySelectedGuard.Visible = false;
                //Set new selected guard as visible
                mySelectedGuard = guard;
                mySelectedGuard.Visible = true; ;
            }
        }

        private void deselectGuard(SneakingGuard guard)
        {
            guard.Visible = false;
        }

        #region EVENT HANDLERS
        void viewClick(object sender, MouseEventArgs e)
        {
            //Check all objects, see if any was selected
            int id = mySelector.getSelectedObjectId(new int[] { e.X, e.Y }, myMap);
            Tile tile;
            //Check type
            if (id > -1)
            {
                switch (id % GameObjects.objectTypes)
                {
                    
                    case Tile.idType://Tile
                        if (e.Button == MouseButtons.Left)
                        {
                            if (patrolEditing)
                            {
                                if (currentPath != null && currentPath.GuardOwners == 0)
                                    addTileToCurrentPath(myMap.getTile(id));
                            }
                            else
                            {
                                //Put coordinates in position boxes
                                entryXText.Text = myMap.getTile(id).MyOrigin.X.ToString();
                                entryYText.Text = myMap.getTile(id).MyOrigin.Y.ToString();
                            }
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            if (patrolEditing)
                            {
                                if (currentPath != null && currentPath.GuardOwners == 0)
                                    deleteTileFromCurrentPath(myMap.getTile(id));
                            }
                        }
                        update();
                        break;
                    /*        
                    case 1://Low Block
                        lBlock = myMap.getLowBlock(id);
                        if (e.Button == MouseButtons.Left)
                            lowBlockLeftClick(lBlock);
                        else if (e.Button == MouseButtons.Right)
                            lowBlockRightClick(lBlock);
                        break;
                    case 2://High block
                        hBlock = myMap.getHighBlock(id);
                        if (e.Button == MouseButtons.Left)
                            highBlockLeftClick(hBlock);
                        else if (e.Button == MouseButtons.Right)
                            highBlockRightClick(hBlock);
                        break;
                    case 3://Low wall
                        lWall = myMap.getLowWall(id);
                        if (e.Button == MouseButtons.Left)
                            lowWallLeftClick(lWall);
                        else if (e.Button == MouseButtons.Right)
                            lowWallRightClick(lWall);
                        break;
                    case 4://High wall
                        hWall = myMap.getHighWall(id);
                        if (e.Button == MouseButtons.Left)
                            highWallLeftClick(hWall);
                        else if (e.Button == MouseButtons.Right)
                            highWallRightClick(hWall);
                        break;*/
                    default:
                        break;
                }
            }


        }       
        private void createPatrol_Click(object sender, EventArgs e)
        {
            if (paths != null)
            {
                paths.Add(new PatrolPath("Patrol #" + paths.Count.ToString()));
                update();
            }

        }
        private void patrolListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            update();
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void patrolListBox_Leave(object sender, EventArgs e)
        {

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

            MyPresenter.saveFullMap(filename);
        }
        private void loadGuardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myMap == null)
            {
                MessageBox.Show("Load a map first");
                return;
            }

            OpenFileDialog grdDialog = new OpenFileDialog();
            grdDialog.Filter = "grd Files (*.grd)|*.grd";
            grdDialog.DefaultExt = ".grd";
            grdDialog.InitialDirectory = "C:/TBSneaking/Guards/";

            string fileName = grdDialog.ShowDialog() == DialogResult.OK ? grdDialog.FileName : null;
            if (fileName == null)
            {
                MessageBox.Show("Couldn't Load Guards");
                return;
            }

            XmlDocument myDoc=new XmlDocument();
            try
            {
                myDoc.Load(fileName);
            }
            catch (Exception)
            {
                return;
            }
            loadGuards(myDoc);
        }
        private void guardListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get selected guard 
            int id = 0;
            if (guardListBox.SelectedItem != null)
            {
                id = ((KeyValuePair<int, string>)guardListBox.SelectedItem).Key;
                selectGuard(id);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            myView.rotateCW();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            myView.rotateCCW();
        }
        private void assignPatrolButton_Click(object sender, EventArgs e)
        {
            if (mySelectedGuard == null || currentPath == null)
            {
                MessageBox.Show("Make sure you have a path and a guard selected");
                return;
            }
            if (!assignPatrolToGuard(mySelectedGuard, currentPath))
                MessageBox.Show("Invalid path. Make sure the start and end tiles are adjecent to guard");
            else
            {
                currentPath.createDirectionLines(myMap.TileSize);

                //Disable Patrol List while path is assigned
                patrolListBox.Enabled = false;
            }
        }
        private void unnassignPatrolButton_Click(object sender, EventArgs e)
        {
            if (mySelectedGuard != null && mySelectedGuard.MyPatrol!=null)
            {
                mySelectedGuard.MyPatrol.GuardOwners--;
                mySelectedGuard.MyPatrol = null;
                patrolListBox.Enabled = true;
            }
            update();
        }
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

        private void addEntryPointButton_Click(object sender, EventArgs e)
        {
            if (entryXText.Text != "" && entryYText.Text != "")
            {
                entryPoints.Add(new PointObj(Int32.Parse(entryXText.Text), Int32.Parse(entryYText.Text), 0));
            }
            updateEntryPointList();
        }

        void updateEntryPointList()
        {
            entryPointsListBox.Items.Clear();
            foreach (IPoint p in entryPoints)
                entryPointsListBox.Items.Add(new KeyValuePair<int, string>(entryPoints.IndexOf(p),
                    p.X.ToString() + "," + p.Y.ToString()));
        }

        private void removeEntryPointButton_Click(object sender, EventArgs e)
        {
            if (entryPointsListBox.SelectedIndex != -1)
            {
                //Clear point
                Tile entry = myMap.getTile(entryPoints[selectedEntryPoint]);
                if (entry != null)
                    entry.MyColor = entry.OriginalColor;
                entryPoints.RemoveAt(selectedEntryPoint);

                updateEntryPointList();
                selectedEntryPoint = -1;
            }
        }

        private void entryPointsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (entryPointsListBox.SelectedIndex != -1)
            {
                Tile entry;
                //Reset previous, if any
                if (selectedEntryPoint != -1)
                {
                    entry = myMap.getTile(entryPoints[selectedEntryPoint]);
                    if (entry != null)
                        entry.MyColor = entry.OriginalColor;
                }

                //Color tile of selected entry point
                selectedEntryPoint = ((KeyValuePair<int, string>)entryPointsListBox.SelectedItem).Key;
                entry = myMap.getTile(entryPoints[selectedEntryPoint]);
                if (entry != null)
                    entry.MyColor = Common.colorOrange;

            }
        }

        private void resetAllEntryTiles()
        {
            Tile current;
            foreach (IPoint entry in entryPoints)
            {
                current = myMap.getTile(entry);
                if (current != null)
                    current.MyColor = current.OriginalColor;
            }
        }
        #endregion

        #region Patrol stuff
        private bool allowDestination(OpenGlMap map,IPoint source,Tile dest)
        {
            List<IDrawable> dr=(map as IWorld).getEntities();
            int direction=0;//0:up,2:right,3:down,4:left Clockwise
            double dX,dY;
            double[] dPos;
            bool isWall;
            IPoint endPos=new PointObj();

            #region Checker Loop
            foreach(IDrawable d in dr)
            {
                isWall=false;

                // Same tiles?
                if(source.equals(dest.origin))
                    return false;

                //Check if empty
                if((dest.origin.equals(d.getPosition()) && (d.getId() % GameObjects.objectTypes==1 /* LowBlock */
                    || GameObjects.objectTypes==2) /*High Block*/ )) // Is there a block in dest position
                    return false;

                //Check distance
                dX=dest.origin.X-source.X;
                dY=dest.origin.Y-source.Y;
                if(Math.Abs(dX)>map.TileSize || Math.Abs(dY)>map.TileSize)//Not next to each other
                    return false;

                //Check corners
                if(Math.Abs(dX)==map.TileSize&&Math.Abs(dY)==map.TileSize)
                    return false;

                //Get direction
                switch((int)dX/map.TileSize)
                {
                    case 0://Up or down
                        if(dY<0)
                            direction=3;//down
                        if(dY>0)
                            direction=1;//up
                        break;
                    case 1://Right
                        direction=2;
                        break;
                    case -1:
                        direction=4;//Left
                        break;
                    default:
                        return false;
                }
                
                dPos=d.getPosition();
                //Check walls
                if(d.getId() % GameObjects.objectTypes==3)/* LowWall */
                {
                    isWall=true;
                    endPos = ((LowWall)d).MyTiles[0,0].MyEnd;
                }else if(d.getId() % GameObjects.objectTypes==4) /*HighWall*/ 
                {
                    isWall=true;
                    endPos = ((HighWall)d).MyTiles[0,0].MyEnd;
                }
                if(isWall)
                {
                    switch(direction)
                    {
                        case 1://up so wall must be horizontal, with origin x,y
                            if(dPos[0]==dest.origin.X && dPos[1]==dest.origin.Y && 
                                endPos.Y==dPos[1])
                                return false;
                            break;
                        case 2://right, so wall must be vertical, with origin x,y
                            if(dPos[0]==dest.origin.X&& dPos[1]==dest.origin.Y &&
                                endPos.X==dPos[0])
                                return false;
                            break;
                        case 3://down, so wall must be horizontal, with origin x,y+tilesize
                            if(dPos[0]==dest.origin.X && dPos[1]==dest.origin.Y+map.TileSize &&
                                endPos.Y==dPos[1])
                                return false;
                            break;
                        case 4://left, so wall must be vertical, with origin x+tile,y
                            if(dPos[0]==dest.origin.X+map.TileSize && dPos[1]==dest.origin.Y &&
                                endPos.X==dPos[0])
                                return false;
                            break;
                        default:
                            break;
                    }
                }

                    

            }
#endregion

            return true;//If no return false in checker loop, then it must be valid dest
        }
        private bool allowEnd(PatrolPath pPath)
        {
            return ((IPoint)pPath.MyWaypoints.ElementAt(0)).
                equals((IPoint)pPath.MyWaypoints.ElementAt(pPath.MyWaypoints.Count - 1));
        }
        PatrolPath findPath(int id)
        {
            foreach (PatrolPath path in paths)
                if (path.MyId == id)
                    return path;
            return null;
        }
        int findIndexInList(PatrolPath path)
        {
            foreach(KeyValuePair<int,string> _path in patrolListBox.Items)
            {
                if(_path.Key==path.MyId)//Found it, return index
                    return patrolListBox.Items.IndexOf(_path);
            }
            return 0;
        }

        public void addTileToCurrentPath(Tile tile)
        {
            if (tile != null && currentPath != null)
            {
                if (currentPath.Last() == null)//first point?
                    currentPath.MyWaypoints.Add(tile.origin);
                else if (allowDestination(myMap, currentPath.Last(), tile))
                    currentPath.MyWaypoints.Add(tile.origin);
            }
        }
        private void deleteTileFromCurrentPath(Tile tile)
        {
            //if tile is last, delete it from path
            if (tile != null && currentPath != null)
            {
                if (currentPath.Last().equals(tile.MyOrigin))
                {
                    currentPath.MyWaypoints.RemoveAt(currentPath.MyWaypoints.Count - 1);
                    deselectTile(tile);
                }
            }
        }

        public bool assignPatrolToGuard(SneakingGuard guard, PatrolPath path)
        {
            //Check start
            if(path!=null&&path.MyWaypoints!=null&&path.MyWaypoints.Count>0)
            {
                if (areAdjacent(path.MyWaypoints.First(), guard.MyPosition) &&
                    areAdjacent(path.MyWaypoints.Last(), guard.MyPosition))
                {
                    guard.MyPatrol = path;
                    path.GuardOwners++;
                    return true;
                }
            }

            return false;
        }

        //Returns whether the flat tiles with origins at p1,p2 are adjacent
        public bool areAdjacent(IPoint p1,IPoint p2)
        {
            if(p1.X==p2.X)
            {
                if(p1.Y==p2.Y-myMap.TileSize||p1.Y==p2.Y+myMap.TileSize)
                    return true;
            }else if(p2.Y==p1.Y)
            {
                if(p1.X==p2.X-myMap.TileSize||p1.X==p2.X+myMap.TileSize)
                    return true;
            }
            return false;
        }
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

       

        



        
    }
}
