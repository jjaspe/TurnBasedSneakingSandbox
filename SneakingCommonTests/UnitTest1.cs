using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sneaking_Gameplay.Sneaking_Drawables;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using SneakingCommon.Utility;
using SneakingCommon.Exceptions;
using Canvas_Window_Template.Drawables;

namespace SneakingCommonTests
{
    [TestClass]
    public class UnitTest1
    {
        XmlDocument fullMapDoc;
        XmlDocument bareMapDoc;
        String saveFileName = Path.GetDirectoryName("//") + "saveTestMap.mgg";

        void openMapFile(String title, ref XmlDocument doc)
        {
            OpenFileDialog mapDialog = new OpenFileDialog() { Title = title };
            mapDialog.Filter = "map Files (*.mgg)|*.mgg";
            mapDialog.DefaultExt = ".mgg";
            mapDialog.InitialDirectory = "//";
            String fileName = mapDialog.ShowDialog() == DialogResult.OK ? mapDialog.FileName : null;
            FileStream mapFileReader;
            if (fileName == null)
            {
                MessageBox.Show("Couldn't find file");
                return;
            }

            try
            {
                mapFileReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't Open Map");
                return;
            }

            doc = new XmlDocument();
            try
            {
                doc.Load(mapFileReader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't Open Map");
                return;
            }
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            openMapFile("Open Full Map", ref fullMapDoc);
            openMapFile("Open Bare Map", ref bareMapDoc);
        }

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
            int width = 10, length = 10, tileSize = 10;
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

        /// <summary>
        /// Make sure tests are working for failed scenario
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidMapException))]
        public void loadBareMapFailTest()
        {
            XmlDocument doc = new XmlDocument();
            SneakingMap map=XmlLoader.loadBareMap(doc);
        }        

        /// <summary>
        /// A test for loadBareMap (test whether map loaded,has the right dimensions and tileOrigins are initialized)
        /// </summary>
        [TestMethod]
        public void loadBareMapTest()
        {
            SneakingMap map = XmlLoader.loadBareMap(bareMapDoc);
            int width = map.MyWidth, length = map.MyLength;
            Assert.IsNotNull(map);
            Assert.IsInstanceOfType(map, typeof(SneakingMap));
            Assert.IsNotNull(map.TileOrigins);
            Assert.AreEqual(map.TileOrigins.Count, width * length);
            foreach (IPoint p in map.TileOrigins)
            {
                Assert.IsNotNull(p);
            }
            Assert.IsNotNull(map.Drawables);
        }


        /// <summary>
        /// A fail test for whether guards list was created. Make sure to load a map with no guards
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidMapException))]
        public void loadMapWithGuardsFailTest()
        {
            SneakingMap map = XmlLoader.loadMapWithGuards(bareMapDoc);
            Assert.IsNotNull(map.getGuards());
            Assert.IsTrue(map.getGuards().Count == 0);            
        }


        /// <summary>
        /// A pass test for whether guards list was created
        /// </summary>
        [TestMethod]
        public void loadMapWithGuardsTest()
        {
            SneakingMap map = XmlLoader.loadMapWithGuards(fullMapDoc);
            Assert.IsNotNull(map.getGuards());
            Assert.IsTrue(map.getGuards().Count > 0);
        }

        /// <summary>
        /// Makes sure loadFullMapTest is working by loading a bare map without guards or distance maps
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidMapException))]
        public void loadFullMapFailTest()
        {
            SneakingMap map = XmlLoader.loadFullMap(bareMapDoc);            
        }


        /// <summary>
        /// Tests that distance maps are getting loaded
        /// </summary>
        [TestMethod]
        public void loadFullMapTest()
        {
            SneakingMap map = XmlLoader.loadFullMap(fullMapDoc);
            Assert.IsTrue(map.DistanceMaps.Count>0);
        }


        /// <summary>
        /// Tests whether bare maps are saved correctly
        /// </summary>
        [TestMethod]
        public void saveBareTest()
        {
            SneakingMap map = XmlLoader.loadBareMap(bareMapDoc);
            XmlLoader.saveBareMap(saveFileName, map);
        }

        /// <summary>
        /// Makes sure test is working
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(BadFileNameException))]
        public void saveBareFailTest()
        {
            SneakingMap map = XmlLoader.loadBareMap(bareMapDoc);
            XmlLoader.saveBareMap("", map);
        }

    }
}
