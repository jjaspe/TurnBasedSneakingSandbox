using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

namespace SneakingCommon.Interfaces.Model
{
    public interface IModelXmlLoader
    {
        List<IGuard> loadGuards(XmlDocument Xml);
        IGuard loadPC(XmlDocument Xml);
        IMap loadMap(XmlDocument Xml);
        void loadSystem(string filename);
        List<IGoal> loadGoals(XmlDocument Xml);
        void test();
    }
}
