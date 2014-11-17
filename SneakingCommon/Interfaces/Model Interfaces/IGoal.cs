using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;        
using Canvas_Window_Template.Drawables;
using System.Xml;
using OpenGlGameCommon.Classes;

namespace SneakingCommon.Interfaces.Model
{
    public interface IGoal
    {
        bool goalReached(ArgOwner argOwner);
        XmlNode toXml(XmlDocument node);
        string Description
        {
            get;
        }
    }
}
