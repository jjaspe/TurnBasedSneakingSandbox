using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.Model_Stuff;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Classes.System_Classes;

namespace Sneaking_Gameplay.MVC_Interfaces
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
        bool tileFreeFromGuards(pointObj tileOrigin);
    }
}
