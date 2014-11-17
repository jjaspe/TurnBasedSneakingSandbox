using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SneakingCommon.System_Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;

using SneakingCommon.System_Classes;
using OpenGlGameCommon.Classes;
using SneakingCommon.Data_Classes;

namespace SneakingCommon.Model_Stuff
{
    public class XmlLoader
    {
        static public List<Guard> getGuards(XmlDocument myDoc)
        {
            XmlNode positionNode, positionXNode, positionYNode,
               patrolNode, wpXNode, wpYNode, orientationNode;
            XmlNodeList guardNodes = myDoc.SelectNodes("Character"),
                waypointNodes;
            List<Guard> guards=new List<Guard>();
            Guard _cGuard;
            PatrolPath _cPath;
            #region READ GUARDS TO LIST
            if (guardNodes != null)
            {
                foreach (XmlNode g in guardNodes)
                {
                    _cGuard = new Guard();
                    _cPath = new PatrolPath();
                    //Read Position
                    _cGuard.fromXml(g);
                    positionNode = ((XmlElement)g).SelectNodes("Position")[0];
                    positionXNode = ((XmlElement)positionNode).SelectNodes("X")[0];
                    positionYNode = ((XmlElement)positionNode).SelectNodes("Y")[0];

                    
                    _cGuard.MyPosition = new PointObj(Int32.Parse(positionXNode.InnerText),
                                                      Int32.Parse(positionYNode.InnerText),
                                                      0);
                    //Read Patrol
                    //First, put guard position as first waypoint
                    _cPath.MyWaypoints.Add(_cGuard.MyPosition);

                    //Add rest of waypoints
                    patrolNode = ((XmlElement)g).SelectNodes("Patrol")[0];
                    if (patrolNode != null)
                    {
                        waypointNodes = ((XmlElement)patrolNode).SelectNodes("Waypoint");
                        foreach (XmlElement wp in waypointNodes)
                        {
                            wpXNode = wp.SelectNodes("X")[0];
                            wpYNode = wp.SelectNodes("Y")[0];
                            _cPath.MyWaypoints.Add(new PointObj(Int32.Parse(wpXNode.InnerText),
                                                                Int32.Parse(wpYNode.InnerText),
                                                                0));
                        }
                    }
                    //save patrol in guard
                    _cGuard.getNPCBehavior().setPatrol(_cPath);

                    //Add guard to list
                    guards.Add(_cGuard);
                }
            }
            #endregion
            return guards;
        }

        static public Map getMap(XmlDocument myDoc)
        {
            Map newMap = new Map();
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
                    currentMap.Add(new valuePoint(new PointObj(
                        currentPointPositionXNode.InnerText.ToString(),
                        currentPointPositionYNode.InnerText.ToString(), "0"),
                        Int32.Parse(currentPointValueNode.InnerText)));
                }

                currentMap.MyOrigin = new PointObj(sourcePositionXNode.InnerText,
                    sourcePositionYNode.InnerText, "0");
                distanceMaps.Add(currentMap);
            }
            newMap.MyDistanceMaps = distanceMaps;
            #endregion
            return newMap;
        }
    }
}
