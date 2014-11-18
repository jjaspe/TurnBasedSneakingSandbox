using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.System_Classes;
using Canvas_Window_Template.Interfaces;  

using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Model;
using SneakingCommon.Interfaces.Behaviors;

namespace SneakingCommon.Interfaces.Controller
{
    public interface IPlayerAI
    {
        void aiStart(IDrawableGuard pc, WorldMessage message, List<object> args);
        void aiGo(IDrawableGuard pc, WorldMessage message, List<ObjectArg> args);
        void setPlayerFoHBehavior(IPlayerFoHBehavior pB);
        void registerObserver(IPlayerObserver obs);
        void removeObserver(IPlayerObserver obs);
        void notifyObservers(PlayerMessage message, List<ObjectArg> args);
    }
}
