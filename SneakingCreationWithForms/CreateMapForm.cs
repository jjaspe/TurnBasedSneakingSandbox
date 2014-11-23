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
using OpenGlGameCommon.Classes;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Interfaces;
using Canvas_Window_Template;
using Sneaking_Gameplay.Sneaking_Drawables;
using SneakingCommon.Utility;
using SneakingCommon.Exceptions;
using System.Xml;

namespace SneakingCreationWithForms
{
    public partial class CreateMapForm : BasicOpenGlTemplate,ICanvasWindow
    {
        public static int TILE_SIZE = 20;
        bool closing = false,drawing=false;
        public SneakingMap Map;
        public selectorObj mySelector;
        public simpleOpenGlView MyView
        {
            get { return myView; }
            set { myView = value; }
        }
        String filepath= (System.Reflection.Assembly.GetExecutingAssembly().Location).
            Replace("SneakingCreationWithForms\\bin\\Debug","TBSneaking Data\\Maps\\");

        public int clickX { get; set; }
        public int clickY { get; set; }

        public CreateMapForm()
        {
            InitializeComponent();
            MyView.InitializeContexts();

            myNavigator.Orientation = Common.planeOrientation.Z;
            myNavigator.MyWindowOwner = this;
            myNavigator.MyView = this.MyView;

            this.WindowState = FormWindowState.Maximized;

            this.MyView.MouseClick += new MouseEventHandler(mClick);
            mySelector = new selectorObj(MyView);
            this.MyView.Dock = DockStyle.None;
        }

        public void drawingLoop(ICanvasWindow window)
        {
            Common myDrawer = new Common();
            simpleOpenGlView MyView = window.getView();
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
                window.refresh();
                Application.DoEvents();
            }
            drawing = false;
        }


        void keyboard(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyData;
            Keys mod = e.Modifiers;

            /* This time the controls are:    	
              "a": move left
              "d": move right
              "w": move forward
              "s": move back
              "t": toggle depth-testing
            */
            /*
            switch (key)
            {
                case Keys.A:
                    xTranslate = tileSize;
                    break;
                case Keys.D:
                    xTranslate = -tileSize;
                    break;
                case Keys.W:
                    zTranslate = tileSize;
                    break;
                case Keys.S:
                    zTranslate = -tileSize;
                    break;
                case Keys.T:
                    if (is_depth)
                    {
                        is_depth = false;
                        Gl.glDisable(Gl.GL_DEPTH_TEST);
                    }
                    else
                    {
                        is_depth = true;
                        Gl.glEnable(Gl.GL_DEPTH_TEST);
                    }
                    break;
                case Keys.R:
                    yRotate = -totYRot;
                    break;
                case Keys.Escape:
                    finished = true;
                    break;
            }
            glDraw();*/
        }
        void mDown(object sender, MouseEventArgs e)
        {
            clickX = e.X;
            clickY = e.Y;
        }
        void mClick(object sender, MouseEventArgs e)
        {
            //Check all objects, see if any was selected
            int id = mySelector.getSelectedObjectId(new int[] {e.X,e.Y},Map);
            Tile tile;
            LowBlock lBlock; HighBlock hBlock; LowWall lWall; HighWall hWall;
            //Check type
            if (id > -1)
            {
                switch (id %GameObjects.objectTypes)
                {
                    case Tile.idType://Tile
                        tile = Map.getTile(id);
                        if (tile != null)
                        {
                            if (e.Button == MouseButtons.Left)
                                tileLeftClick(tile);
                            else if (e.Button == MouseButtons.Right)
                                tileRightClick(tile);
                        }
                        break;
                    case LowBlock.idType://Low Block
                        lBlock = Map.getLowBlock(id);
                        if (e.Button == MouseButtons.Left)
                            lowBlockLeftClick(lBlock);
                        else if (e.Button == MouseButtons.Right)
                            lowBlockRightClick(lBlock);
                        break;
                    case HighBlock.idType://High block
                         hBlock = Map.getHighBlock(id);
                        if (e.Button == MouseButtons.Left)
                            highBlockLeftClick(hBlock);
                        else if (e.Button == MouseButtons.Right)
                            highBlockRightClick(hBlock);
                        break;
                    case LowWall.idType://Low wall
                        lWall = Map.getLowWall(id);
                        if (e.Button == MouseButtons.Left)
                            lowWallLeftClick(lWall);
                        else if (e.Button == MouseButtons.Right)
                            lowWallRightClick(lWall);
                        break;
                    case HighWall.idType://High wall
                        hWall = Map.getHighWall(id);
                        if (e.Button == MouseButtons.Left)
                            highWallLeftClick(hWall);
                        else if (e.Button == MouseButtons.Right)
                            highWallRightClick(hWall);
                        break;
                    default:
                        break;
                }
            }
            
            
        }
        void mMove(object sender, MouseEventArgs e)
        {
            clickX = e.X;
            clickY = e.Y;
        }

