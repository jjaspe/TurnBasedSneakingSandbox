using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using SneakingClasses.System_Classes;

namespace Sneaking_Classes.Drawing_Classes
{
    public class HighWall:wallObj,IDrawable,IOcluding
    {
        static int hWallIds = 4;
        public const int idType = 4;
        public int myId;

        public HighWall()
        {
            assignId();
        }
        public HighWall(int altitude, int startX, int endX, int tileSize)
        {
            assignId();
            pointObj or = new pointObj(startX, altitude, 0);
            pointObj topOr = new pointObj(or.X, or.Y, or.Z + tileSize);
            //Create tiles
            myTiles = new tileObj[2,(endX - startX)/tileSize];
            for (int i = 0; i < (endX-startX)/tileSize; i++)
            {
                myTiles[0,i] = new tileObj(new pointObj(or.X+ i * tileSize, or.Y, or.Z),
                    new pointObj(or.X + (i + 1) * tileSize, or.Y, or.Z+tileSize), Common.colorBrown, Common.colorBlack);
                myTiles[1, i] = new tileObj(new pointObj(topOr.X + i * tileSize, topOr.Y, topOr.Z),
                   new pointObj(topOr.X + (i + 1) * tileSize, topOr.Y, topOr.Z + tileSize), Common.colorBrown, Common.colorBlack);

            }
            MyOrigin = myTiles[0, 0].MyOrigin;
            TileSize = tileSize;
            Orientation = 2;
        }

        private void assignId()
        {
            myId = hWallIds;
            hWallIds += GameObjects.objectTypes;
        }


        public void draw()
        {
            foreach (tileObj tile in myTiles)
                Common.drawTileAndOutline(tile);
        }
        public int getId()
        {
            return myId;
        }
        public int[] getPosition()
        {
            if (myTiles != null)
                return new int[] { myTiles[0,0].MyOrigin.X, myTiles[0,0].MyOrigin.Y, myTiles[0,0].MyOrigin.Z };
            else
                return null;
        }
        public void setPosition(pointObj newPosition)
        {
            return;
        }

        public bool Intercepts(pointObj src, pointObj dest)
        {
            foreach (tileObj tile in MyTiles)
            {
                if (tile.Intercepts(src, dest))
                    return true;
            }
            return false;
        }
        public void turn()
        {
            if (MyOrigin == null || MyTiles == null || MyTiles.Length == 0)
                return;
            int startY = MyOrigin.Y, endY = MyOrigin.Y + MyTiles.Length/2 * TileSize;

            pointObj or = new pointObj(MyOrigin.X,startY, 0);
            pointObj topOr = new pointObj(or.X, or.Y, or.Z + TileSize);
            for (int i = 0; i < (endY - startY) / TileSize; i++)
            {
                myTiles[0, i] = new tileObj(new pointObj(or.X, or.Y + i * TileSize, or.Z),
                    new pointObj(or.X , or.Y + (i + 1) * TileSize, or.Z + TileSize), Common.colorBrown, Common.colorBlack);
                myTiles[1, i] = new tileObj(new pointObj(topOr.X , topOr.Y + i * TileSize , topOr.Z),
                   new pointObj(topOr.X, topOr.Y + (i + 1) * TileSize, topOr.Z + TileSize), Common.colorBrown, Common.colorBlack);

            }
            Orientation = 1;
        }

        public pointObj getLocation()
        {
            return MyOrigin;
        }
    }
}
