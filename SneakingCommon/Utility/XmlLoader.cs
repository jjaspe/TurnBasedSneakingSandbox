using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Sneaking_Gameplay.Sneaking_Drawables;
using OpenGLGameCommon.Classes;
using OpenGlGameCommon.Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;
using OpenGlGameCommon.Drawables;
using OpenGlGameCommon.Data_Classes;
using SneakingCommon.Exceptions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.System_Classes;
using System.IO;

namespace SneakingCommon.Utility
{
    /// <summary>
    /// Used for loading model objects from xml files
    /// </summary>
    public class XmlLoader
    {
       

        /// <summary>
        /// Loads guards from xml and puts them in list
        /// </summary>
        /// <param name="myDoc"></param>
        /// <returns></returns>
        static public List<SneakingGuard> loadGuardsFromDocument(XmlDocument myDoc)
        {
            XmlNode guardListNode = myDoc.DocumentElement.GetElementsByTagName("Guard_List")[0];
            return getGuardsFromNode(guardListNode);
        }
        /// <summary>
        /// Returns list of guards created from guard nodes in myDoc
        /// </summary>
        /// <param name="myDoc"></param>
        /// <returns></returns>
        static List<SneakingGuard> getGuardsFromNode(XmlNode myDoc)
        {
            XmlNodeList guardNodes = myDoc.SelectNodes("Character");
            List<SneakingGuard> guards = new List<SneakingGuard>();
            SneakingGuard currentGuard;
            if (guardNodes != null)
            {
                foreach (XmlNode guardNode in guardNodes)
                {
                    currentGuard = loadGuardFromNode(guardNode); 

                    //Add guard to list
                    guards.Add(currentGuard);
                }
            }
            return guards;
        }

