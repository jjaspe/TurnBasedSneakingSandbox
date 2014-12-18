using SneakingCreationWithForms.MVP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using Sneaking_Gameplay.Sneaking_Drawables;
using OpenGlGameCommon.Data_Classes;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;

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
        XmlDocument patrolMapDoc;


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
            openMapFileWithFilename(filepath+"\\EmptySmall.mgp", ref guardMapDoc);
            openMapFileWithFilename(filepath + "\\EmptySmall.mpt", ref patrolMapDoc);


            //openMapFileWithDialog("Open Full Map", ref fullMapDoc);
            //openMapFileWithDialog("Open Bare Map", ref bareMapDoc);
        }
        /// <summary>
        ///A test for loadGuardsMap, to see if guards are getting their size set correctly when loaded
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

        /// <summary>
        ///A test to check if patrol maps are getting their distance maps loaded correctly
        ///</summary>
        [TestMethod()]
        public void distanceMapsLoadedCorrectly()
        {
            Presenter target = new Presenter(); // TODO: Initialize to an appropriate value
            target.Model = new ExampleModel();

            target.loadPatrolMap(patrolMapDoc);
            Assert.IsTrue(target.Model.Map.DistanceMaps!=null);
            Assert.IsTrue(target.Model.Map.DistanceMaps.Count>0);
        }

        /// <summary>
        ///Fail test for assignPatrolToGuard
        ///</summary>
        [TestMethod()]
        public void guardPatrolSavedAndLoadedCorrectlyTestFail()
        {
            Presenter target = new Presenter(); // TODO: Initialize to an appropriate value
            target.Model = new ExampleModel();

            target.loadPatrolMap(patrolMapDoc);
            SneakingGuard guard = target.Model.Guards[0];
            IPoint position = guard.Position;
            PatrolPath p = new PatrolPath();
            p.MyWaypoints.Add(new PointObj(position.X + 2*target.Model.Map.TileSize, position.Y, position.Z));
            Assert.IsFalse(target.assignPatrolToGuard(guard, p));
        }

        /// <summary>
        ///A test for assignPatrolToGuard
        ///</summary>
        [TestMethod()]
        public void assignPatrolToGuardTest()
        {
            Presenter target = new Presenter(); // TODO: Initialize to an appropriate value
            target.Model = new ExampleModel();

            target.loadPatrolMap(patrolMapDoc);
            SneakingGuard guard = target.Model.Guards[0];
            IPoint position=guard.Position;
            PatrolPath p = new PatrolPath();
            p.MyWaypoints.Add(new PointObj(position.X + target.Model.Map.TileSize, position.Y, position.Z));
            Assert.IsTrue(target.assignPatrolToGuard(guard, p));
        }

        /// <summary>
        ///A test for saving and loading patrols
        ///</summary>
        [TestMethod()]
        public void saveGuardEmptyPatrolTest()
        {
            Presenter target = new Presenter(); // TODO: Initialize to an appropriate value
            target.Model = new ExampleModel();
            target.loadPatrolMap(patrolMapDoc);

            //Get guard and his position
            SneakingGuard guard = target.Model.Guards[0];
            IPoint position = guard.Position;

            guard.MyPatrol = new PatrolPath();

            //Save map, change cursor so we know when it is done
            Cursor.Current = Cursors.WaitCursor;
            target.savePatrolMap(filepath + "\\EmptySmall.mpt");
            Cursor.Current = Cursors.Arrow;

            //load map again
            openMapFileWithFilename(filepath + "\\EmptySmall.mpt", ref patrolMapDoc);
            target.Model = new ExampleModel();
            target.loadPatrolMap(patrolMapDoc);
            //get guard again
            guard = target.Model.Guards[0];

            //Check it's patrol
            Assert.IsTrue(guard.MyPatrol == null);
        }

        

        /// <summary>
        ///A test for saving and loading patrols
        ///</summary>
        [TestMethod()]
        public void saveGuardPatrolTest()
        {
            Presenter target = new Presenter(); // TODO: Initialize to an appropriate value
            target.Model = new ExampleModel();
            target.loadPatrolMap(patrolMapDoc);

            //Get guard and his position
            SneakingGuard guard = target.Model.Guards[0];
            IPoint position = guard.Position;

            //Create patrol, assign one waypoint, assign to guard
            PatrolPath p = new PatrolPath();
            p.MyWaypoints.Add(new PointObj(position.X + target.Model.Map.TileSize, position.Y, position.Z));
            target.assignPatrolToGuard(guard, p);

            //Save map, change cursor so we know when it is done
            Cursor.Current = Cursors.WaitCursor;
            target.savePatrolMap(filepath + "\\EmptySmall.mpt");
            Cursor.Current = Cursors.Arrow;

            //load map again
            openMapFileWithFilename(filepath + "\\EmptySmall.mpt", ref patrolMapDoc);
            target.Model = new ExampleModel();
            target.loadPatrolMap(patrolMapDoc);
            //get guard again
            guard = target.Model.Guards[0];

            //Check it's patrol
            Assert.IsTrue(guard.MyPatrol != null);
            Assert.IsTrue(guard.MyPatrol.MyWaypoints.Count > 0);
            Assert.IsTrue(guard.MyPatrol.MyWaypoints.Count == 2);
        }


    }
}
