using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface ITBSneakingForm
    {
        void start();
        void end();
        string getName();
        void enabled(bool value);
    }
}
