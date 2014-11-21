using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;

namespace Sneaking_Gameplay.MVC_Interfaces.Model_Interfaces
{
    public interface IGuard
    {
        int getValue(string statName);
        pointObj getPosition();
        string getName();
    }
}
