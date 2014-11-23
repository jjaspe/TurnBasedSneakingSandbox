using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Exceptions;

namespace SneakingCommon.Exceptions
{
    public class InvalidSystemException:GeneralException
    {
        public InvalidSystemException(string place)
            : base("", place)
        {
        }
    }
}
