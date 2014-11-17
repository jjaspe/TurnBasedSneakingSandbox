using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.System_Classes;
using SneakingCommon.Drawables;
using Sneaking_Gameplay.Sneaking_Drawables;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IPlayerVisibleBehavior
    {
        bool guardSeesPC(SneakingGuard g, SneakingPC pc,SneakingMap map);
    }
}
