using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Interfaces.Behaviors;
using OpenGlGameCommon.Interfaces.View;
using OpenGlGameCommon.Interfaces.Model;


namespace OpenGlGameCommon.Interfaces.Behaviors
{
    public interface IVisibilityBehavior
    {
        int TileSize
        {
            get;
            set;
        }
        void setDrawableOwner(IDrawableOwner dw);
        void setTileBehavior(ITileBehavior tb);
        List<IPoint> getFoV(IPoint src,List<IPoint> availablePoints,int height);
        bool isVisibleBy(IPoint observer, IPoint observed);
    }
}
