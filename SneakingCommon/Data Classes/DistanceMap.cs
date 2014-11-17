using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Model;
using SneakingCommon.Interfaces.View;

namespace SneakingCommon.Data_Classes
{
    public class DistanceMap
    {
        IPoint myOrigin;
        public IPoint MyOrigin
        {
            get { return myOrigin; }
            set { myOrigin = value; }
        }
        List<valuePoint> myPoints;
        public List<valuePoint> MyPoints
        {
            get { return myPoints; }
            set { myPoints = value; }
        }

        public DistanceMap()
        {
            MyPoints = new List<valuePoint>();
        }

        
        public void Add(valuePoint p)
        {
            MyPoints.Add(p);
        }

        void initialize(IMap map)
        {
            foreach (IPoint point in map.getTileOrigins())
            {
                MyPoints.Add(new valuePoint(point, -1));
            }
        }

        /// <summary>
        /// Creates a map (a List of valuePoints) where each point has as its value the distance to
        /// src.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public List<valuePoint> calculateDistanceMap(IPoint src, IMap map, IDrawableOwner dw)
        {
            List<valuePoint> distMap = new List<valuePoint>();
            myOrigin = src;
            initialize(map);
            List<IPoint> currentPoints = new List<IPoint>(), adjacents = new List<IPoint>(), tempAdjacents;
            currentPoints.Add(src);
            //Put origin point in distance map, with distance 0
            setDistanceForPoint(src, 0);
            bool keepGoing = true;
            int distance = 1;

            do
            {
                adjacents.Clear();
                foreach (IPoint p in currentPoints)//Get all points adjacent to current edge points
                {
                    tempAdjacents = dw.getReachableAdjacents(p);
                    foreach (IPoint _ap in tempAdjacents)
                    {
                        //Only add if it wasn't already in the map
                        if (!isPointInList( _ap))
                            adjacents.Add(_ap);
                    }
                }

                //Remove from adjacents all the elements that have distance!=-1 in distMap (already assigned)              
                adjacents.RemoveAll(
                    delegate(IPoint _p)
                    {
                        return distMap.Find(
                            delegate(valuePoint _dp)
                            {
                                return _dp.p.equals(_p);
                            }).value != -1;
                    });

                //To the points left in adjacents, set distance in distMap
                foreach (IPoint p in adjacents)
                {
                    setDistanceForPoint(p, distance);
                }

                //increase distance
                distance++;

                //Stop if there were no more adjacents
                if (adjacents.Count == 0)
                    keepGoing = false;
                else //add adjacents that were just changed to currents
                {
                    currentPoints.Clear();
                    currentPoints.AddRange(adjacents);
                }
            } while (keepGoing);

            return distMap;
        }

        public bool isPointInList(IPoint p)
        {
            return MyPoints.Find(delegate(valuePoint _p) { return _p.p.equals(p); }) != null;
        }

        public void setDistanceForPoint(IPoint p, int distance)
        {
            valuePoint currentDP;
            currentDP = MyPoints.Find(
                        delegate(valuePoint _dp)
                        {
                            return _dp.p.equals(p);
                        });
            //If it is -1, assign distance, if it already has a distance, see if the new one is smaller
            currentDP.value = currentDP.value == -1 ? distance : Math.Min(currentDP.value, distance);
        }
    }
}