        /// <summary>
        /// Takes a SneakingMap:map without geometry, and it loads geometry objects
        /// from my doc and adds them to map
        /// </summary>
        /// <param name="myDoc">xml file to read geometry from</param>
        /// <param name="map">SneakingMap to add geometry to</param>
        /// <returns>Modified map</returns>
        static SneakingMap addGeometryToMap(XmlDocument myDoc, SneakingMap map)
        {
            var tileSize = map.TileSize;
            #region ADD MAP COMPONENTS

            XmlNodeList lowWallNodes = myDoc.GetElementsByTagName("LowWall");
            XmlNode positionNode, positionXNode, positionYNode, orientationNode;
            LowWall _cLowWall;
            List<LowWall> lowWalls = new List<LowWall>();
            if (lowWallNodes != null)
            {
                foreach (XmlNode g in lowWallNodes)
                {
                    positionNode = ((XmlElement)g).GetElementsByTagName("Position")[0];
                    positionXNode = ((XmlElement)positionNode).GetElementsByTagName("X")[0];
                    positionYNode = ((XmlElement)positionNode).GetElementsByTagName("Y")[0];
                    orientationNode = ((XmlElement)g).GetElementsByTagName("Orientation")[0];

                    _cLowWall = new LowWall(Int32.Parse(positionYNode.InnerText),
                                            Int32.Parse(positionXNode.InnerText),
                                            Int32.Parse(positionXNode.InnerText) + tileSize,
                                            tileSize);
                    if (Int32.Parse(orientationNode.InnerText) == 1)
                        _cLowWall.turn();
                    lowWalls.Add(_cLowWall);
                }
                map.addDrawables(lowWalls.ToList<IDrawable>());
            }

            XmlNodeList highWallNodes = myDoc.GetElementsByTagName("HighWall");
            HighWall _cHighWall;
            List<HighWall> highWalls = new List<HighWall>();
            if (highWallNodes != null)
            {
                foreach (XmlNode g in highWallNodes)
                {
                    positionNode = ((XmlElement)g).GetElementsByTagName("Position")[0];
                    positionXNode = ((XmlElement)positionNode).GetElementsByTagName("X")[0];
                    positionYNode = ((XmlElement)positionNode).GetElementsByTagName("Y")[0];
                    orientationNode = ((XmlElement)g).GetElementsByTagName("Orientation")[0];

                    _cHighWall = new HighWall(Int32.Parse(positionYNode.InnerText),
                                            Int32.Parse(positionXNode.InnerText),
                                            Int32.Parse(positionXNode.InnerText) + tileSize,
                                            tileSize);
                    if (Int32.Parse(orientationNode.InnerText) == 1)
                        _cHighWall.turn();
                    highWalls.Add(_cHighWall);
                }
                map.addDrawables(highWalls.ToList<IDrawable>());
            }

            XmlNodeList lowBlockNodes = myDoc.GetElementsByTagName("LowBlock");
            LowBlock _cLowBlock;
            List<LowBlock> lowBlocks = new List<LowBlock>();
            if (lowBlockNodes != null)
            {
                foreach (XmlNode g in lowBlockNodes)
                {
                    positionNode = ((XmlElement)g).GetElementsByTagName("Position")[0];
                    positionXNode = ((XmlElement)positionNode).GetElementsByTagName("X")[0];
                    positionYNode = ((XmlElement)positionNode).GetElementsByTagName("Y")[0];

                    _cLowBlock = new LowBlock(new int[]{Int32.Parse(positionXNode.InnerText),
                                                        Int32.Parse(positionYNode.InnerText),
                                                        0}, tileSize);
                    lowBlocks.Add(_cLowBlock);
                }
                map.addDrawables(lowBlocks.ToList<IDrawable>());
            }

            XmlNodeList highBlockNodes = myDoc.GetElementsByTagName("HighBlock");
            HighBlock _cHighBlock;
            List<HighBlock> highBlocks = new List<HighBlock>();
            if (highBlockNodes != null)
            {
                foreach (XmlNode g in highBlockNodes)
                {
                    positionNode = ((XmlElement)g).GetElementsByTagName("Position")[0];
                    positionXNode = ((XmlElement)positionNode).GetElementsByTagName("X")[0];
                    positionYNode = ((XmlElement)positionNode).GetElementsByTagName("Y")[0];

                    _cHighBlock = new HighBlock(new int[]{Int32.Parse(positionXNode.InnerText),
                                                        Int32.Parse(positionYNode.InnerText),
                                                        0}, tileSize);
                    highBlocks.Add(_cHighBlock);
                }
                map.addDrawables(highBlocks.ToList<IDrawable>());
            }
            #endregion
            return map;
        }

        /// <summary>
        /// Loads a map with geometry (blocks, walls,etc), but without guards or distance maps
        /// </summary>
        /// <param name="myDoc"></param>
        /// <returns></returns>
        static public SneakingMap loadBareMap(XmlDocument myDoc)
        {
            SneakingMap newMap;
            #region CREATE MAP FROM DATA
            XmlNode mapNode=myDoc.GetElementsByTagName("Map").Count==0?null:myDoc.GetElementsByTagName("Map")[0],
                tileSizeNode, widthNode, lengthNode, originNode;
            int width,length,tileSize,originX,originY;
            XmlElement mapElement = (XmlElement)mapNode;
            if (mapNode != null)
            {
                lengthNode = mapElement.GetElementsByTagName("Length").Count == 0 ? null : mapElement.GetElementsByTagName("Length")[0];
                if(lengthNode==null)
                    throw new InvalidMapException("Length node", "loadMap");

                widthNode = mapElement.GetElementsByTagName("Width").Count == 0 ? null : mapElement.GetElementsByTagName("Width")[0];
                if (widthNode == null)
                    throw new InvalidMapException("Width node", "loadMap");

                tileSizeNode = mapElement.GetElementsByTagName("Tile_Size").Count == 0 ? null : mapElement.GetElementsByTagName("Tile_Size")[0];
                if (tileSizeNode == null)
                    throw new InvalidMapException("Tile_Size node", "loadMap");

                //Get values from nodes
                width = Int32.Parse(widthNode.InnerText);
                length = Int32.Parse(lengthNode.InnerText);
                tileSize = Int32.Parse(tileSizeNode.InnerText);

                originNode = mapElement.GetElementsByTagName("Origin").Count == 0 ? null : mapElement.GetElementsByTagName("Origin")[0];
                if (originNode == null)//No origin, so put origin at -width*tileSize/2,-length*tileSize/3
                {
                    
                    originX=-width*tileSize/2;
                    originY=-length*tileSize/2;                    
                }
                else
                {
                    originX = Int32.Parse(originNode.SelectNodes("X")[0].InnerText);
                    originY = Int32.Parse(originNode.SelectNodes("Y")[0].InnerText);
                }

                newMap = SneakingMap.createInstance(width, length, tileSize, new pointObj(originX, originY, 0));


            }
            else
                throw new InvalidMapException("mapNode", "loadBareMap");

            #endregion
            addGeometryToMap(myDoc, newMap);
            return newMap;
        }

