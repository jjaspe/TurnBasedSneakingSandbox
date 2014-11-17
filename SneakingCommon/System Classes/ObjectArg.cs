using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakingMVCInterfaces.MVC_Interfaces
{
    public class ObjectArg
    {
        ArgNames name;
        public ArgNames myName
        {
            get { return name; }
            set { name = value; }
        }
        object data;
        public object myData
        {
            get { return data; }
            set { data = value; }
        }

        public ObjectArg()
        {
            name = ArgNames.None;
            myData = null;
        }
        public ObjectArg(ArgNames _name, object _data)
        {
            myName = _name; myData = _data;
        }
    }
}
