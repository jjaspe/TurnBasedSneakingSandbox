using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;                         
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Behaviors;
using SneakingCommon.Interfaces.Model;
using OpenGlGameCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Controller
{
    public interface IController
    {
        void messageSent(WorldMessage msg, List<ObjectArg> args);
        void initialize();
        IDrawableGuard getActiveGuard();

        void setNoiseCreationBehavior(INoiseCreationBehavior nB);

    }
}
