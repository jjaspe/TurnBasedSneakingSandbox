using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Entities;

namespace OpenGlGameCommon.Classes
{
    public class OpenGlPC:PC,IDrawable
    {
        public static int pcIds = 7;
        public static int idType=7;        
        int myId;

        public IDrawable myImage;
        int eyeLevel;
        int mySize;        
        int imageSize = 10;        
        public OpenGlGuard.OpenGlGuardOrientation MyOrientation;

        public int ImageSize
        {
          get { return imageSize; }
          set { imageSize = value; }
        }
        public int MySize
        {
            get { return mySize; }
            set { mySize = value; }
        }
        public void assignId()
        {
            myId = pcIds;
            pcIds += GameObjects.objectTypes;
        }
        public int EyeLevel
        {
            private set { eyeLevel = value; }
            get { return eyeLevel; }
        }

        public OpenGlPC()
        {
            MyOrientation = OpenGlGuard.OpenGlGuardOrientation.none;
            createHighImage();
        }

        public void createHighImage()
        {
            if (MyPosition != null)
            {
                int sizeDif=MySize-imageSize;
                IPoint blockCenter = new PointObj(MyPosition.X + sizeDif / 2, MyPosition.Y + sizeDif / 2, 0);
                HighBlock imageBlock = new HighBlock(blockCenter, 
                    imageSize, Common.colorWhite, Common.colorBlack);
                IPoint center = new PointObj(OpenGlMap.getInstance().getTile(MyPosition).getCenter());
                imageBlock.turn45(center);
                myImage = imageBlock;
                EyeLevel = (int)imageBlock.Height;
            }
        }
        public void createLowImage()
        {
            IPoint center = new PointObj(OpenGlMap.getInstance().getTile(MyPosition).getCenter());
            if (MyPosition != null)
            {
                int sizeDif = MySize - imageSize;
                IPoint blockCenter = new PointObj(MyPosition.X + sizeDif / 2, MyPosition.Y + sizeDif / 2, 0);
                LowBlock imageBlock = new LowBlock(blockCenter,
                    imageSize, Common.colorWhite, Common.colorBlack);
                imageBlock.turn45(new PointObj(center.X,center.Y, 0));
                myImage = imageBlock;
                EyeLevel = (int)imageBlock.CubeSize;
            }
        }

        public void move(IPoint newPosition)
        {
            if (newPosition != null)
            {
                setOrientation(newPosition);
                MyPosition = newPosition;
            }
        }        
        public void turnToAdjacent(IPoint towardsOrigin)
        {
            MyOrientation = getDirection(towardsOrigin);
        }

        public OpenGlGuard.OpenGlGuardOrientation getDirection(IPoint nextOrigin)
        {
            OpenGlGuard.OpenGlGuardOrientation or = OpenGlGuard.OpenGlGuardOrientation.none;
            if (nextOrigin.X == MyPosition.X)//Vertical Movement
            {
                if (nextOrigin.Y == MyPosition.Y + MySize)//up
                    or = OpenGlGuard.OpenGlGuardOrientation.up;
                else if (nextOrigin.Y == MyPosition.Y - MySize)//down
                    or = OpenGlGuard.OpenGlGuardOrientation.down;
                else
                    or = OpenGlGuard.OpenGlGuardOrientation.none;
            }
            else if (nextOrigin.Y == MyPosition.Y)//Horizontal Movement
            {
                if (nextOrigin.X == MyPosition.X + MySize)//right
                    or = OpenGlGuard.OpenGlGuardOrientation.right;
                else if (nextOrigin.X == MyPosition.X - MySize)//left
                    or = OpenGlGuard.OpenGlGuardOrientation.left;
                else
                    or = OpenGlGuard.OpenGlGuardOrientation.none;
            }
            else
                or = OpenGlGuard.OpenGlGuardOrientation.none;

            return or;
        }
        public IPoint getEyeLevel()
        {
            IPoint center = new PointObj(OpenGlMap.getInstance().getTile(MyPosition).getCenter());
            return new PointObj(center.X,center.Y, EyeLevel);
        }
        private void setOrientation(IPoint nextOrigin)
        {
            MyOrientation = getDirection(nextOrigin);
        }

        #region IDRAWABLE
        public void draw()
        {
            if (this.getValue("isSneaking") == 1)
                createLowImage();
            else
                createHighImage();
            myImage.draw();
        }
        public int getId()
        {
            return myId;
        }
        public double[] getPosition()
        {
            return MyPosition.toArray();
        }
        public void setPosition(IPoint newPosition)
        {
            MyPosition = newPosition;
        }
        #endregion

        public bool Visible
        {
            set { return; }
        }
    }
}
