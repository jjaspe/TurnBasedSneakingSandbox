using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakingCommon.Interfaces.Observer_Pattern
{
    public interface IFormObserver
    {
        void update(string message, string senderName);
    }
}
