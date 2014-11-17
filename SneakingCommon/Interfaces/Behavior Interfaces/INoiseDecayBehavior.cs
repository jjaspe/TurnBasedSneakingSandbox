using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.System_Classes;
using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;
using SneakingCommon.Data_Classes;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface INoiseDecayBehavior
    {
        void decayNoise(WorldMessage message, NoiseMap noiseMap);
    }
}
