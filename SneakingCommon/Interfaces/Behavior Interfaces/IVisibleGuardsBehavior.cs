using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.System_Classes;
using SneakingCommon.Interfaces.Model;


namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IVisibleGuardsBehavior
    {
        void setVisibleGuards(List<IGuardAgent> guards,IGuard pc,IMap map);
    }


}
