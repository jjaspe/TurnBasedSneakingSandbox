using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Controller
{
    public interface IWorldAI
    {
        List<ObjectArg> getArgs(IGuardAgent ag);
        WorldMessage aiGo(WorldMessage message,List<ObjectArg> args);
        void aiStart();
    }
}
