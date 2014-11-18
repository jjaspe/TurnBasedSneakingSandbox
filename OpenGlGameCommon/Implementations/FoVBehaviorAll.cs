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

namespace OpenGlGameCommon.Implementations
{
    public class FoVBehaviorAll:IFoVBehavior
    {
        public void setDistance(int d)
        {
            return;
        }

        public List<IPoint> getFOVPoints(IDrawableGuard g, List<IPoint> availablePoints)
        {
            return availablePoints;
        }
    }
}
