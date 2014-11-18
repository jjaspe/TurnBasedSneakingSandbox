using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Interfaces.Model;
using SneakingCommon.Interfaces.Behaviors;
using SneakingCommon.Data_Classes;

namespace SneakingCommon.Interfaces.Model
{
    /// <summary>
    /// Extends IGuard to include noise stuff
    /// </summary>
    public interface ISneakingGuard:IDrawableGuard
    {
        ISneakingNPCBehavior NPCBehavior
        {
            get;
            set;
        }
        IKnownNoiseMapBehavior KnownNoiseMapBehavior
        {
            get;
            set;
        }
        NoiseMap UnknownNoiseMap
        {
            get;
            set;
        }
        NoiseMap NoiseMap
        {
            get;
            set;
        }
    }
}
