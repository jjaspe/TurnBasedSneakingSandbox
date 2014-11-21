using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.System_Classes;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;

using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Model;
using SneakingCommon.Interfaces.Behaviors;
using OpenGlGameCommon.Interfaces.Behaviors;
using OpenGlGameCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Controller
{
    public interface IDrawableGuardAI
    {
        void aiStart(IDrawableGuard g, WorldMessage message, List<object> args);
        void aiGo(IDrawableGuard g,WorldMessage message,List<ObjectArg> args);
        void updateGuard(IDrawableGuard g);
        //NoiseMap getModifiedNoiseMap(Guard g, NoiseMap noiseMap, Map map);
        string getName();

        void registerObserver(IDrawableGuardObserver obs);
        void removeObserver(IDrawableGuardObserver obs);
        void notifyObservers(GuardMessage message, List<ObjectArg> args);
    }
}
