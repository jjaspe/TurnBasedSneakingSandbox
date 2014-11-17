using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;    
using Canvas_Window_Template.Drawables;
using SneakingCommon.Interfaces.Model;


namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IFoVBehavior
    {
        List<IPoint> getFOVPoints(IGuard g,List<IPoint> availablePoints);
        void setDistance(int d);
    }
}
