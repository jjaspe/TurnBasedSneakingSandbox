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

namespace SneakingCommon.Interfaces.Controller
{
    public interface IGuardAI
    {
        void aiStart(IGuard g, WorldMessage message, List<object> args);
        void aiGo(IGuard g,WorldMessage message,List<ObjectArg> args);
        void updateGuard(IGuard g);
        //NoiseMap getModifiedNoiseMap(Guard g, NoiseMap noiseMap, Map map);
        string getName();

        void registerObserver(IGuardObserver obs);
        void removeObserver(IGuardObserver obs);
        void notifyObservers(GuardMessage message, List<ObjectArg> args);
    }
}
