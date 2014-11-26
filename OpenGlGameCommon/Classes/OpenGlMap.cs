using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGLGameCommon.Classes;
using OpenGlGameCommon.Interfaces.Model;
using OpenGlGameCommon.Enums;


namespace OpenGlGameCommon.Classes
{
    /// <summary>
    /// Models a map containint geometry objects (wall,blocks,guards). Contains methods for pathfinding,
    /// raytracing, and moving guards. Since it implements IWorld it keeps the geometry elements
    /// as a list of IDrawables that can be accessed to get them drawn
    /// The class variable myInstance is not for singleton pattern, is to give access to map stuff to OpenGlPC (this might change in the future)
    /// </summary>
    public class OpenGlMap : wallObj,IWorld
    {
        public static OpenGlMap myInstance;
        public static OpenGlMap getInstance() { return myInstance; }

        protected List<IDrawable> drawables;
        /// <summary>
        /// This list contains all distance maps, with the key being their origins, and value the actual map.
        /// </summary>
        List<DistanceMap> myDistanceMaps;

        /// <summary>
        /// List of A* generated maps.
        /// </summary>
        public List<DistanceMap> DistanceMaps
        {
            get
            {
                if (myDistanceMaps == null)
                    generateDistanceMaps();
                return myDistanceMaps; 
            }
            set { myDistanceMaps = value; }
        }

        public List<IDrawable> Drawables
        {
            get { return drawables; }
            set { drawables = value; }
        }

        /// <summary>
        /// Creates a map with the given dimensions and tilesize. Calls initialize, and calls fillWall
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="_tileSize"></param>
        public OpenGlMap(int width, int length, int tileSize,IPoint origin, Common.planeOrientation orientation = Common.planeOrientation.Z)
        {
            initialize(width, length, tileSize, orientation);
            if (origin == null)
                this.MyOrigin = new PointObj(-width * tileSize / 2, -length * tileSize / 2, 0);
            else
                this.MyOrigin = origin;
            fillWall();
            myInstance = this;
        }

        protected void initialize(int width, int length, int tileSize, Common.planeOrientation orientation)
        {
            MyWidth = width;
            MyHeight = length;
            TileSize = tileSize;
            this.Orientation = orientation;
            initialize();
        }
        /// <summary>
        /// Set default colors and orientation (perpendicular to Z)
        /// </summary>
        private void initialize()
        {
            
            Orientation = Common.planeOrientation.Z;
            defaultColor = new float[] { Color.Green.R / 256.0f, Color.GreenYellow.G / 256.0f, Color.Green.B / 256.0f };
            defaultOutlineColor = new float[] { Color.Black.R / 256, Color.Black.G / 256, Color.Black.B / 256 };
            drawables = new List<IDrawable>();
        }
        public void reset()
        {
            drawables = new List<IDrawable>();
            MyTiles = null;
        }
        public Tile getTile(int tileId)
        {
            foreach (Tile tile in MyTiles)
            {
                if (tile.myId == tileId)
                    return tile;
            }
            return null;
        }

