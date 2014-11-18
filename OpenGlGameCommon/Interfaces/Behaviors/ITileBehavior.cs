using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Interfaces.Model;

namespace OpenGlGameCommon.Interfaces.Behaviors
{
    public interface ITileBehavior
    {
        /// <summary>
        /// use lowercase for orientation names (Ex "down","up","one", etc)
        /// </summary>
        /// <param name="or"></param>
        void changeOrientation(string or);
        void turnQ(IDrawableGuard g);
        object getOrientation();
        string getStringOrientation();
        bool hasOrientation();
    }
}
