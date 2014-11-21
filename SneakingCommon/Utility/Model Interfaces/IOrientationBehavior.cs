using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sneaking_Gameplay.MVC_Interfaces.Model_Interfaces
{
    public interface IOrientationBehavior
    {
        void changeOrientation(string or);
        void turnQ(IGuard g);
    }
}
