using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using Canvas_Window_Template.Interfaces;  
using Canvas_Window_Template.Drawables;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IDataStore
    {
        void changePCStat(string statName, int value);
        void changeGuardStat(string guardName,string statName, int value);
        void changeWorldStat(string statName, int value);
    }
}
