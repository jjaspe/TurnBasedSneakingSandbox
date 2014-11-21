using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;

namespace OpenGlGameCommon.Interfaces.Behaviors
{
    public interface IPlayerObserver
    {
        void update(int message, List<ObjectArg> args);
    }
}
