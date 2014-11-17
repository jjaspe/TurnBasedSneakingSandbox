using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.Interfaces.Behaviors;
using SneakingCommon.Interfaces.View;


namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IVisibilityBehavior
    {
        void setTileSize(int size);
        int getTileSize();
        void setDrawableOwner(IDrawableOwner dw);
        void setTileBehavior(ITileBehavior tb);
        List<IPoint> getFoV(IPoint src,List<IPoint> availablePoints,int height);
        bool isVisibleBy(IPoint observer, IPoint observed);
    }
}
