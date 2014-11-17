using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.System_Classes;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.Data_Classes;
using SneakingCommon.Interfaces.Behaviors;

namespace SneakingCommon.Model_Stuff
{
    public class NPCBehaviorPC:INPCBehavior
    {
        NoiseMap unknownNoiseMap;
        public NoiseMap MyUnknownNoiseMap
        {
            get { return unknownNoiseMap; }
            set { unknownNoiseMap = value; }
        }    
        public IPoint getTarget()
        {
            throw new NotImplementedException();
        }

        public void setTarget(IPoint t)
        {
            throw new NotImplementedException();
        }

        public void addRememberedPoints(List<IPoint> newPoints)
        {
            throw new NotImplementedException();
        }

        public List<IPoint> getRememberedPoints()
        {
            throw new NotImplementedException();
        }

        public PatrolPath getPatrol()
        {
            throw new NotImplementedException();
        }

        public bool hasWaypoints()
        {
            throw new NotImplementedException();
        }

        public void setPatrol(PatrolPath _patrol)
        {
            throw new NotImplementedException();
        }

        public NoiseMap getKnownNoiseMap()
        {
            throw new NotImplementedException();
        }

        public void setKnownNoiseMap(NoiseMap map)
        {
            throw new NotImplementedException();
        }

        public IPoint getCurrentWaypoint()
        {
            throw new NotImplementedException();
        }

        public IPoint getNextWaypoint()
        {
            throw new NotImplementedException();
        }

        public void goToNextWaypoint()
        {
            throw new NotImplementedException();
        }
        public NoiseMap getUnknownNoiseMap()
        {
            return MyUnknownNoiseMap;
        }
        public void setUnknownNoiseMap(NoiseMap map)
        {
            MyUnknownNoiseMap = map;
        }
        public void resetRememberedPoints()
        {
            throw new NotImplementedException();
        }
        public void resetPatrol() { return; }
        public void reset() { return; }
    }
}
