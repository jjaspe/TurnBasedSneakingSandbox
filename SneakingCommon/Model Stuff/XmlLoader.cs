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

namespace SneakingCommon.Model_Stuff
{
    /// <summary>
    /// Used for loading model objects from xml files
    /// </summary>
    public class XmlLoader
    {
        static public List<SneakingGuard> getGuards(XmlDocument myDoc)
        {
            XmlNode positionNode, positionXNode, positionYNode,
               patrolNode, wpXNode, wpYNode, orientationNode;
            XmlNodeList guardNodes = myDoc.SelectNodes("Character"),
                waypointNodes;
            List<SneakingGuard> guards = new List<SneakingGuard>();
            SneakingGuard _cGuard;
            PatrolPath _cPath;
            #region READ GUARDS TO LIST
            if (guardNodes != null)
            {
                foreach (XmlNode guardNode in guardNodes)
                {
                    _cGuard = new SneakingGuard();
                    _cPath = new PatrolPath();

                    //Read Position,then save it
                    _cGuard.MyCharacter.fromXml(guardNode);
                    positionNode = ((XmlElement)guardNode).SelectNodes("Position")[0];
                    positionXNode = ((XmlElement)positionNode).SelectNodes("X")[0];
                    positionYNode = ((XmlElement)positionNode).SelectNodes("Y")[0];


                    _cGuard.Position = new pointObj(Int32.Parse(positionXNode.InnerText),
                                                      Int32.Parse(positionYNode.InnerText),
                                                      0);
                    //Read Patrol
                    //First, put guard position as first waypoint
                    _cPath.MyWaypoints.Add(_cGuard.Position);

                    //Add rest of waypoints
                    patrolNode = ((XmlElement)guardNode).SelectNodes("Patrol")[0];
                    if (patrolNode != null)
                    {
                        waypointNodes = ((XmlElement)patrolNode).SelectNodes("Waypoint");
                        foreach (XmlElement wp in waypointNodes)
                        {
                            wpXNode = wp.SelectNodes("X")[0];
                            wpYNode = wp.SelectNodes("Y")[0];
                            _cPath.MyWaypoints.Add(new pointObj(Int32.Parse(wpXNode.InnerText),
                                                                Int32.Parse(wpYNode.InnerText),
                                                                0));
                        }
                    }
                    //save patrol in guard
                    _cGuard.SneakingNPCBehavior.setPatrol(_cPath);

                    //Add guard to list
                    guards.Add(_cGuard);
                }
            }
            #endregion
            return guards;
        }

        static public SneakingMap getMap(XmlDocument myDoc)
        {
            SneakingMap newMap;
            #region CREATE MAP FROM DATA
            XmlNode mapNode=myDoc.SelectNodes("Map").Count==0?null:myDoc.SelectNodes("Map")[0],
                tileSizeNode, widthNode, lengthNode, originNode;
            int width,length,tileSize,originX,originY;
            if (mapNode != null)
            {
                lengthNode = mapNode.SelectNodes("Length").Count == 0 ? null : mapNode.SelectNodes("Length")[0];
                if(lengthNode==null)
                    throw new InvalidMapException("Length node", "loadMap");

                widthNode = mapNode.SelectNodes("Width").Count == 0 ? null : mapNode.SelectNodes("Width")[0];
                if (widthNode == null)
                    throw new InvalidMapException("Width node", "loadMap");

                tileSizeNode = mapNode.SelectNodes("Tile_Size").Count == 0 ? null : mapNode.SelectNodes("Tile_Size")[0];
                if (tileSizeNode == null)
                    throw new InvalidMapException("Tile_Size node", "loadMap");

                //Get values from nodes
                width = Int32.Parse(widthNode.InnerText);
                length = Int32.Parse(lengthNode.InnerText);
                tileSize = Int32.Parse(tileSizeNode.InnerText);

                originNode = mapNode.SelectNodes("Origin").Count == 0 ? null : mapNode.SelectNodes("Origin")[0];
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
                throw new InvalidMapException("mapNode", "loadMap");

            #endregion
            #region READ DISTANCE MAPS
            XmlNodeList distanceMapNodes = myDoc.SelectNodes("Distance_Map"), pointNodes;
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
            newMap.MyDistanceMaps = distanceMaps;
            #endregion
            return newMap;
        }
    }
}
