using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Exceptions;

namespace SneakingCommon.Exceptions
{
    class BadFileNameException : GeneralException
    {

        public BadFileNameException(string detailMessage, string place):
            base("",place)
        {
            message += detailMessage;
        }
    }
}
