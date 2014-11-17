using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.Model_Stuff;
using System.Xml;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface IModelXmlLoader
    {
        List<IGuard> loadGuards(XmlDocument Xml);
        IGuard loadPC(XmlDocument Xml);
        IMap loadMap(XmlDocument Xml);
        void loadSystem(string filename);
        void test();
    }
}
