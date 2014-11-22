using SneakingCommon.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using Sneaking_Gameplay.Sneaking_Drawables;
using System.Collections.Generic;

namespace SneakingCommonTests
{
    
    
    /// <summary>
    ///This is a test class for XmlLoaderTest and is intended
    ///to contain all XmlLoaderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class XmlLoaderTest
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
        ///A test for getGuards
        ///</summary>
        [TestMethod()]
        public void getGuardsTest()
        {
            XmlNode myDoc = null; // TODO: Initialize to an appropriate value
            List<SneakingGuard> expected = null; // TODO: Initialize to an appropriate value
            List<SneakingGuard> actual;
            actual = XmlLoader.getGuards(myDoc);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

    }
}