        #region Map Elements
        public List<IPoint> getAllTileOrigins()
        {
            List<IPoint> locations = new List<IPoint>();
            foreach (Tile t in myTiles)
                locations.Add(t.MyOrigin);
            return locations;
        }
        public LowBlock getLowBlock(int lbId)
        {
            foreach (IDrawable block in Drawables)
            {
                if (block.getId() == lbId)
                    return (LowBlock)block;
            }
            return null;
        }
        public HighBlock getHighBlock(int hbId)
        {
            foreach (IDrawable block in Drawables)
            {
                if (block.getId() == hbId)
                    return (HighBlock)block;
            }
            return null;
        }
        public LowWall getLowWall(int lwId)
        {
            foreach (IDrawable wall in Drawables)
            {
                if (wall.getId() == lwId)
                    return (LowWall)wall;
            }
            return null;
        }
        public HighWall getHighWall(int hwId)
        {
            foreach (IDrawable wall in Drawables)
            {
                if (wall.getId() == hwId)
                    return (HighWall)wall;
            }
            return null;
        }
        /* Returns tiles whose origin is points */
        public List<Tile> getTiles(List<IPoint> points)
        {
            List<Tile> tiles=new List<Tile>();
            Tile currentTile;
            if(points!=null)
            {
                foreach(IPoint point in points)
                {
                    currentTile=getTile(point);
                    if(currentTile!=null)
                        tiles.Add(currentTile);
                }
            }
            return tiles;
        }
        public Tile getTile(IPoint origin)
        {
            foreach (Tile tile in MyTiles)
            {
                if (tile.origin.equals(origin))
                    return tile;
            }
            return null;
        }
        public IDrawableGuard getGuard(int gId)
        {
            foreach (IDrawable guard in Drawables)
            {
                if (guard.getId() == gId)
                    return (IDrawableGuard)guard;
            }
            return null;
        }
        public List<HighBlock> getHighBlocks()
        {
            List<HighBlock> blocks = new List<HighBlock>();
            foreach (IDrawable drw in drawables)
            {
                if (drw.getId() % GameObjects.objectTypes == HighBlock.idType)
                    blocks.Add((HighBlock)drw);
            }
            return blocks;
        }
        public List<HighWall> getHighWalls()
        {
            List<HighWall> walls = new List<HighWall>();
            foreach (IDrawable drw in drawables)
            {
                if (drw.getId() % GameObjects.objectTypes == HighWall.idType)
                    walls.Add((HighWall)drw);
            }
            return walls;
        }
        public List<LowWall> getLowWalls()
        {
            List<LowWall> walls = new List<LowWall>();
            foreach (IDrawable drw in drawables)
            {
                if (drw.getId() % GameObjects.objectTypes == LowWall.idType)
                    walls.Add((LowWall)drw);
            }
            return walls;
        }
        public List<IOcluding> getOcluders()
        {
            List<IOcluding> ocluders = new List<IOcluding>();
            ocluders.AddRange(getHighWalls());
            ocluders.AddRange(getHighBlocks());
            return ocluders;
        }
        /// <summary>
        /// Out of all the drawables, return the guards
        /// </summary>
        /// <returns></returns>
        public List<IDrawableGuard> getGuards()
        {
            List<IDrawableGuard> guards = new List<IDrawableGuard>();
            IDrawableGuard drawable;
            foreach (IDrawable drw in drawables)
            {
                drawable = drw as IDrawableGuard;
                if(drawable !=null)
                    guards.Add(drawable);
            }
            return guards;
        }
        #endregion

        public void CreateTiles()
        {
            tileObj[,] tiles=this.createTiles();
            MyTiles = new tileObj[MyWidth, MyHeight];
            for (int i = 0; i < MyWidth; i++)
            {
                for (int j = 0; j < MyHeight; j++)
                {
                    MyTiles[i, j] = new Tile(tiles[i, j].MyOrigin, tiles[i, j].MyEnd);
                    MyTiles[i, j].setColor(defaultColor);
                    MyTiles[i, j].TileSize = this.TileSize;
                    ((Tile)myTiles[i, j]).OriginalColor = myTiles[i, j].MyColor;
                    Drawables.Add((Tile)MyTiles[i,j]);
                }
            }
        }
        /// <summary>
        /// Adds all elements of toAdd to Drawables
        /// </summary>
        /// <param name="toAdd"></param>
        public void addDrawables(List<IDrawable> toAdd)
        {
            foreach (IDrawable d in toAdd)
                Drawables.Add(d);
        }
        /// <summary>
        /// Tries to add all elements of the list to drawables. THe ones that aren't IDrawables wont be added
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toAdd"></param>
        public void addDrawables<T>(ref List<T> toAdd)
        {
            foreach(T d in toAdd)
            {
                IDrawable drawable = d as IDrawable;
                if (drawable != null)
                    Drawables.Add(drawable);
            }
        }

        #region DISTANCE MAP STUFF
        public void setDistanceMaps(List<DistanceMap> distanceMaps)
        {
            DistanceMaps = distanceMaps;
        }
        public DistanceMap getDistanceMap(IPoint src)
        {
            return DistanceMaps.Find(n => n.MyOrigin.equals(src));
        }
        #endregion

