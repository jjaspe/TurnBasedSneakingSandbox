using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Classes;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.Data_Classes;
using SneakingCommon.System_Classes;

namespace SneakingCommon.Drawables
{
    public class DrawableMap:OpenGlMap
    {

        public DrawableMap(int width, int length, int _tileSize):base(width,length,_tileSize,null)
        {
            
        }
            
        #region NOISEMAP STUFF
        public List<IPoint> getFreeAdjacents(IPoint src)
        {
            List<IPoint> points = new List<IPoint>();
            IPoint top = new PointObj(src.X, src.Y + this.TileSize, src.Z)
                , bottom = new PointObj(src.X, src.Y - this.TileSize, src.Z),
                right = new PointObj(src.X + this.TileSize, src.Y, src.Z),
                left = new PointObj(src.X - this.TileSize, src.Y, src.Z);

            if (this.getTile(top) != null && this.isFree(top))
                points.Add(top);
            if (this.getTile(bottom) != null && this.isFree(bottom))
                points.Add(bottom);
            if (this.getTile(right) != null && this.isFree(right))
                points.Add(right);
            if (this.getTile(left) != null && this.isFree(left))
                points.Add(left);
            return points;
        }

        public void createNoiseMap(IPoint src, int level)
        {
            List<valuePoint> noisePoints = new List<valuePoint>();
            this.initializeValueMap(noisePoints, -1);//Now they all have -1
            List<IPoint> currentPoints = new List<IPoint>(), adjacents = new List<IPoint>(), tempAdjacents;
            currentPoints.Add(src);
            //Put origin point in distance map, with distance 0
            this.setPointInMap(src, level, noisePoints);
            bool keepGoing = true;
            level--;

            do
            {
                adjacents.Clear();
                foreach (IPoint p in currentPoints)//Get all points adjacent to current edge points
                {
                    tempAdjacents = getFreeAdjacents(p);
                    foreach (IPoint _ap in tempAdjacents)
                    {
                        //Only add if it wasn't already in the map
                        if (!this.isPointInList(adjacents, _ap))
                            adjacents.Add(_ap);
                    }
                }

                //Remove from adjacents all the elements that have noise!=-1 in noiseMap (already assigned)              
                adjacents.RemoveAll(
                    delegate(IPoint _p)
                    {
                        return noisePoints.Find(
                            delegate(valuePoint _dp)
                            {
                                return _dp.p.equals(_p);
                            }).value != -1;
                    });

                //To the points left in adjacents, set distance in noiseMap
                foreach (IPoint p in adjacents)
                {
                    if (this.areDividedByLowWall(src, p))
                        NoiseMap.sSetNoiseInNoiseMap(p, Math.Max(0, level - SneakingWorld.getValueByName("noiseLowWallFactor")),
                            noisePoints);
                    else if (this.areDividedByHighWall(src, p))
                        NoiseMap.sSetNoiseInNoiseMap(p, Math.Max(0, level - SneakingWorld.getValueByName("noiseHighWallFactor")),
                            noisePoints);
                    else
                        NoiseMap.sSetNoiseInNoiseMap(p, Math.Max(0, level), noisePoints);
                }

                //decrease level
                level -= SneakingWorld.getValueByName("noiseDistanceFromListenerDropoff");

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
        #endregion
    }
}
