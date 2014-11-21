using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Classes;
using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Interfaces;
using OpenGlGameCommon.Interfaces.Model;
using OpenGlGameCommon.Data_Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;
using OpenGlGameCommon.Interfaces.Behaviors;
using CharacterSystemLibrary.Classes;
using OpenGlGameCommon.Enums;

namespace OpenGlGameCommon.Drawables
{
    public class DrawableGuard:IDrawableGuard
    {
        #region IDRAWABLE atts
        IPoint myPosition;
        static int guardIds = 5;
        public const int idType = 5;
        public bool Visible = false;        
        int myId, size;
        protected IDrawable myImage;
        public int MySize
        {
            get { return size; }
            set { size = value; setImage(); }
        }
        public IPoint Position
        {
            get { return myPosition; }
            set { myPosition = value; }
        }
        #endregion

        #region IDrawableGuard Behaviors
        IVisibilityBehavior myVisibilityBehavior;
        ITileBehavior myOrientationBehavior;
        IFoVBehavior myFoVBehavior;
        IDrawableGuardMovementBehavior myMovementBehavior;
        public IDrawableGuardMovementBehavior MovementBehavior
        {
            get { return myMovementBehavior; }
            set { myMovementBehavior = value; }
        }
        public ITileBehavior OrientationBehavior
        {
            get { return myOrientationBehavior; }
            set { myOrientationBehavior = value; }
        }
        public IVisibilityBehavior VisibilityBehavior
        {
            get { return myVisibilityBehavior; }
            set { myVisibilityBehavior = value; }
        }
        public IFoVBehavior FoVBehavior
        {
            get { return myFoVBehavior; }
            set { myFoVBehavior = value; }
        }
        #endregion

        Character myCharacter;
        GuardOrientation myOrientation;
        int currentPatrolWaypoint = 0;
        List<IPoint> fov;
        PatrolPath patrol;
        IPoint target;
        PatrolPath targetPath;

        public GuardOrientation MyOrientation
        {
            get { return myOrientation; }
            set { myOrientation = value; }
        }
        public int CurrentPatrolWaypoint
        {
            get { return currentPatrolWaypoint; }
            set { currentPatrolWaypoint = value; }
        }
        public IPoint Target
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
        public List<IPoint> FOV
        {
            get { return fov; }
            set { fov = value; }
        }
        public string getName()
        {
            return MyCharacter.Name;
        }
        public Character MyCharacter
        {
            get { return myCharacter; }
            set { myCharacter = value; }
        }
        public DrawableGuard()
        {
            MyCharacter = new Character();
        }
        protected void initialize()
        {            
            FOV = new List<IPoint>();
            myId = guardIds;
        }

        /// <summary>
        /// Puts guard back in his first waypoint in map
        /// </summary>
        /// <param name="map"></param>
        public void reset(OpenGlMap map)
        {
            Position = MyPatrol.MyWaypoints[0];
            Target = null;
            CurrentPatrolWaypoint = 0;
        }

