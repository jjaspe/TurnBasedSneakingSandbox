using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Classes.Drawing_Classes;

namespace Sneaking_Classes.System_Classes
{
    public class NoiseMap
    {
        public List<Map.valuePoint> MyNoisePoints;
        Map map;
        public NoiseMap()
        {
            MyNoisePoints = new List<Map.valuePoint>();
        }
        public void createNoiseMap(Map _map, pointObj src, int level)
        {
            map = _map;
            MyNoisePoints = new List<Map.valuePoint>();
            map.initializeValueMap(MyNoisePoints,-1);//Now they all have -1
            List<pointObj> currentPoints = new List<pointObj>(), adjacents = new List<pointObj>(), tempAdjacents;
            currentPoints.Add(src);
            //Put origin point in distance map, with distance 0
            map.setDistancePointInMap(src, level, MyNoisePoints);
            bool keepGoing = true;
            level--;

            do
            {
                adjacents.Clear();
                foreach (pointObj p in currentPoints)//Get all points adjacent to current edge points
                {
                    tempAdjacents = getFreeAdjacents(p, map);
                    foreach (pointObj _ap in tempAdjacents)
                    {
                        //Only add if it wasn't already in the map
                        if (!map.isPointInList(adjacents, _ap))
                            adjacents.Add(_ap);
                    }
                }

                //Remove from adjacents all the elements that have noise!=-1 in noiseMap (already assigned)              
                adjacents.RemoveAll(
                    delegate(pointObj _p)
                    {
                        return MyNoisePoints.Find(
                            delegate(Map.valuePoint _dp)
                            {
                                return _dp.p.equals(_p);
                            }).value != -1;
                    });

                //To the points left in adjacents, set distance in noiseMap
                foreach (pointObj p in adjacents)
                {
                    if (map.areDividedByLowWall(src, p))
                        setNoiseInNoiseMap(p, Math.Max(0, level - SneakingWorld.getValue("noiseLowWallFactor")),
                            MyNoisePoints);
                    else if (map.areDividedByHighWall(src, p))
                        setNoiseInNoiseMap(p, Math.Max(0, level - SneakingWorld.getValue("noiseHighWallFactor")), 
                            MyNoisePoints);
                    else
                        setNoiseInNoiseMap(p, Math.Max(0, level), MyNoisePoints);
                }

                //decrease level
                level-=SneakingWorld.getValue("noiseDistanceFromListenerDropoff");

                //Stop if there were no more adjacents
                if (adjacents.Count == 0)
                    keepGoing = false;
                else //add adjacents that were just changed to currents
                {
                    currentPoints.Clear();
                    currentPoints.AddRange(adjacents);
                }
            } while (keepGoing);

        }
        public int getValueFromList(Map.valuePoint nP, List<Map.valuePoint> distMap)
        {
            Map.valuePoint point = distMap.Find(
                delegate(Map.valuePoint dp) { return dp.p.equals(nP.p); });
            return point == null ? -1 : point.value;
        }

        #region LIKELY TO GO INTO Behaviors LATER ON
        /// <summary>
        /// Reduces noise depending on distance (value in distMap) a constant and the guard's 
        /// perception.
        /// </summary>
        /// <param name="guardPerception"></param>
        /// <param name="distMap"></param>
        public void modify(int guardPerception, List<Map.valuePoint> distMap)
        {
            foreach (Map.valuePoint dp in MyNoisePoints)
            {
                dp.value = Math.Max(0,
                    dp.value - Math.Max(0, getValueFromList(dp, distMap)) * 
                    SneakingWorld.getValue("noiseDistanceFromSourceDropoff") / guardPerception);
            }
        }
        #endregion

        public List<pointObj> getFreeAdjacents(pointObj src, Map map)
        {
            List<pointObj> points = new List<pointObj>();
            pointObj top = new pointObj(src.X, src.Y + map.TileSize, src.Z)
                , bottom = new pointObj(src.X, src.Y - map.TileSize, src.Z),
                right = new pointObj(src.X + map.TileSize, src.Y, src.Z),
                left = new pointObj(src.X - map.TileSize, src.Y, src.Z);

            if (map.getTile(top) != null && map.isFree(top))
                points.Add(top);
            if (map.getTile(bottom) != null && map.isFree(bottom))
                points.Add(bottom);
            if (map.getTile(right) != null && map.isFree(right))
                points.Add(right);
            if (map.getTile(left) != null && map.isFree(left))
                points.Add(left);
            return points;
        }
        /// <summary>
        /// Only sets noise if it didn't have one or if the previous level was lower
        /// </summary>
        /// <param name="p"></param>
        /// <param name="level"></param>
        /// <param name="noiseMap"></param>
        public void setNoiseInNoiseMap(pointObj p, int level, List<Map.valuePoint> noiseMap)
        {
            Map.valuePoint currentNoisePoint;
            currentNoisePoint = noiseMap.Find(
                        delegate(Map.valuePoint _dp)
                        {
                            return _dp.p.equals(p);
                        });
            //If it is -1, assign noise, if it already has a noise, see if the new one is higher
            if (currentNoisePoint != null)
                currentNoisePoint.value = currentNoisePoint.value == -1 ? 
                level : Math.Max(currentNoisePoint.value, level);
        }
        public void setNoise(pointObj p,int level)
        {
            Map.valuePoint currentNoisePoint;
            currentNoisePoint = MyNoisePoints.Find(
                        delegate(Map.valuePoint _dp)
                        {
                            return _dp.p.equals(p);
                        });
            //If it is -1, assign noise, if it already has a noise, see if the new one is higher
            if(currentNoisePoint!=null)
                currentNoisePoint.value = currentNoisePoint.value == -1 ? 
                level : Math.Max(currentNoisePoint.value, level);
        }
        public Map.valuePoint getHighest()
        {
            int highest = 0;
            Map.valuePoint cHighest = null;
            foreach (Map.valuePoint vP in MyNoisePoints)
            {
                if (vP.value > highest)
                {
                    highest = vP.value;
                    cHighest = vP;
                }
            }
            return cHighest;
        }
        public Map.valuePoint getHighest(List<pointObj> available)
        {
            int highest = -1;
            Map.valuePoint cHighest = null;
            foreach (Map.valuePoint vP in MyNoisePoints)
            {
                if (available.Find(delegate (pointObj p){return p.equals(vP.p);})!=null)
                {
                    if (vP.value > highest)
                    {
                        highest = vP.value;
                        cHighest = vP;
                    }
                }
            }
            return cHighest;
        }
        public int getNoiseAt(pointObj p)
        {
            Map.valuePoint vp = MyNoisePoints.Find(
                delegate(Map.valuePoint _vp) { return _vp.p.equals(p); });
            if (vp != null)
                return vp.value;
            else
                return 0;
        }
        public void silencePoints(List<pointObj> points)
        {
            foreach (pointObj p in points)
            {
                setNoise(p, 0);
            }
        }
        public void silenceAllPoints()
        {
            foreach (Map.valuePoint vp in MyNoisePoints)
            {
                setNoise(vp.p, 0);
            }
        }
    }
}
