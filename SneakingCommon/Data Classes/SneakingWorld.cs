using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;

namespace SneakingCommon.Data_Classes
{
    public class SneakingWorld
    {
        static Character myWorldPC;
        //static Character myWorldPC2;

        /*
        public static Character MyWorldChar2
        {
            get { return SneakingWorld.myWorldPC2; }
            set { SneakingWorld.myWorldPC2 = value; }
        }*/

        static public Character MyWorldChar
        {
            get { return myWorldPC; }
            set { myWorldPC = value;}                
        }

        static public void addStat (string name,int value)
        {
            myWorldPC.addStat(new Stat(name,value));
        }
        static public int getValueByName(string name)
        {
            if (myWorldPC.isStat(name))
                return (int)myWorldPC.getStat(name).Value;
            else
                throw new Exception("Stat not found");
        }
        static public void setValue(string name, int increase)
        {
            if (myWorldPC.isStat(name))
                myWorldPC.setStat(name, increase);
            else
                throw new Exception("Stat not found");
        }
        static public void increaseValue(string name, int increase)
        {
            if (myWorldPC.isStat(name))
                myWorldPC.increaseStat(name, increase);
            else
                throw new Exception("Stat not found");
        }

        /*
        static public void addStat2(string name, int value)
        {
            myWorldPC2.addStat(new Stat(name, value));
        }
        static public int getValue2(string name)
        {
            if (myWorldPC2.isStat(name))
                return (int)myWorldPC2.getStat(name).Value;
            else
                throw new Exception("Stat not found");
        }
        static public void setValue2(string name, int increase)
        {
            if (myWorldPC2.isStat(name))
                myWorldPC2.setStat(name, increase);
            else
                throw new Exception("Stat not found");
        }
        static public void increaseValue2(string name, int increase)
        {
            if (myWorldPC2.isStat(name))
                myWorldPC2.increaseStat(name, increase);
            else
                throw new Exception("Stat not found");
        }
         * */
    }
}
