using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Exceptions;

namespace SneakingCommon.Utility
{
    class GuardsInvalidException : GeneralException
    {
        public GuardsInvalidException(string message, string place)
            :base(message,place)
        {
           
        }
    }
}
