using OpenGlGameCommon.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using System.Collections.Generic;
using Canvas_Window_Template.Drawables;

namespace OpenGlMapTester
{
    
    
    /// <summary>
    ///This is a test class for OpenGlMapTest and is intended
    ///to contain all OpenGlMapTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OpenGlMapTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        

        /// <summary>
        ///A test for OpenGlMap Constructor
        ///</summary>
        [TestMethod()]
        public void ConstructorTest()
        {
            int width = 0; // TODO: Initialize to an appropriate value
            int length = 0; // TODO: Initialize to an appropriate value
            int _tileSize = 0; // TODO: Initialize to an appropriate value
            OpenGlMap target = new OpenGlMap(width, length, _tileSize,null);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for OpenGlMap MyTiles
        ///</summary>
        [TestMethod()]
        public void MyTilesNotNullTest()
        {
            int width = 0; // TODO: Initialize to an appropriate value
            int length = 0; // TODO: Initialize to an appropriate value
            int _tileSize = 0; // TODO: Initialize to an appropriate value
            OpenGlMap map = new OpenGlMap(width, length, _tileSize,null);
            object target = map.MyTiles;
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///Tests to see if Drawables have the right size after creation and additions
        ///</summary>
        [TestMethod()]
        public void DrawablesRightSizeTestOne()
        {
            int width = 0; // TODO: Initialize to an appropriate value
            int length = 0; // TODO: Initialize to an appropriate value
            int _tileSize = 0; // TODO: Initialize to an appropriate value
            IPoint origin = new pointObj(0, 0, 0);
            int expected = 0;
            OpenGlMap map = new OpenGlMap(width, length, _tileSize,origin);
            List<IDrawable> target = map.Drawables;
            int actual = target.Count;
            Assert.AreEqual(expected,actual);

            List<IDrawable> l = new List<IDrawable> { new LowBlock() };
            map.addDrawables(l);
            expected=1;
            map.MyHeight=length;
            map.MyWidth = width;
            map.CreateTiles();
            target = map.Drawables;
            actual = target.Count;
            Assert.AreEqual(expected, actual);


            map.addDrawables(l);
            expected = 2;
            map.MyHeight = length;
            map.MyWidth = width;
            map.CreateTiles();
            target = map.Drawables;
            actual = target.Count;
            Assert.AreEqual(expected, actual);
            
        }


        /// <summary>
        /// A test to see if Drawables Lists is not null
        /// </summary>
        ///  [TestMethod()]
        public void DrawablesNotNullTest()
        {
            int width = 0; // TODO: Initialize to an appropriate value
            int length = 0; // TODO: Initialize to an appropriate value
            int _tileSize = 0; // TODO: Initialize to an appropriate value
            OpenGlMap map = new OpenGlMap(width, length, _tileSize, null);
            object target = map.Drawables;
            Assert.IsNotNull(target);
        }

        /// <summary>
        /// Tests to see if MyTiles is has the right size after creation and resizes
        /// </summary>
        [TestMethod()]
        public void MyTilesRightSizeTestOne()
        {
            int width = 0; // TODO: Initialize to an appropriate value
            int length = 0; // TODO: Initialize to an appropriate value
            int _tileSize = 0; // TODO: Initialize to an appropriate value
            IPoint origin = new pointObj(0, 0, 0);
            int expected = width * length;
            OpenGlMap map = new OpenGlMap(width, length, _tileSize, origin);
            tileObj[,] target = map.MyTiles;
            int actual = target.Length;
            Assert.AreEqual(expected,actual);

            length=3;
            width=5;
            expected=length*width;
            map.MyHeight=length;
            map.MyWidth = width;
            map.CreateTiles();
            target = map.MyTiles;
            actual = target.Length;
            Assert.AreEqual(expected, actual);

            length = 40;
            width = 40;
            expected = length * width;
            map.MyHeight = length;
            map.MyWidth = width;
            map.CreateTiles();
            target = map.MyTiles;
            actual = target.Length;
            Assert.AreEqual(expected, actual);
            
        }




    }
}
