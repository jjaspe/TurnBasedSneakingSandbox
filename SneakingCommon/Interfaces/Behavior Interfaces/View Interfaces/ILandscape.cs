using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Gameplay.MVC_Interfaces.Model_Interfaces;
using Sneaking_Classes.System_Classes;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface ILandscape
    {
        bool isTile(pointObj source);
        void lighten(pointObj tileOrigin, int level);
        void darkenAllTiles();
        bool areAdjacent(pointObj p1, pointObj p2);
        void setTileSize(int size);
        int getTileSize();
        void setDrawableOwner(IDrawableOwner dw);
        void setTileBehavior(ITileBehavior tb);
        List<pointObj> getFoV(pointObj src, List<pointObj> availablePoints);
        bool isVisibleBy(pointObj observer, pointObj observed);
        List<pointObj> getAdjacents(pointObj src);
        List<pointObj> getReachables(IMap map, pointObj src);
        PatrolPath getShortestPath(IMap map, pointObj src, pointObj dest, List<pointObj> availableTiles);
        pointObj getClosestInAvailable(IMap map, pointObj dest, List<pointObj> availableTiles);
        List<pointObj> getReachableAdjacents(pointObj src);
        /// <summary>
        /// Returns orientation of dest with respect to source
        /// EX. if source is below dest, returns north
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        string getOrientation(pointObj source, pointObj dest);
        int getWidth();
    }
}
