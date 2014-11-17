using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;

namespace SneakingClasses.Data_Classes
{
    public class DistanceMap
    {
        public List<valuePoint> MyPoints;
        public DistanceMap() 
        {
            MyPoints = new List<valuePoint>();
        }

        public void setDistancePointInMap(pointObj p, int distance, List<valuePoint> distMap)
        {
            valuePoint currentDP;
            currentDP = distMap.Find(
                        delegate(valuePoint _dp)
                        {
                            return _dp.p.equals(p);
                        });
            //If it is -1, assign distance, if it already has a distance, see if the new one is smaller
            currentDP.value = currentDP.value == -1 ? distance : Math.Min(currentDP.value, distance);
        }
        /// <summary>
        /// Creates a map (a List of valuePoints) where each point has as its value the distance to
        /// src.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public List<valuePoint> calculateDistanceMap(pointObj src)
        {
            List<valuePoint> distMap = new List<valuePoint>();
            initializeValueMap(distMap, -1);
            List<pointObj> currentPoints = new List<pointObj>(), adjacents = new List<pointObj>(), tempAdjacents;
            currentPoints.Add(src);
            //Put origin point in distance map, with distance 0
            setDistancePointInMap(src, 0, distMap);
            bool keepGoing = true;
            int distance = 1;

            do
            {
                adjacents.Clear();
                foreach (pointObj p in currentPoints)//Get all points adjacent to current edge points
                {
                    tempAdjacents = getReachableAdjacents(p);
                    foreach (pointObj _ap in tempAdjacents)
                    {
                        //Only add if it wasn't already in the map
                        if (!isPointInList(adjacents, _ap))
                            adjacents.Add(_ap);
                    }
                }

                //Remove from adjacents all the elements that have distance!=-1 in distMap (already assigned)              
                adjacents.RemoveAll(
                    delegate(pointObj _p)
                    {
                        return distMap.Find(
                            delegate(valuePoint _dp)
                            {
                                return _dp.p.equals(_p);
                            }).value != -1;
                    });

                //To the points left in adjacents, set distance in distMap
                foreach (pointObj p in adjacents)
                {
                    setDistancePointInMap(p, distance, distMap);
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


    }
}
