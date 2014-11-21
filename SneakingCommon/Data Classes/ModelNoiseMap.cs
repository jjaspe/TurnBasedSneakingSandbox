using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using SneakingCommon.System_Classes;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;
using SneakingCommon.Data_Classes;
using SneakingCommon.Interfaces.Model;

namespace SneakingCommon.Data_Classes
{
    public class ModelNoiseMap
    {
        public List<valuePoint> MyNoisePoints;
        public ModelNoiseMap()
        {
            MyNoisePoints = new List<valuePoint>();
        }
        public ModelNoiseMap(NoiseMap baseMap)
        {
            MyNoisePoints = baseMap.MyNoisePoints;
        }
        
        public double getValueFromList(valuePoint nP, List<valuePoint> distMap)
        {
            valuePoint point = distMap.Find(
                delegate(valuePoint dp) { return dp.p.equals(nP.p); });
            return point == null ? -1 : point.value;
        }
       
        /// <summary>
        /// Only sets noise if it didn't have one or if the previous level was lower
        /// </summary>
        /// <param name="p"></param>
        /// <param name="level"></param>
        /// <param name="noiseMap"></param>
        public void initialize(int defaultValue, ISneakingMap map)
        {
            foreach (IPoint point in map.TileOrigins)
            {
                MyNoisePoints.Add(new valuePoint(point, -1));
            }
        }
        public void setNoiseInNoiseMap(IPoint p, int level, List<valuePoint> noiseMap)
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
        public static void sSetNoiseInNoiseMap(IPoint p, int level, ModelNoiseMap noiseMap)
        {
            valuePoint currentNoisePoint = noiseMap.Find(p);
            //If it is -1, assign noise, if it already has a noise, see if the new one is higher
            if (currentNoisePoint != null)
                currentNoisePoint.value = currentNoisePoint.value == -1 ?
                level : Math.Max(currentNoisePoint.value, level);
        }
        public void setNoise(IPoint p,int level)
        {
            valuePoint currentNoisePoint = this.Find(p);
            //If it is -1, assign noise, if it already has a noise, see if the new one is higher
            if(currentNoisePoint!=null)
                currentNoisePoint.value = currentNoisePoint.value == -1 ? 
                level : Math.Max(currentNoisePoint.value, level);
        }
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
        public double getNoiseAt(IPoint p)
        {
            valuePoint vp = MyNoisePoints.Find(
                delegate(valuePoint _vp) { return _vp.p.equals(p); });
            if (vp != null)
                return vp.value;
            else
                return 0;
        }
        public void silencePoints(List<IPoint> points)
        {
            foreach (IPoint p in points)
            {
                setNoise(p, 0);
            }
        }
        public void silenceAllPoints()
        {
            foreach (valuePoint vp in MyNoisePoints)
            {
                setNoise(vp.p, 0);
            }
        }
        public valuePoint Find(IPoint _p)
        {
           return MyNoisePoints.Find(
                            delegate(valuePoint _dp)
                            {
                                return _dp.p.equals(_p);
                            });
        }
    }
}
