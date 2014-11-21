using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Canvas_Window_Template.Interfaces;

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

        public object getArg(ArgNames name)
        {
            #region
            /*
                switch(name)
                {
                    case ArgNames.entryPoint:
                        return entryPointArg;
                    case ArgNames.Guard:
                        return guardArg;
                    case ArgNames.GuardMaster:
                        return guardMasterArg;
                        
                    case ArgNames.guards:
                        return guards;
                        
                    case ArgNames.map:
                        return mapArg;
                        
                    case ArgNames.noiseLevel:
                        return noiseLevelArg;
                        
                    case ArgNames.noiseMap:
                        return noiseMapArg;
                        
                    case ArgNames.noiseSource:
                        return noiseSourceArg;
                        
                    case ArgNames.PC:
                        return pcArg;
                        
                    case ArgNames.PlayerMaster:
                        return playerMasterArg;
                        
                    case ArgNames.selectedGuard:
                        return selectedGuardArg;
                        
                    case ArgNames.tileOrigin:
                        return tileOriginArg;
                        
                    case ArgNames.XMLDoc:
                        return xmlDocArg;
                        
                    default:
                        return null;
                }
             * */
            #endregion
            ObjectArg inList = findArg(name);
            return inList==null?null:inList.myData;
        }

        ObjectArg findArg(ArgNames name)
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
            
            #region
            /*
            if (args!=null)
            {
                ObjectArg current;
                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.guards); });
                guards = current == null ? guards : (List<IDrawableGuard>)current.myData;

                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.PC); });
                pcArg = current == null ? pcArg : (IDrawableGuard)current.myData;

                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.selectedGuard); });
                selectedGuardArg = current == null ? selectedGuardArg : (IDrawableGuard)current.myData;

                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.PlayerMaster); });
                playerMasterArg = current == null ? playerMasterArg : (PlayerMaster)current.myData;

                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.noiseMap); });
                noiseMapArg = current == null ? noiseMapArg : (NoiseMap)current.myData;

                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.map); });
                mapArg = current == null ? mapArg : (IMap)current.myData;

                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.Guard); });
                guardArg = current == null ? guardArg : (IDrawableGuard)current.myData;

                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.tileOrigin); });
                tileOriginArg = current == null ? tileOriginArg : (pointObj)current.myData;

                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.GuardMaster); });
                guardMasterArg = current == null ? guardMasterArg : (GuardMaster)current.myData;

                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.noiseSource); });
                noiseSourceArg = current == null ? noiseSourceArg : (pointObj)current.myData;

                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.noiseLevel); });
                noiseLevelArg = current == null ? noiseLevelArg: (int)current.myData;

                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.XMLDoc); });
                xmlDocArg = current == null ? xmlDocArg : (XmlDocument)current.myData;

                current = args.Find(
                    delegate(ObjectArg _arg) { return _arg.myName.Equals(ArgNames.entryPoint); });
                entryPointArg = current == null ? entryPointArg : (pointObj)current.myData;;
            }
            */
            #endregion
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
