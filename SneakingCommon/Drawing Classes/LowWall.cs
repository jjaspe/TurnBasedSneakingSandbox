using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using SneakingClasses.System_Classes;

namespace Sneaking_Classes.Drawing_Classes
{
    public class LowWall:wallObj,IDrawable
    {
        static int lWallIds = 3;
        public const int idType = 3;
        public int myId;

        public LowWall()
        {
            assignId();
        }
        public LowWall(int altitude, int startX, int endX, int tileSize)
        {
            assignId();
            //Create tiles
            myTiles = new tileObj[1,(endX - startX)/tileSize];
            for (int i = 0; i < (endX-startX)/tileSize; i++)
            {
                myTiles[0,i] = new tileObj(new pointObj(startX + i * tileSize, altitude, 0),
                    new pointObj(startX + (i + 1) * tileSize, altitude, tileSize), Common.colorBrown, Common.colorBlack);
            }
            MyOrigin = myTiles[0, 0].MyOrigin;
            TileSize = tileSize;
            Orientation = 2;
        }

        private void assignId()
        {
            myId = lWallIds;
            lWallIds += GameObjects.objectTypes;
        }

        public void turn()
        {
            if (MyOrigin == null || MyTiles == null || MyTiles.Length == 0)
                return;
            int startY = MyOrigin.Y, endY = MyOrigin.Y + MyTiles.Length * TileSize,latitude=MyOrigin.X;
            for (int i = 0; i < (endY - startY) / TileSize; i++)
            {
                myTiles[0, i] = new tileObj(new pointObj(latitude, startY + i * TileSize, 0),
                    new pointObj(latitude, startY + (i + 1) * TileSize, TileSize), Common.colorBrown, Common.colorBlack);
            }
            Orientation = 1;
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
                return myTiles[0,0].MyOrigin.toIntArray();
            else
                return null;
        }
        public void setPosition(pointObj newPosition)
        {
            return;
        }
    }
}
