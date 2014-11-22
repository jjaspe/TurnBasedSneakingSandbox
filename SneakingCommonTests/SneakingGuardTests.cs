using SneakingCommon.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using Sneaking_Gameplay.Sneaking_Drawables;
using System.Collections.Generic;
using Canvas_Window_Template.Basic_Drawing_Functions;
using CharacterSystemLibrary.Classes;
using System.IO;
using System.Windows.Forms;

namespace SneakingCommonTests
{
    
    
    /// <summary>
    ///This is a test class for XmlLoaderTest and is intended
    ///to contain all XmlLoaderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SneakingGuardTests
    {
        private TestContext testContextInstance;
        String saveFilename = (System.Reflection.Assembly.GetExecutingAssembly().Location).
            Replace("SneakingCommonTests\\bin\\Debug\\SneakingCommonTests.dll", "TBSneaking Data\\Guards\\") + "saveGuardsTest.grd";

        /// <summary>
        /// Loads file at filename into doc
        /// </summary>
        /// <param name="filename">Complete path into the file </param>
        /// <param name="doc">XmlDocument to load file into</param>
        void loadGuardsFileWithFilename(String filename, ref XmlDocument doc)
        {
            FileStream mapFileReader;
            try
            {
                mapFileReader = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't open guards file:" + ex.Message);
                return;
            }

            doc = new XmlDocument();
            try
            {
                doc.Load(mapFileReader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't Load guards from file:" + ex.Message);
                return;
            }
        }

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
            XmlDocument myDoc = new XmlDocument(); 
            loadGuardsFileWithFilename(saveFilename,ref myDoc);
            List<SneakingGuard> guards= XmlLoader.loadGuards(myDoc);
            Assert.IsNotNull(guards);
            Assert.IsTrue(guards.Count > 0);
        }

        /// <summary>
        /// Save test, fails when it throws exception
        /// </summary>
        [TestMethod()]
        public void saveGuardsTest()
        {
            SneakingGuard g = new SneakingGuard(new pointObj(30, -20, 0), 10);
            g.MyCharacter = new Character();
            g.MyCharacter.addStat(new Stat("Strength", 20));
            XmlLoader.saveGuards(saveFilename, new List<SneakingGuard>() { g });
        }

        /// <summary>
        /// We create a character, save it, load it then compare the original and loaded
        /// </summary>
        [TestMethod()]
        public void saveLoadGuardsIntegrityTest()
        {
            //Create and save
            SneakingGuard expected = new SneakingGuard(new pointObj(30, -20, 0), 10);
            //expected.MyCharacter = new Character();
            expected.MyCharacter.addStat(new Stat("Blah", 20));
            expected.MyCharacter.addSkill(new Skill("Skill", 10));
            expected.MyCharacter.addAttribute(new CharacterSystemLibrary.Classes.Attribute("Att"));
            XmlLoader.saveGuards(saveFilename, new List<SneakingGuard>() { expected });

            //Load
            XmlDocument doc = new XmlDocument();
            loadGuardsFileWithFilename(saveFilename, ref doc);
            SneakingGuard actual = XmlLoader.loadGuards(doc)[0];

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.MyCharacter.Stats.Count, actual.MyCharacter.Stats.Count);
            Assert.AreEqual(expected.MyCharacter.Skills.Count, actual.MyCharacter.Skills.Count);
            Assert.AreEqual(expected.MyCharacter.Attributes.Count, actual.MyCharacter.Attributes.Count);
        }

    }
}
