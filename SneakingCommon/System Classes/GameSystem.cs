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

        public void loadSystem(string filename = null)
        {
            //Get factors
            string factorFilename = "C:/TBSneaking/StatToSkill Factors.txt";
            XmlDocument myXml = new XmlDocument();
            XmlTextReader xReader = new XmlTextReader(factorFilename);
            xReader.Close();
            myXml.Load(factorFilename);
            try
            {
                #region READ_NODES
                XmlNode APFactorNode = myXml.GetElementsByTagName("APFactor")[0],
                    FoVFactorNode = myXml.GetElementsByTagName("FoVFactor")[0],
                    FoHFactorNode = myXml.GetElementsByTagName("FoHFactor")[0],
                    SPFactorNode = myXml.GetElementsByTagName("SPFactor")[0],
                    APConstantNode = myXml.GetElementsByTagName("APConstant")[0],
                    FoVConstantNode = myXml.GetElementsByTagName("FoVConstant")[0],
                    SPConstantNode = myXml.GetElementsByTagName("SPConstant")[0],
                    FoHConstantNode = myXml.GetElementsByTagName("FoHConstant")[0],
                    HealthConstantNode = myXml.GetElementsByTagName("HealthConstant")[0],
                    HealthFactorNode = myXml.GetElementsByTagName("HealthFactor")[0];

                APFactor = double.Parse(APFactorNode.InnerText);
                FoVFactor = double.Parse(FoVFactorNode.InnerText);
                FoHFactor = double.Parse(FoHFactorNode.InnerText);
                SPFactor = double.Parse(SPFactorNode.InnerText);
                APConstant = double.Parse(APConstantNode.InnerText);
                FoVConstant = double.Parse(FoVConstantNode.InnerText);
                FoHConstant = double.Parse(FoHConstantNode.InnerText);
                SPConstant = double.Parse(SPConstantNode.InnerText);
                HealthConstant = double.Parse(HealthConstantNode.InnerText);
                HealthFactor = double.Parse(HealthFactorNode.InnerText);
                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't find node:"+e.Message);
            }
        }
    }
}
