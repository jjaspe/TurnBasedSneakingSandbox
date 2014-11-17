using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.Interfaces.Model;


namespace SneakingCommon.Interfaces.Behaviors
{
    public interface INoiseReactionBehavior
    {
        void reactToNoise(IGuard g, IMap map);
    }
}
