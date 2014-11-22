using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGlGameCommon.Exceptions
{
    public class GeneralException:Exception
    {
        public string message;
        public GeneralException(String message, String place)
        {
            message = "Location:" + place + "\n";
        }
    }
}
