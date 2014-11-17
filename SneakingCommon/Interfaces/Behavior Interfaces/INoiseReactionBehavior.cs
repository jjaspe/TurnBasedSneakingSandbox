using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.MVC_Interfaces;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface INoiseReactionBehavior
    {
        void reactToNoise(IGuard g, IMap map);
    }
}
