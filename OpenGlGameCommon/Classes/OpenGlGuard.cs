using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;using Canvas_Window_Template.Interfaces;
using CharacterSystemLibrary.Classes;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Entities;

namespace OpenGlGameCommon.Classes
{
    public class OpenGlGuard:Guard,IDrawable
    {
        public enum OpenGlGuardOrientation { none, up, right, down, left };


        static int guardIds=5;
        public const int idType = 5;
        public bool Visible = false;

        //OpenGLStuff
        public int MySize
        {
            get { return size; }
            set { size = value; setImage(); }
        }        
        int myId, size;
        protected IDrawable myImage;
        protected OpenGlGuardOrientation myOrientation;
        public OpenGlGuardOrientation MyOrientation
        {
            get { return myOrientation; }
            set { myOrientation = value; }
        }

        public OpenGlGuard()
        {
            MyPosition=new PointObj(0, 0, 0);
            initialize();
        }
        public OpenGlGuard(IPoint position,int size)
        {
            MyPosition= position;
            MySize = size;
            initialize();
        }
        new protected void initialize()
        {
            myId = guardIds;
            size = 10;
            MyOrientation = OpenGlGuardOrientation.none;
            setImage();

            guardIds += GameObjects.objectTypes;
            this.addStat(new Stat("Strength", 0));
            this.addStat(new Stat("Armor", 0));
            this.addStat(new Stat("Weapon Skill", 0));
            this.addStat(new Stat("Perception", 0));
            this.addStat(new Stat("Intelligence", 0));
            this.addStat(new Stat("Dexterity", 0));
            this.addStat(new Stat("Suspicion", 0));
            this.addStat(new Stat("Alert Status", 0));
            this.addStat(new Stat("Field of View", 0));
            this.addStat(new Stat("Suspicion Propensity", 0));
            this.addStat(new Stat("AP", 0));
            this.addStat(new Stat("Knows Map", 0));
        }
        public void reset(OpenGlMap map)
        {
            
        }
        public void setImage()
        {
            int tileSize = MySize * 2;
            //Create a blue rectangle, that extends to edge of tile only on orientation direction
            // The other sides will be shortened by tileSize/4
            if(MyPosition==null)
                return;
            int sizeDiff=tileSize-size;
            IPoint _bLeft=new PointObj(),_bRight=new PointObj(),
                _tLeft=new PointObj(),_tRight=new PointObj(),_tO=MyPosition;
            GuardRectangle rect = new GuardRectangle();

            #region Determine orientation
            //Determine long side
            switch (MyOrientation)
            {
                case OpenGlGuardOrientation.left://Make distances from {_bLeft,_bRight}{_tLeft,_tRight} Long
                    _bLeft = new PointObj(_tO.X, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tLeft = new PointObj(_tO.X, _tO.Y + size + sizeDiff / 2, _tO.Z + 1);
                    _bRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2 + size, _tO.Z + 1);
                    break;
                case OpenGlGuardOrientation.right://Make distances from {_bLeft,_bRight}{_tLeft,_tRight} Long
                    _bLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + size + sizeDiff / 2, _tO.Z + 1);
                    _bRight = new PointObj(_tO.X + size + sizeDiff, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tRight = new PointObj(_tO.X + size + sizeDiff, _tO.Y + sizeDiff / 2 + size, _tO.Z + 1);
                    break;
                case OpenGlGuardOrientation.up://Make distances from {_bLeft,_tLeft}{_tRight,_bRight} Long
                    _bLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + size + sizeDiff, _tO.Z + 1);
                    _bRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff + size, _tO.Z + 1);
                    break;
                case OpenGlGuardOrientation.down://Make distances from {_bLeft,_tLeft}{_tRight,_bRight} Long                   
                    _bLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y, _tO.Z + 1);
                    _tLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + size + sizeDiff / 2, _tO.Z + 1);
                    _bRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y, _tO.Z + 1);
                    _tRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2 + size, _tO.Z + 1);
                    break;
                case OpenGlGuardOrientation.none://Make tile
                    _bLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y+sizeDiff/2, _tO.Z + 1);
                    break;
                default:
                    break;
            }
            #endregion

            //If orientation is none, create a square else create rectangle
            if (MyOrientation == OpenGlGuardOrientation.none)
                myImage = ((IDrawable)new Tile(_bLeft.toArray(), size));
            else
            {
                rect.MyRectangle = new rectangleObj(_bLeft, _tLeft, _bRight, _tRight,
                    Common.colorBlue, Common.colorBlack);
                myImage = rect;
            }
            
        }
        protected void setOrientation(IPoint nextOrigin)
        {
            int tileSize = MySize * 2;
            if (nextOrigin.X == MyPosition.X)//Vertical Movement
            {
                if (nextOrigin.Y == MyPosition.Y + tileSize)//up
                    MyOrientation = OpenGlGuardOrientation.up;
                else if (nextOrigin.Y == MyPosition.Y - tileSize)//down
                    myOrientation = OpenGlGuardOrientation.down;
                else
                    myOrientation = OpenGlGuardOrientation.none;
            }
            else if (nextOrigin.Y == MyPosition.Y)//Horizontal Movement
            {
                if (nextOrigin.X == MyPosition.X + tileSize)//right
                    MyOrientation = OpenGlGuardOrientation.right;
                else if (nextOrigin.X == MyPosition.X - tileSize)//left
                    myOrientation = OpenGlGuardOrientation.left;
                else
                    myOrientation = OpenGlGuardOrientation.none;
            }
            else
                myOrientation = OpenGlGuardOrientation.none;
        }
        public IPoint getEyeLevel(OpenGlMap map)
        {
            Tile tile = map.getTile(MyPosition);
            return new PointObj((int)tile.getCenter()[0],
                (int)tile.getCenter()[1],
                2 * tile.TileSize);
        }
        public void turnQ()
        {
            MyOrientation++;
        }

        #region IDRAWABLE 
        public void draw()
        {
            if (Visible)
            {
                setImage();
                myImage.draw();
            }
        }
        public int getId()
        {
            return myId;
        }
        public double[] getPosition()
        {
            return MyPosition.toArray();
        }
        public void setPosition(IPoint newPosition)
        {
            MyPosition = newPosition;
        }
        #endregion

        bool Canvas_Window_Template.Interfaces.IDrawable.Visible
        {
            set { return; }
        }
    }

}
