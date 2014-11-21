using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using SneakingCommon.System_Classes;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.Data_Classes;
using OpenGlGameCommon.Data_Classes;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface ISneakingNPCBehavior
    {
        IPoint getTarget();
        void setTarget(IPoint t);
        void addRememberedPoints(List<IPoint> newPoints);
        List<IPoint> getRememberedPoints();
        PatrolPath getPatrol();
        bool hasWaypoints();
        void setPatrol(PatrolPath _patrol);
        NoiseMap getKnownNoiseMap();
        NoiseMap getUnknownNoiseMap();
        void setUnknownNoiseMap(NoiseMap map);
        void setKnownNoiseMap(NoiseMap map);
        IPoint getCurrentWaypoint();
        IPoint getNextWaypoint();
        void goToNextWaypoint();
        void resetRememberedPoints();
        void resetPatrol();
        void reset();
    }
}
