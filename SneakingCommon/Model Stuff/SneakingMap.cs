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

namespace Sneaking_Gameplay.Sneaking_Drawables
{
    public class SneakingMap:OpenGlMap
    {
        static SneakingMap myInstance;
        public static SneakingMap createInstance(ISneakingMap map,int width,int length, int _tileSize,IDrawableOwner dw)
        {
            if(myInstance==null)
                myInstance=new SneakingMap(map,width,length,_tileSize,dw);

            return myInstance;
        }

        #region ATTRIBUTES
        wallObj myWall;
        public wallObj MyWall
        {
            get { return myWall; }
            set { myWall = value; }
        }
        ISneakingMap myMap;
        public ISneakingMap MyMap
        {
            get { return myMap; }
            set 
            { 
                myMap = value;
                MyMap.setTileOrigins(this.getAllTileOrigins());
            }
        }
        #endregion

        private SneakingMap(ISneakingMap map,int width, int length,int _tileSize,IDrawableOwner dw):base(width,length,_tileSize)
        {
            myWall = new wallObj();  
            initializeWall();
            MyMap = map;
            //MyMap.setNoiseCreationBehavior(new NoiseCreationBehaviorView1(dw));
            throw new Exception("Noise creation behavior in Sneaking map not set, Sneaking map constructor");
        }
        private void initializeWall()
        {
            myWall.Orientation = 3;
            myWall.defaultColor = new float[] { Color.Green.R / 256.0f, Color.GreenYellow.G / 256.0f, Color.Green.B / 256.0f };
            myWall.defaultOutlineColor = new float[] { Color.Black.R / 256, Color.Black.G / 256, Color.Black.B / 256 };
           
        }

        new public List<IPoint> getAllTileOrigins()
        {
            List<IPoint> locations = new List<IPoint>();
            foreach (Tile t in MyTiles)
                locations.Add(t.MyOrigin);
            return locations;
        }

        #region WALL STUFF
        /// <summary>
        /// Override to create sneaking tiles instead of regular tiles
        /// </summary>
        public override void fillWall()
        {
            tileObj[,] tiles = this.createTiles();
            MyTiles = new SneakingTile[MyWidth, MyHeight];
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
            return new ModelNoiseMap(
            MyMap.getCreationBehavior().createNoiseMap(src, level, MyMap));
        }
        #endregion 

        #region FIELD OF VIEW STUFF

       
        /*
        public List<IPoint> getCone(IPoint src,OpenGlGuard.OpenGlGuardOrientation direction,int distance)
        {
            List<IPoint> conePoints=new List<IPoint>();
            int startX=src.X/TileSize+MyWidth/2,startY=src.Y/TileSize+MyHeight/2;
            Tile current;
            #region FILL BY CASE
            switch (direction)
            {
                case OpenGlGuard.OpenGlGuardOrientation.up:
                    for(int i=0;i<distance && i+startY<MyHeight;i++)
                    {
                        for(int j=-i;j<=i&&j<MyWidth;j++)
                        {
                            current=getTile(new PointObj((startX+j-MyWidth/2)*TileSize,
                                (startY+i-MyHeight/2)*TileSize,0));
                            if(current!=null&&!hasBlockOnTop(current.MyOrigin))
                                conePoints.Add(current.MyOrigin);
                        }
                    }
                    break;
                case OpenGlGuard.OpenGlGuardOrientation.right:
                    for(int j=0;j<distance && j+startX<MyWidth;j++)
                    {
                        for(int i=-j;i<=j;i++)
                        {                       
                            current=getTile(new PointObj((startX+j-MyWidth/2)*TileSize,
                                (startY+i-MyHeight/2)*TileSize,0));
                            if(current!=null&&!hasBlockOnTop(current.MyOrigin))
                                conePoints.Add(current.MyOrigin);
                        }
                    }
                    break;
                case OpenGlGuard.OpenGlGuardOrientation.down:
                    for(int i=0;i<distance && startY-i>=0;i++)
                    {
                        for(int j=-i;j<=i&&j<MyWidth;j++)
                        {
                            current=getTile(new PointObj((startX+j-MyWidth/2)*TileSize,
                                (startY-i-MyHeight/2)*TileSize,0));
                            if(current!=null&&!hasBlockOnTop(current.MyOrigin))
                                conePoints.Add(current.MyOrigin);
                        }
                    }
                    break;
                case OpenGlGuard.OpenGlGuardOrientation.left:
                    for(int j=0;j<distance && startX-j<MyWidth;j++)
                    {
                        for(int i=-j;i<=j;i++)
                        {                       
                            current=getTile(new PointObj((startX-j-MyWidth/2)*TileSize,
                                (startY+i-MyHeight/2)*TileSize,0));
                            if(current!=null&&!hasBlockOnTop(current.MyOrigin))
                                conePoints.Add(current.MyOrigin);
                        }
                    }
                    break;
                default:
                    break;
            }
            #endregion
            return conePoints;
        }
        */
        #endregion

        #region PATHFINDING STUFF
        public bool tileFreeFromGuards(List<IGuard> _guards, IPoint tilePosition)
        {
            foreach (IGuard g in _guards)
            {
                if (g.getPosition().equals(tilePosition))
                    return false;
            }
            return true;
        }

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
                    vPrevious = getPreviousInPath(getValuePoint(distMap.MyPoints, vPrevious.p), 
                        availableTilesCopy, distMap.MyPoints);
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
    }
}
