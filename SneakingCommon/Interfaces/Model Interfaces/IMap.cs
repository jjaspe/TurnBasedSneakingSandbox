using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.MVC_Interfaces;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;                
using Canvas_Window_Template.Drawables;
using SneakingCommon.Model_Stuff.Structure_Classes;
using Canvas_Window_Template.Interfaces;             
using Canvas_Window_Template.Drawables;
using SneakingCommon.Interfaces.Behaviors;

namespace SneakingCommon.Interfaces.Model
{
    public interface IMap
    {        
        List<IPoint> getTileOrigins();
        void setNoiseCreationBehavior(INoiseCreationBehavior noiseCreationBehavior);
        void setDistanceMaps(List<DistanceMap> distanceMaps);
        void setTileOrigins(List<IPoint> list);
        void setLandscapeBehavior(ILandscape lB);
        DistanceMap getDistanceMap(IPoint src);
        List<IPoint> getReachablePoints(IPoint source);
        INoiseCreationBehavior getCreationBehavior();
        ILandscape getLandscapeBehavior();
        bool isTile(IPoint source);
        void lightPoints(List<IPoint> points);

        int getValue(string name);
        void setValue(string name, int value);
    }
}