        /// <summary>
        /// Loads a map with geometry and guards, but without distance maps
        /// </summary>
        /// <param name="myDoc"></param>
        /// <returns></returns>
        static public SneakingMap loadMapWithGuards(XmlDocument myDoc)
        {
            SneakingMap newMap=loadBareMap(myDoc);
            XmlNode guardListNode = myDoc.GetElementsByTagName("Guard_List").Count==0?null:myDoc.GetElementsByTagName("Guard_List")[0];

            if (guardListNode == null)
                throw new InvalidMapException("GuardListNode", "loadMapWithGuards");
            else
            {
                List<SneakingGuard> guards=getGuardsFromNode(guardListNode);
                newMap.addDrawables<SneakingGuard>(ref guards);
            }
            return newMap;
        }

        /// <summary>
        /// Loads a map with geometry,guards,distance maps.
        /// </summary>
        /// <param name="myDoc"></param>
        /// <returns></returns>
        static public SneakingMap loadFullMap(XmlDocument myDoc)
        {
            SneakingMap map = loadMapWithGuards(myDoc);
            XmlNode distanceList = myDoc.GetElementsByTagName("Distance_Maps").Count==0?null:
            myDoc.GetElementsByTagName("Distance_Maps")[0];
            if (distanceList == null)
                throw new InvalidMapException("Distance_Maps Node", "loadFullMap");

            #region READ DISTANCE MAPS
            XmlNodeList distanceMapNodes = distanceList.SelectNodes("Distance_Map"), pointNodes;
            XmlNode sourceNode, sourcePositionXNode, sourcePositionYNode, currentPointPositionNode,
                currentPointPositionXNode, currentPointPositionYNode, currentPointValueNode;
            List<DistanceMap> distanceMaps =
                new List<DistanceMap>();
            DistanceMap currentMap = new DistanceMap();
            foreach (XmlNode distanceMapNode in distanceMapNodes)
            {
                //Get source
                sourceNode = ((XmlElement)distanceMapNode).SelectNodes("Source_Point").Item(0);
                sourcePositionXNode = ((XmlElement)sourceNode).SelectNodes("Position_X").Item(0);
                sourcePositionYNode = ((XmlElement)sourceNode).SelectNodes("Position_Y").Item(0);

                pointNodes = ((XmlElement)distanceMapNode).SelectNodes("Distance_Point");
                currentMap = new DistanceMap();
                foreach (XmlNode distancePointNode in pointNodes)
                {
                    currentPointPositionNode =
                        ((XmlElement)distancePointNode).SelectNodes("Position").Item(0);
                    currentPointPositionXNode =
                        ((XmlElement)currentPointPositionNode).SelectNodes("Position_X").Item(0);
                    currentPointPositionYNode =
                        ((XmlElement)currentPointPositionNode).SelectNodes("Position_Y").Item(0);
                    currentPointValueNode =
                        ((XmlElement)distancePointNode).SelectNodes("Value").Item(0);

                    //Create and add valuePoint
                    currentMap.Add(new valuePoint(new pointObj(
                        currentPointPositionXNode.InnerText.ToString(),
                        currentPointPositionYNode.InnerText.ToString(), "0"),
                        Int32.Parse(currentPointValueNode.InnerText)));
                }

                currentMap.MyOrigin = new pointObj(sourcePositionXNode.InnerText,
                    sourcePositionYNode.InnerText, "0");
                distanceMaps.Add(currentMap);
            }
            map.DistanceMaps = distanceMaps;
            #endregion

            return map;

        }

