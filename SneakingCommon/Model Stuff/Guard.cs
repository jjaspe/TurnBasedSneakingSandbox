using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.Data_Classes;
using SneakingCommon.System_Classes;
using SneakingCommon.Interfaces.Behaviors;
using SneakingCommon.Interfaces.Model;



namespace SneakingCommon.Model_Stuff
{
    public class Guard:Character, IGuard
    {
        static int guardIds = 0;

        #region ATTRIBUTES
        int id;
        public int Id
        {
            get { return id; }
            private set { id = value; }
        }
        IPoint myPosition;        
        List<IPoint> fov;        
        NoiseMap myNoiseMap;
              
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
        public NoiseMap MyNoiseMap
        {
            get { return myNoiseMap; }
            set { myNoiseMap = value; }
        }
        

        //View dependent stuff
        IVisibilityBehavior myVisibilityBehavior;
        ITileBehavior myOrientationBehavior;
        IFoVBehavior myFoVBehavior;
        IKnownNoiseMapBehavior myKnownNoiseBehavior;
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

        public Guard()
        {
            initialize();
        }
        protected void initialize()
        {
            Id = guardIds;
            guardIds += GameMasterOne.dataTypes;

            //myOrientationBehavior = new TileBehaviorSquare();
            MyNPCBehavior = new NPCBehaviorNPC();
            //myKnownNoiseBehavior = new KnownNoiseMapBehaviorAllGuards();

            MyNoiseMap = new NoiseMap();
            myFOV = new List<IPoint>();

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
            this.addStat(new Stat("Knows Map", 0));
            this.addStat(new Stat("Turn Over", 0));
            this.addStat(new Stat("Remaining AP", this.getStat("AP").Value));
            this.addStat(new Stat("Movement Cost", SneakingWorld.getValueByName("movementCost")));
            this.addStat(new Stat("Turns In Place", 0));
            this.addStat(new Stat("Active", 0));
            this.addStat(new Stat("Selected", 0));
            this.addStat(new Stat("Is Visible", 0));
            this.addStat(new Stat("Is Dead", 0));
            this.addStat(new Stat("Health",this.getStat("Strength").BaseValue*3));

            //Status 
            this.addStat(new Stat("Status Normal", 5));
            this.addStat(new Stat("Status Suspicious", 10));
            this.addStat(new Stat("Status Alert", 15));
            this.getStat("Alert Status").ItsMax = (int)this.getStat("Status Alert").BaseValue;
        }

        #region IGUARD STUFF       
        public int getId() { return id; }
        public List<IPoint> getFOV()
        {
            return myFOV;
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
            MyPosition = myNPCBehavior.getPatrol().MyWaypoints[0];
            MyNoiseMap.initialize(0);
            MyNPCBehavior.reset();
        }
        public void updateVisiblePoints(List<IPoint> availablePoints,int height)
        {
            this.myFOV = MyVisibilityBehavior.getFoV(this.MyPosition, availablePoints,height);
        }
        public void updateFoV(List<IPoint> availablePoints,int height)
        {
            myFOV = MyVisibilityBehavior.getFoV(MyPosition, MyFoVBehavior.getFOVPoints(this, availablePoints),height);
        }

        public List<KeyValuePair<string, int>> getAttacksInfo()
        {
            return null;
        }
        public IGuardMovementBehavior getMovementBehavior()
        {
            return MyMovementBehavior;
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
            return myKnownNoiseBehavior;
        }
        public NoiseMap getNoiseMap()
        {
            return MyNoiseMap;
        }
        public IAttack getAttack(string name)
        {            
            return null;
        }
        public NoiseMap getUnknownNoiseMap()
        {
            return this.myNPCBehavior.getUnknownNoiseMap();
        }

        public void setKnownNoiseMapBehavior(IKnownNoiseMapBehavior knB)
        {
            myKnownNoiseBehavior=knB;
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
        public void setNoiseMap(NoiseMap map)
        {
            MyNoiseMap = map;
        }        
        public void setFOH(NoiseMap foh)
        {
            MyNoiseMap = foh;
        }        
        public void setMovementBehavior(IGuardMovementBehavior mB)
        {
            MyMovementBehavior = mB;
        }
        public void setUnknownNoiseMap(NoiseMap uMap)
        {
            this.myNPCBehavior.setUnknownNoiseMap(uMap);
        }
        
        
        #endregion
    }
}
