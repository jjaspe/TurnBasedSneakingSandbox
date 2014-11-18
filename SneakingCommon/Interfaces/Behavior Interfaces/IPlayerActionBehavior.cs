using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using SneakingCommon.Interfaces.Controller;
using SneakingCommon.Interfaces.Model;


namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IPlayerActionBehavior
    {
        void playerAttacks(IDrawableGuard pc, IDrawableGuard target, IAttack attack,ISneakingMap map, IPlayerAIMaster ai);
        void movePC(IDrawableGuard g, IPoint dest, ISneakingMap map, IPlayerAIMaster ai);
        void turnPC(IDrawableGuard g, IPoint dest,ISneakingMap map, IPlayerAIMaster ai);
    }
}
