using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;                                     
using Canvas_Window_Template.Drawables;
using System.Xml;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Model;
using SneakingCommon.System_Classes;
using OpenGlGameCommon.Interfaces.Model;
using SneakingCommon.Enums;

namespace DummyOne.Goals
{
    public class GoalReachTileAndAction:IGoal
    {
        public IPoint TilePosition { get; private set; }
        public int APLeft {get;private set;}

        public GoalReachTileAndAction(IPoint tilePosition,int ap)
        {
            TilePosition =tilePosition;
            APLeft=ap;
        }
        public bool goalReached(ArgOwner argOwner)
        {
            IDrawableGuard pc=(IDrawableGuard)argOwner.getArg((int)ArgNames.PC);
            if(pc==null)
                return false;
            IPoint pcPosition = pc.Position;
            int ap=(int)pc.MyCharacter.getValue("Remaining AP");
            return (TilePosition.equals(pcPosition) && ap>=APLeft);
        }

        public XmlNode toXml(XmlDocument doc)
        {
            XmlNode goalNode=doc.CreateElement("Goal"), typeNode = doc.CreateElement("Goal_Type"), positionNode = doc.CreateElement("Position"),
                xNode = doc.CreateElement("X"), yNode = doc.CreateElement("Y"),apNode=doc.CreateElement("AP");
            typeNode.InnerText = GoalName.ReachTileAndAction.ToString();
            xNode.InnerText = TilePosition.X.ToString();
            yNode.InnerText = TilePosition.Y.ToString();
            apNode.InnerText = APLeft.ToString();
            goalNode.AppendChild(typeNode);
            positionNode.AppendChild(xNode);
            positionNode.AppendChild(yNode);
            goalNode.AppendChild(positionNode);
            goalNode.AppendChild(apNode);
            return goalNode;
        }

        public string Description
        {
            get
            {
                return "Get to (" + TilePosition.X.ToString() + "," +
                TilePosition.Y.ToString() + ") with " + APLeft.ToString() + " left";
            }
        }

        static public GoalReachTileAndAction fromXml(XmlNode node)
        {
            int x = Int32.Parse(((XmlElement)node).GetElementsByTagName("X")[0].InnerText);
            int y = Int32.Parse(((XmlElement)node).GetElementsByTagName("Y")[0].InnerText);
            int ap = Int32.Parse(((XmlElement)node).GetElementsByTagName("AP")[0].InnerText);
            IPoint point = new PointObj(x, y, 0);
            return new GoalReachTileAndAction(point,ap);
        }
    }
}
