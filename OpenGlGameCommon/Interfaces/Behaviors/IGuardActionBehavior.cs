using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;
using OpenGlGameCommon.Interfaces.Controller;
using OpenGlGameCommon.Interfaces.Model;

namespace OpenGlCommonGame.Interfaces.Behaviors
{
    public interface IGuardActionBehavior
    {
        void decisionsDecisions(IDrawableGuard g, ArgOwner aO);
        void setMaster(IGuardAIMaster gm);
        void resetGuardEndOfTurn(IDrawableGuard g);
        void resetGuardEndOfMatch(IDrawableGuard g);
        //void moveGuard(IGuard g, IPoint dest, IMap map, IGuardAIMaster ai,ArgOwner aO);
        //void turnGuard(IGuard g, IMap map, IGuardAIMaster ai,ArgOwner aO);
    }
}
