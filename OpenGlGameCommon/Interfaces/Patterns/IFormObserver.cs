using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGlGameCommon.Interfaces
{
    public interface IFormObserver
    {
        void update(string message, string senderName);
    }
}
