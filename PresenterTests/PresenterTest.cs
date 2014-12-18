using SneakingCreationWithForms.MVP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace PresenterTests
{
    
    
    /// <summary>
    ///This is a test class for PresenterTest and is intended
    ///to contain all PresenterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PresenterTest
    {
        String filepath = (System.Reflection.Assembly.GetExecutingAssembly().Location).
            Replace("PresenterTests\\bin\\Debug\\PresenterTests.dll", "TBSneaking Data\\Maps");

        XmlDocument guardMapDoc;


        void openMapFileWithFilename(String filename, ref XmlDocument doc)
        {
            FileStream mapFileReader;
            try
            {
                mapFileReader = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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
            guardMapDoc = new XmlDocument();
            openMapFileWithFilename(filepath+"\\TestMap.mgp", ref guardMapDoc);

            //openMapFileWithDialog("Open Full Map", ref fullMapDoc);
            //openMapFileWithDialog("Open Bare Map", ref bareMapDoc);
        }
        /// <summary>
        ///A test for loadGuardsMap
        ///</summary>
        [TestMethod()]
        public void loadedGuardsCorrectSizeTest()
        {
            Presenter target = new Presenter(); // TODO: Initialize to an appropriate value
            target.Model = new ExampleModel();

            target.loadGuardsMap(guardMapDoc);
            Assert.IsTrue(target.Model.Guards[0].MySize > 0);
            Assert.IsTrue(target.Model.Guards[0].MySize == target.Model.Map.TileSize / 2);           
        }
    }
}
