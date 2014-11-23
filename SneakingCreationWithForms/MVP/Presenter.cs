using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Interfaces;
using SneakingCommon.Enums;
using Canvas_Window_Template.Basic_Drawing_Functions;

namespace SneakingCreationWithForms.MVP
{
    public class Presenter
    {
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

        #region model.Map CREATION STUFF
        Elements selectedElement;
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
