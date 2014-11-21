﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.Model_Stuff;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.MVC_Interfaces;
using OpenGlGameCommon.Classes;

namespace SneakingCommon.Model_Stuff.Structure_Classes
{
    public class ValueMap
    {
        public List<valuePoint> MyPoints;

        public ValueMap(IMap map)  
        {
            MyPoints = new List<valuePoint>();
            initialize(map);
        }

        public ValueMap(IMap map, IPoint src, IDrawableOwner dw)
        {
            MyPoints = new List<valuePoint>();
        }

        
        void initialize(IMap map)
        {
            
            foreach (IPoint point in map.getTileOrigins())
            {
                MyPoints.Add(new valuePoint(point, -1));
            }
        }

        public bool isPointInList(IPoint p)
        {
            return MyPoints.Find(delegate(valuePoint _p) { return _p.p.equals(p); }) != null;
        }

        public void setDistancePointInMap(IPoint p, int distance)
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