using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;


namespace SneakingCommon.MVC_Interfaces
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
