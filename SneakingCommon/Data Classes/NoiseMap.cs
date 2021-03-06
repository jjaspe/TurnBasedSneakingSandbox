﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.System_Classes;
using OpenGlGameCommon.Classes;
using Canvas_Window_Template.Interfaces;
using SneakingCommon.Data_Classes;

namespace SneakingCommon.Data_Classes
{
    /// <summary>
    /// Models a grid(MyNoisePoints) where each point has a value corresponding to the noise there 
    /// </summary>
    public class NoiseMap
    {
        public List<valuePoint> MyNoisePoints;
        public NoiseMap()
        {
            MyNoisePoints = new List<valuePoint>();
        }
        public NoiseMap copy()
        {
            NoiseMap copy = new NoiseMap();
            foreach (valuePoint vp in MyNoisePoints)
                copy.MyNoisePoints.Add(new valuePoint(vp.p, vp.value));
            return copy;
        }
        
        public double getValueFromList(valuePoint nP, List<valuePoint> distMap)
        {
            valuePoint point = distMap.Find(
                delegate(valuePoint dp) { return dp.p.equals(nP.p); });
            return point == null ? -1 : point.value;
        }

        #region LIKELY TO GO INTO Behaviors LATER ON
        /// <summary>
        /// Reduces noise depending on distance (value in distMap), a constant, and the a stat
        /// 
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="distMap"></param>
        public void modify(int stat, List<valuePoint> distMap)
        {
            foreach (valuePoint dp in MyNoisePoints)
            {
                dp.value = Math.Max(0,
                    dp.value - Math.Max(0, getValueFromList(dp, distMap)) * 
                    SneakingWorld.getValueByName("noiseDistanceFromSourceDropoff") / stat);
            }
        }
        #endregion




        /// <summary>
        /// Updates noise map at grid point p with level:level
        /// Only sets noise if it didn't have one or if the previous level was lower
        /// </summary>
        /// <param name="p"></param>
        /// <param name="level"></param>
        /// <param name="noiseMap"></param>
        public static void sSetNoiseInNoiseMap(IPoint p, int level, List<valuePoint> noiseMap)
        {
            valuePoint currentNoisePoint;
            currentNoisePoint = noiseMap.Find(
                        delegate(valuePoint _dp)
                        {
                            return _dp.p.equals(p);
                        });
            //If it is -1, assign noise, if it already has a noise, see if the new one is higher
            if (currentNoisePoint != null)
                currentNoisePoint.value = currentNoisePoint.value == -1 ?
                level : Math.Max(currentNoisePoint.value, level);
        }

        /// <summary>
        /// Updates myNoiseMap at grid point p with level:level
        /// Only sets noise if it didn't have one or if the previous level was lower
        /// </summary>
        /// <param name="p"></param>
        /// <param name="level"></param>
        /// <param name="noiseMap"></param>
        public void setNoise(IPoint p,int level)
        {
            NoiseMap.sSetNoiseInNoiseMap(p, level, MyNoisePoints);
        }

        /// <summary>
        /// Returns point from all myNoisePoints with highest noise level
        /// </summary>
        /// <returns></returns>
        public valuePoint getHighest()
        {
            double highest = 0;
            valuePoint cHighest = null;
            foreach (valuePoint vP in MyNoisePoints)
            {
                if (vP.value > highest)
                {
                    highest = vP.value;
                    cHighest = vP;
                }
            }
            return cHighest;
        }

        /// <summary>
        /// Returns point with highest noise level from available points
        /// </summary>
        /// <param name="available"></param>
        /// <returns></returns>
        public valuePoint getHighest(List<IPoint> available)
        {
            double highest = -1;
            valuePoint cHighest = null;
            foreach (valuePoint vP in MyNoisePoints)
            {
                if (available.Find(delegate (IPoint p){return p.equals(vP.p);})!=null)
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
        /// <summary>
        /// Gets highest from available that is not in forbidden
        /// </summary>
        /// <param name="available"></param>
        /// <param name="forbidden"></param>
        /// <returns></returns>
        public valuePoint getHighestNotIn(List<IPoint> available, List<IPoint> forbidden)
        {
            double highest = -1;
            valuePoint cHighest = null;
            foreach (valuePoint vP in MyNoisePoints)
            {
                if (available.Find(delegate(IPoint p) { return p.equals(vP.p); }) != null
                    && forbidden.Find(delegate(IPoint p) { return p.equals(vP.p); })==null)
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

        public double getNoiseAt(IPoint p)
        {
            valuePoint vp = MyNoisePoints.Find(
                delegate(valuePoint _vp) { return _vp.p.equals(p); });
            if (vp != null)
                return vp.value;
            else
                return 0;
        }

        /// <summary>
        /// Sets all points in points to noise=0
        /// </summary>
        /// <param name="points"></param>
        public void silencePoints(List<IPoint> points)
        {
            foreach (IPoint p in points)
            {
                setNoise(p, 0);
            }
        }
        /// <summary>
        /// Sets all points to noise=0
        /// </summary>
        public void silenceAllPoints()
        {
            foreach (valuePoint vp in MyNoisePoints)
            {
                setNoise(vp.p, 0);
            }
        }
        /// <summary>
        /// Starts all points with noise=value
        /// </summary>
        /// <param name="value"></param>
        public void initialize(int value = 0)
        {
            foreach (valuePoint vp in MyNoisePoints)
                vp.value = value;
        }
    }
}
