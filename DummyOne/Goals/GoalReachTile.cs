using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;  
using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Basic_Drawing_Functions;
using System.Xml;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Model;
using SneakingCommon.System_Classes;
using OpenGlGameCommon.Interfaces.Model;
using SneakingCommon.Enums;

namespace DummyOne.Goals
{
    public class GoalReachTile:IGoal
    {
        public IPoint TilePosition { get; private set; }
        public GoalReachTile(IPoint tilePosition)
        {
            TilePosition = tilePosition;
        }
        public bool goalReached(ArgOwner argOwner)
        {
            IPoint pcPosition = ((IDrawableGuard)argOwner.getArg((int)ArgNames.PC)).Position;
            return (TilePosition.equals(pcPosition));
        }

        public XmlNode toXml(XmlDocument doc)
        {
            XmlNode goalNode=doc.CreateElement("Goal"),typeNode = doc.CreateElement("Goal_Type"), positionNode = doc.CreateElement("Position"),
                xNode = doc.CreateElement("X"), yNode = doc.CreateElement("Y");
            typeNode.InnerText = GoalName.ReachTile.ToString();
            xNode.InnerText = TilePosition.X.ToString();
            yNode.InnerText = TilePosition.Y.ToString();
            goalNode.AppendChild(typeNode);
            positionNode.AppendChild(xNode);
            positionNode.AppendChild(yNode);
            goalNode.AppendChild(positionNode);
            return goalNode;
        }

        public string Description
        {
            get { return "Get to (" + TilePosition.X.ToString() 
                + "," + TilePosition.Y.ToString() + ")"; }
        }

        static public GoalReachTile fromXml(XmlNode node)
        {
            int x=Int32.Parse(((XmlElement)node).GetElementsByTagName("X")[0].InnerText);
            int y=Int32.Parse(((XmlElement)node).GetElementsByTagName("Y")[0].InnerText);
            IPoint point = new PointObj(x, y, 0);
            return new GoalReachTile(point);
        }
    }
}
