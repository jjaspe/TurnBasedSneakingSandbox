using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Classes;
using SneakingCommon.Data_Classes;
using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Interfaces;
using SneakingCommon.System_Classes;
using SneakingCommon.Data_Classes;
using OpenGlGameCommon.Interfaces.Model;
using OpenGlGameCommon.Data_Classes;
using OpenGlGameCommon.Interfaces.Behaviors;

namespace SneakingCommon.Drawables
{
    public class DrawableGuard:IDrawableGuard
    {
        List<IPoint> fov;
        IVisibilityBehavior myVisibilityBehavior;
        ITileBehavior myOrientationBehavior;
        IFoVBehavior myFoVBehavior;
        IDrawableGuardMovementBehavior myMovementBehavior;
        public IPoint MyPosition
        {
            get { return myPosition; }
            set { myPosition = value; }
        }
        public string MyName
        {
            get { return name; }
            set { name = value; }
        }

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

        public List<IPoint> FOV
        {
            get { return fov; }
            set { fov = value; }
        }

        public IPoint Position
        {
            get { return myPosition; }
            set { myPosition = value; }
        }


        //View independent stuff

        int currentPatrolWaypoint = 0;
        List<IPoint> fov;
        PatrolPath patrol;
        IPoint target;
        PatrolPath targetPath;

        public DrawableGuard()
            : base()
        {
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
        
        new protected void initialize()
        {            
            FOV = new List<IPoint>();
            base.initialize();
        }

        /// <summary>
        /// Puts guard back in his first waypoint in map
        /// </summary>
        /// <param name="map"></param>
        public new void reset(OpenGlMap map)
        {
            MyPosition = MyPatrol.MyWaypoints[0];
            Target = null;
            CurrentPatrolWaypoint = 0;
            base.reset(map);
        }

        #region IDRAWABLE
        new public void draw()
        {

            int tileSize = MySize * 2;
            if (Visible)
            {
                if (MyPatrol != null && MyPatrol.DirectionLines != null && MyPatrol.MyWaypoints.Count > 0)
                {
                    //First, line from guard to first tile
                    IPoint start = new PointObj(MyPosition.X + tileSize / 2,
                        MyPosition.Y + tileSize / 2, MyPosition.Z),
                        end = new PointObj(MyPatrol.MyWaypoints.First().X + tileSize / 2,
                            MyPatrol.MyWaypoints.First().Y + tileSize / 2, MyPatrol.MyWaypoints.First().Z);
                    (new DirectionLine(start, end)).draw();

                    //Then path lines
                    foreach (DirectionLine d in MyPatrol.DirectionLines)
                        d.draw();
                }
            }
            base.draw();
        }
        #endregion

        #region FOV STUFF
        public void setFoV(OpenGlMap myMap)
        {
            IPoint tileP, cP = MyPosition;
            int distance = 0, size = myMap.TileSize;
            FOV.Clear();
            switch (MyOrientation)
            {
                case OpenGlGuard.OpenGlGuardOrientation.left:
                    while (distance < getStat("Field of View").Value)
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
                MyPosition = dest;
                return true;
            }
            return false;
        }
        #endregion        
    }
}
