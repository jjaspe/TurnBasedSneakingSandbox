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
using Canvas_Window_Template.Interfaces;
using SneakingCommon.Exceptions;

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
        static String savePath=(System.Reflection.Assembly.GetExecutingAssembly().Location).
            Replace("SneakingCommonTests\\bin\\Debug\\SneakingCommonTests.dll", "TBSneaking Data\\");
        static String guardsFilename = savePath+"Guards\\saveGuardsTest.grd",guardMapFilename=savePath+"Maps\\saveGuardMapTest.mgp";
        

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

        void loadMapFileWithFilename(String filename, ref XmlDocument doc)
        {
            FileStream mapFileReader;
            try
            {
                mapFileReader = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't open Map file:" + ex.Message);
                return;
            }

            doc = new XmlDocument();
            try
            {
                doc.Load(mapFileReader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't Load Map from file:" + ex.Message);
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
            loadGuardsFileWithFilename(guardsFilename,ref myDoc);
            List<SneakingGuard> guards= XmlLoader.loadGuardsFromDocument(myDoc);
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
            XmlLoader.saveGuards(guardsFilename, new List<SneakingGuard>() { g });
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
            XmlLoader.saveGuards(guardsFilename, new List<SneakingGuard>() { expected });

            //Load
            XmlDocument doc = new XmlDocument();
            loadGuardsFileWithFilename(guardsFilename, ref doc);
            SneakingGuard actual = XmlLoader.loadGuardsFromDocument(doc)[0];

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.MyCharacter.Stats.Count, actual.MyCharacter.Stats.Count);
            Assert.AreEqual(expected.MyCharacter.Skills.Count, actual.MyCharacter.Skills.Count);
            Assert.AreEqual(expected.MyCharacter.Attributes.Count, actual.MyCharacter.Attributes.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidMapException))]
        public void saveGuardMapFailTest()
        {
            XmlDocument doc = new XmlDocument();
            loadMapFileWithFilename(guardsFilename, ref doc);
            SneakingMap actual = XmlLoader.loadGuardsMap(doc);
        }

        [TestMethod()]
        public void saveGuardMapTest()
        {
            SneakingMap expected = SneakingMap.createInstance(20, 20, 10, new pointObj(0, 0, 0));
            SneakingGuard g = new SneakingGuard(new pointObj(30, 20, 0), 10),g1=new SneakingGuard(new pointObj(40, 20, 0),10);
            
            g.MyCharacter = new Character();
            g.MyCharacter.addStat(new Stat("Strength", 20));
            expected.addDrawables(new List<IDrawable> { g,g1 });
            XmlLoader.saveGuardMap(guardMapFilename,expected);

            XmlDocument doc=new XmlDocument();
            loadMapFileWithFilename(guardMapFilename,ref doc);
            SneakingMap actual = XmlLoader.loadGuardsMap(doc);

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.getGuards());
            Assert.IsTrue(actual.getGuards().Count>0);
            Assert.AreEqual(expected.getGuards().Count,actual.getGuards().Count);
        }

    }
}
