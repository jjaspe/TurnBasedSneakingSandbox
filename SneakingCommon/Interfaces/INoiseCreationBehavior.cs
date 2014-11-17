using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Gameplay.Model_Stuff;
using Sneaking_Gameplay.Model_Stuff.Structure_Classes;
using Sneaking_Gameplay.View_Stuff.View_1;
using Sneaking_Classes.System_Classes;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface INoiseCreationBehavior
    {
        NoiseMap createNoiseMap(pointObj src, int level, IMap map);
        void setDrawableOwner(IDrawableOwner _dw);
    }
}
