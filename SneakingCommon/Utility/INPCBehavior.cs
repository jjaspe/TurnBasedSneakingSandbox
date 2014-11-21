using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Classes.System_Classes;

namespace Sneaking_Gameplay.Model_Stuff
{
    public interface INPCBehavior
    {
        pointObj getTarget();
        void setTarget(pointObj t);
        void addRememberedPoints(List<pointObj> newPoints);
        List<pointObj> getRememberedPoints();
        PatrolPath getPatrol();
        bool hasWaypoints();
        void setPatrol(PatrolPath _patrol);
        NoiseMap getKnownNoiseMap();
        void setKnownNoiseMap(NoiseMap map);
        pointObj getCurrentWaypoint();
        pointObj getNextWaypoint();
        void goToNextWaypoint();
        void resetRememberedPoints();
    }
}
