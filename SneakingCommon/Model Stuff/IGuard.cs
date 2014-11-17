using System;
using System.Collections.Generic;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Classes.System_Classes;
using Sneaking_Gameplay.MVC_Interfaces;
using Sneaking_Gameplay.MVC_Interfaces.Model_Interfaces;
using Sneaking_Gameplay.Game_Components.Interfaces.Behaviors;
namespace Sneaking_Gameplay.Model_Stuff
{
    public interface IGuard
    {
        IVisibility getVisibilityBehavior();
        ITileBehavior getOrientationBehavior();
        ICharacterFoVBehavior getFoVBehavior();
        INPCBehavior getNPCBehavior();

        void setVisibilityBehavior(IVisibility vB);
        void setOrientationBehavior(ITileBehavior tB);
        void setFoVBehavior(ICharacterFoVBehavior fB);
        void setNPCBehavior(INPCBehavior nB);
        
        NoiseMap getNoiseMap();
        void setNoiseMap(NoiseMap map);        
        void decreaseValue(string statName, int value);
        void increaseValue(string statName, int value);
        List<pointObj> getFOV();
        string getName();       
        pointObj getPosition();
        int getValue(string statName);        
        void reset();        
        void setPosition(pointObj pos);
        void setValue(string statName, int value);
        void turnQ();
        void updateFoV(List<pointObj> availablePoints);
        

        
    }
}