        static SneakingGuard loadGuardFromNode(XmlNode guardNode)
        {
            XmlNode positionNode, positionXNode, positionYNode,
               patrolNode, wpXNode, wpYNode;
            XmlNodeList waypointNodes;
            PatrolPath guardPatrolPath;

            SneakingGuard guard = new SneakingGuard();
            guardPatrolPath = new PatrolPath();

            //Load guard stats
            guard.MyCharacter.fromXml(guardNode);
            //Read Position,then save it
            positionNode = ((XmlElement)guardNode).SelectNodes("Position")[0];
            positionXNode = ((XmlElement)positionNode).SelectNodes("X")[0];
            positionYNode = ((XmlElement)positionNode).SelectNodes("Y")[0];


            guard.Position = new pointObj(Int32.Parse(positionXNode.InnerText),
                                              Int32.Parse(positionYNode.InnerText),
                                              0);
            //Read Patrol
            //First, put guard position as first waypoint
            guardPatrolPath.MyWaypoints.Add(guard.Position);

            //Add rest of waypoints
            patrolNode = ((XmlElement)guardNode).SelectNodes("Patrol")[0];
            if (patrolNode != null)
            {
                waypointNodes = ((XmlElement)patrolNode).SelectNodes("Waypoint");
                foreach (XmlElement wp in waypointNodes)
                {
                    wpXNode = wp.SelectNodes("X")[0];
                    wpYNode = wp.SelectNodes("Y")[0];
                    guardPatrolPath.MyWaypoints.Add(new pointObj(Int32.Parse(wpXNode.InnerText),
                                                        Int32.Parse(wpYNode.InnerText),
                                                        0));
                }
            }
            guard.MyPatrol = guardPatrolPath;
            return guard;
        }

        /// <summary>
        /// Loads stat to skill factors data to create a game system from file at filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public GameSystem loadSystem(string filename)
        {
            XmlDocument myXml = new XmlDocument();
            try
            {
                myXml.Load(filename);
            }
            catch (Exception e)
            {
                throw new BadFileNameException("", "loadSystem" + e.Message);
            }



            GameSystem system = GameSystem.getInstance();
            //Get factors
            try
            {
                #region READ_NODES
                XmlNode APFactorNode = myXml.GetElementsByTagName("APFactor")[0],
                    FoVFactorNode = myXml.GetElementsByTagName("FoVFactor")[0],
                    FoHFactorNode = myXml.GetElementsByTagName("FoHFactor")[0],
                    SPFactorNode = myXml.GetElementsByTagName("SPFactor")[0],
                    APConstantNode = myXml.GetElementsByTagName("APConstant")[0],
                    FoVConstantNode = myXml.GetElementsByTagName("FoVConstant")[0],
                    SPConstantNode = myXml.GetElementsByTagName("SPConstant")[0],
                    FoHConstantNode = myXml.GetElementsByTagName("FoHConstant")[0],
                    HealthConstantNode = myXml.GetElementsByTagName("HealthConstant")[0],
                    HealthFactorNode = myXml.GetElementsByTagName("HealthFactor")[0];

                system.APFactor = double.Parse(APFactorNode.InnerText);
                system.FoVFactor = double.Parse(FoVFactorNode.InnerText);
                system.FoHFactor = double.Parse(FoHFactorNode.InnerText);
                system.SPFactor = double.Parse(SPFactorNode.InnerText);
                system.APConstant = double.Parse(APConstantNode.InnerText);
                system.FoVConstant = double.Parse(FoVConstantNode.InnerText);
                system.FoHConstant = double.Parse(FoHConstantNode.InnerText);
                system.SPConstant = double.Parse(SPConstantNode.InnerText);
                system.HealthConstant = double.Parse(HealthConstantNode.InnerText);
                system.HealthFactor = double.Parse(HealthFactorNode.InnerText);
                #endregion
            }
            catch (Exception e)
            {
                throw new InvalidSystemException("XmlLoader:loadSystem");
            }
            return system;
        }

