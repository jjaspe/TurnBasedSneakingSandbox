using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces; 
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Controller
{
    public interface IPlayerAIMaster
    {
        void update(PlayerMessage msg, List<ObjectArg> args);
        ISneakingGameMaster getModel();
    }
}
