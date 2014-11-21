using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.Sneaking_Drawables;
using Canvas_Window_Template.Basic_Drawing_Functions;
using OpenGlGameCommon.Interfaces.Model;
using OpenGlGameCommon.Drawables;

namespace DummyOne
{
    class Program
    {
        static void Main(string[] args)
        {
            //First lets create a map
            SneakingMap map = SneakingMap.createInstance(10, 10, 10, new pointObj(0, 0, 0));

            //Let's make a guard
            IDrawableGuard g = new DrawableGuard();
            map.Drawables.Add(g);

        }
    }
}
