using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Exceptions;

namespace SneakingCommon.Exceptions
{
    public class CharacterIsNullException:GeneralException
    {
        public CharacterIsNullException(string place)
            : base("", place)
        { }
    }
}