        static public void saveGuards(String filename, List<SneakingGuard> guards)
        {
            XmlDocument creator = new XmlDocument();

            XmlWriter xWriter;
            try
            {
                xWriter = new XmlTextWriter(filename, null);
            }
            catch (Exception e)
            {
                throw new GuardsInvalidException("Couldn't open save file", "XmlLoader:saveGuards");
            }
            xWriter.WriteStartElement("Root");
            xWriter.Close();
            creator.Load(filename);

            //get root
            XmlNode root = creator.DocumentElement;
            
            XmlNode guardListNode = getGuardsNode(guards,creator);            

            //Save
            root.AppendChild(guardListNode);
            creator.Save(filename);
        }
        /// <summary>
        /// Saves a SneakingMap:map with geometry only (Tiles,Blocks,Walls) to filename
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="map"></param>
        static public void saveBareMap(String filename,SneakingMap map)
        {
            XmlDocument creator = new XmlDocument();

            XmlWriter xWriter;
            try
            {
                xWriter = new XmlTextWriter(filename, null);
            }
            catch (Exception e)
            {
                throw new BadFileNameException("map filename:" + filename, "Save Bare Map");
            }
            xWriter.WriteStartElement("Root");
            xWriter.Close();
            creator.Load(filename); 
            
            XmlNode root = creator.DocumentElement;
            

            root.AppendChild(getBareMapNode(map,creator));

            creator.Save(filename);
        }

        static public void saveGuardMap(String filename, SneakingMap map, List<SneakingGuard> guards)
        {
            XmlDocument creator = new XmlDocument();

            XmlWriter xWriter;
            try
            {
                xWriter = new XmlTextWriter(filename, null);
            }
            catch (Exception e)
            {
                throw new BadFileNameException("map filename:" + filename, "Save Guard Map");
            }
            xWriter.WriteStartElement("Root");
            xWriter.Close();
            creator.Load(filename);

            XmlNode root = creator.DocumentElement;

            root.AppendChild(getGuardsNode(guards, creator));
            root.AppendChild(getBareMapNode(map, creator));
            creator.Save(filename);
           
        }

