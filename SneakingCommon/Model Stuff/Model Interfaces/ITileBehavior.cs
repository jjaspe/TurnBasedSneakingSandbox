using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.Model_Stuff;

namespace Sneaking_Gameplay.MVC_Interfaces.Model_Interfaces
{
    public interface ITileBehavior
    {
        /// <summary>
        /// use lowercase for orientation names (Ex "down","up","one", etc)
        /// </summary>
        /// <param name="or"></param>
        void changeOrientation(string or);
        void turnQ(IGuard g);
        object getOrientation();
        string getStringOrientation();
        bool hasOrientation();
    }
}
