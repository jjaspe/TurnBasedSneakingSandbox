using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;
using OpenGLGameCommon.Classes;
using OpenGlGameCommon.Interfaces.View;

namespace OpenGlGameCommon.Interfaces.Model
{
    public interface IMap
    {
        /// <summary>
        /// Left-Bootom points for all the tiles in the map
        /// </summary>
        List<IPoint> TileOrigins
        {
            get;
            set;
        }

        /// <summary>
        /// A* generated maps with the distance from a source point (DistanceMap:source) to any other point in the map
        /// </summary>
        List<DistanceMap> DistanceMaps
        {
            get;
            set;
        }
        ILandscapeBehavior LandscapeBehavior
        {
            get;
            set;
        }


        DistanceMap getDistanceMap(IPoint src);
        List<IPoint> getReachablePoints(IPoint source);

        bool isTile(IPoint source);
        void lightPoints(List<IPoint> points);

        int getValue(string name);
        void setValue(string name, int value);
    }
}
