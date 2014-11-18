using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;

using OpenGlGameCommon.Data_Classes;
using OpenGlGameCommon.Interfaces.Behaviors;
using OpenGlGameCommon.Interfaces.Model;
using OpenGlCommonGame.Interfaces.Behaviors;

namespace OpenGlGameCommon.Interfaces.View
{
    public interface ILandscapeBehavior
    {
        bool isTile(IPoint source);
        void lighten(IPoint tileOrigin, int level);
        void darkenAllTiles();
        bool areAdjacent(IPoint p1, IPoint p2);
        void setTileSize(int size);
        int getTileSize();
        void setDrawableOwner(IDrawableOwner dw);
        void setTileBehavior(ITileBehavior tb);
        List<IPoint> getFoV(IPoint src, List<IPoint> availablePoints);
        bool isVisibleBy(IPoint observer, IPoint observed);
        List<IPoint> getAdjacents(IPoint src);
        List<IPoint> getReachables(ISneakingMap map, IPoint src);
        PatrolPath getShortestPath(ISneakingMap map, IPoint src, IPoint dest, List<IPoint> availableTiles);
        IPoint getClosestInAvailable(ISneakingMap map, IPoint dest, List<IPoint> availableTiles);
        List<IPoint> getReachableAdjacents(IPoint src);
        /// <summary>
        /// Returns orientation of dest with respect to source
        /// EX. if source is below dest, returns north
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        string getOrientation(IPoint source, IPoint dest);
        int getWidth();
    }
}
