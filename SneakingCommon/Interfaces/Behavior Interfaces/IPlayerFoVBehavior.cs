using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.System_Classes;

using SneakingCommon.MVC_Interfaces;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IPlayerFoVBehavior
    {
        void setPlayerFoV(IGuard myPc, IMap myMap);
    }
}
