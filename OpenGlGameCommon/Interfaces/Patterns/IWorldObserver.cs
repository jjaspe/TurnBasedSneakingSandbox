using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;    
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;

namespace OpenGlGameCommon.Interfaces.Behaviors
{
    public interface IWorldObserver
    {
        /// <summary>
        /// In a specific implementation, the message will be casted from an enum
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void update(int message, List<ObjectArg> args);
    }
}
