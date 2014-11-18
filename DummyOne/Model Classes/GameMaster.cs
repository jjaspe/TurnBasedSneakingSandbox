using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;     
using Canvas_Window_Template.Drawables;
using System.Xml;
using SneakingCommon.System_Classes;
using SneakingCommon.Data_Classes;
using OpenGlGameCommon.Classes;
using SneakingCommon.Interfaces.Observer_Pattern;
using SneakingCommon.Interfaces.Model;
using DummyOne.Goals;

namespace SneakingCommon.Model_Stuff
{
    public class GameMasterOne:IGameMaster,IModelXmlLoader
    {
        public static int dataTypes=3;

        static GameMasterOne myInstance;
        public static GameMasterOne createInstance()
        {
            if (myInstance == null)
            {
                myInstance = new GameMasterOne();
            }
            return myInstance;
        }
        public static GameMasterOne getInstance()
        {
            return myInstance;
        }

        List<IGuard> myGuards;
        IGuard myPC;
        ISneakingMap myMap;
        NoiseMap myNoiseMap;
        List<IGuard> mySortedGuards;

        
        

        private GameMasterOne()
        {
            myObservers = new List<IModelObserver>();
        }

        #region IGameMaster
        public List<IGuard> Guards
        {
            get { return myGuards; }
            set { myGuards = value; }
        }
        public IGuard MyPC
        {
            get { return myPC; }
            set { myPC = value; }
        }
        public ISneakingMap MyMap
        {
            get { return myMap; }
            set { myMap = value; }
        }
        public NoiseMap MyNoiseMap
        {
            get { return myNoiseMap; }
            set { myNoiseMap = value; }
        }
        public bool GameStarted
        {
            get { return MyMap.getValue("Game Started") == 1; }
            set { MyMap.setValue("Game Started", value == false ? 0 : 1); }
        }
        public void setNoiseMap(NoiseMap nM)
        {
            MyNoiseMap = nM;
        }       
        public IGuard ActiveGuard
        {
            get
            {
                foreach (Guard g in Guards)
                {
                    if (g.getValue("Active") == 1)
                        return g;
                }
                return null;
            }
        }
        public List<IGuard> getSortedGuards()
        {
            List<IGuard> guards = new List<IGuard>();
            foreach (Guard g in mySortedGuards)
                guards.Add(g);
            return guards;
        }
        public void sortGuards(string statName)
        {
            //Key is original index, value is index in new array
            List<KeyValuePair<int, int>> indeces = new List<KeyValuePair<int, int>>();

            //Fill indeces
            for (int i = 0; i < Guards.Count; i++)
            {
                indeces.Add(new KeyValuePair<int, int>(i, i));
            }

            //Insertion Sort
            try
            {
                int j = 0;
                int temp = 0;
                for (int i = 1; i < Guards.Count; i++)
                {
                    temp = Guards[i].getValue(statName);
                    j = i - 1;
                    while (j >= 0 && temp > Guards[j].getValue(statName))
                    {
                        //Move up Value of lower dex guard                        
                        indeces[j] = new KeyValuePair<int, int>(indeces[j].Key, indeces[j].Value + 1);
                        j--;

                    }
                    //Move down Value of higher dex guard
                    indeces[i] = new KeyValuePair<int, int>(indeces[i].Key, indeces[j + 1].Key);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Stat:" + statName + " doesn't exist \n"+e.Message);
            }

            //Create sorted guard array
            mySortedGuards = new List<Guard>();
            foreach (KeyValuePair<int, int> kv in indeces)
                mySortedGuards.Add(Guards[kv.Value]);
        }
        public void commitChanges()
        {
            notifyObservers(null, this);
        }

        #region HELPER METHODS
        public bool tileFreeFromGuards(IPoint tilePosition)
        {
            foreach (Guard g in Guards)
            {
                if (g.MyPosition.equals(tilePosition))
                    return false;
            }
            return true;
        }
        #endregion
        #endregion

        #region IModelSubject

        List<IModelObserver> myObservers;
        public void registerObserver(IModelObserver obs)
        {
            myObservers.Add(obs);
        }

        public void removeObserver(IModelObserver obs)
        {
            myObservers.Remove(obs);
        }

        public void notifyObservers(string msg, IModelSubject sub)
        {
            foreach (IModelObserver obs in myObservers)
                obs.update(msg, this);

        }
        #endregion 

        #region ILOADER
        public List<IGuard> loadGuards(XmlDocument myDoc)
        {
            XmlNode positionNode, positionXNode, positionYNode,
               patrolNode, wpXNode, wpYNode, orientationNode;
            XmlNodeList guardNodes = myDoc.GetElementsByTagName("Character"),
                waypointNodes;
            Guards = new List<Guard>();
            Guard _cGuard;
            PatrolPath _cPath;
            #region READ GUARDS TO LIST
            if (guardNodes != null)
            {
                foreach (XmlNode g in guardNodes)
                {
                    _cGuard = new Guard();
                    _cPath = new PatrolPath();
                   
                    _cGuard.fromXml(g);
                    _cGuard.setStat("Status Alert", SneakingWorld.getValueByName("Status Alert"));
                    _cGuard.setStat("Status Suspicious", SneakingWorld.getValueByName("Status Suspicious"));
                    _cGuard.setStat("Status Normal", SneakingWorld.getValueByName("Status Normal"));

                    //Read Position
                    positionNode = ((XmlElement)g).SelectNodes("Position")[0];
                    positionXNode = ((XmlElement)positionNode).SelectNodes("X")[0];
                    positionYNode = ((XmlElement)positionNode).SelectNodes("Y")[0];


                    _cGuard.MyPosition = new PointObj(Int32.Parse(positionXNode.InnerText),
                                                      Int32.Parse(positionYNode.InnerText),
                                                      0);
                    //Read Patrol
                    //First, put guard position as first waypoint
                    _cPath.MyWaypoints.Add(_cGuard.MyPosition);

                    //Add rest of waypoints
                    patrolNode = ((XmlElement)g).SelectNodes("Patrol")[0];
                    if (patrolNode != null)
                    {
                        waypointNodes = ((XmlElement)patrolNode).GetElementsByTagName("Waypoint");
                        foreach (XmlElement wp in waypointNodes)
                        {
                            wpXNode = wp.SelectNodes("X")[0];
                            wpYNode = wp.SelectNodes("Y")[0];
                            _cPath.MyWaypoints.Add(new PointObj(Int32.Parse(wpXNode.InnerText),
                                                                Int32.Parse(wpYNode.InnerText),
                                                                0));
                        }
                    }
                    //save patrol in guard
                    _cGuard.getNPCBehavior().setPatrol(_cPath);

                    //Add guard to list
                    Guards.Add(_cGuard);
                }
            }
            #endregion
            List<IGuard> iguards = new List<IGuard>();
            foreach (Guard g in Guards)
                iguards.Add(g);

            return iguards;
        }
        void setNPCBehaviorOnGuards()
        {
            foreach (Guard g in Guards)
                g.setNPCBehavior(new NPCBehaviorNPC());
        }
        public IGuard loadPC(XmlDocument myDoc)
        {
            XmlNode pcNode = myDoc.GetElementsByTagName("Character")[0];
            string attackPath = "C:/TBSneaking/Attacks.txt";
            XmlDocument myAttackDoc = new XmlDocument();
            try
            {
                myAttackDoc.Load(attackPath);
            }
            catch(Exception e)
            {
                return null;
            }

            if (pcNode != null)
            {
                MyPC = new PC();
                MyPC.fromXml(pcNode);
                MyPC.MyAttacks = loadAttacks(myAttackDoc);
                return MyPC;
            }
            return null;            
        }
        public ISneakingMap loadMap(XmlDocument myDoc)
        {
            MyMap = new Map();
            
            #region READ DISTANCE MAPS
            XmlNodeList distanceMapNodes = myDoc.GetElementsByTagName("Distance_Map"), pointNodes;
            XmlNode sourceNode, sourcePositionXNode, sourcePositionYNode, currentPointPositionNode,
                currentPointPositionXNode, currentPointPositionYNode, currentPointValueNode;
            XmlNodeList tester;
            List<DistanceMap> distanceMaps =
                new List<DistanceMap>();
            DistanceMap currentMap = new DistanceMap();
            int test = 0;
            
            foreach (XmlNode distanceMapNode in distanceMapNodes)
            {
                test = 0;
                //Get source
                sourceNode = ((XmlElement)distanceMapNode).SelectNodes("Source_Point").Item(0);
                sourcePositionXNode = ((XmlElement)sourceNode).SelectNodes("Position_X").Item(0);
                sourcePositionYNode = ((XmlElement)sourceNode).SelectNodes("Position_Y").Item(0);
                pointNodes = ((XmlElement)distanceMapNode).GetElementsByTagName("Distance_Point");
                currentMap = new DistanceMap();
                
                foreach (XmlNode distancePointNode in pointNodes)
                {
                    if (test < 256)
                    {
                        test++;
                        //tester = ((XmlElement)distancePointNode).SelectNodes("Position");
                        
                        currentPointPositionNode =
                            ((XmlElement)distancePointNode).SelectNodes("Position").Item(0);
                        currentPointPositionXNode =
                            ((XmlElement)currentPointPositionNode).SelectNodes("Position_X").Item(0);
                        currentPointPositionYNode =
                            ((XmlElement)currentPointPositionNode).SelectNodes("Position_Y").Item(0);
                        currentPointValueNode =
                            ((XmlElement)distancePointNode).SelectNodes("Value").Item(0);

                        //Create and add valuePoint
                        currentMap.Add(new valuePoint(new PointObj(
                            currentPointPositionXNode.InnerText.ToString(),
                            currentPointPositionYNode.InnerText.ToString(), "0"),
                            Int32.Parse(currentPointValueNode.InnerText)));
                    }

                }

                currentMap.MyOrigin = new PointObj(sourcePositionXNode.InnerText,
                    sourcePositionYNode.InnerText, "0");
                distanceMaps.Add(currentMap);
                
            }
            MyMap.MyDistanceMaps = distanceMaps;
            #endregion
            
            return MyMap;
        }
        public Attack loadAttack(XmlNode attackNode)
        {
            Attack attack = new Attack();
            XmlNode nameNode = attackNode.SelectNodes("Name")[0], apCostNode = attackNode.SelectNodes("APCost")[0],
                noiseNode = attackNode.SelectNodes("Noise")[0], damageNode = attackNode.SelectNodes("Damage")[0];
            if (nameNode != null)
                attack.Name = nameNode.InnerText;
            if (apCostNode != null)
                attack.APCost = Int32.Parse(apCostNode.InnerText);
            if (noiseNode != null)
                attack.Noise = Int32.Parse(noiseNode.InnerText);
            if (damageNode != null)
                attack.Damage = Int32.Parse(damageNode.InnerText);
            return attack;
        }
        public List<Attack> loadAttacks(XmlDocument myDoc)
        {
            List<Attack> attacks=new List<Attack>();
            XmlNodeList attackNodes=myDoc.GetElementsByTagName("Attack");
            foreach(XmlNode node in attackNodes)
            {
                attacks.Add(loadAttack(node));
            }
            return attacks;
        }
        public List<IGoal> loadGoals(XmlDocument myDoc)
        {
            XmlNode root = myDoc.DocumentElement;
            XmlNodeList goalNodes = root.SelectNodes("Goal");
            List<IGoal> goals = new List<IGoal>();
            XmlNode typeNode;
            GoalName type;
            foreach (XmlNode node in goalNodes)
            {
                typeNode = null;
                typeNode = ((XmlElement)node).GetElementsByTagName("Goal_Type")[0];
                type = (GoalName)Enum.Parse(typeof(GoalName), typeNode.InnerText);
                goals.Add(loadGoal(type, node));
            }
            return goals;
        }

        public IGoal loadGoal(GoalName type, XmlNode node)
        {
            switch (type)
            {
                case GoalName.ReachTile:
                    return GoalReachTile.fromXml(node);
                case GoalName.ReachTileAndAction:
                    return GoalReachTileAndAction.fromXml(node);
                case GoalName.ReachTileAndActionAndTile:
                    return GoalReachTileAndActionAndTile.fromXml(node);
                case GoalName.Guards:
                    return GoalKillNumberOfGuards.fromXml(node);
                case GoalName.SpecialGuard:
                    return GoalKillSpecialGuard.fromXml(node);
                default:
                    return null;
            }
        }
                    

        public void loadSystem(string filename)
        {
            GameSystem.getInstance().loadSystem(filename);
        }

        public void test()
        {
        }
        #endregion
    }
}
