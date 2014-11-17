using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Controller;
using SneakingCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IGuardActionBehavior
    {
        void decisionsDecisions(IGuard g, ArgOwner aO);
        void setMaster(IGuardAIMaster gm);
        void resetGuardEndOfTurn(IGuard g);
        void resetGuardEndOfMatch(IGuard g);
        //void moveGuard(IGuard g, IPoint dest, IMap map, IGuardAIMaster ai,ArgOwner aO);
        //void turnGuard(IGuard g, IMap map, IGuardAIMaster ai,ArgOwner aO);
    }
}
