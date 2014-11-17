using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;

using SneakingCommon.Model_Stuff.Structure_Classes;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.View;
using SneakingCommon.Interfaces.Behaviors;
using SneakingCommon.Interfaces.Model;

namespace SneakingCommon.Model_Stuff
{
    public class Map:Character,IMap
    {
        #region ATTRIBUTES
        ILandscapeBehavior myLandscapeBehavior;
        public ILandscapeBehavior MyLandscapeBehavior
        {
            get { return myLandscapeBehavior; }
            set { myLandscapeBehavior = value; }
        }
        List<IPoint> myTileOrigins;
        INoiseCreationBehavior myNoiseCreationBehavior;
        public INoiseCreationBehavior MyNoiseCreationBehavior
        {
            get { return myNoiseCreationBehavior; }
            set { myNoiseCreationBehavior = value; }
        }
        List<DistanceMap> myDistanceMaps;
        public List<IPoint> MyTileOrigins
        {
            get { return myTileOrigins; }
            set { myTileOrigins = value; }
        }       
        public List<DistanceMap> MyDistanceMaps
        {
            get
            {
                return myDistanceMaps;
            }
            set { myDistanceMaps = value; }
        }
        #endregion

        public int getValue(string statName)
        {
            if (isStat(statName))
                return (int)getStat(statName).Value;
            else
                throw new Exception("Stat not Found");
        }
        public void setValue(string statName, int value)
        {
            if (isStat(statName))
                setStat(statName,value);
            else
                throw new Exception("Stat not Found");
        }

        public Map()
        {
            initialize();
        }

        protected void initialize()
        {
            MyDistanceMaps = new List<DistanceMap>();
            this.addStat(new Stat("Game Started", 0));
        }

        #region IMAP
        public List<IPoint> getReachablePoints(IPoint source)
        {
            DistanceMap wholeMap = getDistanceMap(source);
            List<IPoint> reachableMap = new List<IPoint>();
            foreach (valuePoint vp in wholeMap.MyPoints)
            {
                if (vp.value > -1)
                    reachableMap.Add(vp.p);
            }
            return reachableMap; ;
        }
        public bool isTile(IPoint source)
        {
            return MyLandscapeBehavior.isTile(source);
        }

        public INoiseCreationBehavior getCreationBehavior()
        {
            return MyNoiseCreationBehavior;
        }
        public List<IPoint> getTileOrigins()
        {
            return MyTileOrigins;
        }
        public DistanceMap getDistanceMap(IPoint src)
        {
            return MyDistanceMaps.Find(
                delegate(DistanceMap dm)
                {
                    return dm.MyOrigin.equals(src);
                });
        }
        public void setLandscapeBehavior(ILandscapeBehavior lB)
        {
            MyLandscapeBehavior = lB;
        }
        public ILandscapeBehavior getLandscapeBehavior()
        {
            return MyLandscapeBehavior;
        }
        public void setNoiseCreationBehavior(INoiseCreationBehavior noiseCreationBehavior)
        {
            myNoiseCreationBehavior = noiseCreationBehavior;
        }
        public void setDistanceMaps(List<DistanceMap> distanceMaps)
        {
            myDistanceMaps = distanceMaps;
        }
        public void setTileOrigins(List<IPoint> list)
        {
            MyTileOrigins = list;
        }
        public void lightPoints(List<IPoint> points)
        {
            foreach (IPoint p in points)
                MyLandscapeBehavior.lighten(p, 10);
        }
        #endregion


        
    }
}
