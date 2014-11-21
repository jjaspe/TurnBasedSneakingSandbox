using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Classes;
using OpenGlGameCommon.Enums;

namespace OpenGlGameCommon.Classes
{
    public class ObjectArg
    {
        int name;
        public int myName
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
            name = 0;
            myData = null;
        }
        public ObjectArg(int _name, object _data)
        {
            myName = _name; myData = _data;
        }
    }
}
