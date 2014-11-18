using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.System_Classes;
using SneakingCommon.Data_Classes;
using SneakingCommon.Interfaces.Behaviors;


namespace SneakingCommon.Model_Stuff
{
    public class NPCBehaviorNPC:ISneakingNPCBehavior
    {
        #region ATTRIBUTES
        int currentPatrolWaypoint = 0;
        PatrolPath patrol;
        IPoint target;
        PatrolPath targetPath;
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
        public bool hasWaypoints()
        {
            return (MyPatrol != null && MyPatrol.MyWaypoints != null && MyPatrol.MyWaypoints.Count > 1);
        }
        public List<IPoint> rememberedPoints = new List<IPoint>();
        private NoiseMap myKnownNoiseMap;
        NoiseMap unknownNoiseMap;  
        public NoiseMap MyKnownNoiseMap
        {
            get { return myKnownNoiseMap; }
            set { myKnownNoiseMap = value; }
        }
        public NoiseMap MyUnknownNoiseMap
        {
            get { return unknownNoiseMap; }
            set { unknownNoiseMap = value; }
        }
        #endregion

        public NPCBehaviorNPC()
        {
            MyKnownNoiseMap = new NoiseMap();
            MyUnknownNoiseMap = new NoiseMap();
        }

        #region INPCBehavior
        public void addRememberedPoints(List<IPoint> newPoints)
        {
            foreach (IPoint p in newPoints)
            {
                if (rememberedPoints.Find(delegate(IPoint _p) { return p.equals(_p); }) == null)
                    rememberedPoints.Add(p);
            }
        }
        public List<IPoint> getRememberedPoints()
        {
            return rememberedPoints;
        }
        public void resetRememberedPoints()
        {
            rememberedPoints = new List<IPoint>();
        }
        public IPoint getCurrentWaypoint()
        {
            return MyPatrol.MyWaypoints[CurrentPatrolWaypoint];
        }
        public void resetPatrol()
        {
            CurrentPatrolWaypoint = 0;
        }
        public IPoint getNextWaypoint()
        {
            return MyPatrol.MyWaypoints[(CurrentPatrolWaypoint + 1) % MyPatrol.MyWaypoints.Count];
        }
        public void goToNextWaypoint()
        {
            CurrentPatrolWaypoint = (CurrentPatrolWaypoint + 1) % MyPatrol.MyWaypoints.Count;
        }
        public IPoint getTarget()
        {
            return Target;
        }
        public void setTarget(IPoint t)
        {
            Target = t;
        }
        public NoiseMap getKnownNoiseMap()
        {
            return MyKnownNoiseMap;
        }
        public void setKnownNoiseMap(NoiseMap map)
        {
            MyKnownNoiseMap = map;
        }
        public NoiseMap getUnknownNoiseMap()
        {
            return MyUnknownNoiseMap;
        }
        public void setUnknownNoiseMap(NoiseMap map)
        {
            MyUnknownNoiseMap = map;
        }
        public PatrolPath getPatrol()
        {
            return patrol;
        }
        public void setPatrol(PatrolPath _patrol)
        {
            patrol = _patrol;
        }
        public void reset()
        {
            resetPatrol();
            target = null;
            resetRememberedPoints();
            MyKnownNoiseMap.initialize(0);
        }
        #endregion
    }
}
