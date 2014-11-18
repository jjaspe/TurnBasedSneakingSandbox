using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGLGameCommon.Classes;


namespace OpenGlGameCommon.Classes
{
    public class OpenGlMap : wallObj,IWorld
    {       
        static OpenGlMap myInstance;
        public static OpenGlMap createInstance(int width,int height, int _tileSize,IPoint origin,Common.planeOrientation orientation=Common.planeOrientation.Z)
        {
            if(myInstance==null)
            {
                myInstance=new OpenGlMap(width,height,_tileSize,orientation);
                if (origin == null)
                    myInstance.MyOrigin = new PointObj(-width * _tileSize / 2, -height * _tileSize / 2, 0);
                else
                    myInstance.MyOrigin = origin;
                myInstance.fillWall();
            }
            return myInstance;
        }
        public static OpenGlMap getInstance()
        {
            return myInstance;
        }

        protected List<IDrawable> tiles;
        /// <summary>
        /// This list contains all distance maps, with the key being their origins, and value the actual map.
        /// </summary>
        List<DistanceMap> myDistanceMaps;

        public List<DistanceMap> MyDistanceMaps
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
            get { return tiles; }
            set { tiles = value; }
        }     

        /// <summary>
        /// Creates a map with the given dimensions and tilesize. Calls initialize, which sets orientation, colors and
        /// creates drawable list
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="_tileSize"></param>
        public OpenGlMap(int width, int length,int _tileSize)
        {
            MyWidth = width;
            MyHeight = length;
            TileSize = _tileSize;
            initialize();
        }
        public OpenGlMap(int width, int length, int tileSize, Common.planeOrientation orientation)
        {
            MyWidth = width;
            MyHeight = length;
            TileSize = tileSize;
            initialize();
            this.Orientation = orientation;
        }
        /// <summary>
        /// Set default colors and orientation (perpendicular to Z)
        /// </summary>
        private void initialize()
        {
            Orientation = Common.planeOrientation.Z;
            defaultColor = new float[] { Color.Green.R / 256.0f, Color.GreenYellow.G / 256.0f, Color.Green.B / 256.0f };
            defaultOutlineColor = new float[] { Color.Black.R / 256, Color.Black.G / 256, Color.Black.B / 256 };
            tiles = new List<IDrawable>();
        }
        public void reset()
        {
            tiles = new List<IDrawable>();
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
        public OpenGlGuard getGuard(int gId)
        {
            foreach (IDrawable guard in Drawables)
            {
                if (guard.getId() == gId)
                    return (OpenGlGuard)guard;
            }
            return null;
        }
        public List<HighBlock> getHighBlocks()
        {
            List<HighBlock> blocks = new List<HighBlock>();
            foreach (IDrawable drw in tiles)
            {
                if (drw.getId() % GameObjects.objectTypes == HighBlock.idType)
                    blocks.Add((HighBlock)drw);
            }
            return blocks;
        }
        public List<HighWall> getHighWalls()
        {
            List<HighWall> walls = new List<HighWall>();
            foreach (IDrawable drw in tiles)
            {
                if (drw.getId() % GameObjects.objectTypes == HighWall.idType)
                    walls.Add((HighWall)drw);
            }
            return walls;
        }
        public List<LowWall> getLowWalls()
        {
            List<LowWall> walls = new List<LowWall>();
            foreach (IDrawable drw in tiles)
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
        public List<OpenGlGuard> getGuards()
        {
            List<OpenGlGuard> guards = new List<OpenGlGuard>();
            foreach (IDrawable drw in tiles)
            {
                if (drw.getId() % GameObjects.objectTypes == OpenGlGuard.idType)
                    guards.Add((OpenGlGuard)drw);
            }
            return guards;
        }

        public override void fillWall()
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
        public void addDrawables(List<IDrawable> toAdd)
        {
            foreach (IDrawable d in toAdd)
                tiles.Add(d);
        }

        #region DISTANCE MAP STUFF
        public void setDistanceMaps(List<DistanceMap> distanceMaps)
        {
            MyMap.setDistanceMaps(distanceMaps);
        }
        public DistanceMap getDistanceMap(IPoint src)
        {
            return .getDistanceMap(src);
        }
        #endregion

        #region GUARDS STUFF
        public bool moveGuard(OpenGlGuard g, IPoint p)
        {
            Tile newP = getTile(p);
            if (newP != null)
            {
                //g.MyCurrentTile = newP;
                g.MyPosition = p;
                return true;
            }
            return false;
        }
        #endregion

        #region SHADOWCASTING (Not used anymore)
        public double getSlope(Tile src, IPoint p)
        {
            return ((src.bottomLeft()[1] - p.Y) / (src.bottomLeft()[0] - p.X));
        }
        public double getSlope(Tile src, double[] p)
        {
            return ((src.bottomLeft()[1] - p[1]) / (src.bottomLeft()[0] - p[0]));
        }
        public double getSlope(double[] p1, double[] p2)
        {
            return p1[1]==p2[1]?0:(p1[1] - p2[1]) / (p1[0] - p2[0]);
        }
        public double getInvSlope(Tile src, IPoint p)
        {
            return ((src.bottomLeft()[0] - p.X) / (src.bottomLeft()[1] - p.Y));
        }
        public double getInvSlope(Tile src, double[] p)
        {
            return (src.bottomLeft()[0] - p[0]) / (src.bottomLeft()[1] - p[1]);
        }
        public double getInvSlope(double[] p1, double[] p2)
        {
            return p1[0]==p2[0]?0:(p1[0] - p2[0]) / (p1[1] - p2[1]);
        }

        private class Slope
        {
            public Slope(double v)
            {
                value = v;
            }
            public double value;
        }

        /// <summary>
        ///   Based on src, creates octancts as arrays.
        ///   Each octanct starts at lower left corner (i.e., [0,0] is at the leftmost corner)
        ///   The following variables are used for creating the octanct sizes
        ///       Xone        XTwo
        ///   <-------><------------------->  
        ///   ****************************** ^ 
        ///   ****************************** | 
        ///   ****************************** | YTwo
        ///   ****************************** | 
        ///   ****************************** v 
        ///   ********src******************* ^ 
        ///   ****************************** | YOne
        ///   ****************************** | 
        ///   ****************************** v 
        ///   Octancts are then populated from obstacle map. Values outside the map are set to -1.
        /// </summary>
        /// <param name="src"></param>
        /// <returns>List of Octancts</returns>
        public valuePoint[][,] getOctancts(IPoint src)
        {
            double sizeXOne = MyWidth / 2 + src.X / TileSize + 1, sizeXTwo = MyWidth / 2 - src.X / TileSize;
            double sizeYOne = MyHeight / 2 + src.Y / TileSize + 1, sizeYTwo = MyHeight / 2 - src.Y / TileSize;
            int currentSize;

            #region Init octancts
            valuePoint[][,] octancts = new valuePoint[8][,];
            //Octanct 1
            currentSize = (int)sizeYTwo;
            octancts[0] = new valuePoint[currentSize, currentSize];
            for (int i = 0; i < currentSize; i++)
            {
                for (int j = 0; j < currentSize; j++)
                {
                    octancts[0][i, j] = new valuePoint(new PointObj(src.X - (currentSize - j-1) * TileSize, 
                        src.Y + i * TileSize, 0), -1);
                }
            }

            //Octanct 2
            octancts[1] = new valuePoint[currentSize, currentSize];
            for (int i = 0; i < currentSize; i++)
            {
                for (int j = 0; j < currentSize; j++)
                {
                    octancts[1][i, j] = new valuePoint(new PointObj(src.X + j * TileSize, 
                        src.Y + i * TileSize, 0), -1);
                }
            }

            //Octanct 3
            currentSize = (int)sizeXTwo;
            octancts[2] = new valuePoint[currentSize, currentSize];
            for (int i = 0; i < currentSize; i++)
            {
                for (int j = 0; j < currentSize; j++)
                {
                    octancts[2][i, j] = new valuePoint(new PointObj(src.X + j * TileSize,
                        src.Y + i * TileSize, 0), -1);
                }
            }

            //Octanct 4
            octancts[3] = new valuePoint[currentSize, currentSize];
            for (int i = 0; i < currentSize; i++)
            {
                for (int j = 0; j < currentSize; j++)
                {
                    octancts[3][i, j] = new valuePoint(new PointObj(src.X + j * TileSize, 
                        src.Y - (currentSize - i-1) * TileSize, 0), -1);
                }
            }

            //Octanct 5
            currentSize = (int)sizeYOne;
            octancts[4] = new valuePoint[currentSize, currentSize];
            for (int i = 0; i < currentSize; i++)
            {
                for (int j = 0; j < currentSize; j++)
                {
                    octancts[4][i, j] = new valuePoint(new PointObj(src.X + j * TileSize, 
                        src.Y - (currentSize - i-1) * TileSize, 0), -1);
                }
            }

            //Octanct 6
            octancts[5] = new valuePoint[currentSize, currentSize];
            for (int i = 0; i < currentSize; i++)
            {
                for (int j = 0; j < currentSize; j++)
                {
                    octancts[5][i, j] = new valuePoint(new PointObj(src.X - (currentSize - j-1) * TileSize,
                        src.Y - (currentSize - i-1) * TileSize, 0), -1);
                }
            }

            //Octanct 7
            currentSize = (int)sizeXOne;
            octancts[6] = new valuePoint[currentSize, currentSize];
            for (int i = 0; i < currentSize; i++)
            {
                for (int j = 0; j < currentSize; j++)
                {
                    octancts[6][i, j] = new valuePoint(new PointObj(src.X - (currentSize - j-1) * TileSize,
                        src.Y - (currentSize - i-1) * TileSize, 0), -1);
                }
            }

            //Octanct 8
            octancts[7] = new valuePoint[currentSize, currentSize];
            for (int i = 0; i < currentSize; i++)
            {
                for (int j = 0; j < currentSize; j++)
                {
                    octancts[7][i, j] = new valuePoint(new PointObj(src.X - (currentSize - j-1) * TileSize, 
                        src.Y + i * TileSize, 0), -1);
                }
            }
            #endregion

            valuePoint[,] obstacleMap = getObstacleWorldMap();
            foreach (valuePoint[,] oc in octancts)
            {
                fillOctanct(obstacleMap, oc);
            }
            return octancts;

        }

        /// <summary>
        /// Fills octanct with values from obstacle map. 
        /// Values outside of map bounds are not changed.
        /// </summary>
        /// <param name="obstacleMap"></param>
        /// <param name="octanct"></param>
        void fillOctanct(valuePoint[,] obstacleMap, valuePoint[,] octanct)
        {
            int edgeXnear = -MyWidth / 2 * TileSize, edgeXFar = edgeXnear * -1;
            int edgeYnear = -MyHeight / 2 * TileSize, edgeYFar = edgeXnear * -1;

            foreach (valuePoint vp in octanct)
            {
                if (vp.p.X >= edgeXnear && vp.p.X < edgeXFar && vp.p.Y >= edgeYnear && vp.p.Y < edgeYFar)//is within map bounds
                    vp.value = obstacleMap[(int)vp.p.Y / TileSize + MyHeight / 2, (int)vp.p.X / TileSize + MyWidth / 2].value;
            }
        }

        /// <summary>
        /// Creates array representation of map for shadowtracing purposes
        /// 0: empty tile, with low wall, with low block
        /// 1: tile with highblock
        /// 2: tile with horizontal High Wall only
        /// 3: tile with vertical High Wall only
        /// 4: tile with horizontal and vertical High Walls
        /// </summary>
        /// <returns> Array representation of map</returns>
        public valuePoint[,] getObstacleWorldMap()
        {
            valuePoint[,] obstacleWM = new valuePoint[MyHeight, MyWidth];
            for (int i = 0; i < MyHeight; i++)
            {
                for (int j = 0; j < MyWidth; j++)
                    obstacleWM[i, j] = new valuePoint(new PointObj((j - MyWidth / 2) * TileSize, 
                        (i - MyHeight / 2) * TileSize, 0), 0);
            }

            IPoint obstacleSrc;
            Common.planeOrientation or;
            valuePoint current;

            List<HighWall> hWalls = getHighWalls();
            foreach (HighWall hw in hWalls)
            {
                obstacleSrc = hw.MyOrigin;
                or = (Common.planeOrientation)hw.Orientation;
                current = obstacleWM[(int)obstacleSrc.Y / TileSize + MyHeight / 2,(int)obstacleSrc.X / TileSize + MyWidth / 2];
                if (or == Common.planeOrientation.X)//Vertical wall
                {
                    if (current.value == 0)
                        current.value = 3;
                    else if (current.value == 2)//has horizontal wall, so assign both
                        current.value = 4;
                }
                else if (or == Common.planeOrientation.Y)//Horizontal wall
                {
                    if (current.value == 0)
                        current.value = 2;
                    else if (current.value == 3)//has vertical wall, so assign both
                        current.value = 4;
                }
            }

            List<HighBlock> blocks = getHighBlocks();
            foreach (HighBlock hb in blocks)
            {
                obstacleSrc = hb.MyOrigin;
                current = obstacleWM[(int)obstacleSrc.Y / TileSize + MyHeight / 2, (int)obstacleSrc.X / TileSize + MyWidth / 2];
                current.value = 1;
            }
            return obstacleWM;
        }

        /// <summary>
        /// Cuts off original, starting from [0,0], ending at [row-1,col-1]
        /// </summary>
        /// <param name="original"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>clipped octanct</returns>
        valuePoint[,] clipObstacleOctanct(valuePoint[,] original, int row, int col,bool rowEnd=true,bool colEnd=true)
        {
            int orXLength = original.GetLength(1), orYLength = original.GetLength(0);
            valuePoint[,] newOctanct=new valuePoint[rowEnd?orYLength - row:row,colEnd?orXLength - col:col];
            if (rowEnd)
            {
                if (colEnd)//Send the last orXLength Columns,last orYLength rows
                {
                    for (int i = row; i < orYLength; i++)
                    {
                        for (int j = col; j < orXLength; j++)
                            newOctanct[i - row, j - col] = original[i, j];
                    }
                }
                else//send the first orXLength-col columns, last orYLength rows
                {
                    for (int i = row; i < orYLength; i++)
                    {
                        for (int j = 0; j < col; j++)
                            newOctanct[i - row, j] = original[i, j];
                    }
                }
            }
            else
            {
                if (colEnd)//Send the last orXLength Columns,first orYLength rows
                {
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = col; j < orXLength; j++)
                            newOctanct[i, j - col] = original[i, j];
                    }
                }
                else//send the first orXLength-col columns, first orYLength rows
                {
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                            newOctanct[i, j] = original[i, j];
                    }
                }
            }

