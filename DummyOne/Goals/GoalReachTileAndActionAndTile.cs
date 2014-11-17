using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;                                                                                                                            
using Canvas_Window_Template.Drawables;
using System.Xml;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Model;
using SneakingCommon.System_Classes;

namespace DummyOne.Goals
{
    public class GoalReachTileAndActionAndTile:IGoal
    {
        IGoal reachFirstTile,reachSecondTile;
        bool firstGoalReached = false;

        public GoalReachTileAndActionAndTile(IPoint firstTile, int ap, IPoint secondTile)
        {
            if (firstTile != null && secondTile != null && ap >= 0)
            {
                reachFirstTile = new GoalReachTileAndAction(firstTile, ap);
                reachSecondTile = new GoalReachTile(secondTile);
            }
        }
        public bool goalReached(ArgOwner argOwner)
        {
            if (!firstGoalReached)
            {
                if (reachFirstTile.goalReached(argOwner))
                {
                    firstGoalReached = true;
                    return reachSecondTile.goalReached(argOwner);
                }
                else
                    return false;
            }
            else
                return reachSecondTile.goalReached(argOwner);
        }

        public XmlNode toXml(XmlDocument doc)
        {
            XmlNode goalNode=doc.CreateElement("Goal"), typeNode = doc.CreateElement("Goal_Type"),
                firstGoalNode = reachFirstTile.toXml(doc),
                secondGoalNode = reachSecondTile.toXml(doc),
                firstPositionNode = ((XmlElement)firstGoalNode).GetElementsByTagName("Position")[0],
                secondPositionNode = ((XmlElement)secondGoalNode).GetElementsByTagName("Position")[0],
                firstXNode = ((XmlElement)firstPositionNode).GetElementsByTagName("X")[0],
                firstYNode = ((XmlElement)firstPositionNode).GetElementsByTagName("Y")[0],
                secondXNode = ((XmlElement)secondPositionNode).GetElementsByTagName("X")[0],
                secondYNode = ((XmlElement)secondPositionNode).GetElementsByTagName("Y")[0],
                apNode = ((XmlElement)firstGoalNode).GetElementsByTagName("AP")[0];
                

            typeNode.InnerText = GoalName.ReachTileAndActionAndTile.ToString();
            firstPositionNode = secondPositionNode = null;
            firstPositionNode = doc.CreateElement("First_Position");
            firstPositionNode.AppendChild(firstXNode);
            firstPositionNode.AppendChild(firstYNode);
            secondPositionNode = doc.CreateElement("Second_Position");
            secondPositionNode.AppendChild(secondXNode);
            secondPositionNode.AppendChild(secondYNode);
            goalNode.AppendChild(typeNode);
            goalNode.AppendChild(firstPositionNode);
            goalNode.AppendChild(apNode);
            goalNode.AppendChild(secondPositionNode);
            return goalNode;
        }

        public string Description
        {
            get
            {
                return reachFirstTile.Description + ", then " +
                reachSecondTile.Description;
            }
        }

        static public GoalReachTileAndActionAndTile fromXml(XmlNode node)
        {
            XmlNode position1Node = ((XmlElement)node).GetElementsByTagName("First_Position")[0],
                position2Node = ((XmlElement)node).GetElementsByTagName("Second_Position")[0];

            int x1 = Int32.Parse(((XmlElement)position1Node).GetElementsByTagName("X")[0].InnerText),
                x2=Int32.Parse(((XmlElement)position2Node).GetElementsByTagName("X")[0].InnerText);
            int y1 = Int32.Parse(((XmlElement)position1Node).GetElementsByTagName("Y")[0].InnerText),
                y2 = Int32.Parse(((XmlElement)position2Node).GetElementsByTagName("Y")[0].InnerText);
            int ap = Int32.Parse(((XmlElement)node).GetElementsByTagName("AP")[0].InnerText);
            IPoint point1 = new PointObj(x1, y1, 0),
                point2 = new PointObj(x2, y2, 0);
            return new GoalReachTileAndActionAndTile(point1, ap,point2);
        }
    }
}
