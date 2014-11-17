using System;
using System.Collections.Generic;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;         
using Canvas_Window_Template.Drawables;
using SneakingCommon.System_Classes;
using SneakingCommon.Model_Stuff;
using SneakingCommon.Data_Classes;
using SneakingCommon.Interfaces.View;
using SneakingCommon.Interfaces.Behaviors;


namespace SneakingCommon.Interfaces.Model
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
        List<IPoint> getFOV();

        void setVisibilityBehavior(IVisibilityBehavior vB);
        void setOrientationBehavior(ITileBehavior tB);
        void setFoVBehavior(IFoVBehavior fB);
        void setNPCBehavior(INPCBehavior nB);
        void setMovementBehavior(IGuardMovementBehavior mB);
        void setKnownNoiseMapBehavior(IKnownNoiseMapBehavior kB);
        void setUnknownNoiseMap(NoiseMap nm);
        void setNoiseMap(NoiseMap map);
        void decreaseValue(string statName, double value);
        void increaseValue(string statName, double value);
        
        
        
        string getName();       
        IPoint getPosition();
        int getValue(string statName);        
        void reset();        
        void setPosition(IPoint pos);
        void setValue(string statName, int value);
        void turnQ();
        void updateVisiblePoints(List<IPoint> availablePoints,int height);
        void updateFoV(List<IPoint> availablePoints,int height);
        int getId();        
    }
}
