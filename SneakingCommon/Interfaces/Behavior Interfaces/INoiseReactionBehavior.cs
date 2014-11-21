using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.Interfaces.Model;
using OpenGlGameCommon.Interfaces.Model;


namespace SneakingCommon.Interfaces.Behaviors
{
    public interface INoiseReactionBehavior
    {
        void reactToNoise(IDrawableGuard g, ISneakingMap map);
    }
}
