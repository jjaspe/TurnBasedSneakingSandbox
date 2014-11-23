using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Exceptions;

namespace SneakingCommon.Exceptions
{
    class GuardsInvalidException : GeneralException
    {
        public GuardsInvalidException(string message, string place)
            :base(message,place)
        {
           
        }
    }
}
