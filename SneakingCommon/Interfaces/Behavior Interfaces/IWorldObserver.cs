using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;    
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IWorldObserver
    {
        void update(WorldMessage message, List<ObjectArg> args);
    }
}
