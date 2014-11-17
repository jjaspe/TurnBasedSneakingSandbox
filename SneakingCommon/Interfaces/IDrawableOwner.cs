using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface IDrawableOwner
    {
        System.Collections.Generic.List<Sneaking_Classes.Drawing_Classes.HighBlock> getHighBlocks();
        System.Collections.Generic.List<Sneaking_Classes.Drawing_Classes.HighWall> getHighWalls();
        System.Collections.Generic.List<Sneaking_Classes.Drawing_Classes.LowWall> getLowWalls();
        System.Collections.Generic.List<Sneaking_Classes.Drawing_Classes.LowBlock> getLowBlocks();
        bool hasBlockOnTop(pointObj point);
        List<pointObj> getReachableAdjacents(pointObj src);
        List<pointObj> getFreeAdjacents(pointObj src);
        bool areDividedByWall(pointObj src, pointObj dest);
        bool areDividedByLowWall(pointObj src, pointObj dest);
        bool areDividedByHighWall(pointObj src, pointObj dest);
        bool isReachable(pointObj src, pointObj dest);
    }
}
