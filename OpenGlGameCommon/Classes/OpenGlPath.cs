using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using System.Xml;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;

namespace OpenGlGameCommon.Classes
{
    public class OpenGlPath
    {
        static int patrolIds = 0;

        protected List<IPoint> myWaypoints;
        protected List<DirectionLine> directionLines;

        public List<DirectionLine> DirectionLines
        {
            get { return directionLines; }
            set { directionLines = value; }
        }
        int myId;

        public int MyId
        {
            get { return myId; }
        }
        public string name;
        public List<IPoint> MyWaypoints
        {
            get { return myWaypoints; }
            set { myWaypoints = value; }
        }
        public IPoint Last()
        {
            if (myWaypoints.Count > 0)
                return MyWaypoints[myWaypoints.Count - 1];
            else
                return null;
        }
        public OpenGlPath()
        {
            myWaypoints = new List<IPoint>();
            name = "patrol";
            myId = patrolIds;
            patrolIds++;
        }
        public OpenGlPath(string _name)
        {
            myWaypoints = new List<IPoint>();
            name = _name;
            myId = patrolIds;
            patrolIds++;
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
