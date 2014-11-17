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
    public class GoalKillNumberOfGuards:IGoal
    {
        public int NumberOfGuards { get; private set; }

        public GoalKillNumberOfGuards(int number)
        {
            NumberOfGuards = number;
        }
        public bool goalReached(ArgOwner argOwner)
        {
            List<IGuard> guards = (List<IGuard>)argOwner.getArg(ArgNames.guards);
            int n = 0;
            foreach (IGuard g in guards)
            {
                if (g.getValue("Dead") == 1)
                    n++;
            }
            return (n >= NumberOfGuards);
        }

        public XmlNode toXml(XmlDocument doc)
        {
            XmlNode goalNode = doc.CreateElement("Goal"),
                typeNode = doc.CreateElement("Goal_Type"),guardsNode=doc.CreateElement("Number_Of_Guards");
            typeNode.InnerText = GoalName.Guards.ToString();
            guardsNode.InnerText = NumberOfGuards.ToString();
            goalNode.AppendChild(typeNode);
            goalNode.AppendChild(guardsNode);
            return goalNode;
        }

        public string Description
        {
            get{return "Kill " + NumberOfGuards.ToString() + " Guards";}
        }
        static public GoalKillNumberOfGuards fromXml(XmlNode node)
        {
            int number = Int32.Parse(((XmlElement)node).
                GetElementsByTagName("Number_Of_Guards")[0].InnerText);
            GoalKillNumberOfGuards goal = new GoalKillNumberOfGuards(number);
            return goal;
        }
    }
}
