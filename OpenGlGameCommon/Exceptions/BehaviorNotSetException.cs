﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace OpenGlGameCommon.Exceptions
{
    public class BehaviorNotSetException : GeneralException
    {
        public BehaviorNotSetException(String type, String place):base("",place)
        {
            message += "\nBehavior:" + type;
        }
    }
}