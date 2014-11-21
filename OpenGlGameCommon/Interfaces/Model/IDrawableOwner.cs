using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;

using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;

namespace OpenGlGameCommon.Interfaces.Model
{
    public interface IDrawableOwner
    {
        List<HighBlock> getHighBlocks();
        List<HighWall> getHighWalls();
        List<LowWall> getLowWalls();
        List<LowBlock> getLowBlocks();
        bool hasBlockOnTop(IPoint point);
        List<IPoint> getReachableAdjacents(IPoint src);
        List<IPoint> getFreeAdjacents(IPoint src);
        bool areDividedByWall(IPoint src, IPoint dest);
        bool areDividedByLowWall(IPoint src, IPoint dest);
        bool areDividedByHighWall(IPoint src, IPoint dest);
        bool isReachable(IPoint src, IPoint dest);
    }
}
