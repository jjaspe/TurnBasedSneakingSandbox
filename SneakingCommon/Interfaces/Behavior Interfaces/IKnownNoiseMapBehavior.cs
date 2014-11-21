using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.System_Classes;
using SneakingCommon.System_Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;


using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;
using SneakingCommon.Data_Classes;
using SneakingCommon.Interfaces.Model;
using OpenGlGameCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IKnownNoiseMapBehavior
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g">Guard whose known map is updating</param>
        /// <param name="knownMap">guard's known noise map</param>
        /// <param name="noiseLevel">level of generated noise</param>
        /// <param name="src">source of generated noise</param>
        void updateKnownNoiseMap(IDrawableGuard g,NoiseMap knownMap,int noiseLevel,IPoint src,NoiseSources noiseSource);
        void updateUnknownNoiseMap(IDrawableGuard g, NoiseMap unknownMap, int noiseLevel, IPoint src, NoiseSources noiseSource);
    }
}
