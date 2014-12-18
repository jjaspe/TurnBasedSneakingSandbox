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
using OpenGlGameCommon.Data_Classes;

namespace SneakingCreationWithForms.MVP
{
    public class Presenter
    {

        public static int TILE_SIZE = 20;
        public static float[] selectedEntryTileColor = Common.colorOrange;

        IView view;
        IModel model;
        public selectorObj mySelector;
        public List<PatrolPath> Patrols = new List<PatrolPath>();

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
        public void createPatrolsWindowStart()
        {
            Model.Guards = new List<SneakingGuard>();
            Model.Map = null;
            View.startPatrolCreation();
        }
        #endregion

        #region MAP CREATION STUFF
        Elements selectedElement;
        /// <summary>
        /// Loads a map from XmlDocument.
        /// </summary>
        /// <param name="filename"></param>
        public void loadBareMap(XmlDocument doc)
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
            //Check with same name
            var g = from guard in Model.Guards
                    where guard.MyName.Equals(name)
                    select guard;
            if (g.Count() > 0)
                return null;

            SneakingGuard newGuard = new SneakingGuard();

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
        public void saveGuardsMap(String filename)
        {
            XmlLoader.saveGuardMap(filename, Model.Map);
        }

        /// <summary>
        /// Loads a map with guards from XmlDocument.
        /// </summary>
        /// <param name="filename"></param>
        public void loadGuardsMap(XmlDocument doc)
        {
            this.model.Map = XmlLoader.loadGuardsMap(doc);
            this.model.Guards = this.model.Map.getGuards();            
        }

        public void loadSystem(string filename)
        {
            Model.System = XmlLoader.loadSystem(filename);
        }
        #endregion

