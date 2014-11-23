using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SneakingCommon.Utility;
using SneakingCommon.Exceptions;

namespace SneakingCommonTests
{
    /// <summary>
    /// Summary description for GameSystemTest
    /// </summary>
    [TestClass]
    public class GameSystemTest
    {
        String filepath = (System.Reflection.Assembly.GetExecutingAssembly().Location).
            Replace("SneakingCommonTests\\bin\\Debug\\SneakingCommonTests.dll", "TBSneaking Data\\"),
            correctFilename="Stat To Skill Factors.txt",
            wrongFilename="World Stats.txt";
        public GameSystemTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        /// <summary>
        /// Should fail because bad filename
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(BadFileNameException))]
        public void loadSystemFailTest()
        {
            XmlLoader.loadSystem("empty");
        }

        
        /// <summary>
        /// Should fail because bad system
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidSystemException))]
        public void loadSystemFailTest2()
        {
            XmlLoader.loadSystem(filepath+wrongFilename);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void loadSystemTest()
        {
            XmlLoader.loadSystem(filepath + correctFilename);
        }
    }
}
