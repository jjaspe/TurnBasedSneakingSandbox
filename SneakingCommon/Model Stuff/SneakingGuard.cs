using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.Drawables;
using Canvas_Window_Template.Basic_Drawing_Functions;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Model;


namespace Sneaking_Gameplay.Sneaking_Drawables
{
    /// <summary>
    /// Models a guard. It holds both data for the guard character (MyGuard) and an
    /// image that to draw in the map
    /// </summary>
    public class SneakingGuard:DrawableGuard
    {
        static int guardIds=5;

        //OpenGLStuff
        /// <summary>
        /// When changed, redraws image
        /// </summary>
        public new int MySize
        {
            get { return size; }
            set { size = value; setImage(); }
        }        
        int myId, size;
        IGuard myGuard;
        public IGuard MyGuard
        {
            get { return myGuard; }
            set { myGuard = value; }
        }

        /// <summary>
        /// Return the position of the guard, not of the drawable
        /// </summary>
        public new IPoint MyPosition
        {
            get { return MyGuard==null?null:MyGuard.getPosition(); }
        }
        public string MyName
        {
            get { return MyGuard.getName(); }
        }
        public SneakingGuard()
        {
            initialize();
        }
        public SneakingGuard(IPoint position,int size)
        {
            MyGuard.setPosition(position);
            MySize = size;
            initialize();
        }
        
        /// <summary>
        /// Creates id and size for image
        /// </summary>
        new private void  initialize()
        {
            myId = guardIds;
            size = 10;
            //setImage();
            guardIds += GameObjects.objectTypes;
        }        
        /// <summary>
        /// Creates guard image, using IGuard's position and orientation
        /// </summary>
        public new void setImage()
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
                case OpenGlGuard.OpenGlGuardOrientation.left://Make distances from {_bLeft,_bRight}{_tLeft,_tRight} Long
                    _bLeft = new PointObj(_tO.X, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tLeft = new PointObj(_tO.X, _tO.Y + size + sizeDiff / 2, _tO.Z + 1);
                    _bRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2 + size, _tO.Z + 1);
                    break;
                case OpenGlGuard.OpenGlGuardOrientation.right://Make distances from {_bLeft,_bRight}{_tLeft,_tRight} Long
                    _bLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + size + sizeDiff / 2, _tO.Z + 1);
                    _bRight = new PointObj(_tO.X + size + sizeDiff, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tRight = new PointObj(_tO.X + size + sizeDiff, _tO.Y + sizeDiff / 2 + size, _tO.Z + 1);
                    break;
                case OpenGlGuard.OpenGlGuardOrientation.up://Make distances from {_bLeft,_tLeft}{_tRight,_bRight} Long
                    _bLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + size + sizeDiff, _tO.Z + 1);
                    _bRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff + size, _tO.Z + 1);
                    break;
                case OpenGlGuard.OpenGlGuardOrientation.down://Make distances from {_bLeft,_tLeft}{_tRight,_bRight} Long                   
                    _bLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y, _tO.Z + 1);
                    _tLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + size + sizeDiff / 2, _tO.Z + 1);
                    _bRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y, _tO.Z + 1);
                    _tRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2 + size, _tO.Z + 1);
                    break;
                case OpenGlGuard.OpenGlGuardOrientation.none://Make tile
                    _bLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y+sizeDiff/2, _tO.Z + 1);
                    break;
                default:
                    break;
            }
            #endregion

            //If orientation is none, create a square else create rectangle
            if (MyOrientation == OpenGlGuardOrientation.none)
                myImage=((IDrawable)new Tile(_bLeft.toArray(), size));
            else
            {
                rect.MyRectangle = new rectangleObj(_bLeft, _tLeft, _bRight, _tRight,
                    Common.colorBlue, Common.colorBlack);
                myImage = rect;
            }
            
        }        
        /// <summary>
        /// Get value of guard's stat
        /// </summary>
        /// <param name="statName"></param>
        /// <returns></returns>
        public int getValue(string statName)
        {
            return myGuard.getValue(statName);
        }
        /// <summary>
        /// Returns a point at the center of the guard's tile, but raised 2 tile sizes in
        /// the z direction
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public IPoint getEyeLevel(SneakingMap map)
        {
            SneakingTile tile = (SneakingTile)map.getTile(MyPosition);
            return new PointObj((int)tile.getCenter()[0],
                (int)tile.getCenter()[1],
                2 * tile.TileSize);
        }
        
        #region FOV STUFF
        /*
        public void setFoV(OpenGlMap myMap)
        {
            IPoint tileP, cP = MyPosition;
            int distance = 0, size = myMap.TileSize;
            myGuard.getFOV().Clear();
            switch (MyOrientation)
            {
                case OpenGlGuard.OpenGlGuardOrientation.left:
                    while (distance < getStat("Field of View").Value)
                    {
                        for (int j = -distance; j <= distance; j++)
                        {
                            tileP = new PointObj(cP.X - distance * size, cP.Y + j * size, cP.Z);
                            if (myMap.getTile(tileP) != null)
                                myGuard.getFOV().Add(tileP);
                        }
                        distance++;
                    }
                    break;
                case OpenGlGuard.OpenGlGuardOrientation.right:
                    while (distance < getStat("Field of View").Value)
                    {
                        for (int j = -distance; j <= distance; j++)
                        {
                            tileP = new PointObj(cP.X + distance * size, cP.Y + j * size, cP.Z);
                            if (myMap.getTile(tileP) != null)
                                FOV.Add(tileP);
                        }
                        distance++;
                    }
                    break;
                case OpenGlGuard.OpenGlGuardOrientation.up:
                    while (distance < getStat("Field of View").Value)
                    {
                        for (int j = -distance; j <= distance; j++)
                        {
                            tileP = new PointObj(cP.X + j * size, cP.Y + distance * size, cP.Z);
                            if (myMap.getTile(tileP) != null)
                                FOV.Add(tileP);
                        }
                        distance++;
                    }
                    break;
                case OpenGlGuard.OpenGlGuardOrientation.down:
                    while (distance < getStat("Field of View").Value)
                    {
                        for (int j = -distance; j <= distance; j++)
                        {
                            tileP = new PointObj(cP.X + j * size, cP.Y - distance * size, cP.Z);
                            if (myMap.getTile(tileP) != null)
                                FOV.Add(tileP);
                        }
                        distance++;
                    }
                    break;
                default:
                    break;
            }
        }
         * */
        #endregion
    }
}
