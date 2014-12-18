using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using SneakingCommon.Drawables;
using System.Drawing;
using Canvas_Window_Template.Drawables;
using SneakingCommon.System_Classes;
using SneakingCommon.Data_Classes;
using SneakingCommon.Interfaces.View;
using SneakingCommon.Interfaces.Model;
using OpenGlGameCommon.Interfaces.View;
using OpenGlGameCommon.Data_Classes;
using OpenGlGameCommon.Interfaces.Model;
using OpenGLGameCommon.Classes;
using SneakingCommon.Interfaces.Behaviors;
using OpenGlGameCommon.Exceptions;

namespace Sneaking_Gameplay.Sneaking_Drawables
{
    /// <summary>
    /// Extends OpenGlMap to take into account noise stuff. Contains behaviors for noise creation and landscape
    /// creation
    /// </summary>
    public class SneakingMap:OpenGlMap,ISneakingMap
    {
        static new SneakingMap myInstance;
        public static SneakingMap createInstance(int width,int height, int _tileSize,IPoint origin)
        {
            myInstance = new SneakingMap(width, height, _tileSize, origin,Common.planeOrientation.Z);
            if (origin == null)
                myInstance.MyOrigin = new PointObj(-width * _tileSize / 2, -height * _tileSize / 2, 0);
            else
                myInstance.MyOrigin = origin;
            myInstance.fillWall();
            return myInstance;
        }

        INoiseCreationBehavior myNoiseCreationBehavior;
        ILandscapeBehavior myLandscapeBehavior;
        public int MyLength
        {
            get
            {
                return MyHeight;
            }
            set { MyHeight = value; }
        }
        List<IPoint> entryPoints = new List<IPoint>();


        public ILandscapeBehavior LandscapeBehavior
        {
            get { return myLandscapeBehavior; }
            set { myLandscapeBehavior = value; }
        }
        public INoiseCreationBehavior NoiseCreationBehavior
        {
            get { return myNoiseCreationBehavior; }
            set { myNoiseCreationBehavior = value; }
        }

        private SneakingMap(int width, int length,int tileSize,IPoint origin,Common.planeOrientation orientation=Common.planeOrientation.Z)
            :base(width,length,tileSize,origin,orientation)
        {
            initializeWall();
        }
        private void initializeWall()
        {
            Orientation = Common.planeOrientation.Z;
            defaultColor = new float[] { Color.Green.R / 256.0f, Color.GreenYellow.G / 256.0f, Color.Green.B / 256.0f };
            defaultOutlineColor = new float[] { Color.Black.R / 256, Color.Black.G / 256, Color.Black.B / 256 };
           
        }

        new public List<IPoint> getAllTileOrigins()
        {
            List<IPoint> locations = new List<IPoint>();
            foreach (Tile t in MyTiles)
                locations.Add(t.MyOrigin);
            return locations;
        }

        new public List<SneakingGuard> getGuards()
        {
            return (from g in base.getGuards()
                    select g as SneakingGuard).ToList<SneakingGuard>();
        }

        #region WALL STUFF
        /// <summary>
        /// Override to create sneaking tiles instead of regular tiles
        /// </summary>
        public override void fillWall()
        {
            //Use createTiles to get origins
            tileObj[,] tiles = this.createTiles();
            MyTiles = new SneakingTile[MyWidth, MyHeight];
            this.Drawables.Clear();
            //Using origins, create MyTiles
            for (int i = 0; i < MyWidth; i++)
            {
                for (int j = 0; j < MyHeight; j++)
                {
                    MyTiles[i, j] = new SneakingTile(tiles[i, j].MyOrigin, tiles[i, j].MyEnd);
                    MyTiles[i, j].setColor(defaultColor);
                    MyTiles[i, j].TileSize = this.TileSize;
                    ((SneakingTile)myTiles[i, j]).OriginalColor = myTiles[i, j].MyColor;
                    this.Drawables.Add((SneakingTile)MyTiles[i, j]);
                }
            }
        }
        #endregion

        #region NOISE STUFF ACCESSORS
        public ModelNoiseMap createNoiseMap(IPoint src, int level)
        {
            if (NoiseCreationBehavior == null)
                throw new BehaviorNotSetException(NoiseCreationBehavior.GetType().ToString(), "SneakingMap:createNoiseMap");
            return new ModelNoiseMap(
            NoiseCreationBehavior.createNoiseMap(src, level, this));
        }
        #endregion 


        #region PATHFINDING STUFF
        
        /// <summary>
        /// Overrides the one from OpenGlMap to use PatrolPaths instead of OpenGlPath
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="availableTiles"></param>
        /// <returns></returns>
        new public PatrolPath getShortestPath(IPoint src, IPoint dest, List<IPoint> availableTiles)
        {
            DistanceMap distMap = getDistanceMap(src);
            PatrolPath reverse=new PatrolPath();
            valuePoint vPrevious = new valuePoint();
            List<IPoint> availableTilesCopy = availableTiles.GetRange(0,availableTiles.Count);
            if (isPointInList(availableTiles, dest))
            {
                //Get last point, add to path,get next, add, etc
                reverse.MyWaypoints.Add(dest);
                vPrevious.p=dest;
                while (!vPrevious.p.equals(src))
                {
                    vPrevious = getPreviousInPath(getValuePoint(distMap, vPrevious.p), 
                        availableTilesCopy, distMap);
                    if (vPrevious != null && vPrevious.p != null)
                    {
                        reverse.MyWaypoints.Add(vPrevious.p);
                        availableTilesCopy.Remove(availableTilesCopy.Find(delegate(IPoint p) { return p.equals(vPrevious.p); }));
                    }
                    else //Ran out of available points before reaching end
                        return null;
                }
                //Reverse path
                reverse.MyWaypoints.Reverse();
            }
            return reverse;
        }
        #endregion


        public List<IPoint> EntryPoints
        {
            get { return entryPoints; }
            set { entryPoints = value; }
        }

        public List<IPoint> TileOrigins
        {
            get
            {
                return getAllTileOrigins();
            }
            set
            {
                return;
            }
        }        

        public List<IPoint> getReachablePoints(IPoint source)
        {
            throw new NotImplementedException();
        }

        public bool isTile(IPoint source)
        {
            throw new NotImplementedException();
        }

        public void lightPoints(List<IPoint> points)
        {
            throw new NotImplementedException();
        }

        public int getValue(string name)
        {
            throw new NotImplementedException();
        }

        public void setValue(string name, int value)
        {
            throw new NotImplementedException();
        }
    }
}
