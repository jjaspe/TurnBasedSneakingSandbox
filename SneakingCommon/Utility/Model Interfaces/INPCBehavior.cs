using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Classes.System_Classes;

namespace Sneaking_Gameplay.MVC_Interfaces.Model_Interfaces
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
        NoiseMap getUnknownNoiseMap();
        void setUnknownNoiseMap(NoiseMap map);
        void setKnownNoiseMap(NoiseMap map);
        pointObj getCurrentWaypoint();
        pointObj getNextWaypoint();
        void goToNextWaypoint();
        void resetRememberedPoints();
        void resetPatrol();
        void reset();
    }
}
