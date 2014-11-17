﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.System_Classes;
using SneakingCommon.System_Classes;
using SneakingCommon.MVC_Interfaces;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.Data_Classes;
using SneakingCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IGuardFoHBehavior
    {
        void setGuardNoiseMap(IGuard g, NoiseMap noiseMap, IMap map);
    }
}