        static XmlNode getBareMapNode(SneakingMap map,XmlDocument creator)
        {
            XmlNode mapNode = creator.CreateElement("Map"),widthNode=creator.CreateElement("Width"),lengthNode=
                creator.CreateElement("Length"),tileSizeNode=creator.CreateElement("Tile_Size");

            widthNode.InnerText = map.MyWidth.ToString();
            lengthNode.InnerText = map.MyLength.ToString();
            tileSizeNode.InnerText = map.TileSize.ToString();

            mapNode.AppendChild(widthNode.Clone());
            mapNode.AppendChild(lengthNode.Clone());
            mapNode.AppendChild(tileSizeNode.Clone());

            List<XmlNode> geometryNodes = getGeometryNodes(map, creator);
            foreach (XmlNode node in geometryNodes)
                mapNode.AppendChild(node.Clone());

            return mapNode;
        }
        static XmlNode getGuardsNode(List<SneakingGuard> guards,XmlDocument creator)
        {
            XmlNode guardsNode = creator.CreateElement("Guard_List"), currentGuardNode;

            //Get nodes
            XmlElement idNode = creator.CreateElement("Id"),
                positionNode = creator.CreateElement("Position"),
                positionXNode = creator.CreateElement("X"),
                positionYNode = creator.CreateElement("Y");

            //Get guard data, add it to guard node, add guard node to list node
            foreach (SneakingGuard g in guards)
            {
                currentGuardNode = g.MyCharacter.toXml(creator);
                idNode.InnerText = g.getId().ToString();
                positionXNode.InnerText = g.getPosition()[0].ToString();
                positionYNode.InnerText = g.getPosition()[1].ToString();
                positionNode.AppendChild(positionXNode.Clone());
                positionNode.AppendChild(positionYNode.Clone());
                currentGuardNode.AppendChild(idNode);
                currentGuardNode.AppendChild(positionNode);
                guardsNode.AppendChild(currentGuardNode.Clone());
            }

            return guardsNode;

        }
        /// <summary>
        /// Loads wall and block from map and returns them their nodes
        /// </summary>
        /// <param name="map"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        static List<XmlNode> getGeometryNodes(SneakingMap map,XmlDocument creator)
        {
            List<XmlNode> list = new List<XmlNode>();
            XmlElement current, cPositionNode, cOrientationNode, cPositionXNode, cPositionYNode;
            double[] cPosition;

            #region COMPONENT NODES CREATION
            foreach (IDrawable drw in map.Drawables)
            {
                cPositionNode = creator.CreateElement("Position");
                cOrientationNode = creator.CreateElement("Orientation");
                cPositionXNode = creator.CreateElement("X");
                cPositionYNode = creator.CreateElement("Y");

                cPosition = drw.getPosition();
                cPositionXNode.InnerText = cPosition[0].ToString();
                cPositionYNode.InnerText = cPosition[1].ToString();
                cPositionNode.AppendChild(cPositionXNode.Clone());
                cPositionNode.AppendChild(cPositionYNode.Clone());

                switch (drw.getId() % GameObjects.objectTypes)
                {
                    case Tile.idType:
                        current = creator.CreateElement("Tile");
                        break;
                    case LowBlock.idType:
                        current = creator.CreateElement("LowBlock");
                        break;
                    case HighBlock.idType:
                        current = creator.CreateElement("HighBlock");
                        break;
                    case HighWall.idType:
                        current = creator.CreateElement("HighWall");
                        cOrientationNode.InnerText = ((int)((HighWall)drw).Orientation).ToString();
                        current.AppendChild(cOrientationNode.Clone());
                        break;
                    case LowWall.idType:
                        current = creator.CreateElement("LowWall");
                        cOrientationNode.InnerText = ((int)((LowWall)drw).Orientation).ToString();
                        current.AppendChild(cOrientationNode.Clone());
                        break;
                    default:
                        current = creator.CreateElement("null");
                        break;
                }

                if (!current.Name.Equals("null"))//Dont add drawn guards, already added
                {
                    current.AppendChild(cPositionNode.Clone());
                    list.Add(current.Clone());
                }
            }
            #endregion

            return list;
        }

        


        #region OBSOLOTE METHODS

