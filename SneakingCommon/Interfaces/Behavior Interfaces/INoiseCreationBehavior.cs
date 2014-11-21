﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.Model_Stuff;
using SneakingCommon.Model_Stuff.Structure_Classes;
using SneakingCommon.System_Classes;
using SneakingCommon.Data_Classes;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface INoiseCreationBehavior
    {
        NoiseMap createNoiseMap(IPoint src, int level, IMap map);
        void setDrawableOwner(IDrawableOwner _dw);
    }
}