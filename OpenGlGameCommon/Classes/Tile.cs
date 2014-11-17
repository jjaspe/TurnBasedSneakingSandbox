using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;using Canvas_Window_Template.Interfaces;
using SneakingClasses.Data_Classes;

namespace CreationClasses.Classes
{
    public class Tile : tileObj, IDrawable
    {
        static int tileIds = 0;
        public const int idType = 0;
        public int myId;

        /// <summary>
        /// 1 is regular color, >1 makes it darker
        /// </summary>
        int shade = 1;

        float[] originalColor;

        public float[] OriginalColor
        {
            get { return originalColor; }
            set { originalColor = value; }
        }

        public int Shaded
        {
            get { return shade; }
            set { shade = Math.Max(1, value); setShade(); }
        }

        public Tile(IPoint tileStart, IPoint tileEnd)
        {
            MyOrigin = tileStart;
            MyEnd = tileEnd;
            assignId();
        }
        public Tile(int[] start, int[] end)
        {
            MyOrigin = new PointObj(start[0], start[1], start[2]);
            MyEnd = new PointObj(end[0], end[1], end[2]);
            assignId();

        }
        public Tile(int[] start, int _tileSize, int orientation = 3)
        {
            MyOrigin = new PointObj(start[0], start[1], start[2]);
            MyEnd = new PointObj(start[0] + _tileSize, start[1] + _tileSize, start[2]);
            this.TileSize = _tileSize;
            assignId();
        }

        private void assignId()
        {
            myId = tileIds;
            tileIds += GameObjects.objectTypes;
        }
        /* 0 for nothing, 1 for low block, 2 for high block, 3 for guard, 4 for pc*/
        public int myState;

        /* 0 for none, 1 for low wall, 2 for high wall */
        public int upWall, downWall, rightWall, leftWall;

        private void setShade()
        {
            MyColor = new float[] { originalColor[0] / shade, originalColor[1] / shade, originalColor[2] / shade };
        }
        public void lighten(int level)
        {
            Shaded = Math.Max(1, shade - level);
        }

        public void draw()
        {
            Common.drawTileAndOutline(this);
        }
        public int getId()
        {
            return myId;
        }
        public int[] getPosition()
        {
            return new int[] { MyOrigin.X, MyOrigin.Y, MyOrigin.Z };
        }
        public void setPosition(IPoint newPosition)
        {
            MyOrigin.X = newPosition.X;
            MyOrigin.Y = newPosition.Y;
            MyOrigin.Z = newPosition.Z;
        }

        /*
        public float[] getCenter(OpenGlMap.tileSide side)
        {
            switch (side)
            {
                case OpenGlMap.tileSide.Top:
                    return new float[] { MyOrigin.X + TileSize / 2, MyOrigin.Y + TileSize };
                case OpenGlMap.tileSide.Right:
                    return new float[] { MyOrigin.X + TileSize, MyOrigin.Y + TileSize / 2 };
                case OpenGlMap.tileSide.Bottom:
                    return new float[] { MyOrigin.X + TileSize / 2, MyOrigin.Y };
                case OpenGlMap.tileSide.Left:
                    return new float[] { MyOrigin.X, MyOrigin.Y + TileSize / 2 };
                default:
                    return getCenter();
            }
        }*/
        public float[] getCenter(OpenGlMap.tileSide side)
        {
            switch (side)
            {
                case OpenGlMap.tileSide.Top:
                    return new float[] { MyOrigin.X + TileSize / 2, MyOrigin.Y + TileSize };
                case OpenGlMap.tileSide.Right:
                    return new float[] { MyOrigin.X + TileSize, MyOrigin.Y + TileSize / 2 };
                case OpenGlMap.tileSide.Bottom:
                    return new float[] { MyOrigin.X + TileSize / 2, MyOrigin.Y };
                case OpenGlMap.tileSide.Left:
                    return new float[] { MyOrigin.X, MyOrigin.Y + TileSize / 2 };
                default:
                    return getCenter();
            }
        }
        public float[] getCenter()
        {
            return new float[]{MyOrigin.X+TileSize/2,MyOrigin.Y+TileSize/2};
        }
        public float[] topLeft()
        {
            return new float[] { this.MyOrigin.X, this.MyOrigin.Y + TileSize };
        }
        public float[] topRight()
        {
            return new float[] { MyOrigin.X + TileSize, MyOrigin.Y + TileSize };
        }
        public float[] bottomLeft()
        {
            return new float[] { MyOrigin.X, MyOrigin.Y };
        }
        public float[] bottomRight()
        {
            return new float[] { MyOrigin.X + TileSize, MyOrigin.Y };
        }
    }
}
