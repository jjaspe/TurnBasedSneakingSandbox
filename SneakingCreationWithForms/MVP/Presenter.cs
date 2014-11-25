using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Interfaces;
using SneakingCommon.Enums;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Gameplay.Sneaking_Drawables;
using System.Xml;
using SneakingCommon.Utility;

namespace SneakingCreationWithForms.MVP
{
    public class Presenter
    {

        public static int TILE_SIZE = 20;

        IView view;
        IModel model;
        public selectorObj mySelector;

        public IModel Model
        {
            get { return model; }
            set { model = value; }
        }
        public IView View
        {
            get { return view; }
            set { view = value; }
        }

        #region MAIN MENU STUFF
        public void createMapWindowStart()
        {
            View.startMapCreation();
        }
        public void createGuardWindowStart()
        {
            View.startGuardCreation();
        }
        #endregion


        #region MAP CREATION STUFF
        Elements selectedElement;
        /// <summary>
        /// Loads a map from XmlDocument.
        /// </summary>
        /// <param name="filename"></param>
        public void loadMap(XmlDocument doc)
        {
            this.model.Map = XmlLoader.loadBareMap(doc);
        }

        /// <summary>
        /// Tries to save the model's map to filename
        /// </summary>
        /// <param name="filename"></param>
        public void saveMap(string filename)
        {
            XmlLoader.saveBareMap(filename, model.Map);
        }

        public void createMapSelected(int width, int length, Int32 originX=Int32.MaxValue, Int32 originY=Int32.MaxValue)
        {
            IPoint origin;
            if (originX == Int32.MaxValue || originY == Int32.MaxValue)
                origin = new pointObj(-width * TILE_SIZE / 2, -length * TILE_SIZE / 2, 0);
            else
                origin = new pointObj(originX, originY, 0);
            model.Map = SneakingMap.createInstance(width, length, TILE_SIZE, origin);
        }
        public void viewClicked(int objectId, MouseButtons button)
        {
            //Check all objects, see if any was selected
            Tile tile;
            LowBlock lBlock; HighBlock hBlock; LowWall lWall; HighWall hWall;
            //Check type
            if (objectId > -1)
            {
                switch (objectId % GameObjects.objectTypes)
                {
                    case Tile.idType://Tile
                        tile = model.Map.getTile(objectId);
                        if (tile != null)
                        {
                            if (button == MouseButtons.Left)
                                tileLeftClick(tile);
                            else if (button == MouseButtons.Right)
                                tileRightClick(tile);
                        }
                        break;
                    case LowBlock.idType://Low Block
                        lBlock = model.Map.getLowBlock(objectId);
                        if (button == MouseButtons.Left)
                            lowBlockLeftClick(lBlock);
                        else if (button == MouseButtons.Right)
                            lowBlockRightClick(lBlock);
                        break;
                    case HighBlock.idType://High block
                        hBlock = model.Map.getHighBlock(objectId);
                        if (button == MouseButtons.Left)
                            highBlockLeftClick(hBlock);
                        else if (button == MouseButtons.Right)
                            highBlockRightClick(hBlock);
                        break;
                    case LowWall.idType://Low wall
                        lWall = model.Map.getLowWall(objectId);
                        if (button == MouseButtons.Left)
                            lowWallLeftClick(lWall);
                        else if (button == MouseButtons.Right)
                            lowWallRightClick(lWall);
                        break;
                    case HighWall.idType://High wall
                        hWall = model.Map.getHighWall(objectId);
                        if (button == MouseButtons.Left)
                            highWallLeftClick(hWall);
                        else if (button == MouseButtons.Right)
                            highWallRightClick(hWall);
                        break;
                    default:
                        break;
                }
            }
        }
        public void geometryElementSelected(Elements el)
        {
            selectedElement = el;
        }