        #region GUARDS STUFF
        public bool moveGuard(IDrawableGuard g, IPoint p)
        {
            Tile newP = getTile(p);
            if (newP != null)
            {
                //g.MyCurrentTile = newP;
                g.Position = p;
                return true;
            }
            return false;
        }
        #endregion

        #region FIELD OF VIEW STUFF       
      
        public void darkenTiles()
        {
            foreach (Tile t in myTiles)
            {
                t.Shaded = 10;
            }
        }
        public void lightenFoVCone(List<IPoint> points)
        {
            Tile currentTile;
            foreach (IPoint p in points)
            {
                currentTile = getTile(p);
                if (currentTile != null)
                    currentTile.Shaded = 0;
            }
        }
        public List<IPoint> getCone(IPoint src,GuardOrientation direction,int distance)
        {
            List<IPoint> conePoints=new List<IPoint>();
            int startX = (int)src.X / TileSize + MyWidth / 2, startY = (int)src.Y / TileSize + MyHeight / 2;
            Tile current;
            #region FILL BY CASE
            switch (direction)
            {
                case GuardOrientation.up:
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
                case GuardOrientation.right:
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
                case GuardOrientation.down:
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
                case GuardOrientation.left:
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

        #endregion

        #region PATHFINDING STUFF
        public void initializeValueMap(List<valuePoint> map,int defaultValue)
        {
            foreach (Tile t in myTiles)
            {
                map.Add(new valuePoint(t.MyOrigin, defaultValue));
            }
        }
               
        public bool isFree(IPoint src)
        {
            foreach (IDrawable drw in drawables)
            {
                if (drw.getId() % GameObjects.objectTypes == LowBlock.idType ||
                    drw.getId() % GameObjects.objectTypes == HighBlock.idType)
                {
                    if (src.equals(drw.getPosition()))
                        return false;
                }
            }
            return true;
        }
        public bool tileFreeFromGuards(List<IDrawableGuard> _guards, IPoint tilePosition)
        {
            foreach (IDrawableGuard g in _guards)
            {
                if (g.Position.equals(tilePosition))
                    return false;
            }
            return true;
        }
        public bool areAdjacent(IPoint p1, IPoint p2)
        {
            if (p1.X == p2.X)
            {
                if (p1.Y == p2.Y - TileSize || p1.Y == p2.Y + TileSize)
                    return true;
            }
            else if (p2.Y == p1.Y)
            {
                if (p1.X == p2.X - TileSize || p1.X == p2.X + TileSize)
                    return true;
            }
            return false;
        }
        /* Is there a wall at src, with specified orientation */
        public bool wallAt(IPoint src, Common.planeOrientation orientation)
        {
            foreach (HighWall hw in getHighWalls())
            {
                if (hw.MyOrigin.equals(src) && hw.Orientation == orientation)
                    return true;
            }
            foreach (LowWall lw in getLowWalls())
            {
                if (lw.MyOrigin.equals(src) && lw.Orientation == orientation)
                    return true;
            }
            return false;
        }
        public bool lowWallAt(IPoint src, Common.planeOrientation orientation)
        {
            foreach (LowWall lw in getLowWalls())
            {
                if (lw.MyOrigin.equals(src) && lw.Orientation == orientation)
                    return true;
            }
            return false;
        }
        public bool highWallAt(IPoint src, Common.planeOrientation orientation)
        {
            foreach (HighWall hw in getHighWalls())
            {
                if (hw.MyOrigin.equals(src) && hw.Orientation == orientation)
                    return true;
            }
            return false;
        }
        public bool areDividedByWall(IPoint src, IPoint dest)
        {
            if (areAdjacent(src, dest))
            {
                if (src.X == dest.X)
                {
                    if (src.Y == dest.Y - TileSize)//go up
                    {
                        if (wallAt(dest, Common.planeOrientation.Y))
                            return true;
                    }
                    else//go down
                    {
                        if (wallAt(src, Common.planeOrientation.Y))
                            return true;
                    }
                }
                else
                {
                    if (src.X == dest.X - TileSize)//go right
                    {
                        if (wallAt(dest, Common.planeOrientation.X))
                            return true;
                    }
                    else//go left
                    {
                        if (wallAt(src, Common.planeOrientation.X))
                            return true;
                    }
                }
            }
            return false;
        }
        public bool areDividedByLowWall(IPoint src, IPoint dest)
        {
            if (areAdjacent(src, dest))
            {
                if (src.X == dest.X)
                {
                    if (src.Y == dest.Y - TileSize)//go up
                    {
                        if (lowWallAt(dest, Common.planeOrientation.Y))
                            return true;
                    }
                    else//go down
                    {
                        if (lowWallAt(src, Common.planeOrientation.Y))
                            return true;
                    }
                }
                else
                {
                    if (src.X == dest.X - TileSize)//go right
                    {
                        if (lowWallAt(dest, Common.planeOrientation.X))
                            return true;
                    }
                    else//go left
                    {
                        if (lowWallAt(src, Common.planeOrientation.X))
                            return true;
                    }
                }
            }
            return false;
        }
        public bool areDividedByHighWall(IPoint src, IPoint dest)
        {
            if (areAdjacent(src, dest))
            {
                if (src.X == dest.X)
                {
                    if (src.Y == dest.Y - TileSize)//go up
                    {
                        if (highWallAt(dest, Common.planeOrientation.Y))
                            return true;
                    }
                    else//go down
                    {
                        if (highWallAt(src, Common.planeOrientation.Y))
                            return true;
                    }
                }
                else
                {
                    if (src.X == dest.X - TileSize)//go right
                    {
                        if (highWallAt(dest, Common.planeOrientation.X))
                            return true;
                    }
                    else//go left
                    {
                        if (highWallAt(src, Common.planeOrientation.X))
                            return true;
                    }
                }
            }
            return false;
        }
        public List<IPoint> getReachableAdjacents(IPoint src)
        {
            List<IPoint> points = new List<IPoint>();
            IPoint top = new PointObj(src.X, src.Y + TileSize, src.Z)
                , bottom = new PointObj(src.X, src.Y - TileSize, src.Z),
                right = new PointObj(src.X + TileSize, src.Y, src.Z),
                left = new PointObj(src.X - TileSize, src.Y, src.Z);

            if (getTile(top) != null && !areDividedByWall(src, top) && isFree(top))
                points.Add(top);
            if (getTile(bottom) != null && !areDividedByWall(src, bottom) && isFree(bottom))
                points.Add(bottom);
            if (getTile(right) != null && !areDividedByWall(src, right) && isFree(right))
                points.Add(right);
            if (getTile(left) != null && !areDividedByWall(src, left) && isFree(left))
                points.Add(left);
            return points;
        }
        public List<IPoint> getAdjacents(IPoint src)
        {
            List<IPoint> points = new List<IPoint>();
            IPoint top = new PointObj(src.X, src.Y + TileSize, src.Z)
                , bottom = new PointObj(src.X, src.Y - TileSize, src.Z),
                right = new PointObj(src.X + TileSize, src.Y, src.Z),
                left = new PointObj(src.X - TileSize, src.Y, src.Z);

            if (getTile(top) != null )
                points.Add(top);
            if (getTile(bottom) != null)
                points.Add(bottom);
            if (getTile(right) != null)
                points.Add(right);
            if (getTile(left) != null )
                points.Add(left);
            return points;
        }
        public bool isPointInList(List<IPoint> list,IPoint p)
        {
            return list.Find(delegate (IPoint _p){return _p.equals(p);})!=null;
        }
        public double getValueFromPoint(DistanceMap map,IPoint p)
        {
            return map.MyPoints.Find( delegate (valuePoint vp){return vp.p.equals(p);}).value;
        }
        public valuePoint getValuePoint(DistanceMap map,IPoint p)
        {
            return map.MyPoints.Find( delegate (valuePoint vp){return vp.p.equals(p);});
        }
        /// <summary>
        /// Puts p in map if map doesn't have a point there or if the one
        /// there has a smaller value
        /// </summary>
        /// <param name="p"></param>
        /// <param name="distance"></param>
        /// <param name="distMap"></param>
        public void setPointInMap(IPoint p, int distance,List<valuePoint> distMap)
        {
            valuePoint currentDP;
            currentDP = distMap.Find(
                        delegate(valuePoint _dp)
                        {
                            return _dp.p.equals(p);
                        });
            //If it is -1, assign distance, if it already has a distance, see if the new one is smaller
            currentDP.value = currentDP.value == -1 ? distance : Math.Min(currentDP.value, distance);
        }                            
        /// <summary>
        /// Creates a map where each point has as its value the distance to
        /// src.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public DistanceMap calculateDistanceMap(IPoint src)
        {
            DistanceMap distMap=new DistanceMap();
            initializeValueMap(distMap.MyPoints,-1);
            List<IPoint> currentPoints=new List<IPoint>(),adjacents=new List<IPoint>(),tempAdjacents;
            currentPoints.Add(src);
            //Put origin point in distance map, with distance 0
            setPointInMap(src, 0,distMap.MyPoints);
            bool keepGoing=true;
            int distance=1;

            do
            {
                adjacents.Clear();
                foreach (IPoint p in currentPoints)//Get all points adjacent to current edge points
                {
                    tempAdjacents = getReachableAdjacents(p);
                    foreach (IPoint _ap in tempAdjacents)
                    {
                        //Only add if it wasn't already in the map
                        if (!isPointInList(adjacents,_ap))
                            adjacents.Add(_ap);
                    }
                }

                //Remove from adjacents all the elements that have distance!=-1 in distMap (already assigned)              
                adjacents.RemoveAll(
                    delegate (IPoint _p)
                    {
                        return distMap.MyPoints.Find(
                            delegate (valuePoint _dp)
                            {
                                return _dp.p.equals(_p);
                            }).value!=-1;
                    });

                //To the points left in adjacents, set distance in distMap
                foreach (IPoint p in adjacents)
                {
                    setPointInMap(p, distance, distMap.MyPoints);
                }

                //increase distance
                distance++;

                //Stop if there were no more adjacents
                if (adjacents.Count == 0)
                    keepGoing = false;
                else //add adjacents that were just changed to currents
                {
                    currentPoints.Clear();
                    currentPoints.AddRange(adjacents);
                }
            } while (keepGoing);

            return distMap;
        }
        public void generateDistanceMaps()
        {
            myDistanceMaps = new List<DistanceMap>();
            foreach (IPoint src in this.getAllTileOrigins())
            {
                myDistanceMaps.Add(calculateDistanceMap(src));
            }
        }

        public OpenGlPath getShortestPath(IPoint src, IPoint dest, List<IPoint> availableTiles)
        {
            DistanceMap distMap = getDistanceMap(src);
            OpenGlPath reverse=new OpenGlPath();
            valuePoint vPrevious = new valuePoint();
            List<IPoint> availableTilesCopy = availableTiles.GetRange(0,availableTiles.Count);
            if (isPointInList(availableTiles, dest))
            {
                //Get last point, add to path,get next, add, etc
                reverse.MyWaypoints.Add(dest);
                vPrevious.p=dest;
                while (!vPrevious.p.equals(src))
                {
                    vPrevious = getPreviousInPath(getValuePoint(distMap, vPrevious.p), availableTilesCopy, distMap);
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
        public IPoint getClosestInAvailable(IPoint dest,List<IPoint> availableTiles)
        {
            DistanceMap distMap=getDistanceMap(dest);
            IPoint closest=null;
            double closestDistance=5000,currentDistance;
            foreach (IPoint p in availableTiles)
            {
                currentDistance = getValueFromPoint(distMap, p);
                if (currentDistance < closestDistance &&  currentDistance>-1)
                {
                    closestDistance = currentDistance;
                    closest = p;
                }                
            }
            return closest;
        }
        public valuePoint getPreviousInPath(valuePoint src, List<IPoint> availableTiles, DistanceMap distMap)
        {
            List<IPoint> adjacents = getAdjacents(src.p);
            double lowest = -1, current;
            valuePoint lowestPoint = null;
            foreach (IPoint p in adjacents)
            {
                current = getValueFromPoint(distMap, p);
                if (isPointInList(availableTiles, p) && current != -1)
                {
                    if (lowest == -1)//First one
                    {
                        lowest = current;
                        lowestPoint = getValuePoint(distMap, p);
                    }
                    else if (current < lowest)//Assign if new one is lower
                    {
                        lowest = current;
                        lowestPoint = getValuePoint(distMap, p);
                    }
                }
            }
            return lowestPoint;
        }
        public bool isReachable(IPoint src, IPoint dest)
        {
            if (!isFree(dest))
                return false;            
            
            return false;
        }
        public List<IPoint> getReachables(IPoint src)
        {
            DistanceMap wholeMap = getDistanceMap(src);
            List<IPoint> reachableMap = new List<IPoint>();
            foreach (valuePoint vp in wholeMap.MyPoints)
            {
                if (vp.value > -1)
                    reachableMap.Add(vp.p);
            }
            return reachableMap;
        }
        #endregion

       
        #region COLLISION RAYTRACING
        bool hasBlockOnTop(IPoint src)
        {
            //Get High blocks
            List<HighBlock> blocks = getHighBlocks();
            foreach (HighBlock block in blocks)
            {
                if (block.MyOrigin.equals(src))
                    return true;
            }
            return false;
        }
        bool getCollision(IPoint src, IPoint dest)
        {
            //Get High blocks
            List<HighBlock> blocks = getHighBlocks();
            //Get High walls
            List<HighWall> walls = getHighWalls();
            

            foreach (HighBlock block in blocks)
            {
                if (block.Intercepts(src, dest))
                    return true;
            }

            foreach (HighWall wall in walls)
            {
                if (wall.Intercepts(src, dest))
                    return true;
            }

            return false;
        }
        public List<IPoint> getRaytracedFOV(IPoint gSrc, GuardOrientation orientation)
        {
            List<IPoint> RTFoV = new List<IPoint>();
            List<Tile> freeTiles = new List<Tile>();
            //Create new srcPoint to keep guard position intact,and raise it to High level height
            IPoint fSrc=new PointObj((int)getTile(gSrc).getCenter((Common.tileSide)orientation)[0],(int)getTile(gSrc).getCenter()[1],
                TileSize+1),
                    fDest;
            foreach (Tile tile in myTiles)
            {
                if (!hasBlockOnTop(tile.origin))
                    freeTiles.Add(tile);
            }
            foreach (Tile tile in freeTiles)
            {
                //raise origin
                fDest=new PointObj((int)tile.getCenter()[0],(int)tile.getCenter()[1],
                TileSize+1);
                if (!getCollision(fSrc,fDest))
                    RTFoV.Add(tile.MyOrigin);
            }
            return RTFoV;            
        }
        public List<IPoint> getRaytracedFOV(IPoint gSrc, GuardOrientation orientation,
            List<IPoint> FOV)
        {
            List<IPoint> RTFoV = new List<IPoint>();
            List<Tile> freeTiles = new List<Tile>();
            //Create new srcPoint to keep guard position intact,and raise it to High level
            IPoint fSrc = new PointObj((int)getTile(gSrc).getCenter((Common.tileSide)orientation)[0], (int)getTile(gSrc).getCenter()[1],
                TileSize + 1),
                    fDest;
            foreach (IPoint point in FOV)
            {
                if (!hasBlockOnTop(point))
                    freeTiles.Add(getTile(point));
            }
            foreach (Tile tile in freeTiles)
            {
                //raise origin
                fDest = new PointObj((int)tile.getCenter()[0], (int)tile.getCenter()[1],
                TileSize + 1);
                if (!getCollision(fSrc, fDest))
                    RTFoV.Add(tile.MyOrigin);
            }
            return RTFoV;
        }
        public bool isVisibleBy(IPoint observed, IPoint observer)
        {
            return !getCollision(observer, observed);
        }
        #endregion

        List<IDrawable> IWorld.getEntities()
        {
            return Drawables;
        }

        void IWorld.add(IDrawable d)
        {
            Drawables.Add(d);
        }

        IDrawable IWorld.remove(IDrawable d)
        {
            return Drawables.Remove(d)?d:null;
        }
    }        
}
