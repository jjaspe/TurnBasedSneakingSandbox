using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Entities;
using OpenGlGameCommon.Drawables;

namespace OpenGlGameCommon.Classes
{
    public class DrawablePC:DrawableGuard
    {
        public static int pcIds = 7;
        public static int idType=7;        
        int myId;

        public IDrawable myImage;
        int eyeLevel;
        int mySize;        
        int imageSize = 10;        
        public GuardOrientation MyOrientation;

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

        public DrawablePC()
        {
            MyOrientation = GuardOrientation.none;
            createHighImage();
        }

        public void createHighImage()
        {
            if (Position != null)
            {
                int sizeDif=MySize-imageSize;
                IPoint blockCenter = new PointObj(Position.X + sizeDif / 2, Position.Y + sizeDif / 2, 0);
                HighBlock imageBlock = new HighBlock(blockCenter, 
                    imageSize, Common.colorWhite, Common.colorBlack);
                IPoint center = new PointObj(OpenGlMap.getInstance().getTile(Position).getCenter());
                imageBlock.turn45(center);
                myImage = imageBlock;
                EyeLevel = (int)imageBlock.Height;
            }
        }
        public void createLowImage()
        {
            IPoint center = new PointObj(OpenGlMap.getInstance().getTile(Position).getCenter());
            if (Position != null)
            {
                int sizeDif = MySize - imageSize;
                IPoint blockCenter = new PointObj(Position.X + sizeDif / 2, Position.Y + sizeDif / 2, 0);
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
                Position = newPosition;
            }
        }        
        public void turnToAdjacent(IPoint towardsOrigin)
        {
            MyOrientation = getDirection(towardsOrigin);
        }

        public GuardOrientation getDirection(IPoint nextOrigin)
        {
            GuardOrientation or = GuardOrientation.none;
            if (nextOrigin.X == Position.X)//Vertical Movement
            {
                if (nextOrigin.Y == Position.Y + MySize)//up
                    or = GuardOrientation.up;
                else if (nextOrigin.Y == Position.Y - MySize)//down
                    or = GuardOrientation.down;
                else
                    or = GuardOrientation.none;
            }
            else if (nextOrigin.Y == Position.Y)//Horizontal Movement
            {
                if (nextOrigin.X == Position.X + MySize)//right
                    or = GuardOrientation.right;
                else if (nextOrigin.X == Position.X - MySize)//left
                    or = GuardOrientation.left;
                else
                    or = GuardOrientation.none;
            }
            else
                or = GuardOrientation.none;

            return or;
        }

        #region IDRAWABLE
        public new void draw()
        {
            if (this.MyCharacter.getProperty("isSneaking").Value == 1)
                createLowImage();
            else
                createHighImage();
            myImage.draw();
        }
        #endregion IDRAWABLE
    }
}
