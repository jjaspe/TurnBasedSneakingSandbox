using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using SneakingClasses.System_Classes;

namespace Sneaking_Classes.Drawing_Classes
{
    public class LowBlock:cubeObj,IDrawable
    {
        static int lowBlockIds = 1;
        public const int idType = 1;
        public int myId;
        public LowBlock()
        {
            assignId();
        }
        public LowBlock(pointObj or, int size, float[] color=null, float[] outlineColor=null)
        {
            //Default checking
            color=color==null?Common.colorRed:color;
            outlineColor = outlineColor == null ? Common.colorBlack : outlineColor;
            this.MyOrigin = or;
            this.OutlineColor = outlineColor;
            this.CubeSize = size;
            this.Color = color;
            assignId();

            this.createCubeTiles();
        }
        public LowBlock(int[] or, int size, float[] color = null, float[] outlineColor = null)
        {
            //Default checking
            color = color == null ? Common.colorRed : color;
            outlineColor = outlineColor == null ? Common.colorBlack : outlineColor;
            this.MyOrigin = new pointObj(or[0],or[1],or[2]);
            this.OutlineColor = outlineColor;
            this.CubeSize = size;
            this.Color = color;
            assignId();

            this.createCubeTiles();
        }

        private void assignId()
        {
            myId = lowBlockIds;
            lowBlockIds += GameObjects.objectTypes;
        }

        public void draw()
        {
            Common.drawCubeAndOutline(this);
        }
        public int getId()
        {
            return myId;
        }
        public int[] getPosition()
        { return new int[] { MyOrigin.X, MyOrigin.Y, MyOrigin.Z }; }
        public void setPosition(pointObj newPosition)
        {
            MyOrigin = newPosition;
            this.createCubeTiles();
        }

        public void turn45(pointObj pointObj)
        {
            this.turn45();
        }
    }
}
