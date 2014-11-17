using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using CharacterSystemLibrary.Classes;
using Sneaking_Classes.System_Classes;
using SneakingClasses.System_Classes;

namespace Sneaking_Classes.Drawing_Classes
{
    public class Guard:Character,IDrawable
    {
        public enum orientation { none,up, right, down, left };
        static int guardIds=5;
        public const int idType = 5;
        public bool Visible = false;
        
        public int MySize
        {
            get { return size; }
            set { size = value; setImage(); }
        }        
        public Tile MyCurrentTile
        {
            get { return myCurrentTile; }
            set
            {
                if(value==null)
                    myCurrentTile=null;
                else if (myCurrentTile == null)
                {
                    myCurrentTile = value;
                    myPosition = myCurrentTile.MyOrigin;
                    setImage();
                }
                else//A currentTile exists, so a change means movement
                {
                    setOrientation(value);
                    if (MyOrientation != orientation.none)
                    {
                        myCurrentTile = value; 
                        setImage();
                        myPosition = myCurrentTile.MyOrigin;
                    }            
                }
            }
        }
        public pointObj MyPosition
        {
            get { return myPosition; }
            set { myPosition = value;}
        }
        public orientation MyOrientation
        {
            get { return myOrientation; }
            set { myOrientation = value; }
        }
        public int CurrentPatrolWaypoint
        {
            get { return currentPatrolWaypoint; }
            set { currentPatrolWaypoint = value; }
        }
        public pointObj Target
        {
            get { return target; }
            set { target = value; }
        }
        public PatrolPath TargetPath
        {
            get { return targetPath; }
            set { targetPath = value; }
        }
        public PatrolPath MyPatrol
        {
            get { return patrol; }
            set { patrol = value; }
        }
        public List<pointObj> FOV
        {
            get { return fov; }
            set { fov = value; }
        }
        public List<pointObj> rememberedPoints=new List<pointObj>();
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

        int myId, size;
        Tile myCurrentTile;
        pointObj myPosition;
        int currentPatrolWaypoint = 0;
        orientation myOrientation;
        rombusObj myRombus;
        Tile mySquare;
        List<pointObj> fov;
        PatrolPath patrol;
        pointObj target;        
        PatrolPath targetPath;
        NoiseMap myNoiseMap;
        NoiseMap myKnownNoiseMap;
        IDrawable myImage;

