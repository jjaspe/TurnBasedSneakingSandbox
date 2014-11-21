﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;
using SneakingCommon.MVC_Interfaces;
using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;


namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IPlayerActionBehavior
    {
        void playerAttacks(IGuard pc, IGuard target, IAttack attack,IMap map, IPlayerAIMaster ai);
        void movePC(IGuard g, IPoint dest, IMap map, IPlayerAIMaster ai);
        void turnPC(IGuard g, IPoint dest,IMap map, IPlayerAIMaster ai);
    }
}