        bool hasWall(IPoint origin,int tileSize)
        {
            double[] originA = origin.toArray();
            foreach (IDrawable obj in (Map as IWorld).getEntities())
            {
                if (obj.getId() % GameObjects.objectTypes == 3)//low walls
                {
                    if ( (new PointObj(obj.getPosition())).equals(origin)   && 
                        ((LowWall)obj).MyTiles[0,0].MyEnd.X==origin.X+tileSize ) 
                        return true;
                }
                if (obj.getId() % GameObjects.objectTypes == 4)//high walls
                {
                    if ((new PointObj(obj.getPosition())).equals(origin) && 
                        ((HighWall)obj).MyTiles[0,0].MyEnd.X==origin.X+tileSize)
                        return true;
                }
            }
            return false;
        }

        void tileLeftClick(Tile tile)
        {
            foreach(Control ctr in addGroup.Controls)
            {
                if (ctr.GetType() != typeof(RadioButton))
                    continue;
                if (((RadioButton)ctr).Checked)
                {
                    switch(Int32.Parse(ctr.Tag.ToString()))
                    {
                        case 0: // Add low block                            
                                Map.Drawables.Add(
                                    new LowBlock(tile.MyOrigin,Map.TileSize,Common.colorRed,Common.colorBlack));
                            break;
                        case 1: // Add high block
                            Map.Drawables.Add(
                                    new HighBlock(tile.MyOrigin, Map.TileSize, Common.colorRed, Common.colorBlack));
                            break;
                        case 2: // Add low wall
                            if (!hasWall(tile.MyOrigin, Map.TileSize))
                            Map.Drawables.Add(
                                new LowWall(tile.origin.Y, tile.origin.X, 
                                    tile.origin.X + Map.TileSize, Map.TileSize));
                            break;
                        case 3: // Add high wall
                            if (!hasWall(tile.MyOrigin, Map.TileSize))
                            Map.Drawables.Add(
                                new HighWall(tile.origin.Y, tile.origin.X,
                                    tile.origin.X + Map.TileSize, Map.TileSize));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        void tileRightClick(Tile tile)
        {
        }
        void lowBlockLeftClick(LowBlock block)
        {
        }
        void lowBlockRightClick(LowBlock block)
        {
            Map.Drawables.Remove(block);
        }
        void highBlockLeftClick(HighBlock block)
        {
        }
        void highBlockRightClick(HighBlock block)
        {
            Map.Drawables.Remove(block);
        }
        void lowWallRightClick(LowWall lWall)
        {
            Map.Drawables.Remove(lWall);
        }
        void lowWallLeftClick(LowWall lWall)
        {
            //Check bounds, if dest is outside, only remove source
            if (lWall.MyTiles[0, 0].MyOrigin.Y >= Map.MyHeight * Map.TileSize / 2)
            {
                Map.Drawables.Remove(lWall);
                return;
            }

            //Check destination
            foreach (IDrawable obj in (Map as IWorld).getEntities())
            {
                if (obj.getId() %GameObjects.objectTypes == 3 )//low walls
                {
                    //Check that there isn't a tile in destination, if so remove source
                    if (((LowWall)obj).MyTiles[0, 0].MyOrigin.equals(lWall.MyTiles[0, 0].MyOrigin)
                        && ((LowWall)obj).MyTiles[0, 0].MyEnd.Y - Map.TileSize == lWall.MyTiles[0, 0].MyEnd.Y)
                    {
                        Map.Drawables.Remove(lWall);
                        return;
                    }
                }
                if (obj.getId() %GameObjects.objectTypes == 4)//high walls
                {
                    //Check that there isn't a tile in destination, if so remove source
                    if (((HighWall)obj).MyTiles[0, 0].MyOrigin.equals(lWall.MyTiles[0, 0].MyOrigin)
                        && ((HighWall)obj).MyTiles[0, 0].MyEnd.Y - Map.TileSize == lWall.MyTiles[0, 0].MyEnd.Y)
                    {
                        Map.Drawables.Remove(lWall);
                        return;
                    }
                }
            }

            //Change Orientation
            lWall.MyTiles[0, 0].MyEnd.Y += Map.TileSize;
            lWall.MyTiles[0, 0].MyEnd.X -= Map.TileSize;
        }
        void highWallRightClick(HighWall hWall)
        {
            Map.Drawables.Remove(hWall);
        }
        void highWallLeftClick(HighWall hWall)
        {
            //Check bounds, if dest is outside, only remove source
            if (hWall.MyTiles[0, 0].MyOrigin.Y >= Map.MyHeight * Map.TileSize / 2)
            {
                Map.Drawables.Remove(hWall);
                return;
            }

            //Check destination
            foreach (IDrawable obj in (Map as IWorld).getEntities())
            {
                if (obj.getId() %GameObjects.objectTypes == 3)//low walls
                {
                    //Check that there isn't a tile in destination, if so remove source
                    if (((LowWall)obj).MyTiles[0, 0].MyOrigin.equals(hWall.MyTiles[0, 0].MyOrigin)
                        && ((LowWall)obj).MyTiles[0, 0].MyEnd.Y - Map.TileSize == hWall.MyTiles[0, 0].MyEnd.Y)
                    {
                        Map.Drawables.Remove(hWall);
                        return;
                    }
                }
                if (obj.getId() %GameObjects.objectTypes == 4)//high walls
                {
                    //Check that there isn't a tile in destination, if so remove source
                    if (((HighWall)obj).MyTiles[0, 0].MyOrigin.equals(hWall.MyTiles[0, 0].MyOrigin)
                        && ((HighWall)obj).MyTiles[0, 0].MyEnd.Y - Map.TileSize == hWall.MyTiles[0, 0].MyEnd.Y)
                    {
                        Map.Drawables.Remove(hWall);
                        return;
                    }
                }
            }

            //Change Orientation
            hWall.MyTiles[0, 0].MyEnd.Y += Map.TileSize;
            hWall.MyTiles[0, 0].MyEnd.X -= Map.TileSize;

            hWall.MyTiles[1, 0].MyEnd.Y += Map.TileSize;
            hWall.MyTiles[1, 0].MyEnd.X -= Map.TileSize;
        }

        #region HANDLERS

        private void applySizeButton_Click(object sender, EventArgs e)
        {
            int width=Map.MyWidth,length=Map.MyHeight;
            //Check textboxes have integers
            try
            {
                width = Int32.Parse(this.XTextBox.Text);
                length = Int32.Parse(this.YTextBox.Text);
            }
            catch (Exception ex)
            {
            }
            
            IPoint origin = new pointObj(-width * TILE_SIZE/ 2, -length * TILE_SIZE / 2, 0);
            Map = SneakingMap.createInstance(width, length, TILE_SIZE, origin);
            
            drawingLoop(this);
        }

        private void myView_Load(object sender, EventArgs e)
        {

        }

        private void eyeFrontMenuItem_Click(object sender, EventArgs e)
        {
            myView.PerspectiveEye = this.MyView.eyeFront;
        }

        private void eyeTopMenuItem_Click(object sender, EventArgs e)
        {
            myView.PerspectiveEye = this.MyView.eyeTop;
        }

        private void cornerMenuItem_Click(object sender, EventArgs e)
        {
            myView.PerspectiveEye = this.MyView.eyeFrontIso;
        }

        private void isometricMenuItem_Click_1(object sender, EventArgs e)
        {
            myView.PerspectiveEye = this.MyView.eyeIso;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            SaveFileDialog mapDialog = new SaveFileDialog();
            mapDialog.Filter = "map Files (*.map)|*.map";
            mapDialog.DefaultExt = ".map";
            mapDialog.InitialDirectory = filepath;

            string filename = mapDialog.ShowDialog() == DialogResult.OK ? mapDialog.FileName : null;
            if (filename == null)
            {
                MessageBox.Show("Couldn't open file");
                return;
            }

            try
            {
                XmlLoader.saveBareMap(filename, Map);
            }
            catch (InvalidMapException exc)
            {
                MessageBox.Show("Error saving map\n"+exc.message);
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog mapDialog = new OpenFileDialog();
            mapDialog.Filter = "map Files (*.map)|*.map";
            mapDialog.DefaultExt = ".map";
            mapDialog.InitialDirectory = "//";
            String filename = mapDialog.ShowDialog() == DialogResult.OK ? mapDialog.FileName : null;
            if (filename == null)
            {
                MessageBox.Show("Couldn't find file");
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
                drawingLoop(this);

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
            return Map;
        }
        #endregion 


        private void MapCreation_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing = true;
        }

        
    }
}
