using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces; 
using Canvas_Window_Template.Drawables;
using System.Xml;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;

namespace SneakingCommon.Data_Classes
{
    public class PatrolPath:OpenGlPath
    {
        
        int guardOwners = 0;

        public PatrolPath(string s)
            : base(s)
        {
        }

        public PatrolPath():base()
        {
        }
        public int GuardOwners
        {
            get { return guardOwners; }
            set { guardOwners = Math.Max(0, value); }
        }
        public XmlElement toXml(XmlDocument doc)
        {
            XmlElement top = doc.CreateElement("Patrol"), current;
            XmlNode positionXNode, positionYNode;
            foreach (IPoint p in myWaypoints)
            {
                current = doc.CreateElement("Waypoint");
                positionXNode = doc.CreateElement("X");
                positionYNode = doc.CreateElement("Y");
                positionXNode.InnerText = p.X.ToString();
                positionYNode.InnerText = p.Y.ToString();
                current.AppendChild(positionXNode);
                current.AppendChild(positionYNode);
                top.AppendChild(current);
            }
            return top;
        }

        public void createDirectionLines(int tileSize)
        {
            directionLines = new List<DirectionLine>();
            int offset = tileSize / 2;
            IPoint current, next;
            for (int i = 0; i < myWaypoints.Count - 1; i++)//Dont draw lines starting from last point
            {
                current = myWaypoints[i];
                next = myWaypoints[i + 1];
                directionLines.Add(new DirectionLine(new PointObj(current.X + offset, current.Y + offset, current.Z),
                    new PointObj(next.X + offset, next.Y + offset, next.Z)));

            }
        }

    }
}
