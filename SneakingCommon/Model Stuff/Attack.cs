using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.MVC_Interfaces;


namespace SneakingCommon.Model_Stuff
{
    public class Attack:IAttack
    {
        public string Name{get;set;}
        public int APCost { get; set; }
        public int Noise { get; set; }
        public int Damage { get; set; }

        public string getName()
        {
            return Name;
        }

        public int getAPCost()
        {
            return APCost;
        }

        public int getValue(string valueName)
        {
            switch (valueName)
            {
                case "NOISE":
                case "noise":
                case "Noise":
                    return Noise;
                case "DAMAGE":
                case "damage":
                case "Damage":
                    return Damage;
                default:
                    return -1;
            }
        }

        public int getDamage()
        {
            return Damage;
        }
    }
}
