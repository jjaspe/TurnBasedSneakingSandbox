using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.MVC_Interfaces;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Gameplay.View_Stuff.View_1.View_Specific_Behaviors;
using Sneaking_Gameplay.Model_Stuff.Structure_Classes;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface IMap
    {        
        List<pointObj> getTileOrigins();
        void setNoiseCreationBehavior(INoiseCreationBehavior noiseCreationBehavior);
        void setDistanceMaps(List<DistanceMap> distanceMaps);
        void setTileOrigins(List<pointObj> list);
        void setLandscapeBehavior(ILandscape lB);
        DistanceMap getDistanceMap(pointObj src);
        List<pointObj> getReachablePoints(pointObj source);
        INoiseCreationBehavior getCreationBehavior();
        ILandscape getLandscapeBehavior();
        bool isTile(pointObj source);
        void lightPoints(List<pointObj> points);
    }
}