            return newOctanct;
        }

        /// <summary>
        /// Joins the base of an shade octanct to a piece got from recursion
        /// </summary>
        /// <param name="baseOctanct"></param>
        /// <param name="recursionPart"></param>
        /// <returns></returns>
        valuePoint[,] joinShadeOctanct(valuePoint[,] baseOctanct, valuePoint[,] recursionPart,
            bool rowEnd=true,bool colEnd=true)
        {
            int xSizeSmall = recursionPart.GetLength(1), ySizeSmall = recursionPart.GetLength(0);
            int xSizeBig = baseOctanct.GetLength(1), ySizeBig = baseOctanct.GetLength(0);
            if (rowEnd)
            {
                if (colEnd)//Overwrite the last orXLength Columns,last orYLength rows
                {
                    for (int i = 0; i < ySizeSmall; i++)
                    {
                        for (int j = 0; j < xSizeSmall; j++)
                        {
                            if (recursionPart[i, j].value == 1)
                                baseOctanct[ySizeBig - ySizeSmall + i, xSizeBig - xSizeSmall + j].value = 1;
                        }
                    }
                }
                else//Overwrite the first orXLength-col columns, last orYLength rows
                {
                    for (int i = 0; i < ySizeSmall; i++)
                    {
                        for (int j = 0; j < xSizeSmall; j++)
                        {
                            if (recursionPart[i, j].value == 1)
                                baseOctanct[ySizeBig - ySizeSmall + i,j].value = 1;
                        }
                    }
                }
            }
            else
            {
                if (colEnd)//Send the last orXLength Columns,first orYLength rows
                {
                    for (int i = 0; i < ySizeSmall; i++)
                    {
                        for (int j = 0; j < xSizeSmall; j++)
                        {
                            if (recursionPart[i, j].value == 1)
                                baseOctanct[i, xSizeBig - xSizeSmall + j].value = 1;
                        }
                    }
                }
                else//send the first orXLength-col columns, first orYLength rows
                {
                    for (int i = 0; i < ySizeSmall; i++)
                    {
                        for (int j = 0; j < xSizeSmall; j++)
                        {
                            if (recursionPart[i, j].value == 1)
                                baseOctanct[i,j].value = 1;
                        }
                    }
                }
            }
            
            return baseOctanct;
        }

        /// <summary>
        /// RECURSION BASED 
        /// Creates a shade map for the octanct, based on obstacle map
        /// -1: Not available
        /// 0: not visible
        /// 1: visible
        /// </summary>
        /// <param name="obsOctanct">obstacle map</param>
        /// <param name="octanctNumber"></param>
        /// <param name="src">src point</param>
        /// <param name="slope1">leave default</param>
        /// <param name="slope2">leave default</param>
        /// <returns></returns>
        private valuePoint[,] getShadesInOctanct(valuePoint[,] obsOctanct, int octanctNumber, IPoint src,
            Slope slope1 = null, Slope slope2 = null)
        {
            //Init shade map
            int orXLength = obsOctanct.GetLength(1), orYLength = obsOctanct.GetLength(0);
            double small = 0.01f;
            valuePoint[,] shadeMap = new valuePoint[orYLength, orXLength];
            for (int i = 0; i < orYLength; i++)
            {
                for (int j = 0; j < orXLength; j++)
                {
                    shadeMap[i, j] = new valuePoint(obsOctanct[i, j].p, -1);
                }
            }

            double previousObstacle = 0;
            bool obstacleGroupBefore = false, obstaclesInThisLine = false,
                firstInLine=true,callRecursion=false;
            double obstacleValue;
            double begSlope, endSlope, currentSlope, recursionBegSlope, recursionEndSlope;

            switch (octanctNumber)
            {
                case 0://Octanct 1.
                    begSlope = slope1 != null ? slope1.value : -1;
                    endSlope = slope2 != null ? slope2.value : 0;
                    #region OCTANCT 1
                    for (int i = 0; i < orYLength && !obstaclesInThisLine; i++)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int j = 0; j < orXLength; j++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getInvSlope(getTile(src).getCenter(), 
                                    getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope > begSlope && currentSlope <= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        if (previousObstacle==1)//Block
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(), 
                                                getTile(obsOctanct[i, j].p).topLeft())-small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 2 || previousObstacle==4)//Hor wall or both walls
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 3)//set beg to topLeft of previous tile
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(),
                                               getTile(obsOctanct[i, j-1].p).topLeft()) - small;
                                            callRecursion = true;
                                        }
                                        shadeMap[i, j].value = 1;
                                        firstInLine=false;
                                    }
                                    else if (obstacleValue == 1||obstacleValue==2||
                                        obstacleValue==3||obstacleValue==4)
                                        //Found obstacle
                                    {
                                        if (previousObstacle==0)//Previous was a not obstacle so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                recursionEndSlope = getInvSlope(getTile(src).getCenter(),
                                                    getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                                recursionBegSlope = begSlope-small;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, i + 1, 0), octanctNumber,
                                                    src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                                                callRecursion = false;
                                            }
                                        }
                                        if (obstacleValue == 3)
                                            shadeMap[i, j].value = 1;

                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)//Recursion not called because it ended on free tile
                        {
                            recursionEndSlope = endSlope + small;
                            recursionBegSlope = begSlope - small;
                            //do recursion
                            joinShadeOctanct(shadeMap,
                                getShadesInOctanct(clipObstacleOctanct(obsOctanct, i + 1, 0), octanctNumber,
                                src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                        }

                    }
                    #endregion
                    break;
                case 1://Octanct 2.
                    begSlope = slope1 != null ? slope1.value : 0;
                    endSlope = slope2 != null ? slope2.value : 1;
                    #region OCTANCT 2
                    for (int i = 0; i < orYLength && !obstaclesInThisLine; i++)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int j = 0; j < orXLength; j++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getInvSlope(getTile(src).getCenter(), 
                                    getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope > begSlope && currentSlope <= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                        if (previousObstacle==1 || 
                                            previousObstacle == 2 || previousObstacle == 4)//obstacle before
                                        {                                            
                                            begSlope = getInvSlope(getTile(src).getCenter(), 
                                                getTile(obsOctanct[i, j].p).bottomLeft())-small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 3)//set beg to topLeft of previous tile
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(),
                                               getTile(obsOctanct[i, j - 1].p).topLeft()) - small;
                                            callRecursion = true;
                                        }
                                    }                                    
                                    else if (obstacleValue == 1||obstacleValue==2||
                                        obstacleValue==3||obstacleValue==4)//Obstacle
                                    {
                                        if (previousObstacle==0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 3 || obstacleValue == 4)
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(),
                                                        getTile(obsOctanct[i, j].p).topLeft()) + small;
                                                else if (obstacleValue == 2)
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                                else
                                                    recursionEndSlope = endSlope;

                                                recursionBegSlope = begSlope;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, i + 1, 0), octanctNumber,
                                                    src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                                                callRecursion = false;
                                            }
                                            else//First in line, test for first hole special case
                                            {
                                                if (currentSlope != 0.0f)//there's a hole, so call recursion on it
                                                {
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(),
                                                    getTile(obsOctanct[i, j].p).topLeft()) + small;
                                                    recursionBegSlope = begSlope;

                                                    //do recursion
                                                    joinShadeOctanct(shadeMap,
                                                        getShadesInOctanct(clipObstacleOctanct(obsOctanct, i + 1, 0), octanctNumber,
                                                        src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                                                }

                                            }
                                        }
                                        if (obstacleValue == 3)
                                            shadeMap[i, j].value = 1;
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }

                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)//Recursion not called because it ended on free tile
                        {
                            recursionEndSlope = endSlope + small;
                            recursionBegSlope = begSlope - small;
                            //do recursion
                            joinShadeOctanct(shadeMap,
                                getShadesInOctanct(clipObstacleOctanct(obsOctanct, i + 1, 0), octanctNumber,
                                src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                        }
                    }
                    #endregion
                    break;
                case 2://Octanct 3.
                    begSlope = slope1 != null ? slope1.value : 0;
                    endSlope = slope2 != null ? slope2.value : 1;
                    #region OCTANCT 3
                    for (int j = 0; j < orXLength && !obstaclesInThisLine; j++)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int i = 0; i < orYLength; i++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getSlope(getTile(src).getCenter(), getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope > begSlope && currentSlope <= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        if (previousObstacle==1 || previousObstacle==3
                                            ||previousObstacle==4)//obstacle before,recalculate beggining slope, reset obstacle found,set visible
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(), 
                                                getTile(obsOctanct[i, j].p).bottomLeft())-small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 2)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(),
                                                getTile(obsOctanct[i-1, j].p).bottomLeft()) - small;
                                            callRecursion = true;
                                        }
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                    }
                                    else//Found high block,begin new scan
                                    {
                                        if (previousObstacle==0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 2 
                                                    || obstacleValue == 4)
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(),
                                                        getTile(obsOctanct[i, j].p).bottomRight()) + small;
                                                else if (obstacleValue == 3)
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                                else
                                                    recursionEndSlope = endSlope;
                                                recursionBegSlope = begSlope;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j + 1), octanctNumber,
                                                    src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                                                callRecursion = false;
                                            }
                                            else
                                            {
                                                if (currentSlope != 0)
                                                {
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(),
                                                    getTile(obsOctanct[i, j].p).bottomRight()) + small;
                                                    recursionBegSlope = begSlope;
                                                    //do recursion
                                                    joinShadeOctanct(shadeMap,
                                                        getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j + 1), octanctNumber,
                                                        src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                                                }
                                            }
                                        }
                                        if (obstacleValue == 2)
                                            shadeMap[i, j].value = 1;
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)//Recursion not called because it ended on free tile
                        {
                            recursionEndSlope = endSlope + small;
                            recursionBegSlope = begSlope - small;
                            //do recursion
                            joinShadeOctanct(shadeMap,
                                getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j+1), octanctNumber,
                                src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                        }
                    }
                    #endregion
                    break;
                case 3://Octanct 4.
                    begSlope = slope1 != null ? slope1.value : -1;
                    endSlope = slope2 != null ? slope2.value : 0;
                    #region OCTANCT 4
                    for (int j = 0; j < orXLength && !obstaclesInThisLine; j++)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int i = 0; i < orYLength; i++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getSlope(getTile(src).getCenter(), getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope > begSlope && currentSlope <= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                        if (previousObstacle==1)
                                        {                                            
                                            begSlope = getSlope(getTile(src).getCenter(), 
                                                getTile(obsOctanct[i, j].p).bottomRight())-small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 3 || previousObstacle == 4)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                            callRecursion = true;
                                        }else if(previousObstacle ==2)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(),
                                                getTile(obsOctanct[i-1, j].p).bottomLeft()) - small;
                                            callRecursion = true;
                                        }                                        
                                    }
                                    else//Found high block,begin new scan
                                    {
                                        if (previousObstacle==0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 2
                                                    || obstacleValue == 3 || obstacleValue == 4)
                                                {
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                                    recursionBegSlope = begSlope;
                                                    //do recursion
                                                    joinShadeOctanct(shadeMap,
                                                        getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j + 1), octanctNumber,
                                                        src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                                                    callRecursion = false;
                                                }
                                            }
                                        }
                                        if (obstacleValue == 2)
                                            shadeMap[i, j].value = 1;
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)//Recursion not called because it ended on free tile
                        {
                            recursionEndSlope = endSlope + small;
                            recursionBegSlope = begSlope - small;
                            //do recursion
                            joinShadeOctanct(shadeMap,
                                getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j + 1), octanctNumber,
                                src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                        }
                    }
                    #endregion
                    break;
                case 4://Octanct 5.
                    begSlope = slope1 != null ? slope1.value : 0;
                    endSlope = slope2 != null ? slope2.value : -1;
                    #region OCTANCT 5
                    for (int i = orYLength - 1; i >= 0 && !obstaclesInThisLine; i--)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int j = 0; j < orXLength; j++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getInvSlope(getTile(src).getCenter(), getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope < begSlope && currentSlope >= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        firstInLine = false;
                                        shadeMap[i, j].value = 1;
                                        if (previousObstacle==1)
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(), 
                                                getTile(obsOctanct[i, j].p).topLeft())+small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 2 || previousObstacle == 4)
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 3)
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(),
                                                getTile(obsOctanct[i, j-1].p).topLeft()) + small;
                                            callRecursion = true;
                                        }
                                    }
                                    else//Found obstacle
                                    {
                                        if (previousObstacle==0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 2 || obstacleValue == 3 ||
                                                    obstacleValue == 4)
                                                {
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                                }
                                                else
                                                    recursionEndSlope = endSlope;

                                                recursionBegSlope = begSlope;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, i, 0, false, true), octanctNumber,
                                                    src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), false, true);
                                                callRecursion = false;
                                                
                                            }
                                            else
                                            {
                                                if (currentSlope != 0)
                                                {
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(),
                                                    getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                                    recursionBegSlope = begSlope;
                                                    //do recursion
                                                    joinShadeOctanct(shadeMap,
                                                        getShadesInOctanct(clipObstacleOctanct(obsOctanct, i, 0, false, true), octanctNumber,
                                                        src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), false, true);

                                                }
                                            }
                                        }
                                        if (obstacleValue == 2 || obstacleValue == 3 || obstacleValue == 4)
                                            shadeMap[i, j].value = 1; 
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (callRecursion && obstaclesInThisLine)
                        {
                            recursionEndSlope = endSlope - small;
                            recursionBegSlope = begSlope + small;
                            //do recursion
                            joinShadeOctanct(shadeMap, getShadesInOctanct(clipObstacleOctanct(obsOctanct,
                                i, 0, false, true), octanctNumber, src, new Slope(recursionBegSlope),
                                new Slope(recursionEndSlope)), false, true);
                        }                                            
                    }
                    #endregion
                    break;
                case 5://Octanct 6.
                    begSlope = slope1 != null ? slope1.value : 1;
                    endSlope = slope2 != null ? slope2.value : 0;
                    #region OCTANCT 6
                    for (int i = orYLength - 1; i >= 0 && !obstaclesInThisLine; i--)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int j = 0; j < orXLength; j++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getInvSlope(getTile(src).getCenter(), getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope <= begSlope && currentSlope >= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                        if (previousObstacle==1 || previousObstacle==2||previousObstacle==4)
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(), 
                                                getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 3)
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(),
                                                getTile(obsOctanct[i, j-1].p).bottomLeft()) + small;
                                            callRecursion = true;
                                        }
                                    }
                                    else//Found obstacle
                                    {
                                        if (previousObstacle==0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 3 || obstacleValue == 4)
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(),
                                                        getTile(obsOctanct[i, j].p).topLeft()) - small;
                                                else if (obstacleValue == 2)
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                                else
                                                    recursionEndSlope = endSlope;
                                                recursionBegSlope = begSlope;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, i, 0, false, true), octanctNumber,
                                                    src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), false, true);
                                                callRecursion = false;
                                            }
                                        }
                                        if (obstacleValue == 2 || obstacleValue == 3 || obstacleValue == 4)
                                            shadeMap[i, j].value = 1; 
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)
                        {
                            recursionEndSlope = endSlope - small;
                            recursionBegSlope = begSlope + small;
                            //do recursion
                            joinShadeOctanct(shadeMap, getShadesInOctanct(clipObstacleOctanct(obsOctanct, i, 0, false, true), octanctNumber,
                                             src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), false, true);
                        }

                    }
                    #endregion
                    break;
                case 6://Octanct 7.
                    begSlope = slope1 != null ? slope1.value : 1;
                    endSlope = slope2 != null ? slope2.value : 0;
                    #region OCTANCT 7
                    for (int j = orXLength - 1; j >= 0 && !obstaclesInThisLine; j--)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int i = 0; i < orYLength; i++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getSlope(getTile(src).getCenter(), getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope < begSlope && currentSlope >= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                        if (previousObstacle==1||previousObstacle==3||previousObstacle==4)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(), 
                                                getTile(obsOctanct[i, j].p).bottomLeft())+small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 2)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(),
                                                getTile(obsOctanct[i-1, j].p).bottomLeft()) + small;
                                            callRecursion = true;
                                        }
                                    }
                                    else//Found obstacle
                                    {
                                        if (previousObstacle==0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 2 || obstacleValue == 4)
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(),
                                                        getTile(obsOctanct[i, j].p).bottomRight()) - small;
                                                else if (obstacleValue == 3)
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                                else
                                                    recursionEndSlope = endSlope;
                                                recursionBegSlope = begSlope;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j,true,false), octanctNumber,
                                                    src, new Slope(recursionBegSlope), new Slope(recursionEndSlope))
                                                    ,true,false);
                                                callRecursion = false;
                                            }
                                        }
                                        if (obstacleValue == 2 || obstacleValue == 3 || obstacleValue == 4)
                                            shadeMap[i, j].value = 1; 
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)
                        {
                            recursionEndSlope = endSlope - small;
                            recursionBegSlope = begSlope + small;
                            joinShadeOctanct(shadeMap, getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0,
                                                    j,true,false), octanctNumber,
                                             src, new Slope(recursionBegSlope), new Slope(recursionEndSlope))
                                             ,true,false);
                        }

                    }
                    #endregion
                    break;
                case 7://Octanct 8.
                    begSlope = slope1 != null ? slope1.value : 0;
                    endSlope = slope2 != null ? slope2.value : -1;
                    #region OCTANCT 8
                    for (int j = orXLength - 1; j >= 0 && !obstaclesInThisLine; j--)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int i = 0; i < orYLength; i++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getSlope(getTile(src).getCenter(), getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope < begSlope && currentSlope >= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                        if (previousObstacle==1)//obstacle before,recalculate beggining slope, reset obstacle found,set visible
                                        {                                            
                                            begSlope = getSlope(getTile(src).getCenter(),
                                                getTile(obsOctanct[i, j].p).bottomRight())+small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 3 || previousObstacle == 4)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 2)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(),
                                                getTile(obsOctanct[i-1, j].p).bottomRight()) + small;
                                            callRecursion = true;
                                        }
                                    }
                                    else//Found obstacle
                                    {
                                        if (previousObstacle==0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 2 || obstacleValue == 3
                                                    || obstacleValue == 4)
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                                else
                                                    recursionEndSlope = endSlope;
                                                recursionBegSlope = begSlope;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j, true, false), octanctNumber,
                                                    src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), true, false);
                                                callRecursion = false;
                                            }
                                            else
                                            {
                                                if (currentSlope != 0)
                                                {
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(),
                                                    getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                                    recursionBegSlope = begSlope;
                                                    //do recursion
                                                    joinShadeOctanct(shadeMap,
                                                        getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j, true, false), octanctNumber,
                                                        src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), true, false);
                                                
                                                }
                                            }
                                        }
                                        if (obstacleValue == 2 || obstacleValue == 3 || obstacleValue == 4)
                                            shadeMap[i, j].value = 1; 
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)
                        {
                            recursionEndSlope = endSlope - small;
                            recursionBegSlope = begSlope + small;
                            //do recursion
                            joinShadeOctanct(shadeMap,
                                getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j, true, false), octanctNumber,
                                src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), true, false);
                        }                    
                    }
                    #endregion
                    break;
            }
            return shadeMap;
        }

        /// <summary>
        /// RECURSION BASED 
        /// Creates a shade map for the octanct, based on obstacle map
        /// -1: Not available
        /// 0: not visible
        /// 1: visible
        /// </summary>
        /// <param name="obsOctanct">obstacle map</param>
        /// <param name="octanctNumber"></param>
        /// <param name="src">src point</param>
        /// <param name="slope1">leave default</param>
        /// <param name="slope2">leave default</param>
        /// <returns></returns>
        private valuePoint[,] getShadesInOctanct(valuePoint[,] obsOctanct, int octanctNumber, IPoint src,
            Common.tileSide direction,Slope slope1 = null, Slope slope2 = null)
        {
            //Init shade map
            int orXLength = obsOctanct.GetLength(1), orYLength = obsOctanct.GetLength(0);
            double small = 0.01f;
            valuePoint[,] shadeMap = new valuePoint[orYLength, orXLength];
            for (int i = 0; i < orYLength; i++)
            {
                for (int j = 0; j < orXLength; j++)
                {
                    shadeMap[i, j] = new valuePoint(obsOctanct[i, j].p, -1);
                }
            }

            double previousObstacle = 0;
            bool obstacleGroupBefore = false, obstaclesInThisLine = false,
                firstInLine = true, callRecursion = false;
            double obstacleValue;
            double begSlope, endSlope, currentSlope, recursionBegSlope, recursionEndSlope;

            switch (octanctNumber)
            {
                case 0://Octanct 1.
                    begSlope = slope1 != null ? slope1.value : -1;
                    endSlope = slope2 != null ? slope2.value : 0;
                    #region OCTANCT 1
                    for (int i = 0; i < orYLength && !obstaclesInThisLine; i++)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int j = 0; j < orXLength; j++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getInvSlope(getTile(src).getCenter(direction),
                                    getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope > begSlope && currentSlope <= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        if (previousObstacle == 1)//Block
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).topLeft()) - small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 2 || previousObstacle == 4)//Hor wall or both walls
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 3)//set beg to topLeft of previous tile
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(direction),
                                               getTile(obsOctanct[i, j - 1].p).topLeft()) - small;
                                            callRecursion = true;
                                        }
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                    }
                                    else if (obstacleValue == 1 || obstacleValue == 2 ||
                                        obstacleValue == 3 || obstacleValue == 4)
                                    //Found obstacle
                                    {
                                        if (previousObstacle == 0)//Previous was a not obstacle so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                recursionEndSlope = getInvSlope(getTile(src).getCenter(direction),
                                                    getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                                recursionBegSlope = begSlope - small;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, i + 1, 0), octanctNumber,
                                                    src,direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                                                callRecursion = false;
                                            }
                                        }
                                        if (obstacleValue == 3)
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).topLeft())-small;
                                            callRecursion = true;
                                            shadeMap[i, j].value = 1;
                                        }

                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)//Recursion not called because it ended on free tile
                        {
                            recursionEndSlope = endSlope + small;
                            recursionBegSlope = begSlope - small;
                            //do recursion
                            joinShadeOctanct(shadeMap,
                                getShadesInOctanct(clipObstacleOctanct(obsOctanct, i + 1, 0), octanctNumber,
                                src,direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                        }

                    }
                    #endregion
                    break;
                case 1://Octanct 2.
                    begSlope = slope1 != null ? slope1.value : 0;
                    endSlope = slope2 != null ? slope2.value : 1;
                    #region OCTANCT 2
                    for (int i = 0; i < orYLength && !obstaclesInThisLine; i++)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int j = 0; j < orXLength; j++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getInvSlope(getTile(src).getCenter(direction),
                                    getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope > begSlope && currentSlope <= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                        if (previousObstacle == 1 ||
                                            previousObstacle == 2 || previousObstacle == 4)//obstacle before
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 3)//set beg to topLeft of previous tile
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(direction),
                                               getTile(obsOctanct[i, j - 1].p).topLeft()) - small;
                                            callRecursion = true;
                                        }
                                    }
                                    else if (obstacleValue == 1 || obstacleValue == 2 ||
                                        obstacleValue == 3 || obstacleValue == 4)//Obstacle
                                    {
                                        if (previousObstacle == 0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 3 || obstacleValue == 4)
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(direction),
                                                        getTile(obsOctanct[i, j].p).topLeft()) + small;
                                                else if (obstacleValue == 2)
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(direction),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                                else
                                                    recursionEndSlope = endSlope;

                                                recursionBegSlope = begSlope;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, i + 1, 0), octanctNumber,
                                                    src,direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                                                callRecursion = false;
                                            }
                                            else//First in line, test for first hole special case
                                            {
                                                if (currentSlope != 0.0f)//there's a hole, so call recursion on it
                                                {
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(direction),
                                                    getTile(obsOctanct[i, j].p).topLeft()) + small;
                                                    recursionBegSlope = begSlope;

                                                    //do recursion
                                                    joinShadeOctanct(shadeMap,
                                                        getShadesInOctanct(clipObstacleOctanct(obsOctanct, i + 1, 0), octanctNumber,
                                                        src, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                                                }

                                            }
                                        }
                                        if (obstacleValue == 3)
                                            shadeMap[i, j].value = 1;
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }

                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)//Recursion not called because it ended on free tile
                        {
                            recursionEndSlope = endSlope + small;
                            recursionBegSlope = begSlope - small;
                            //do recursion
                            joinShadeOctanct(shadeMap,
                                getShadesInOctanct(clipObstacleOctanct(obsOctanct, i + 1, 0), octanctNumber,
                                src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                        }
                    }
                    #endregion
                    break;
                case 2://Octanct 3.
                    begSlope = slope1 != null ? slope1.value : 0;
                    endSlope = slope2 != null ? slope2.value : 1;
                    #region OCTANCT 3
                    for (int j = 0; j < orXLength && !obstaclesInThisLine; j++)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int i = 0; i < orYLength; i++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getSlope(getTile(src).getCenter(direction), getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope > begSlope && currentSlope <= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        if (previousObstacle == 1 || previousObstacle == 3
                                            || previousObstacle == 4)//obstacle before,recalculate beggining slope, reset obstacle found,set visible
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 2)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i - 1, j].p).bottomLeft()) - small;
                                            callRecursion = true;
                                        }
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                    }
                                    else//Found high block,begin new scan
                                    {
                                        if (previousObstacle == 0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 2
                                                    || obstacleValue == 4)
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(direction),
                                                        getTile(obsOctanct[i, j].p).bottomRight()) + small;
                                                else if (obstacleValue == 3)
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(direction),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                                else
                                                    recursionEndSlope = endSlope;
                                                recursionBegSlope = begSlope;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j + 1), octanctNumber,
                                                    src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                                                callRecursion = false;
                                            }
                                            else
                                            {
                                                if (currentSlope != 0)
                                                {
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(direction),
                                                    getTile(obsOctanct[i, j].p).bottomRight()) + small;
                                                    recursionBegSlope = begSlope;
                                                    //do recursion
                                                    joinShadeOctanct(shadeMap,
                                                        getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j + 1), octanctNumber,
                                                        src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                                                }
                                            }
                                        }
                                        if (obstacleValue == 2)
                                            shadeMap[i, j].value = 1;
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)//Recursion not called because it ended on free tile
                        {
                            recursionEndSlope = endSlope + small;
                            recursionBegSlope = begSlope - small;
                            //do recursion
                            joinShadeOctanct(shadeMap,
                                getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j + 1), octanctNumber,
                                src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                        }
                    }
                    #endregion
                    break;
                case 3://Octanct 4.
                    begSlope = slope1 != null ? slope1.value : -1;
                    endSlope = slope2 != null ? slope2.value : 0;
                    #region OCTANCT 4
                    for (int j = 0; j < orXLength && !obstaclesInThisLine; j++)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int i = 0; i < orYLength; i++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getSlope(getTile(src).getCenter(direction), getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope > begSlope && currentSlope <= endSlope
                                    &&!src.equals(obsOctanct[i,j].p))
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                        if (previousObstacle == 1)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).bottomRight()) - small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 3 || previousObstacle == 4)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 2)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i - 1, j].p).bottomLeft()) - small;
                                            callRecursion = true;
                                        }
                                    }
                                    else//Found high block,begin new scan
                                    {
                                        if (previousObstacle == 0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 2
                                                    || obstacleValue == 3 || obstacleValue == 4)
                                                {
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(direction),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                                    recursionBegSlope = begSlope;
                                                    //do recursion
                                                    joinShadeOctanct(shadeMap,
                                                        getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j + 1), octanctNumber,
                                                        src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                                                    callRecursion = false;
                                                }
                                            }
                                        }
                                        if (obstacleValue == 2)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(direction),
                                               getTile(obsOctanct[i, j].p).bottomRight()) - small;
                                            callRecursion = true;
                                            shadeMap[i, j].value = 1;
                                        }
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)//Recursion not called because it ended on free tile
                        {
                            recursionEndSlope = endSlope + small;
                            recursionBegSlope = begSlope - small;
                            //do recursion
                            joinShadeOctanct(shadeMap,
                                getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j + 1), octanctNumber,
                                src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)));
                        }
                    }
                    #endregion
                    break;
                case 4://Octanct 5.
                    begSlope = slope1 != null ? slope1.value : 0;
                    endSlope = slope2 != null ? slope2.value : -1;
                    #region OCTANCT 5
                    for (int i = orYLength - 1; i >= 0 && !obstaclesInThisLine; i--)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int j = 0; j < orXLength; j++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getInvSlope(getTile(src).getCenter(direction), getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope < begSlope && currentSlope >= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        firstInLine = false;
                                        shadeMap[i, j].value = 1;
                                        if (previousObstacle == 1)
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).topLeft()) + small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 2 || previousObstacle == 4)
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 3)
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j - 1].p).topLeft()) + small;
                                            callRecursion = true;
                                        }
                                    }
                                    else//Found obstacle
                                    {
                                        if (previousObstacle == 0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 2 || obstacleValue == 3 ||
                                                    obstacleValue == 4)
                                                {
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(direction),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                                }
                                                else
                                                    recursionEndSlope = endSlope;

                                                recursionBegSlope = begSlope;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, i, 0, false, true), octanctNumber,
                                                    src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), false, true);
                                                callRecursion = false;

                                            }
                                            else
                                            {
                                                if (currentSlope != 0)
                                                {
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(direction),
                                                    getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                                    recursionBegSlope = begSlope;
                                                    //do recursion
                                                    joinShadeOctanct(shadeMap,
                                                        getShadesInOctanct(clipObstacleOctanct(obsOctanct, i, 0, false, true), octanctNumber,
                                                        src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), false, true);

                                                }
                                            }
                                        }
                                        if (obstacleValue == 2 || obstacleValue == 3 || obstacleValue == 4)
                                            shadeMap[i, j].value = 1;
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (callRecursion && obstaclesInThisLine)
                        {
                            recursionEndSlope = endSlope - small;
                            recursionBegSlope = begSlope + small;
                            //do recursion
                            joinShadeOctanct(shadeMap, getShadesInOctanct(clipObstacleOctanct(obsOctanct,
                                i, 0, false, true), octanctNumber, src, direction, new Slope(recursionBegSlope),
                                new Slope(recursionEndSlope)), false, true);
                        }
                    }
                    #endregion
                    break;
                case 5://Octanct 6.
                    begSlope = slope1 != null ? slope1.value : 1;
                    endSlope = slope2 != null ? slope2.value : 0;
                    #region OCTANCT 6
                    for (int i = orYLength - 1; i >= 0 && !obstaclesInThisLine; i--)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int j = 0; j < orXLength; j++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getInvSlope(getTile(src).getCenter(direction), getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope <= begSlope && currentSlope >= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                        if (previousObstacle == 1 || previousObstacle == 2 || previousObstacle == 4)
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 3)
                                        {
                                            begSlope = getInvSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j - 1].p).bottomLeft()) + small;
                                            callRecursion = true;
                                        }
                                    }
                                    else//Found obstacle
                                    {
                                        if (previousObstacle == 0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 3 || obstacleValue == 4)
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(direction),
                                                        getTile(obsOctanct[i, j].p).topLeft()) - small;
                                                else if (obstacleValue == 2)
                                                    recursionEndSlope = getInvSlope(getTile(src).getCenter(direction),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                                else
                                                    recursionEndSlope = endSlope;
                                                recursionBegSlope = begSlope;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, i, 0, false, true), octanctNumber,
                                                    src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), false, true);
                                                callRecursion = false;
                                            }
                                        }
                                        if (obstacleValue == 2 || obstacleValue == 3 || obstacleValue == 4)
                                        {
                                            if (obstacleValue == 3)
                                            {
                                                begSlope = getInvSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                                callRecursion = true;
                                            }
                                            shadeMap[i, j].value = 1;
                                        }
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)
                        {
                            recursionEndSlope = endSlope - small;
                            recursionBegSlope = begSlope + small;
                            //do recursion
                            joinShadeOctanct(shadeMap, getShadesInOctanct(clipObstacleOctanct(obsOctanct, i, 0, false, true), octanctNumber,
                                             src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), false, true);
                        }

                    }
                    #endregion
                    break;
                case 6://Octanct 7.
                    begSlope = slope1 != null ? slope1.value : 1;
                    endSlope = slope2 != null ? slope2.value : 0;
                    #region OCTANCT 7
                    for (int j = orXLength - 1; j >= 0 && !obstaclesInThisLine; j--)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int i = 0; i < orYLength; i++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getSlope(getTile(src).getCenter(direction), getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope <= begSlope && currentSlope >= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                        if (previousObstacle == 1 || previousObstacle == 3 || previousObstacle == 4)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 2)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i - 1, j].p).bottomLeft()) + small;
                                            callRecursion = true;
                                        }
                                    }
                                    else//Found obstacle
                                    {
                                        if (previousObstacle == 0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 2 || obstacleValue == 4)
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(direction),
                                                        getTile(obsOctanct[i, j].p).bottomRight()) - small;
                                                else if (obstacleValue == 3)
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(direction),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                                else
                                                    recursionEndSlope = endSlope;
                                                recursionBegSlope = begSlope;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j, true, false), octanctNumber,
                                                    src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope))
                                                    , true, false);
                                                callRecursion = false;
                                            }
                                        }
                                        if (obstacleValue == 2 || obstacleValue == 3 || obstacleValue == 4)
                                        {
                                            if (obstacleValue == 2)
                                            {
                                                begSlope = getSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                                callRecursion = true;
                                            }
                                            shadeMap[i, j].value = 1;
                                        }
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)
                        {
                            recursionEndSlope = endSlope - small;
                            recursionBegSlope = begSlope + small;
                            joinShadeOctanct(shadeMap, getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0,
                                                    j, true, false), octanctNumber,
                                             src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope))
                                             , true, false);
                        }

                    }
                    #endregion
                    break;
                case 7://Octanct 8.
                    begSlope = slope1 != null ? slope1.value : 0;
                    endSlope = slope2 != null ? slope2.value : -1;
                    #region OCTANCT 8
                    for (int j = orXLength - 1; j >= 0 && !obstaclesInThisLine; j--)
                    {
                        firstInLine = true;
                        obstaclesInThisLine = false;
                        obstacleGroupBefore = false;
                        previousObstacle = 0;
                        callRecursion = false;
                        for (int i = 0; i < orYLength; i++)
                        {
                            //Clip to map
                            if (obsOctanct[i, j].value > -1)
                            {
                                //Clip to slopes
                                currentSlope = getSlope(getTile(src).getCenter(direction), getTile(obsOctanct[i, j].p).getCenter());
                                if (currentSlope <= begSlope && currentSlope >= endSlope)
                                {
                                    //Get value from obstacle map
                                    obstacleValue = obsOctanct[i, j].value;
                                    if (obstacleValue == 0)
                                    {
                                        shadeMap[i, j].value = 1;
                                        firstInLine = false;
                                        if (previousObstacle == 1)//obstacle before,recalculate beggining slope, reset obstacle found,set visible
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).bottomRight()) + small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 3 || previousObstacle == 4)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i, j].p).bottomLeft()) + small;
                                            callRecursion = true;
                                        }
                                        else if (previousObstacle == 2)
                                        {
                                            begSlope = getSlope(getTile(src).getCenter(direction),
                                                getTile(obsOctanct[i - 1, j].p).bottomRight()) + small;
                                            callRecursion = true;
                                        }
                                    }
                                    else//Found obstacle
                                    {
                                        if (previousObstacle == 0)//Previous was a free tile so,
                                        //change end slope for recursion,call recursion
                                        {
                                            if (!firstInLine)
                                            {
                                                if (obstacleValue == 1 || obstacleValue == 2 || obstacleValue == 3
                                                    || obstacleValue == 4)
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(direction),
                                                        getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                                else
                                                    recursionEndSlope = endSlope;
                                                recursionBegSlope = begSlope;
                                                //do recursion
                                                joinShadeOctanct(shadeMap,
                                                    getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j, true, false), octanctNumber,
                                                    src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), true, false);
                                                callRecursion = false;
                                            }
                                            else
                                            {
                                                /*
                                                if (currentSlope != 0)
                                                {
                                                    recursionEndSlope = getSlope(getTile(src).getCenter(direction),
                                                    getTile(obsOctanct[i, j].p).bottomLeft()) - small;
                                                    recursionBegSlope = begSlope;
                                                    //do recursion
                                                    joinShadeOctanct(shadeMap,
                                                        getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j, true, false), octanctNumber,
                                                        src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), true, false);

                                                }*/
                                            }
                                        }
                                        if (obstacleValue == 2 || obstacleValue == 3 || obstacleValue == 4)
                                            shadeMap[i, j].value = 1;
                                        obstacleGroupBefore = true;
                                        obstaclesInThisLine = true;
                                    }
                                    previousObstacle = obstacleValue;
                                }
                            }
                        }
                        if (obstaclesInThisLine && callRecursion)
                        {
                            recursionEndSlope = endSlope - small;
                            recursionBegSlope = begSlope + small;
                            //do recursion
                            joinShadeOctanct(shadeMap,
                                getShadesInOctanct(clipObstacleOctanct(obsOctanct, 0, j, true, false), octanctNumber,
                                src, direction, new Slope(recursionBegSlope), new Slope(recursionEndSlope)), true, false);
                        }
                    }
                    #endregion
                    break;
            }
            return shadeMap;
        }

        enum enumQuadrant{q1,q2,q3,q4};

        /// <summary>
        /// Return the valuePoints surrounding src, null if outside of map
        /// </summary>
        /// <param name="src"></param>
        /// <param name="obstacleMap"></param>
        /// <returns></returns>
        valuePoint[] getSurrounders(IPoint src, valuePoint[,] obstacleMap)
        {
            valuePoint[] surrounders = new valuePoint[4];
            int xIndex = (int)src.X / TileSize + MyWidth / 2, yIndex = (int)src.Y / TileSize + MyHeight / 2, currentXIndex, currentYIndex;
            
            //Quadrant 1, tile itself
            currentXIndex = xIndex;
            currentYIndex = yIndex;
            if (currentXIndex >= 0 && currentXIndex < obstacleMap.GetLength(1)
                && currentYIndex >= 0 && currentYIndex < obstacleMap.GetLength(0))
            {
                surrounders[0] = obstacleMap[currentYIndex, currentXIndex];
            }
            else
                surrounders[0] = null;

            //Quadrant 2, i-1
            currentXIndex = xIndex;
            currentYIndex = yIndex-1;
            if (currentXIndex >= 0 && currentXIndex < obstacleMap.GetLength(1)
                && currentYIndex >= 0 && currentYIndex < obstacleMap.GetLength(0))
            {
                surrounders[1] = obstacleMap[currentYIndex, currentXIndex];
            }
            else
                surrounders[1] = null;

            //Quadrant 3, i-1,j-1
            currentXIndex = xIndex-1;
            currentYIndex = yIndex-1;
            if (currentXIndex >= 0 && currentXIndex < obstacleMap.GetLength(1)
                && currentYIndex >= 0 && currentYIndex < obstacleMap.GetLength(0))
            {
                surrounders[2] = obstacleMap[currentYIndex, currentXIndex];
            }
            else
                surrounders[2] = null;

            //Quadrant 4, i,j-1
            currentXIndex = xIndex - 1;
            currentYIndex = yIndex ;
            if (currentXIndex >= 0 && currentXIndex < obstacleMap.GetLength(1)
                && currentYIndex >= 0 && currentYIndex < obstacleMap.GetLength(0))
            {
                surrounders[3] = obstacleMap[currentYIndex, currentXIndex];
            }
            else
                surrounders[3] = null;

            return surrounders;

        }

        void doDiagonalShading(IPoint src, IPoint first, Slope begSlope, 
            Slope endSlope,enumQuadrant quadrant,valuePoint[,] shadeMap)
        {
            int startX = (int)first.X / TileSize + MyWidth / 2, startY = (int)first.Y / TileSize + MyHeight / 2;
            double currentSlope;
            switch (quadrant)
            {
                case enumQuadrant.q3:
                    for (int i = 0; i < startY; i++)
                    {
                        for (int j = 0; j < startX; j++)
                        {
                            currentSlope = getSlope(getTile(src).getCenter(),
                                getTile(shadeMap[i, j].p).getCenter());
                            if (currentSlope >= begSlope.value && currentSlope <= endSlope.value)
                                shadeMap[i, j].value = 0;
                        }
                    } break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Sets possible diagonal shaded tiles missed by shadowcasting
        /// </summary>
        /// <param name="obstacleMap"></param>
        /// <param name="shadeMap"></param>
        /// <param name="src"></param>
        private void setDiagonalShades(valuePoint[,] obstacleMap,valuePoint[,] shadeMap,IPoint src)
        {
            int xIndex = (int)src.X / TileSize + MyWidth / 2, yIndex = (int)src.Y / TileSize + MyHeight / 2,
                minIndex=Math.Min(xIndex,yIndex);
            valuePoint[] currSurrounders;
            bool doShading=false;
            double begSlope=1,endSlope=1;
            IPoint current;

            //Quadrant 3 diagonal
            #region QUADRANT 3 CASES
            for (int i = 0; i <=minIndex; i++)
            {
                current =obstacleMap[yIndex-i,xIndex-i].p;
                currSurrounders = getSurrounders(current, obstacleMap);

                #region CASES
                if (currSurrounders[0] != null && currSurrounders[1] != null && currSurrounders[2] != null
                    && currSurrounders[3] != null)
                {
                    if (currSurrounders[0].value == 3 && currSurrounders[1].value == 3)
                    {
                        doShading = true;
                        begSlope = getSlope(getTile(src).getCenter(), 
                            getTile(currSurrounders[0].p).topLeft());
                        endSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[1].p).bottomLeft());
                    }

                    if (currSurrounders[0].value == 2 && currSurrounders[1].value == 4)
                    {
                        doShading = true;
                        begSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[3].p).topLeft());
                        endSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[0].p).bottomRight());
                    }

                    if (currSurrounders[0].value == 2 && currSurrounders[1].value == 2)
                    {
                        doShading = true;
                        begSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[3].p).bottomLeft());
                        endSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[0].p).bottomRight());
                    }

                    if (currSurrounders[1].value == 4 && currSurrounders[3].value == 4)
                    {
                        doShading = true;
                        begSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[3].p).topLeft());
                        endSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[1].p).bottomRight());
                    }

                    if (currSurrounders[1].value == 4 && currSurrounders[3].value == 2)
                    {
                        doShading = true;
                        begSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[3].p).bottomLeft());
                        endSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[1].p).bottomRight());
                    }

                    if (currSurrounders[1].value == 3 && currSurrounders[3].value == 4)
                    {
                        doShading = true;
                        begSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[3].p).topLeft());
                        endSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[1].p).bottomLeft());
                    }

                    if (currSurrounders[1].value == 3 && currSurrounders[3].value == 2)
                    {
                        doShading = true;
                        begSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[3].p).bottomLeft());
                        endSlope = getSlope(getTile(src).getCenter(),
                            getTile(currSurrounders[1].p).bottomLeft());
                    }
                    if (doShading)
                    {
                        doDiagonalShading(src, current, new Slope(begSlope), new Slope(endSlope),
                            enumQuadrant.q3, shadeMap);
                        break;
                    }
                }
                else
                    break;
            #endregion
            }
            #endregion

        }

        /// <summary>
        /// Joins octanct to create a full shade map
        /// </summary>
        /// <param name="octancts"></param>
        /// <returns></returns>
        private valuePoint[,] joinOctancts(valuePoint[][,] octancts)
        {
            int xOne = octancts[7].GetLength(1), xTwo = MyWidth - xOne+1, 
                yOne = octancts[5].GetLength(0), yTwo = MyHeight - yOne+1;
            valuePoint[,] wholeMap = new valuePoint[MyHeight, MyWidth];
            #region CREATE QUADRANTS
            valuePoint[,] Quadrant4 = new valuePoint[yTwo, xOne];
            for (int i = 0; i < yTwo; i++)
            {
                for (int j = 0; j < xOne; j++)
                {
                    if (xOne - j > i)//Assign from octanct 8
                        Quadrant4[i, j] = octancts[7][i, j];
                    else//assign from octanct 1
                        Quadrant4[i, j] = octancts[0][i, j - (xOne - yTwo)];
                }
            }

            valuePoint[,] Quadrant1 = new valuePoint[yTwo, xTwo];
            for (int i = 0; i < yTwo; i++)
            {
                for (int j = 0; j < xTwo; j++)
                {
                    if (i >= j)//Assign from octanct 2
                        Quadrant1[i, j] = octancts[1][i, j];
                    else//assign from octanct 3
                        Quadrant1[i, j] = octancts[2][i, j];
                }
            }

            valuePoint[,] Quadrant2 = new valuePoint[yOne, xTwo];
            for (int i = 0; i < yOne; i++)
            {
                for (int j = 0; j < xTwo; j++)
                {
                    if (j >= yOne - i)//Assign from octanct 4
                        Quadrant2[i, j] = octancts[3][i - (yOne - xTwo), j];
                    else//assign from octanct 5
                        Quadrant2[i, j] = octancts[4][i, j];
                }
            }

            valuePoint[,] Quadrant3 = new valuePoint[yOne, xOne];
            for (int i = 0; i < yOne; i++)
            {
                for (int j = 0; j < xOne; j++)
                {
                    if (yOne - i >= xOne - j)//Assign from octanct 6
                        Quadrant3[i, j] = octancts[5][i, j + (yOne - xOne)];
                    else//assign from octanct 7
                        Quadrant3[i, j] = octancts[6][i - (yOne - xOne), j];
                }
            }
            #endregion

            #region JOIN QUADRANTS
            for (int i = 0; i < MyHeight; i++)
            {
                for (int j = 0; j < MyWidth; j++)
                {
                    if (i >= yOne)//Top side
                    {
                        if (j < xOne)//Left Quadrant 4
                            wholeMap[i, j] = Quadrant4[i - yOne+1, j];
                        else//Right Quadrant 1
                            wholeMap[i, j] = Quadrant1[i - yOne+1, j - xOne+1];
                    }
                    else//Bottom
                    {
                        if (j < xOne)//Left Quadrant 3
                            wholeMap[i, j] = Quadrant3[i, j];
                        else//Right Quadrant 2
                            wholeMap[i, j] = Quadrant2[i, j - xOne+1];
                    }
                }
            }
            #endregion

            return wholeMap;
        }

        /// <summary>
        /// Creates a FoV map using shadowcasting based on source point.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public List<IPoint> getShadowPointsFromWholeMap(IPoint src,Common.tileSide dir)
        {
            if (!isFree(src))
                return null;
            List<IPoint> shaded = new List<IPoint>();
            valuePoint[][,] octancts = getOctancts(src);
            valuePoint[][,] shadeOctancts = new valuePoint[8][,];
            for (int i = 0; i < 8; i++)
                shadeOctancts[i] = getShadesInOctanct(octancts[i], i, src,Common.tileSide.None);
            valuePoint[,] map = joinOctancts(shadeOctancts);
            setDiagonalShades(getObstacleWorldMap(), map, src);
            foreach (valuePoint vp in map)
            {
                if (vp!=null && vp.value == 1)
                    shaded.Add(vp.p);
            }
            
            //Add origin if not in there
            if (!isPointInList(shaded, src))
                shaded.Add(src);
            return shaded;
        }
        #endregion

        #region SIMPLE OCLUSION (Not used anymore)
        public void ocludeTiles(OpenGlGuard g)
        {
            List<IOcluding> ocluders = getOcluders();
            foreach (IOcluding oc in ocluders)
                ocludeTiles(g, oc);
        }
        public void ocludeTiles(OpenGlGuard g, IOcluding oc)
        {
            double gX = g.MyPosition.X, gY = g.MyPosition.Y, oX = oc.getLocation().X, oY = oc.getLocation().Y;
            double newX, newY;
            int size = this.TileSize;
            bool stop = false;

            Tile cTile;

            //Get antipode
            newX = oX + (oX - gX);
            newY = oY + (oY - gY);

            while (!stop)
            {
                cTile = getTile(new PointObj(newX, newY, 0));
                if (cTile != null)//Tile exists?
                    //Oclude
                    cTile.Shaded = 10;

                if (newX == oX && newY == oY)
                {
                    if (Math.Abs(oX - gX) != Math.Abs(oY - gY))
                    {
                        //Oclude last tile, only if lateral distance<3 
                        if (Math.Abs(oX - gX) < Math.Abs(oY - gY) && Math.Abs(oX - gX) < 3 * size)//oclude tile vertically adjacent to ocluder
                        {
                            newX = oX;
                            newY = oY > gY ? oY + size : oY - size;
                        }
                        else if (Math.Abs(oX - gX) > Math.Abs(oY - gY) && Math.Abs(oY - gY) < 3 * size)//oclude tile horizontally adjacent to ocluder
                        {
                            newX = oX > gX ? oX + size : oX - size;
                            newY = oY;
                        }
                        cTile = getTile(new PointObj(newX, newY, 0));
                        if (cTile != null)//Tile exists?
                            //Oclude
                            cTile.Shaded = 10;
                    }
                    stop = true;
                }
                else
                {
                    //Get new point 
                    if (Math.Abs(oX - newX) > Math.Abs(oY - newY))//Oclude horizontal
                        newX = oX < gX ? newX + size : newX - size;
                    else if (Math.Abs(oX - newX) < Math.Abs(oY - newY))
                        newY = oY < gY ? newY + size : newY - size;
                    else // equal, so decrease both
                    {
                        newX = oX < gX ? newX + size : newX - size;
                        newY = oY < gY ? newY + size : newY - size;
                    }
                }
            }
        }
        #endregion

        #region FIELD OF VIEW STUFF
       
        #region OBSOLETE
        List<IPoint>[] getOctancts(List<IPoint> points, IPoint src)
        {
            List<IPoint>[] Octancts = new List<IPoint>[8];
            for (int i = 0; i < 8; i++)
                Octancts[i] = new List<IPoint>();

            foreach (IPoint p in points)
            {
                if (p.X > src.X)//Right Half
                {
                    if (p.Y > src.Y)//TopRight Quadrant
                    {
                        if (getSlope(getTile(src), p) > 1)//Octanct 2
                            Octancts[1].Add(p);
                        else //Octanct 3
                            Octancts[2].Add(p);
                    }
                    else //BottomRight Quadrant
                    {
                        if (getSlope(getTile(src), p) > -1)//Octanct 4
                            Octancts[3].Add(p);
                        else//Octanct 5
                            Octancts[4].Add(p);
                    }
                }
                else//Left Half
                {
                    if (p.Y > src.Y)//TopLeft Quadrant
                    {
                        if (getSlope(getTile(src), p) > -1)//Octanct 8
                            Octancts[7].Add(p);
                        else
                            Octancts[0].Add(p); //Octanct 1
                    }
                    else //BottomLeft Quadrant
                    {
                        if (getSlope(getTile(src), p) > 1)//Octanct 6
                            Octancts[4].Add(p);
                        else//Octanct 7
                            Octancts[5].Add(p);
                    }
                }
            }
            return Octancts;
        }
        IPoint[,] octanctToArray(List<IPoint> octanct, IPoint src, int octNumber)
        {
            IPoint[,] octanctArray;
            int width;

            switch (octNumber)
            {
                case 0://Octanct 1
                    width = getSide(octanct.Count, 1);
                    octanctArray = new IPoint[width, width];
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            octanctArray[i, j] = new PointObj(src.X - j * TileSize, src.Y + i * TileSize, 0);
                        }
                    }
                    break;
                case 1://Octanct 2
                    width = getSide(octanct.Count, 0);
                    octanctArray = new IPoint[width, width];
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            octanctArray[i, j] = new PointObj(src.X + j * TileSize, src.Y + i * TileSize, 0);
                        }
                    }
                    break;
                case 2://Octanct 3
                    width = getSide(octanct.Count, 0);
                    octanctArray = new IPoint[width, width];
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            octanctArray[i, j] = new PointObj(src.X + j * TileSize, src.Y + i * TileSize, 0);
                        }
                    }
                    break;
                case 3://Octanct 4
                    width = getSide(octanct.Count, 1);
                    octanctArray = new IPoint[width, width];
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            octanctArray[i, j] = new PointObj(src.X + j * TileSize, src.Y - i * TileSize, 0);
                        }
                    }
                    break;
                case 4://Octanct 5
                    width = getSide(octanct.Count, 0);
                    octanctArray = new IPoint[width, width];
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            octanctArray[i, j] = new PointObj(src.X + j * TileSize, src.Y - i * TileSize, 0);
                        }
                    }
                    break;
                case 5://Octanct 6
                    width = getSide(octanct.Count, 0);
                    octanctArray = new IPoint[width, width];
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            octanctArray[i, j] = new PointObj(src.X - j * TileSize, src.Y - i * TileSize, 0);
                        }
                    }
                    break;
                case 6://Octanct 7
                    width = getSide(octanct.Count, 2);
                    octanctArray = new IPoint[width, width];
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            octanctArray[i, j] = new PointObj(src.X - j * TileSize, src.Y - i * TileSize, 0);
                        }
                    }
                    break;
                case 7://Octanct 8
                    width = getSide(octanct.Count, 0);
                    octanctArray = new IPoint[width, width];
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            octanctArray[i, j] = new PointObj(src.X - j * TileSize, src.Y + i * TileSize, 0);
                        }
                    }
                    break;
                default:
                    octanctArray = new IPoint[0, 0];
                    break;
            }

            return octanctArray;
        }
        public int getSide(int n, int type)
        {
            switch (type)
            {
                case 0://None equal
                    return (int)(1 + Math.Sqrt(1 + 8 * (n - 1))) / 2;
                case 1://One axis equal, not origin
                    return (int)(-1 + Math.Sqrt(1 + 8 * (n - 1))) / 2;
                case 2://One axis equal, origin too
                    return (int)(1 + Math.Sqrt(-1 + 8 * n)) / 2;
                default:
                    return 0;
            }
        }
        #endregion

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
        public List<IPoint> getCone(IPoint src,OpenGlGuard.OpenGlGuardOrientation direction,int distance)
        {
            List<IPoint> conePoints=new List<IPoint>();
            int startX = (int)src.X / TileSize + MyWidth / 2, startY = (int)src.Y / TileSize + MyHeight / 2;
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

        #endregion

        #region PATHFINDING STUFF
        public void initializeValueMap(List<valuePoint> distMap,int defaultValue)
        {
            foreach (Tile t in myTiles)
            {
                distMap.Add(new valuePoint(t.MyOrigin, defaultValue));
            }
        }
               
        public bool isFree(IPoint src)
        {
            foreach (IDrawable drw in tiles)
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
        public bool tileFreeFromGuards(List<OpenGlGuard> _guards, IPoint tilePosition)
        {
            foreach (OpenGlGuard g in _guards)
            {
                if (g.MyPosition.equals(tilePosition))
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
        public void setDistancePointInMap(IPoint p, int distance,DistanceMap distMap)
        {
            valuePoint currentDP;
            currentDP = distMap.MyPoints.Find(
                        delegate(valuePoint _dp)
                        {
                            return _dp.p.equals(p);
                        });
            //If it is -1, assign distance, if it already has a distance, see if the new one is smaller
            currentDP.value = currentDP.value == -1 ? distance : Math.Min(currentDP.value, distance);
        }                            
        /// <summary>
        /// Creates a map (a List of valuePoints) where each point has as its value the distance to
        /// src.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public List<valuePoint> calculateDistanceMap(IPoint src)
        {
            List<valuePoint> distMap=new List<valuePoint>();
            initializeValueMap(distMap,-1);
            List<IPoint> currentPoints=new List<IPoint>(),adjacents=new List<IPoint>(),tempAdjacents;
            currentPoints.Add(src);
            //Put origin point in distance map, with distance 0
            setDistancePointInMap(src, 0,distMap);
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
                        return distMap.Find(
                            delegate (valuePoint _dp)
                            {
                                return _dp.p.equals(_p);
                            }).value!=-1;
                    });

                //To the points left in adjacents, set distance in distMap
                foreach (IPoint p in adjacents)
                {
                    setDistancePointInMap(p, distance, distMap);
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
                List<valuePoint> points=calculateDistanceMap(src);
                myDistanceMaps.Add(new DistanceMap() { MyOrigin = src, MyPoints = points });
            }
        }
        public DistanceMap getDistanceMap(IPoint src)
        {
            return myDistanceMaps.Find(
                 delegate(DistanceMap distMap)
                 {
                     return distMap.MyOrigin.equals(src);
                 });
        }

        public OpenGlPath getShortestPath(IPoint src, IPoint dest, List<IPoint> availableTiles)
        {
            List<valuePoint> distMap = getDistanceMap(src);
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
            List<valuePoint> distMap=getDistanceMap(dest);
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
            List<valuePoint> wholeMap = getDistanceMap(src);
            List<IPoint> reachableMap = new List<IPoint>();
            foreach (valuePoint vp in wholeMap)
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
        public List<IPoint> getRaytracedFOV(IPoint gSrc, OpenGlGuard.OpenGlGuardOrientation orientation)
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
        public List<IPoint> getRaytracedFOV(IPoint gSrc, OpenGlGuard.OpenGlGuardOrientation orientation,
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
