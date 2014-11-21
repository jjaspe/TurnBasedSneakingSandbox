using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;
using SneakingClasses.Data_Classes;
using Sneaking_Gameplay.MVC_Interfaces;

namespace Sneaking_Gameplay.Model_Stuff
{
    public class World:Character,IModel
    {
        #region MODEL SUBJECT STUFF
        List<IModelObserver> myModelObservers;
        public void registerObserver(IModelObserver obs)
        {
            myModelObservers.Add(obs);
        }
        public void removeObserver(IModelObserver obs)
        {
            myModelObservers.Remove(obs);
        }
        public void notifyObservers(string msg, IModelSubject sub)
        {
            foreach (IModelObserver obs in myModelObservers)
                obs.update(msg, this);
        }

        public void setGuards(List<Guard> guards)
        {
            myGuards=guards;
        }
        public List<Guard> getGuards()
        {
            return myGuards;
        }
        #endregion

        List<Guard> myGuards;
        PC myPC;


        /// <summary>
        /// This list contains all distance maps, with the key being their origins, and value the actual map.
        /// </summary>
        List<KeyValuePair<pointObj, List<valuePoint>>> myDistanceMaps;
        public List<KeyValuePair<pointObj, List<valuePoint>>> MyDistanceMaps
        {
            get
            {
                return myDistanceMaps;
            }
            set { myDistanceMaps = value; }
        }
        public List<valuePoint> getDistanceMap(pointObj src)
        {
            return MyDistanceMaps.Find(
                 delegate(KeyValuePair<pointObj, List<valuePoint>> distMap)
                 {
                     return distMap.Key.equals(src);
                 }).Value;
        }


        public World()
        {
            myModelObservers = new List<IModelObserver>();
        }







        
    }
}
