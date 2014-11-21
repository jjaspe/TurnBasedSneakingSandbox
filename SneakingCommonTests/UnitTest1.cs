using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sneaking_Gameplay.Sneaking_Drawables;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;

namespace SneakingCommonTests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Creates a map and tests the following:
        /// Map isnt null
        /// Map is the right type
        /// TileOrigins is created and not null
        /// TileOrigins has the right count
        /// No point of tileOrigins is null
        /// </summary>
        [TestMethod]
        public void MapCreationTest()
        {
            int width=10,length=10,tileSize=10;
            SneakingMap map = SneakingMap.createInstance(width, length, tileSize, new pointObj(0, 0, 0));
            Assert.IsNotNull(map);
            Assert.IsInstanceOfType(map, typeof(SneakingMap));
            Assert.IsNotNull(map.TileOrigins);
            Assert.AreEqual(map.TileOrigins.Count, width * length);
            foreach (IPoint p in map.TileOrigins)
            {
                Assert.IsNotNull(p);
            }
        }

        [TestMethod]
        public void MapCreationTest()
        {
            SneakingMap map = SneakingMap.createInstance(width, length, tileSize, new pointObj(0, 0, 0));
            Assert.IsNotNull(map);
            Assert.IsInstanceOfType(map, typeof(SneakingMap));
            Assert.IsNotNull(map.TileOrigins);
            Assert.AreEqual(map.TileOrigins.Count, width * length);
            foreach (IPoint p in map.TileOrigins)
            {
                Assert.IsNotNull(p);
            }
        }
    }
}