        /// <summary>
        /// Obsolote method that turns SneakingMap into a character array
        /// with different characters for each geometry object. Abandoned for xml methods.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        static public char[] mapToCharArray(SneakingMap map)
        {
            //e:empty tile
            //b:low block
            //h:high block
            //f:no wall
            //l:low wall
            //L:high wall
            int w = map.MyWidth, h = map.MyHeight;
            char[] cA = new char[(2 * w + 1) * 2 * h];
            char[,] tileA = new char[w, h], wallA = new char[2 * w + 1, h];
            int size = map.TileSize;

            tileObj temp;

            //Create temp arrays for tiles and walls
            #region FILL temp arrays
            foreach (IDrawable obj in (map as IWorld).getEntities())
            {
                switch (obj.getId() % GameObjects.objectTypes)
                {
                    case 0: // Tile, for origin=(x,y), put in (2*width+1)*2*y+2*x
                        temp = ((tileObj)obj);
                        tileA[((int)temp.MyOrigin.X / size + w / 2), ((int)temp.MyOrigin.Y / size + h / 2)] = 'e';
                        break;
                    case 1: // lowBlock, same place as tile
                        tileA[(int)(((LowBlock)obj).MyOrigin.X / size + w / 2),
                            ((int)((LowBlock)obj).MyOrigin.Y / size + h / 2)] = 'b';
                        break;
                    case 2: // highBlock, same place as tile
                        tileA[((int)((HighBlock)obj).MyOrigin.X / size + w / 2),
                            ((int)((HighBlock)obj).MyOrigin.Y / size + h / 2)] = 'h';
                        break;
                    case 3: // lowWall, 
                        //Check orientation
                        temp = ((LowWall)obj).MyTiles[0, 0];
                        if (temp.MyEnd.X == temp.MyOrigin.X) // Vertical, 
                            wallA[2 * ((int)temp.MyOrigin.X / size + w / 2), ((int)temp.MyOrigin.Y / size + h / 2)] = 'l';
                        else if (temp.MyEnd.X == temp.MyOrigin.X + size) // Horizontal,
                            wallA[2 * ((int)temp.MyOrigin.X / size + w / 2) + 1, ((int)temp.MyOrigin.Y / size + h / 2)] = 'l';
                        break;
                    case 4: // highWall
                        temp = ((HighWall)obj).MyTiles[0, 0];
                        if (temp.MyEnd.X == temp.MyOrigin.X) // Vertical, 
                            wallA[2 * ((int)temp.MyOrigin.X / size + w / 2), ((int)temp.MyOrigin.Y / size + h / 2)] = 'L';
                        else if (temp.MyEnd.X == temp.MyOrigin.X + size) // Horizontal,
                            wallA[2 * ((int)(int)temp.MyOrigin.X / size + w / 2) + 1, (int)(temp.MyOrigin.Y / size + h / 2)] = 'L';
                        break;
                    default:
                        break;
                }
            }
            //Fill in empty walls
            for (int i = 0; i < 2 * w + 1; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (wallA[i, j] == '\0')
                        wallA[i, j] = 'f';
                }
            }
            #endregion

            //Fill character array using temp arrays
            #region FILL
            for (int j = 0; j < 2 * h; j++)
            {
                for (int i = 0; i < 2 * w + 1; i++)
                {
                    if (j % 2 == 0)//wall row
                    {
                        if (i % 2 == 0)//empty
                            cA[j * (2 * w + 1) + i] = ' ';
                        else if (i % 2 == 1)// horizontal wall, get value from wallA
                            cA[j * (2 * w + 1) + i] = wallA[i, j / 2];
                    }
                    else if (j % 2 == 1)//Tile row
                    {
                        if (i % 2 == 0)//vertical wall, get value from wallA at previous row
                            cA[j * (2 * w + 1) + i] = wallA[i, (j - 1) / 2];
                        if (i % 2 == 1)//tile or block, get value from tileA
                            cA[j * (2 * w + 1) + i] = tileA[(i - 1) / 2, (j - 1) / 2];
                    }
                }
                //Put endline char at the end of each row
                cA[(j + 1) * (2 * w + 1) - 1] = '\r';
            }
            #endregion


            return cA;
        }

