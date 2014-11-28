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
using SneakingCreationWithForms.MVP;
using SneakingCommon.Enums;

namespace SneakingCreationWithForms
{
    public partial class CreateMapForm : BasicOpenGlTemplate,ICanvasWindow
    {
        bool closing = false,drawing=false;
        public selectorObj mySelector;
        public simpleOpenGlView MyView
        {
            get { return myView; }
            set { myView = value; }
        }
        String filepath= (System.Reflection.Assembly.GetExecutingAssembly().Location).
            Replace("SneakingCreationWithForms\\bin\\Debug","TBSneaking Data\\Maps\\");
        Presenter presenter;

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
                myDrawer.drawWorld(MyPresenter.Model.Map);
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
            int id = mySelector.getSelectedObjectId(new int[] { e.X, e.Y, }, MyPresenter.Model.Map);
            MyPresenter.viewClicked(id,e.Button);
        }
        void mMove(object sender, MouseEventArgs e)
        {
            clickX = e.X;
            clickY = e.Y;
        }


        #region HANDLERS

        private void MapCreation_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing = true;
        }

        private void lowBlockButton_CheckedChanged(object sender, EventArgs e)
        {
            MyPresenter.geometryElementSelected(Elements.LowBlock);
        }

        private void highBlockButton_CheckedChanged(object sender, EventArgs e)
        {
            MyPresenter.geometryElementSelected(Elements.HighBlock);
        }

        private void lowWallButton_CheckedChanged(object sender, EventArgs e)
        {
            MyPresenter.geometryElementSelected(Elements.LowWall);
        }

        private void highWallButton_CheckedChanged(object sender, EventArgs e)
        {
            MyPresenter.geometryElementSelected(Elements.HighWall);
        }

        private void applySizeButton_Click(object sender, EventArgs e)
        {
            int width=0, length=0;
            //Check textboxes have integers
            try
            {
                width = Int32.Parse(this.XTextBox.Text);
                length = Int32.Parse(this.YTextBox.Text);
                MyPresenter.createMapSelected(width, length);

                if (!drawing)
                    drawingLoop(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid integer:" +ex.Message);
            }
            
        }

        private void myView_Load(object sender, EventArgs e)
        {
            MyPresenter.geometryElementSelected(Elements.LowBlock);
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
                MyPresenter.saveMap(filename);
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
                MessageBox.Show("Couldn't Open Map:"+ex.Message);
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

            MyPresenter.loadBareMap(doc);
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
            return MyPresenter.Model.Map;
        }
        #endregion 



        
    }
}