        /// <summary>
        /// Creates an image for the guard using a blue rombus
        /// </summary>
        public void setImage()
        {
            int tileSize = MySize * 2;
            //Create a blue rectangle, that extends to edge of tile only on orientation direction
            // The other sides will be shortened by tileSize/4
            if (Position == null)
                return;
            int sizeDiff = tileSize - size;
            IPoint _bLeft = new PointObj(), _bRight = new PointObj(),
                _tLeft = new PointObj(), _tRight = new PointObj(), _tO = Position;
            GuardRectangle rect = new GuardRectangle();

            #region Determine orientation
            //Determine long side
            switch (MyOrientation)
            {
                case GuardOrientation.left://Make distances from {_bLeft,_bRight}{_tLeft,_tRight} Long
                    _bLeft = new PointObj(_tO.X, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tLeft = new PointObj(_tO.X, _tO.Y + size + sizeDiff / 2, _tO.Z + 1);
                    _bRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2 + size, _tO.Z + 1);
                    break;
                case GuardOrientation.right://Make distances from {_bLeft,_bRight}{_tLeft,_tRight} Long
                    _bLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + size + sizeDiff / 2, _tO.Z + 1);
                    _bRight = new PointObj(_tO.X + size + sizeDiff, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tRight = new PointObj(_tO.X + size + sizeDiff, _tO.Y + sizeDiff / 2 + size, _tO.Z + 1);
                    break;
                case GuardOrientation.up://Make distances from {_bLeft,_tLeft}{_tRight,_bRight} Long
                    _bLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + size + sizeDiff, _tO.Z + 1);
                    _bRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    _tRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff + size, _tO.Z + 1);
                    break;
                case GuardOrientation.down://Make distances from {_bLeft,_tLeft}{_tRight,_bRight} Long                   
                    _bLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y, _tO.Z + 1);
                    _tLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + size + sizeDiff / 2, _tO.Z + 1);
                    _bRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y, _tO.Z + 1);
                    _tRight = new PointObj(_tO.X + size + sizeDiff / 2, _tO.Y + sizeDiff / 2 + size, _tO.Z + 1);
                    break;
                case GuardOrientation.none://Make tile
                    _bLeft = new PointObj(_tO.X + sizeDiff / 2, _tO.Y + sizeDiff / 2, _tO.Z + 1);
                    break;
                default:
                    break;
            }
            #endregion

            //If orientation is none, create a square else create rectangle
            if (MyOrientation == GuardOrientation.none)
                myImage = ((IDrawable)new Tile(_bLeft.toArray(), size));
            else
            {
                rect.MyRectangle = new rectangleObj(_bLeft, _tLeft, _bRight, _tRight,
                    Common.colorBlue, Common.colorBlack);
                myImage = rect;
            }

        }

        /// <summary>
        /// Calculates guardOrientation based on difference between the guards' current 
        /// origin and nextOrigin (i.e. if current is due west of nextOrigin, orientation is right)
        /// </summary>
        /// <param name="nextOrigin"></param>
        protected void setOrientation(IPoint nextOrigin)
        {
            int tileSize = MySize * 2;
            if (nextOrigin.X == Position.X)//Vertical Movement
            {
                if (nextOrigin.Y == Position.Y + tileSize)//up
                    MyOrientation = GuardOrientation.up;
                else if (nextOrigin.Y == Position.Y - tileSize)//down
                    myOrientation = GuardOrientation.down;
                else
                    myOrientation = GuardOrientation.none;
            }
            else if (nextOrigin.Y == Position.Y)//Horizontal Movement
            {
                if (nextOrigin.X == Position.X + tileSize)//right
                    MyOrientation = GuardOrientation.right;
                else if (nextOrigin.X == Position.X - tileSize)//left
                    myOrientation = GuardOrientation.left;
                else
                    myOrientation = GuardOrientation.none;
            }
            else
                myOrientation = GuardOrientation.none;
        }

        /// <summary>
        /// Returns a point at the same position as guard but at 2*tileSize height (z-dir)
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public IPoint getEyeLevel(OpenGlMap map)
        {
            Tile tile = map.getTile(Position);
            return new PointObj((int)tile.getCenter()[0],
                (int)tile.getCenter()[1],
                2 * tile.TileSize);
        }

        /// <summary>
        /// Turns the guard 90 counterclockwise
        /// </summary>
        public void turnQ()
        {
            MyOrientation++;
        }

        #region IDRAWABLE
        public int getId()
        {
            return myId;
        }
        public double[] getPosition()
        {
            return Position.toArray();
        }
        public void setPosition(IPoint newPosition)
        {
            Position = newPosition;
        }
        bool IDrawable.Visible
        {
            set { return; }
        }
        /// <summary>
        /// Draws patrol lines and guard image
        /// </summary>
        public void draw()
        {
            int tileSize = MySize * 2;
            if (Visible)
            {
                if (MyPatrol != null && MyPatrol.DirectionLines != null && MyPatrol.MyWaypoints.Count > 0)
                {
                    //First, line from guard to first tile
                    IPoint start = new PointObj(Position.X + tileSize / 2,
                        Position.Y + tileSize / 2, Position.Z),
                        end = new PointObj(MyPatrol.MyWaypoints.First().X + tileSize / 2,
                            MyPatrol.MyWaypoints.First().Y + tileSize / 2, MyPatrol.MyWaypoints.First().Z);
                    (new DirectionLine(start, end)).draw();

                    //Then path lines
                    foreach (DirectionLine d in MyPatrol.DirectionLines)
                        d.draw();
                }

                setImage();
                myImage.draw();
            }
        }
        #endregion

        #region FOV STUFF
        public void setFoV(OpenGlMap myMap)
        {
            IPoint tileP, cP = Position;
            int distance = 0, size = myMap.TileSize;
            FOV.Clear();
            switch (MyOrientation)
            {
                case GuardOrientation.left:
                    while (distance < MyCharacter.getStat("Field of View").Value)
                    {
                        for (int j = -distance; j <= distance; j++)
                        {
                            tileP = new PointObj(cP.X - distance * size, cP.Y + j * size, cP.Z);
                            if (myMap.getTile(tileP) != null)
                                FOV.Add(tileP);
                        }
                        distance++;
                    }
                    break;
                case GuardOrientation.right:
                    while (distance < MyCharacter.getStat("Field of View").Value)
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
                case GuardOrientation.up:
                    while (distance < MyCharacter.getStat("Field of View").Value)
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
                case GuardOrientation.down:
                    while (distance < MyCharacter.getStat("Field of View").Value)
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
        #endregion

        #region PATROL STUFF
        public bool hasWaypoints()
        {
            return (MyPatrol != null && MyPatrol.MyWaypoints != null && MyPatrol.MyWaypoints.Count > 1);
        }

        public bool moveGuard(IPoint dest, OpenGlMap myMap)
        {
            Tile newP = myMap.getTile(dest);
            if (newP != null)
            {
                setOrientation(dest);
                Position = dest;
                return true;
            }
            return false;
        }
        #endregion        
    

        public List<KeyValuePair<string, int>> getAttacksInfo()
        {
            throw new NotImplementedException();
        }

        public IAttack getAttack(string name)
        {
            throw new NotImplementedException();
        }

        public List<IPoint> getFOV()
        {
            return fov;
        }

        public void updateVisiblePoints(List<IPoint> availablePoints, int height)
        {
            this.FOV = VisibilityBehavior.getFoV(this.Position, availablePoints, height);
        }

        public void updateFoV(List<IPoint> availablePoints, int height)
        {
            this.FOV = VisibilityBehavior.getFoV(Position, FoVBehavior.getFOVPoints(this, availablePoints), height);
        }
        public virtual void reset()
        {
            
        }


        
    }
}
