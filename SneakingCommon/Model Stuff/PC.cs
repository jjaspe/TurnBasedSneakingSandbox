using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;    
using Canvas_Window_Template.Drawables;
using SneakingCommon.System_Classes;
using SneakingCommon.Data_Classes;
using SneakingCommon.Interfaces.Behaviors;
using SneakingCommon.Interfaces.Model;


namespace SneakingCommon.Model_Stuff
{
    public class PC:Character,IGuard
    {
        static int pcIds = 1;

        #region ATTRIBUTES
        int id;
        public int Id
        {
            get { return id; }
            private set { id = value; }
        }
        IPoint myPosition;
        IPoint myEntryPoint;
        public NoiseMap MyFoH { get; set; }
        public IPoint MyEntryPoint
        {
            get { return myEntryPoint; }
            set { myEntryPoint = value; }
        }
        List<IPoint> fov;
        NoiseMap foh;
        List<Attack> myAttacks;

        public List<Attack> MyAttacks
        {
            get { return myAttacks; }
            set { myAttacks = value; }
        }
        public NoiseMap FoH
        {
            get { return foh; }
            set { foh = value; }
        }
        public IPoint MyPosition
        {
            get { return myPosition; }
            set { myPosition = value; }
        }
        public List<IPoint> myFOV
        {
            get { return fov; }
            set { fov = value; }
        }

        IVisibilityBehavior myVisibilityBehavior;
        ITileBehavior myOrientationBehavior;
        IFoVBehavior myFoVBehavior;
        INPCBehavior myNPCBehavior;
        IGuardMovementBehavior myMovementBehavior;

        public IGuardMovementBehavior MyMovementBehavior
        {
            get { return myMovementBehavior; }
            set { myMovementBehavior = value; }
        }
        public INPCBehavior MyNPCBehavior
        {
            get { return myNPCBehavior; }
            set { myNPCBehavior = value; }
        }
        public IFoVBehavior MyFoVBehavior
        {
            get { return myFoVBehavior; }
            set { myFoVBehavior = value; }
        }
        public IVisibilityBehavior MyVisibilityBehavior
        {
            get { return myVisibilityBehavior; }
            set { myVisibilityBehavior = value; }
        }
        public ITileBehavior MyOrientationBehavior
        {
            get { return myOrientationBehavior; }
            set { myOrientationBehavior = value; }
        }
        #endregion 

        public PC()
        {
            initialize();
        }
        protected void initialize()
        {
            Id = pcIds;
            pcIds += GameMasterOne.dataTypes;

            //myOrientationBehavior = new TileBehaviorSquare();
            throw new Exception("Tile behavior not set on PC. Method:Initialize()");
            MyNPCBehavior = new NPCBehaviorPC();
            MyFoH = new NoiseMap();
            myFOV = new List<IPoint>();
            myAttacks = new List<Attack>();
            this.addStat(new Stat("Strength", 0));
            this.addStat(new Stat("Armor", 0));
            this.addStat(new Stat("Weapon Skill", 0));
            this.addStat(new Stat("Perception", 0));
            this.addStat(new Stat("Intelligence", 0));
            this.addStat(new Stat("Dexterity", 0));
            this.addStat(new Stat("Suspicion", 0));
            this.addStat(new Stat("Alert Status", 0));
            this.addStat(new Stat("Field of View", 0));
            this.addStat(new Stat("Suspicion Propensity", 0));
            this.addStat(new Stat("AP", 0));
            this.addStat(new Stat("Turn Over", 0));
            this.addStat(new Stat("Remaining AP", this.getStat("AP").Value));
            this.addStat(new Stat("Movement Cost", SneakingWorld.getValueByName("movementCost")));
            this.addStat(new Stat("Turns In Place", 0));
            this.addStat(new Stat("Active", 0));
            this.addStat(new Stat("Selected", 0));
            this.addStat(new Stat("Is Visible", 0));
            this.addStat(new Stat("Is Sneaking", 0));
        }

