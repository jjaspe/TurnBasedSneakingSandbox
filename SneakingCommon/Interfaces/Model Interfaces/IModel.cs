﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.Model_Stuff;
using Canvas_Window_Template.Basic_Drawing_Functions;using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;
using SneakingCommon.System_Classes;
using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;
using SneakingCommon.Data_Classes;

namespace SneakingCommon.MVC_Interfaces
{
    public interface IModel:IModelSubject
    {
        NoiseMap getNoiseMap();
        void setNoiseMap(NoiseMap nm);
        void setGuards(List<Guard> guards);
        List<IGuard> getGuards();
        void setPC(PC pc);
        IGuard getPC();
        void setMap(Map map);
        Map getMap();
        IGuard getActiveGuard();
        void sortGuards(string statName);
        List<IGuard> getSortedGuards();
        void commitChanges();
        bool tileFreeFromGuards(IPoint tileOrigin);
        bool GameStarted { get; set; }
    }
}