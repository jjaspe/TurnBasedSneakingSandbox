using System;
using System.Collections.Generic;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Classes.System_Classes;
using Sneaking_Gameplay.MVC_Interfaces;
using Sneaking_Gameplay.MVC_Interfaces.Model_Interfaces;
using Sneaking_Gameplay.Game_Components.Interfaces.Behaviors;
using Sneaking_Gameplay.Model_Stuff;
using Sneaking_Gameplay.MVC_Interfaces.Behavior_Interfaces;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface IGuard
    {
        IVisibilityBehavior getVisibilityBehavior();
        ITileBehavior getOrientationBehavior();
        IFoVBehavior getFoVBehavior();
        INPCBehavior getNPCBehavior();
        IGuardMovementBehavior getMovementBehavior();
        IKnownNoiseMapBehavior getKnownNoiseMapBehavior();
        List<KeyValuePair<string, int>> getAttacksInfo();
        IAttack getAttack(string name);
        NoiseMap getUnknownNoiseMap();
        NoiseMap getNoiseMap();
        List<pointObj> getFOV();

        void setVisibilityBehavior(IVisibilityBehavior vB);
        void setOrientationBehavior(ITileBehavior tB);
        void setFoVBehavior(IFoVBehavior fB);
        void setNPCBehavior(INPCBehavior nB);
        void setMovementBehavior(IGuardMovementBehavior mB);
        void setKnownNoiseMapBehavior(IKnownNoiseMapBehavior kB);
        void setUnknownNoiseMap(NoiseMap nm);
        void setNoiseMap(NoiseMap map);
        void decreaseValue(string statName, int value);
        void increaseValue(string statName, int value);
        
        
        
        string getName();       
        pointObj getPosition();
        int getValue(string statName);        
        void reset();        
        void setPosition(pointObj pos);
        void setValue(string statName, int value);
        void turnQ();
        void updateVisiblePoints(List<pointObj> availablePoints,int height);
        void updateFoV(List<pointObj> availablePoints,int height);
        int getId();        
    }
}
