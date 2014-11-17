﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using SneakingClasses.System_Classes;

namespace Sneaking_Classes.Drawing_Classes
{
    public class HighBlock:IDrawable,IOcluding
    {
        static int highBlockIds = 2;
        public const int idType = 2;
        public int myId;
        float height;

        public float Height
        {
            get { return height; }
            private set { height = value; }
        }
        private cubeObj[] myCubes;
        pointObj myOrigin;

        public pointObj MyOrigin
        {
            get { return myCubes[0].MyOrigin; }
        }

        public HighBlock()
        {
            assignId(); 
        }
        public HighBlock(pointObj or, int size, float[] color, float[] outlineColor)
        {    
            assignId();
            createBlock(or, size, color, outlineColor);
        }
        public HighBlock(int[] or, int size, float[] color = null, float[] outlineColor = null)
        {
            //Default checking
            color = color == null ? Common.colorRed : color;
            outlineColor = outlineColor == null ? Common.colorBlack : outlineColor;
            createBlock(new pointObj(or[0],or[1],or[2]),size,color,outlineColor);
            
            assignId();
        }
        public void assignId()
        {
            myId = highBlockIds;
            highBlockIds += GameObjects.objectTypes;
        }
        private void createBlock(pointObj or, int size, float[] color, float[] outlineColor)
        {
            pointObj topOr = new pointObj(or.X, or.Y, or.Z + size);
            //Create two cubes, one on top of the other
            myCubes = new cubeObj[2];
            myCubes[0] = new cubeObj(or, size, color, outlineColor);
            myCubes[1] = new cubeObj(topOr, size, color, outlineColor);
            myCubes[0].createCubeTiles();
            myCubes[1].createCubeTiles();

            myOrigin = myCubes[0].MyOrigin;
            height = myCubes[0].CubeSize * 2;
        }

        #region IDRAWABLE
        public void draw()
        {
            foreach(cubeObj cube in myCubes)
                Common.drawCubeAndOutline(cube);
        }
        public int getId()
        {
            return myId;
        }
        public int[] getPosition()
        {
            return myCubes[0].MyOrigin.toIntArray();
        }
        public void setPosition(pointObj newPosition)
        {
            createBlock(newPosition,myCubes[0].CubeSize,myCubes[0].Color,myCubes[0].OutlineColor);
        }
        #endregion

        public bool Intercepts(pointObj src, pointObj dest)
        {
            foreach (cubeObj cube in myCubes)
            {
                if (cube.Intercepts(src, dest))
                    return true;
            }
            return false;
        }
        public void turn45()
        {
            foreach (cubeObj cube in myCubes)
                cube.turn45();
        }
        public void turn45(pointObj axis)
        {
            foreach (cubeObj cube in myCubes)
            {
                cube.RotationAxis = axis;
                cube.turn45();
            }
        }

        public pointObj getLocation()
        {
            return myOrigin;
        }
    }
}