        #region IGUARD STUFF 
        public List<KeyValuePair<string,int>> getAttacksInfo()
        {
            List<KeyValuePair<string,int>> attackInfos= new List<KeyValuePair<string,int>>();
            foreach(Attack at in myAttacks)
            {
                if (at != null)
                    attackInfos.Add(new KeyValuePair<string, int>(at.Name, at.APCost));
            }
            return attackInfos;
        }
        public int getId() { return id; }
        public NoiseMap getNoiseMap()
        {
            return MyFoH;
        }
        public void setNoiseMap(NoiseMap map)
        {
            MyFoH = map;
        }
        public IPoint getPosition()
        {
            return MyPosition;
        }
        public void setPosition(IPoint pos)
        {
            MyPosition = pos;
        }
        public string getName()
        {
            return Name;
        }
        public void turnQ()
        {
            MyOrientationBehavior.turnQ(this);
        }
        public int getValue(string statName)
        {
            if (isStat(statName))
                return (int)getStat(statName).Value;
            else
                throw new Exception("Stat not Found");
            return 0;
        }
        public void setValue(string statName, int value)
        {
            if (this.isStat(statName))
                this.setStat(statName, value);
            else
                throw new Exception("Stat not Found");
        }
        public void decreaseValue(string statName, double value)
        {
            if (isStat(statName))
                setStat(statName, getValue(statName) - value);
            else
                throw new Exception("Stat not Found");
        }
        public void increaseValue(string statName, double value)
        {
            if (isStat(statName))
                setStat(statName, getValue(statName) + value);
            else
                throw new Exception("Stat not Found");
        }   
        public void reset()
        {
            try
            {
                MyPosition = MyEntryPoint;
            }
            catch (Exception)
            {
                Console.WriteLine("PC Entry Point was null");
            }
        }

        public List<IPoint> getFOV()
        {
            return myFOV;
        }        
        public IVisibilityBehavior getVisibilityBehavior()
        {
            return MyVisibilityBehavior;
        }
        public ITileBehavior getOrientationBehavior()
        {
            return MyOrientationBehavior;
        }
        public IFoVBehavior getFoVBehavior()
        {
            return MyFoVBehavior;
        }
        public INPCBehavior getNPCBehavior()
        {
            return myNPCBehavior;
        }
        public IKnownNoiseMapBehavior getKnownNoiseMapBehavior()
        {
            return null;
        }
        public IGuardMovementBehavior getMovementBehavior()
        {
            return MyMovementBehavior;
        }
        public NoiseMap getUnknownNoiseMap()
        {
            return myNPCBehavior.getUnknownNoiseMap();
        }
        public IAttack getAttack(string name)
        {
            foreach (Attack at in myAttacks)
            {
                if (at.Name == name)
                    return at;
            }
            return null;
        }

        public void setVisibilityBehavior(IVisibilityBehavior vB)
        {
            MyVisibilityBehavior = vB;
        }
        public void setOrientationBehavior(ITileBehavior tB)
        {
            MyOrientationBehavior = tB;
        }
        public void setFoVBehavior(IFoVBehavior fB)
        {
            MyFoVBehavior = fB;
        }
        public void setNPCBehavior(INPCBehavior nB)
        {
            MyNPCBehavior = nB;
        }
        public void setKnownNoiseMapBehavior(IKnownNoiseMapBehavior kB)
        {
            return;
        }
        public void setMovementBehavior(IGuardMovementBehavior mB)
        {
            MyMovementBehavior = mB;
        }        
        public void setFOH(NoiseMap foh)
        {
            MyFoH=foh;
        }       
        
        /// <summary>
        /// updates FoV taking into account FoV Behavior and visibility behavior
        /// </summary>
        /// <param name="availablePoints"></param>
        public void updateFoV(List<IPoint> availablePoints,int height)
        {
            myFOV=MyVisibilityBehavior.getFoV(MyPosition,MyFoVBehavior.getFOVPoints(this,availablePoints),height);
        }
        public void updateVisiblePoints(List<IPoint> availablePoints, int height)
        {
            this.myFOV = MyVisibilityBehavior.getFoV(this.MyPosition, availablePoints, height);
        }
        public void setUnknownNoiseMap(NoiseMap uMap)
        {
            this.myNPCBehavior.setUnknownNoiseMap(uMap);
        }
        #endregion
    }
}
