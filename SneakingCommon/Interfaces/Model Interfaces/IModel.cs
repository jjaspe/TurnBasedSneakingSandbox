using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;           
using Canvas_Window_Template.Drawables;
using SneakingCommon.System_Classes;
using SneakingCommon.Data_Classes;
using SneakingCommon.Interfaces.Model;
using SneakingCommon.Interfaces.Observer_Pattern;

namespace SneakingCommon.Interfaces.Model
{
    public interface IGameMaster:IModelSubject
    {
        NoiseMap getNoiseMap();
        void setNoiseMap(NoiseMap nm);
        void setGuards(List<IGuard> guards);
        List<IGuard> getGuards();
        void setPC(IGuard pc);
        IGuard getPC();
        void setMap(IMap map);
        IMap getMap();
        IGuard getActiveGuard();
        void sortGuards(string statName);
        List<IGuard> getSortedGuards();
        void commitChanges();
        bool tileFreeFromGuards(IPoint tileOrigin);
        bool GameStarted { get; set; }
    }
}
