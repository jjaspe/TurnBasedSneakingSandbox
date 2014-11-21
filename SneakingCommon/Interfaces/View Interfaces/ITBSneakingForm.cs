using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakingCommon.Interfaces.View
{
    public interface ITBSneakingForm
    {
        void start();
        void end();
        string getName();
        void enabled(bool value);
    }
}
