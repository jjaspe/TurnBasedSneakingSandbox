using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using OpenGlGameCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Model
{
    public interface IModelXmlLoader
    {
        List<IDrawableGuard> loadGuards(XmlDocument Xml);
        IDrawableGuard loadPC(XmlDocument Xml);
        ISneakingMap loadMap(XmlDocument Xml);
        void loadSystem(string filename);
        List<IGoal> loadGoals(XmlDocument Xml);
        void test();
    }
}