        public Guard()
        {
            myPosition=new pointObj(0, 0, 0);
            initialize();
        }
        public Guard(pointObj position,int size)
        {
            myPosition= position;
            MySize = size;
            initialize();
        }
        private void initialize()
        {
            myId = guardIds;
            size = 10;
            myOrientation = orientation.none;       
            FOV = new List<pointObj>();
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

            myNoiseMap = new NoiseMap();
        }
        public void reset(Map map)
        {
            MyCurrentTile = null;
            MyCurrentTile = map.getTile(MyPatrol.MyWaypoints[0]);
            rememberedPoints = new List<pointObj>();
            Target = null;
            CurrentPatrolWaypoint = 0;
        }
        public void setImage()
        {
            //Create a blue rectangle, that extends to edge of tile only on orientation direction
            // The other sides will be shortened by tileSize/4
            if(myPosition==null||myCurrentTile==null)
                return;
            int sizeDiff=myCurrentTile.TileSize-size;
            pointObj _bLeft=new pointObj(),_bRight=new pointObj(),
                _tLeft=new pointObj(),_tRight=new pointObj(),_tO=myCurrentTile.MyOrigin;
            GuardRectangle rect = new GuardRectangle();

            #region Determine orientation
            //Determine long side
            switch (MyOrientation)
            {
                case orientation.left://Make distances from {_bLeft,_bRight}{_tLeft,_tRight} Long
                    _bLeft = new pointObj(_tO.X, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tLeft = new pointObj(_tO.X, _tO.Y + size + sizeDiff / 2, _tO.Z + 1);
                    _bRight = new pointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tRight = new pointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2 + size, _tO.Z + 1);
                    break;
                case orientation.right://Make distances from {_bLeft,_bRight}{_tLeft,_tRight} Long
                    _bLeft = new pointObj(_tO.X + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tLeft = new pointObj(_tO.X + sizeDiff / 2, _tO.Y + size + sizeDiff / 2, _tO.Z + 1);
                    _bRight = new pointObj(_tO.X + size + sizeDiff, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tRight = new pointObj(_tO.X + size + sizeDiff, _tO.Y + sizeDiff / 2 + size, _tO.Z + 1);
                    break;
                case orientation.up://Make distances from {_bLeft,_tLeft}{_tRight,_bRight} Long
                    _bLeft = new pointObj(_tO.X + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tLeft = new pointObj(_tO.X + sizeDiff / 2, _tO.Y + size + sizeDiff, _tO.Z + 1);
                    _bRight = new pointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tRight = new pointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff + size, _tO.Z + 1);
                    break;
                case orientation.down://Make distances from {_bLeft,_tLeft}{_tRight,_bRight} Long                   
                    _bLeft = new pointObj(_tO.X + sizeDiff / 2, _tO.Y, _tO.Z + 1);
                    _tLeft = new pointObj(_tO.X + sizeDiff / 2, _tO.Y + size + sizeDiff / 2, _tO.Z + 1);
                    _bRight = new pointObj(_tO.X + size + sizeDiff / 2, _tO.Y, _tO.Z + 1);
                    _tRight = new pointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2 + size, _tO.Z + 1);
                    break;
                case orientation.none://Make tile
                    _bLeft = new pointObj(_tO.X + sizeDiff / 2, _tO.Y+sizeDiff/2, _tO.Z + 1);
                    break;
                default:
                    break;
            }
            #endregion

            //If orientation is none, create a square else create rectangle
            if (MyOrientation == orientation.none)
                myImage = ((IDrawable)new Tile(_bLeft.toIntArray(), size));
            else
            {
                rect.MyRectangle = new rectangleObj(_bLeft, _tLeft, _bRight, _tRight,
                    Common.colorBlue, Common.colorBlack);
                myImage = rect;
            }
            
        }
        private void setOrientation(Tile nextTile)
        {
            if (nextTile.MyOrigin.X == myCurrentTile.MyOrigin.X)//Vertical Movement
            {
                if (nextTile.MyOrigin.Y == myCurrentTile.MyOrigin.Y + myCurrentTile.TileSize)//up
                    MyOrientation = orientation.up;
                else if (nextTile.MyOrigin.Y == myCurrentTile.MyOrigin.Y - myCurrentTile.TileSize)//down
                    myOrientation = orientation.down;
                else
                    myOrientation = orientation.none;
            }
            else if (nextTile.MyOrigin.Y == myCurrentTile.MyOrigin.Y)//Horizontal Movement
            {
                if (nextTile.MyOrigin.X == myCurrentTile.MyOrigin.X + myCurrentTile.TileSize)//right
                    MyOrientation = orientation.right;
                else if (nextTile.MyOrigin.X == myCurrentTile.MyOrigin.X - myCurrentTile.TileSize)//left
                    myOrientation = orientation.left;
                else
                    myOrientation = orientation.none;
            }
            else
                myOrientation = orientation.none;
        }
        
        public void turnQ()
        {
            myOrientation++;
            setImage();
        }

        #region REMEMBERED POINTS STUFF
        public void addRememberedPoints(List<pointObj> newPoints)
        {
            foreach (pointObj p in newPoints)
            {
                if (rememberedPoints.Find(delegate(pointObj _p) { return p.equals(_p); }) == null)
                    rememberedPoints.Add(p);
            }
        }
        #endregion
        #region FOV STUFF
        public void setFoV(Map myMap)
        {
            pointObj tileP, cP = MyCurrentTile.MyOrigin;
            int distance = 0, size = MyCurrentTile.TileSize;
            FOV.Clear();
            switch (MyOrientation)
            {
                case Guard.orientation.left:
                    while (distance < getStat("Field of View").Value)
                    {
                        for (int j = -distance; j <= distance; j++)
                        {
                            tileP = new pointObj(cP.X - distance * size, cP.Y + j * size, cP.Z);
                            if (myMap.getTile(tileP) != null)
                                FOV.Add(tileP);
                        }
                        distance++;
                    }
                    break;
                case Guard.orientation.right:
                    while (distance < getStat("Field of View").Value)
                    {
                        for (int j = -distance; j <= distance; j++)
                        {
                            tileP = new pointObj(cP.X + distance * size, cP.Y + j * size, cP.Z);
                            if (myMap.getTile(tileP) != null)
                                FOV.Add(tileP);
                        }
                        distance++;
                    }
                    break;
                case Guard.orientation.up:
                    while (distance < getStat("Field of View").Value)
                    {
                        for (int j = -distance; j <= distance; j++)
                        {
                            tileP = new pointObj(cP.X + j * size, cP.Y + distance * size, cP.Z);
                            if (myMap.getTile(tileP) != null)
                                FOV.Add(tileP);
                        }
                        distance++;
                    }
                    break;
                case Guard.orientation.down:
                    while (distance < getStat("Field of View").Value)
                    {
                        for (int j = -distance; j <= distance; j++)
                        {
                            tileP = new pointObj(cP.X + j * size, cP.Y - distance * size, cP.Z);
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
        #endregion


        public NoiseMap getModifiedNoiseMap(NoiseMap noiseMap, Map map)
        {
            NoiseMap newNoiseMap = noiseMap;
            newNoiseMap.modify((int)this.getStat("Perception").Value,
                                map.getDistanceMap(this.MyPosition));//Modify noise map
            return newNoiseMap;
        }
        #region PATROL STUFF
        public bool hasWaypoints()
        {
            return (MyPatrol != null && MyPatrol.MyWaypoints!=null && MyPatrol.MyWaypoints.Count > 1);
        }
        
        public bool moveGuard(pointObj dest, Map myMap)
        {
            Tile newP = myMap.getTile(dest);
            if (newP != null)
            {
                MyCurrentTile = newP;
                return true;
            }
            return false;
        }
        #endregion        

        #region IDRAWABLE 
        public void draw()
        {
            if (Visible)
            {
                myImage.draw();
                int tileSize = myCurrentTile.TileSize;

                if (patrol != null && patrol.DirectionLines != null && patrol.MyWaypoints.Count > 0)
                {
                    //First, line from guard to first tile
                    pointObj start = new pointObj(MyPosition.X + tileSize / 2,
                        MyPosition.Y + tileSize / 2, MyPosition.Z),
                        end = new pointObj(patrol.MyWaypoints.First().X + tileSize / 2,
                            patrol.MyWaypoints.First().Y + tileSize / 2, patrol.MyWaypoints.First().Z);
                    (new DirectionLine(start, end)).draw();

                    //Then path lines
                    foreach (DirectionLine d in patrol.DirectionLines)
                        d.draw();
                }
            }
        }
        public int getId()
        {
            return myId;
        }
        public int[] getPosition()
        {
            return myPosition.toIntArray();
        }
        #endregion
    }

}
