using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Canvas_Window_Template.Interfaces;
using OpenGlGameCommon.Enums;

namespace OpenGlGameCommon.Classes
{
    public class ArgOwner
    {
        static private ArgOwner myInstance;
        static public ArgOwner getInstance()
        {
            if (myInstance == null)
                myInstance = new ArgOwner();
            return myInstance;
        }

        List<ObjectArg> objectsArgs;

        private ArgOwner()
        {
            objectsArgs = new List<ObjectArg>();
        }

        public object getArg(int name)
        {
            ObjectArg inList = findArg(name);
            return inList==null?null:inList.myData;
        }

        ObjectArg findArg(int name)
        {
            foreach (ObjectArg arg in objectsArgs)
            {
                if (arg.myName == name)
                    return arg;
            }
            return null;
        }
        /// <summary>
        /// Adds or updates arg list
        /// </summary>
        /// <param name="args"></param>
        public void setArgs(List<ObjectArg> args)
        {
            if (args != null)
            {
                foreach (ObjectArg arg in args)
                {
                    ObjectArg inListArg = findArg(arg.myName);
                    if (inListArg == null)
                        objectsArgs.Add(arg);
                    else
                        inListArg.myData = arg.myData;
                }
            }
        }
        public void setArg(ObjectArg arg)
        {
            ObjectArg inListArg = findArg(arg.myName);
            if (inListArg == null)
                objectsArgs.Add(arg);
            else
                inListArg.myData = arg.myData;
        }

            

    }
}
