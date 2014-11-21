using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;         
using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Interfaces;    
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Interfaces.Behaviors;
using OpenGlGameCommon.Interfaces.Model;

namespace Sneaking_Gameplay.Game_Components.Implementations.Behaviors
{
    public class FoVBehaviorNone:IFoVBehavior
    {
        int distance = 0;
        public void setDistance(int d)
        {
            distance=Math.Max(0,d);
        }

        public List<IPoint> getFOVPoints(IDrawableGuard g, List<IPoint> availablePoints)
        {
            return new List<IPoint>();
        }
    }
}
