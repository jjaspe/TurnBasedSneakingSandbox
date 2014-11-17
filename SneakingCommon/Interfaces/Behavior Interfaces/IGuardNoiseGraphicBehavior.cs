using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.System_Classes;
using SneakingCommon.System_Classes;

using SneakingCommon.MVC_Interfaces;
using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;
using SneakingCommon.Data_Classes;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IGuardNoiseGraphicBehavior
    {
        void setNoiseVisual(NoiseMap noiseMap,IMap map,IGuard pc,List<IGuard> guards);
    }
}
