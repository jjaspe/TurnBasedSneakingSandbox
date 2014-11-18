using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;        
using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Interfaces;                  
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Interfaces.Behaviors;
using OpenGlGameCommon.Interfaces.Model;

namespace Sneaking_Gameplay.Game_Components.Implementations.Behaviors
{
    /// <summary>
    /// Models visibility as a cone in front of the subject
    /// </summary>
    public class FoVBehaviorCone:IFoVBehavior
    {

        int distance,angle,tileSize;
        GuardOrientation myOrientation;
        IDrawableOwner myDwOwner;

        public IDrawableOwner MyDwOwner
        {
          get { return myDwOwner; }
          set { myDwOwner = value; }
        }
        public GuardOrientation MyOrientation
        {
          get { return myOrientation; }
          set { myOrientation = value; }
        }
        public int MyDistance
        {
          get { return distance; }
          set { distance = value; }
        }
        public int MyAngle
        {
          get { return angle; }
          set { angle = value; }
        }
        public int TileSize
        {
            get { return tileSize; }
            set { tileSize = value; }
        }

        public FoVBehaviorCone(int _angle, int _tileSize, IDrawableOwner _dw, int _distance = 0)
        {
            MyDistance = _distance;
            MyAngle = _angle;
            TileSize = _tileSize;
            myDwOwner = _dw;
        }
        public void setDistance(int d)
        {
            MyDistance = d;
        }

        /// <summary>
        /// To be within the cone the destination points x and y coords must be within distance of source point x and y coords.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="availablePoints"></param>
        /// <returns></returns>
        public List<IPoint> getFOVPoints(IDrawableGuard g,List<IPoint> availablePoints)
        {
            List<IPoint> conePoints = new List<IPoint>();
            this.getOrientationFromGuard(g);
            IPoint src = g.Position;
            double xDif,yDif;

            #region FILL BY CASE
            foreach (IPoint point in availablePoints)
            {
                xDif = (point.X - src.X) / TileSize ;
                yDif = (point.Y - src.Y) / TileSize;

                switch (MyOrientation)
                {
                    case GuardOrientation.up:
                        yDif ++;
                        //Points with y higher than source,x within distance, yDif>=xDif
                        if (yDif > 0 && yDif < distance && Math.Abs(xDif) < distance 
                            && Math.Abs(yDif) >= Math.Abs(xDif))
                        {
                            if(!myDwOwner.hasBlockOnTop(point))
                                conePoints.Add(point);
                        }
                        break;
                    case GuardOrientation.right:
                        xDif ++;
                        //Points with x higher than source,y within distance, yDif>=xDif
                        if (xDif > 0 && xDif < distance && Math.Abs(yDif) < distance 
                            && Math.Abs(xDif) >= Math.Abs(yDif))
                        {
                            if(!myDwOwner.hasBlockOnTop(point))
                                conePoints.Add(point);
                        }
                        break;
                    case GuardOrientation.down:
                        yDif --;
                        //Points with y lower than source,x within distance, -yDif>=xDif
                        if (-yDif > 0 && -yDif < distance && Math.Abs(xDif) < distance 
                            && Math.Abs(yDif) >= Math.Abs(xDif))
                        {
                            if (!myDwOwner.hasBlockOnTop(point))
                                conePoints.Add(point);
                        }
                        break;
                    case GuardOrientation.left:
                        xDif --;
                        //Points with x higher than source,y within distance, yDif>=xDif
                        if (-xDif > 0 && -xDif < distance && Math.Abs(yDif) < distance
                            && Math.Abs(xDif) >= Math.Abs(yDif))
                        {
                            if (!myDwOwner.hasBlockOnTop(point))
                                conePoints.Add(point);
                        }
                        break;
                    default:
                        break;
                }
            }
            #endregion

            return conePoints;
        }

        void getOrientationFromGuard(IDrawableGuard g)
        {
            MyOrientation = (GuardOrientation)g.OrientationBehavior.getOrientation();
        }
    }
}
