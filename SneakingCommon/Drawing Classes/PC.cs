using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Classes.System_Classes;
using SneakingClasses.System_Classes;

namespace Sneaking_Classes.Drawing_Classes
{
    public class PC:Character,IDrawable
    {
        public static int pcIds = 7;
        public static int idType=7;        
        int myId;

        public IDrawable myImage;
        pointObj myPosition;
        Tile myTile;
        List<pointObj> fov;
        NoiseMap foh;
        public Guard.orientation MyOrientation;

        public List<pointObj> FOV
        {
            get { return fov; }
            set { fov = value; }
        }
        public NoiseMap FOH
        {
            get { return foh; }
            set { foh = value; }
        }

        public Tile MyTile
        {
            get { return myTile; }
            set { myTile = value; myPosition = value.origin; createImage(); }
        }
        int imageSize = 10;        

        public int ImageSize
        {
          get { return imageSize; }
          set { imageSize = value; }
        }
        public pointObj MyPosition
        {
            get { return myPosition; }
            set { myPosition = value; createImage(); }
        }
        public void assignId()
        {
            myId = pcIds;
            pcIds += GameObjects.objectTypes;
        }

        public PC()
        {
            MyOrientation = Guard.orientation.none;
        }

        private void createImage()
        {
            if (MyPosition != null && MyTile!=null)
            {
                int sizeDif=MyTile.TileSize-imageSize;
                pointObj blockCenter = new pointObj(myPosition.X + sizeDif / 2, myPosition.Y + sizeDif / 2, 0);
                HighBlock imageBlock = new HighBlock(blockCenter, 
                    imageSize, Common.colorWhite, Common.colorBlack);
                imageBlock.turn45(new pointObj((int)myTile.getCenter()[0],(int)myTile.getCenter()[1],0));
                myImage = imageBlock;
            }
        }

        public void move(Tile newTile)
        {
            if (newTile != null)
            {
                setOrientation(newTile);
                MyTile = newTile;
            }
        }
        public void turnToAdjacent(Tile towardsTile)
        {
            MyOrientation = getDirection(towardsTile);
        }

        public Guard.orientation getDirection(Tile nextTile)
        {
            Guard.orientation or = Guard.orientation.none;
            if (nextTile.MyOrigin.X == myTile.MyOrigin.X)//Vertical Movement
            {
                if (nextTile.MyOrigin.Y == myTile.MyOrigin.Y + myTile.TileSize)//up
                    or = Guard.orientation.up;
                else if (nextTile.MyOrigin.Y == myTile.MyOrigin.Y - myTile.TileSize)//down
                    or = Guard.orientation.down;
                else
                    or = Guard.orientation.none;
            }
            else if (nextTile.MyOrigin.Y == myTile.MyOrigin.Y)//Horizontal Movement
            {
                if (nextTile.MyOrigin.X == myTile.MyOrigin.X + myTile.TileSize)//right
                    or = Guard.orientation.right;
                else if (nextTile.MyOrigin.X == myTile.MyOrigin.X - myTile.TileSize)//left
                    or = Guard.orientation.left;
                else
                    or = Guard.orientation.none;
            }
            else
                or = Guard.orientation.none;

            return or;
        }
        private void setOrientation(Tile nextTile)
        {
            MyOrientation = getDirection(nextTile);
        }

        #region IDRAWABLE
        public void draw()
        {
            myImage.draw();
        }

        public int getId()
        {
            return myId;
        }

        public int[] getPosition()
        {
            return myPosition.toIntArray();
        }
        #endregion
    }
}