        bool hasWall(IPoint origin, int tileSize)
        {
            double[] originA = origin.toArray();
            foreach (IDrawable obj in (model.Map as IWorld).getEntities())
            {
                if (obj.getId() % GameObjects.objectTypes == 3)//low walls
                {
                    if ((new PointObj(obj.getPosition())).equals(origin) &&
                        ((LowWall)obj).MyTiles[0, 0].MyEnd.X == origin.X + tileSize)
                        return true;
                }
                if (obj.getId() % GameObjects.objectTypes == 4)//high walls
                {
                    if ((new PointObj(obj.getPosition())).equals(origin) &&
                        ((HighWall)obj).MyTiles[0, 0].MyEnd.X == origin.X + tileSize)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Left clicked a tile, so we are gonna add some element, depending on selectedElement
        /// </summary>
        /// <param name="tile"></param>
        void tileLeftClick(Tile tile)
        {
            switch (selectedElement)
            {
                case Elements.LowBlock: // Add low block                            
                    model.Map.Drawables.Add(
                        new LowBlock(tile.MyOrigin, model.Map.TileSize, Common.colorRed, Common.colorBlack));
                    break;
                case Elements.HighBlock: // Add high block
                    model.Map.Drawables.Add(
                            new HighBlock(tile.MyOrigin, model.Map.TileSize, Common.colorRed, Common.colorBlack));
                    break;
                case Elements.LowWall: // Add low wall
                    if (!hasWall(tile.MyOrigin, model.Map.TileSize))
                        model.Map.Drawables.Add(
                            new LowWall(tile.origin.Y, tile.origin.X,
                                tile.origin.X + model.Map.TileSize, model.Map.TileSize));
                    break;
                case Elements.HighWall: // Add high wall
                    if (!hasWall(tile.MyOrigin, model.Map.TileSize))
                        model.Map.Drawables.Add(
                            new HighWall(tile.origin.Y, tile.origin.X,
                                tile.origin.X + model.Map.TileSize, model.Map.TileSize));
                    break;
                default:
                    break;
            }
        }
        void tileRightClick(Tile tile)
        {
        }
        void lowBlockLeftClick(LowBlock block)
        {
        }
        void lowBlockRightClick(LowBlock block)
        {
            model.Map.Drawables.Remove(block);
        }
        void highBlockLeftClick(HighBlock block)
        {
        }
        void highBlockRightClick(HighBlock block)
        {
            model.Map.Drawables.Remove(block);
        }
        void lowWallRightClick(LowWall lWall)
        {
            model.Map.Drawables.Remove(lWall);
        }
        void lowWallLeftClick(LowWall lWall)
        {
            //Check bounds, if dest is outside, only remove source
            if (lWall.MyTiles[0, 0].MyOrigin.Y >= model.Map.MyHeight * model.Map.TileSize / 2)
            {
                model.Map.Drawables.Remove(lWall);
                return;
            }

            //Check destination
            foreach (IDrawable obj in (model.Map as IWorld).getEntities())
            {
                if (obj.getId() % GameObjects.objectTypes == 3)//low walls
                {
                    //Check that there isn't a tile in destination, if so remove source
                    if (((LowWall)obj).MyTiles[0, 0].MyOrigin.equals(lWall.MyTiles[0, 0].MyOrigin)
                        && ((LowWall)obj).MyTiles[0, 0].MyEnd.Y - model.Map.TileSize == lWall.MyTiles[0, 0].MyEnd.Y)
                    {
                        model.Map.Drawables.Remove(lWall);
                        return;
                    }
                }
                if (obj.getId() % GameObjects.objectTypes == 4)//high walls
                {
                    //Check that there isn't a tile in destination, if so remove source
                    if (((HighWall)obj).MyTiles[0, 0].MyOrigin.equals(lWall.MyTiles[0, 0].MyOrigin)
                        && ((HighWall)obj).MyTiles[0, 0].MyEnd.Y - model.Map.TileSize == lWall.MyTiles[0, 0].MyEnd.Y)
                    {
                        model.Map.Drawables.Remove(lWall);
                        return;
                    }
                }
            }

            //Change Orientation
            lWall.MyTiles[0, 0].MyEnd.Y += model.Map.TileSize;
            lWall.MyTiles[0, 0].MyEnd.X -= model.Map.TileSize;
        }
        void highWallRightClick(HighWall hWall)
        {
            model.Map.Drawables.Remove(hWall);
        }
        void highWallLeftClick(HighWall hWall)
        {
            //Check bounds, if dest is outside, only remove source
            if (hWall.MyTiles[0, 0].MyOrigin.Y >= model.Map.MyHeight * model.Map.TileSize / 2)
            {
                model.Map.Drawables.Remove(hWall);
                return;
            }

            //Check destination
            foreach (IDrawable obj in (model.Map as IWorld).getEntities())
            {
                if (obj.getId() % GameObjects.objectTypes == 3)//low walls
                {
                    //Check that there isn't a tile in destination, if so remove source
                    if (((LowWall)obj).MyTiles[0, 0].MyOrigin.equals(hWall.MyTiles[0, 0].MyOrigin)
                        && ((LowWall)obj).MyTiles[0, 0].MyEnd.Y - model.Map.TileSize == hWall.MyTiles[0, 0].MyEnd.Y)
                    {
                        model.Map.Drawables.Remove(hWall);
                        return;
                    }
                }
                if (obj.getId() % GameObjects.objectTypes == 4)//high walls
                {
                    //Check that there isn't a tile in destination, if so remove source
                    if (((HighWall)obj).MyTiles[0, 0].MyOrigin.equals(hWall.MyTiles[0, 0].MyOrigin)
                        && ((HighWall)obj).MyTiles[0, 0].MyEnd.Y - model.Map.TileSize == hWall.MyTiles[0, 0].MyEnd.Y)
                    {
                        model.Map.Drawables.Remove(hWall);
                        return;
                    }
                }
            }

            //Change Orientation
            hWall.MyTiles[0, 0].MyEnd.Y += model.Map.TileSize;
            hWall.MyTiles[0, 0].MyEnd.X -= model.Map.TileSize;

            hWall.MyTiles[1, 0].MyEnd.Y += model.Map.TileSize;
            hWall.MyTiles[1, 0].MyEnd.X -= model.Map.TileSize;
        }
        #endregion

    }
}
