using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Exceptions;

namespace SneakingCommon.Exceptions
{
    public class InvalidMapException:GeneralException
    {
        public InvalidMapException(String node, String place):base("",place)
        {
            message += "\nNode:" + node;
        }
    }
}
