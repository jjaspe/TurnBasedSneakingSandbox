using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SneakingCommon.System_Classes
{
    public class GameSystem
    {
        static GameSystem GameSystemInstance = new GameSystem();
        public static GameSystem getInstance()
        {
            return GameSystemInstance;
        }

        private GameSystem()
        {
            return;
        }
        public double FoVFactor = 1, APFactor = 1, SPFactor = 1,FoHFactor=1,
                FoVConstant = 0, APConstant = 0, SPConstant = 0,FoHConstant=0,
                HealthConstant=0,HealthFactor=0;
                
        public  double getFoV(double perception)
        {
            return perception * FoVFactor+FoVConstant;
        }
        public  double getAP(double dexterity)
        {
            return dexterity * APFactor+APConstant;
        }
        public  double getSP(double intelligence)
        {
            return intelligence * SPFactor+SPConstant;
        }
        public double getFoH(double perception)
        {
            return perception * FoHFactor + FoHConstant;
        }
        public double getHealth(double strength)
        {
            return strength*HealthFactor + HealthConstant;
        }

        
    }
}
