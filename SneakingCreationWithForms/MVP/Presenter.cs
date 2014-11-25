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
using CharacterSystemLibrary.Classes;
using OpenGlGameCommon.Enums;

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
            Model.Map = null;
            View.startMapCreation();
        }
        public void createGuardWindowStart()
        {
            Model.Guards = new List<SneakingGuard>();
            Model.Map = null;
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


        #region GUARD CREATION STUFF
        /// <summary>
        /// Returns the guard with given id, null if there isn't one
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SneakingGuard findGuard(int id)
        {
            foreach (SneakingGuard guard in Model.Guards)
                if (guard.getId() == id)
                    return guard;
            return null;
        }

        /// <summary>
        /// Removes a guard with id:id from the list of drawables in the map. Returns removed guard, null if not found
        /// </summary>
        /// <param name="id"></param>
        public SneakingGuard removeGuard(int id)
        {
            //Find guard
            SneakingGuard guard = null;
            foreach (SneakingGuard g in Model.Guards)
            {
                if (g.getId() == id)
                {
                    guard = g;
                    break;
                }
            }

            if (guard != null)
            {
                //If found remove from list;
                Model.Guards.Remove(guard);
                //remove from drawables
                Model.Map.Drawables.Remove(guard);
            }
            return guard;
        }

        /// <summary>
        /// Returns whether a tile has a guard on top
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        bool tileOcuppied(Tile tile)
        {
            foreach (SneakingGuard g in Model.Guards)
            {
                if (tile.MyOrigin.equals(g.Position))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Creates a guard with name:name, at position:positionTile, and with all the stats:stats. It initializes guard's orientation to left by default.
        /// IF there is a guard with the same name dont' add  and return null
        /// </summary>
        /// <param name="name"></param>
        /// <param name="positionTile"></param>
        /// <param name="stats"></param>
        /// <returns></returns>
        public SneakingGuard createGuard(String name,Tile positionTile,List<Stat> stats)
        {
            SneakingGuard newGuard = new SneakingGuard();

            //Check with same name
            var g = from guard in Model.Guards
                    where guard.MyName.Equals(name)
                    select guard;
            if (g.Count() > 0)
                return null;

            if (name != "" && positionTile != null && !tileOcuppied(positionTile))
            {
                newGuard.MyCharacter.Name = name;
                newGuard.Position = positionTile.MyOrigin;
                newGuard.MySize = (int)positionTile.TileSize / 2;
                newGuard.MyOrientation = GuardOrientation.left;

                foreach (Stat s in stats)
                    newGuard.MyCharacter.addStat(s);
                
                newGuard.Visible = true;
                Model.Guards.Add(newGuard);
                Model.Map.Drawables.Add(newGuard);
                return newGuard;                
            }
            return null;
        }

        /// <summary>
        /// Saves guards to file with path:filename
        /// </summary>
        /// <param name="filename"></param>
        public void saveGuards(String filename)
        {
            XmlLoader.saveGuards(filename, Model.Guards);
        }

        public void loadSystem(string filename)
        {
            Model.System = XmlLoader.loadSystem(filename);
        }
        #endregion

    }
}