        static public OpenGlMap loadMap(string filename)
        {
            int w, h, size;
            StreamReader sr = new StreamReader(filename);
            //First read map dimensions and tile size
            string data = sr.ReadLine();
            int firstComa = data.IndexOf(','), secondComa = data.Substring(firstComa + 1).IndexOf(',') + firstComa;
            w = Int32.Parse(data.Substring(0, firstComa));
            h = Int32.Parse(data.Substring(firstComa + 1, secondComa - firstComa));
            size = Int32.Parse(data.Substring(secondComa + 2, data.Length - secondComa - 2));
            //Now read map
            data = sr.ReadToEnd();
            

            //Create temp Lists
            char[] charMap = data.ToCharArray();
            char[,] tiles = new char[w, h];
            char[,] walls = new char[2 * w, h];
            int xOffset = -w * size / 2, yOffset = -h * size / 2;
            List<Tile> dTiles = new List<Tile>();
            List<LowBlock> dLBlocks = new List<LowBlock>();
            List<HighBlock> dHBlocks = new List<HighBlock>();
            List<LowWall> dLWall = new List<LowWall>();
            List<HighWall> dHWall = new List<HighWall>();

            List<IDrawable> drawables = new List<IDrawable>();


            //Fill temp lists from charMap
            for (int j = 0; j < 2 * h; j++)
            {
                for (int i = 0; i < 2 * w; i++)
                {
                    if (j % 2 == 0)//wall row
                    {
                        if (i % 2 == 1)// horizontal wall,
                            walls[i, j / 2] = charMap[j * (2 * w + 1) + i];
                    }
                    else if (j % 2 == 1)//Tile row
                    {
                        if (i % 2 == 0)//vertical wall, get value from walls at previous row
                            walls[i, (j - 1) / 2] = charMap[j * (2 * w + 1) + i];
                        if (i % 2 == 1)//tile or block, get value from tiles
                            tiles[(i - 1) / 2, (j - 1) / 2] = charMap[j * (2 * w + 1) + i];
                    }
                }
            }

            //Create objArrays from tempLists
            IPoint or;
            LowWall tempLWall;
            HighWall tempHWall;
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < 2 * w; i++)
                {
                    if (i % 2 == 0) //Only add tiles once every two values of i
                    {
                        if (tiles[i / 2, j] == 'b')//add low block, use i,j for origin coords
                        {
                            or = new PointObj(i / 2 * size + xOffset, j * size + yOffset, 0);
                            drawables.Add(new LowBlock(or, size, Common.colorRed, Common.colorRed));
                        }
                        else if (tiles[i / 2, j] == 'h')// add High Block
                        {
                            or = new PointObj(i / 2 * size + xOffset, j * size + yOffset, 0);
                            drawables.Add(new HighBlock(or, size, Common.colorRed, Common.colorRed));
                        }
                        else if (walls[i, j] == 'l')// add low wall
                        {
                            //Horizontal wall
                            tempLWall = new LowWall(j * size + yOffset, i / 2 * size + xOffset,
                                (i / 2 + 1) * size + xOffset, size);
                            tempLWall.turn();
                            drawables.Add(tempLWall);
                        }
                        else if (walls[i, j] == 'L')// add low wall
                        {
                            //Horizontal wall
                            tempHWall = new HighWall(j * size + yOffset, i / 2 * size + xOffset,
                               (i / 2 + 1) * size + xOffset, size);
                            tempHWall.turn();
                            drawables.Add(tempHWall);
                        }
                    }
                    else if (i % 2 == 1) //Only add walls
                    {
                        if (walls[i, j] == 'l')// add low wall
                        {
                            //Vertical wall
                            drawables.Add(new LowWall(j * size + yOffset, (i - 1) / 2 * size + xOffset,
                                ((i - 1) / 2 + 1) * size + xOffset, size));
                        }
                        else if (walls[i, j] == 'L')// add low wall
                        {
                            //Vertical wall
                            drawables.Add(new HighWall(j * size + yOffset, (i - 1) / 2 * size + xOffset,
                                ((i - 1) / 2 + 1) * size + xOffset, size));
                        }
                    }

                }
            }

            //Now create map and add drawables loaded
            SneakingMap newMap = SneakingMap.createInstance(w,h,size,null);
            newMap.addDrawables(drawables);

            return newMap;

        }

        #endregion


    }
}
