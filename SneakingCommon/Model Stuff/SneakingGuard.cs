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
using SneakingCommon.Data_Classes;
using OpenGlGameCommon.Interfaces.Model;
using SneakingCommon.Interfaces.Behaviors;


namespace Sneaking_Gameplay.Sneaking_Drawables
{
    /// <summary>
    /// Models a guard. It holds both data for the guard character (MyGuard) and an
    /// image that to draw in the map, and noise maps for noise calculation
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
        public List<IPoint> rememberedPoints = new List<IPoint>();
        NoiseMap myNoiseMap;
        NoiseMap myKnownNoiseMap;
        ISneakingNPCBehavior NPCBehavior
        {
            get;
            set;
        }
        IKnownNoiseMapBehavior KnownNoiseMapBehavior
        {
            get;
            set;
        }
        NoiseMap UnknownNoiseMap
        {
            get;
            set;
        }
        NoiseMap NoiseMap
        {
            get;
            set;
        }
        

        /// <summary>
        /// Return the position of the guard, not of the drawable
        /// </summary>
        public new IPoint MyPosition
        {
            get { return MyGuard==null?null:MyGuard.Position; }
        }
        public string MyName
        {
            get { return MyGuard.getName(); }
        }
        public NoiseMap MyNoiseMap
        {
            get { return myNoiseMap; }
            set { myNoiseMap = value; }
        }
        public NoiseMap MyKnownNoiseMap
        {
            get { return myKnownNoiseMap; }
            set { myKnownNoiseMap = value; }
        }        

        public SneakingGuard()
        {
            initialize();
        }
        public SneakingGuard(IPoint position, int size)
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
            MyNoiseMap = new NoiseMap();
            rememberedPoints = new List<IPoint>();

            this.MyGuard.addStat(new Stat("Strength", 0));
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
        /// <summary>
        /// Creates guard image, using IDrawableGuard's position and orientation
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
            if (MyOrientation == GuardOrientation.none)
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

        public void reset()
        {
            Position = NPCBehavior.getPatrol().MyWaypoints[0];
            MyNoiseMap.initialize(0);
            NPCBehavior.reset();
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

        #region NOISE STUFF
        /// <summary>
        /// Returns a new noise map built from noiseMap and modified by the guards perception 
        /// value
        /// </summary>
        /// <param name="noiseMap"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public NoiseMap getModifiedNoiseMap(NoiseMap noiseMap, OpenGlMap map)
        {
            NoiseMap newNoiseMap = noiseMap;
            newNoiseMap.modify((int)this.getStat("Perception").Value,
                                map.getDistanceMap(this.MyPosition));//Modify noise map
            return newNoiseMap;
        }
        /// <summary>
        /// Adds points to the list of remembered points, needed for noise calculations
        /// </summary>
        /// <param name="newPoints"></param>
        public void addRememberedPoints(List<IPoint> newPoints)
        {
            foreach (IPoint p in newPoints)
            {
                if (rememberedPoints.Find(delegate(IPoint _p) { return p.equals(_p); }) == null)
                    rememberedPoints.Add(p);
            }
        }
        #endregion
    }
}
