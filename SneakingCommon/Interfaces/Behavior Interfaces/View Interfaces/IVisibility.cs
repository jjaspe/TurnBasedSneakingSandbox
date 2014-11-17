using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Gameplay.MVC_Interfaces.Model_Interfaces;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface IVisibilityBehavior
    {
        void setTileSize(int size);
        int getTileSize();
        void setDrawableOwner(IDrawableOwner dw);
        void setTileBehavior(ITileBehavior tb);
        List<pointObj> getFoV(pointObj src,List<pointObj> availablePoints,int height);
        bool isVisibleBy(pointObj observer, pointObj observed);
    }
}
