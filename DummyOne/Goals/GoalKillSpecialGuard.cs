using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Model;
using SneakingCommon.System_Classes;
using OpenGlGameCommon.Interfaces.Model;
using SneakingCommon.Enums;

namespace DummyOne.Goals
{
    public class GoalKillSpecialGuard:IGoal
    {
        string guardName;
        public GoalKillSpecialGuard(string name)
        {
            guardName = name;
        }
        public bool goalReached(ArgOwner argOwner)
        {
            List<IDrawableGuard> guards = (List < IDrawableGuard > )argOwner.getArg((int)ArgNames.guards);
            foreach (IDrawableGuard g in guards)
            {
                if (g.getName() == guardName && g.MyCharacter.getValue("Is Dead") == 1)
                    return true;
            }
            return false;
        }
        public XmlNode toXml(XmlDocument doc)
        {
            XmlNode goalNode = doc.CreateElement("Goal"),
                typeNode = doc.CreateElement("Goal_Type"), guardsNode = doc.CreateElement("Number_Of_Guards");
            typeNode.InnerText = GoalName.SpecialGuard.ToString();
            guardsNode.InnerText = guardName;
            goalNode.AppendChild(typeNode);
            goalNode.AppendChild(guardsNode);
            return goalNode;
        }

        public static GoalKillSpecialGuard fromXml(XmlNode node)
        {
            string name = ((XmlElement)node).
                GetElementsByTagName("Number_Of_Guards")[0].InnerText;
            GoalKillSpecialGuard goal = new GoalKillSpecialGuard(name);
            return goal;
        }

        public string Description
        {
            get { return "Kill " + guardName; ;}
        }
    }
}