        #region PATROL CREATION STUFF
        private bool allowDestination(SneakingMap map, IPoint source, Tile dest)
        {
            List<IDrawable> dr = (map as IWorld).getEntities();
            int direction = 0;//0:up,2:right,3:down,4:left Clockwise
            double dX, dY;
            double[] dPos;
            bool isWall;
            IPoint endPos = new PointObj();

            #region Checker Loop
            foreach (IDrawable d in dr)
            {
                isWall = false;

                // Same tiles?
                if (source.equals(dest.origin))
                    return false;

                //Check if empty
                if ((dest.origin.equals(d.getPosition()) && (d.getId() % GameObjects.objectTypes == 1 /* LowBlock */
                    || GameObjects.objectTypes == 2) /*High Block*/ )) // Is there a block in dest position
                    return false;

                //Check distance
                dX = dest.origin.X - source.X;
                dY = dest.origin.Y - source.Y;
                if (Math.Abs(dX) > map.TileSize || Math.Abs(dY) > map.TileSize)//Not next to each other
                    return false;

                //Check corners
                if (Math.Abs(dX) == map.TileSize && Math.Abs(dY) == map.TileSize)
                    return false;

                //Get direction
                switch ((int)dX / map.TileSize)
                {
                    case 0://Up or down
                        if (dY < 0)
                            direction = 3;//down
                        if (dY > 0)
                            direction = 1;//up
                        break;
                    case 1://Right
                        direction = 2;
                        break;
                    case -1:
                        direction = 4;//Left
                        break;
                    default:
                        return false;
                }

                dPos = d.getPosition();
                //Check walls
                if (d.getId() % GameObjects.objectTypes == 3)/* LowWall */
                {
                    isWall = true;
                    endPos = ((LowWall)d).MyTiles[0, 0].MyEnd;
                }
                else if (d.getId() % GameObjects.objectTypes == 4) /*HighWall*/
                {
                    isWall = true;
                    endPos = ((HighWall)d).MyTiles[0, 0].MyEnd;
                }
                if (isWall)
                {
                    switch (direction)
                    {
                        case 1://up so wall must be horizontal, with origin x,y
                            if (dPos[0] == dest.origin.X && dPos[1] == dest.origin.Y &&
                                endPos.Y == dPos[1])
                                return false;
                            break;
                        case 2://right, so wall must be vertical, with origin x,y
                            if (dPos[0] == dest.origin.X && dPos[1] == dest.origin.Y &&
                                endPos.X == dPos[0])
                                return false;
                            break;
                        case 3://down, so wall must be horizontal, with origin x,y+tilesize
                            if (dPos[0] == dest.origin.X && dPos[1] == dest.origin.Y + map.TileSize &&
                                endPos.Y == dPos[1])
                                return false;
                            break;
                        case 4://left, so wall must be vertical, with origin x+tile,y
                            if (dPos[0] == dest.origin.X + map.TileSize && dPos[1] == dest.origin.Y &&
                                endPos.X == dPos[0])
                                return false;
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            return true;//If no return false in checker loop, then it must be valid dest
        }
        Tile findTile(int id)
        {
            foreach(tileObj t in Model.Map.MyTiles)
            {
                Tile tile=t as Tile;
                if(tile != null && tile.myId==id)
                    return tile;
            }
            return null;
        }
        /// <summary>
        /// Selects the given tile by darkening its color
        /// </summary>
        /// <param name="t"></param>
        public void selectTile(Tile t)
        {
            t.Shaded++;
        }
        /// <summary>
        /// Deselects given tile by lightening its color
        /// </summary>
        /// <param name="t"></param>
        public void deselectTile(Tile t)
        {
            t.Shaded--;
        }
        public void selectTile(int id)
        {
            Tile tile=findTile(id);
            if(tile!=null)
                tile.Shaded++;
        }
        public void deselectTile(int id)
        {
            Tile tile=findTile(id);
                tile.Shaded--;
        }

        /// <summary>
        /// Returns patrol with given id, if it exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PatrolPath findPatrol(int id)
        {
            return Patrols.Find(n => n.MyId == id);
        }
        /// <summary>
        /// Selects given path by darkening all its tiles
        /// </summary>
        /// <param name="path"></param>
        public void selectPath(PatrolPath path)
        {            
            if (path == null)
                return;
            if (path.MyWaypoints == null)
                return;

            foreach(Tile t in Model.Map.getTiles(path.MyWaypoints))
                t.Shaded++;
        }
        /// <summary>
        /// Deselects given path by lightening all its tiles
        /// </summary>
        /// <param name="path"></param>
        public void deselectPath(PatrolPath path)
        {            
            if (path == null)
                return;
            if (path.MyWaypoints == null)
                return;

            foreach(Tile t in Model.Map.getTiles(path.MyWaypoints))
                t.Shaded--;
        }

        public void deselectAllPaths()
        {
            foreach (PatrolPath p in Patrols)
                deselectPath(p);
        }

        /// <summary>
        /// Adds tile to end of patrol if possible
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="patrol"></param>
        public void addTileToPatrol(Tile tile,PatrolPath patrol)
        {
            if (tile != null && patrol != null)
            {
                if (patrol.Last() == null || allowDestination(Model.Map, patrol.Last(), tile))//first point?
                {
                    patrol.MyWaypoints.Add(tile.origin);
                    selectTile(tile);
                }                
            }
        }

        /// <summary>
        /// Removes given tile from patrol if it is the last one
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="patrol"></param>
        public void removeTileFromPatrol(Tile tile,PatrolPath patrol)
        {
            //if tile is last, delete it from path
            if (tile != null && patrol != null)
            {
                if (patrol.Last().equals(tile.MyOrigin))
                {
                    patrol.MyWaypoints.RemoveAt(patrol.MyWaypoints.Count - 1);
                    deselectTile(tile);
                }
            }
        }
        /// <summary>
        /// Returns whether pPath can be closed.
        /// Basically it checks whether the endpoints match
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public bool allowEnd(PatrolPath pPath)
        {
            return ((IPoint)pPath.MyWaypoints.ElementAt(0)).
                equals((IPoint)pPath.MyWaypoints.ElementAt(pPath.MyWaypoints.Count - 1));
        }
       
        /// <summary>
        /// Returns a string with the coordinates of all of the given path's waypoints
        /// (x1,y1)(x2,y2)...(xn,yn)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string getPathString(PatrolPath path)
        {
            string sPath = "";
            if (path.MyWaypoints != null)
            {
                sPath = "";
                foreach (IPoint point in path.MyWaypoints)
                    sPath += "(" + point.X.ToString() + "," + point.Y.ToString() + ")";
            }
            return sPath == "" ? null : sPath;
        }
        /// <summary>
        /// Adds a new path to list of paths in model, with name given by it's position in the list,
        /// and returns created path
        /// </summary>
        public PatrolPath createPath()
        {
            if (Patrols != null)
            {
                PatrolPath p = new PatrolPath("Patrol #" + Patrols.Count.ToString());
                Patrols.Add(p);
                return p;
            }
            return null;
        }
        /// <summary>
        /// Assigns given patrol to given guard if all conditions pass (endpoints adjacent to guard's position, no pointers are null)
        /// </summary>
        /// <param name="guard"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool assignPatrolToGuard(SneakingGuard guard, PatrolPath path)
        {
            //Check pointers
            if (path != null && path.MyWaypoints != null && path.MyWaypoints.Count > 0 && path.GuardOwners==0)
            {
                //Check endpoints of path are adjacent to guard's position
                if (Model.Map.areAdjacent(path.MyWaypoints.First(), guard.Position) &&
                    Model.Map.areAdjacent(path.MyWaypoints.Last(), guard.Position))
                {
                    guard.MyPatrol = path;
                    path.GuardOwners++;
                    path.createDirectionLines(Model.Map.TileSize);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Unassigns patrol from given guard, if no pointers are null
        /// </summary>
        /// <param name="guard"></param>
        /// <param name="path"></param>
        /// <returns>true if carried out, false if not carried out because a pointer was null</returns>
        public bool unassignePatrolToGuard(SneakingGuard guard)
        {
            if (guard != null && guard.MyPatrol != null)
            {
                guard.MyPatrol.GuardOwners--;
                guard.MyPatrol = null;
                return true;
            }

            return false;
        }
        #endregion

        #region ENTRY POINT STUFF

        /// <summary>
        /// Resets all entry point tiles to their original color
        /// </summary>
        public void resetAllEntryPoints()
        {
            foreach (IPoint p in Model.Map.EntryPoints)
                resetEntryPointTile(p);
        }
        /// <summary>
        /// Resets color of tile at entryPoint
        /// </summary>
        /// <param name="entryPoint"></param>
        public void resetEntryPointTile(IPoint entryPoint)
        {
           Tile tile= Model.Map.getTile(entryPoint);
            if(tile!=null)
                tile.MyColor = tile.OriginalColor;
        }

        /// <summary>
        /// Adds entry point to map with coordinates X,Y,0,
        ///  and returns it
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public IPoint addEntryPoint(int X, int Y)
        {
            IPoint point=new PointObj(X,Y,0);
            Model.Map.EntryPoints.Add(point);
            return point;
        }

        /// <summary>
        /// Removes entry point at index from list and resets color of tile
        /// it was in. Returns removed entry point
        /// </summary>
        /// <param name="index"></param>
        public IPoint removeEntryPoint(int index)
        {
            //Clear tile
            resetEntryPointTile(Model.Map.EntryPoints[index]);
            IPoint point = Model.Map.EntryPoints[index];
            //Remove from list
            Model.Map.EntryPoints.RemoveAt(index);
            return point;
        }

        /// <summary>
        /// Sets the color of the tile of entryPoint with index:index to selected color
        /// </summary>
        /// <param name="index"></param>
        public void selectEntryPoint(int index)
        {
            if (index > -1)
            {
                Tile entryTile = Model.Map.getTile(Model.Map.EntryPoints[index]);
                if (entryTile != null)
                    entryTile.MyColor = selectedEntryTileColor;
            }
        }
        #endregion


        public void saveFullMap(string filename)
        {
            XmlLoader.saveFullMap(filename, Model.Map);
        }

        public void deselectAllGuards()
        {
            foreach (SneakingGuard g in Model.Guards)
                deselectGuard(g);
        }
        /// <summary>
        /// Sets guard and it's patrol to visible
        /// </summary>
        /// <param name="guard"></param>
        public void selectGuard(SneakingGuard guard)
        {
            guard.Visible = true;
            selectPath(guard.MyPatrol);
        }

        /// <summary>
        /// Makes guard invisible and lightens patrol tiles
        /// </summary>
        /// <param name="guard"></param>
        public void deselectGuard(SneakingGuard guard)
        {
            guard.Visible = false;
            deselectPath(guard.MyPatrol);
        }

        public bool removePatrol(PatrolPath selectedPatrol)
        {
            return Patrols.Remove(selectedPatrol);
        }
    }
}
