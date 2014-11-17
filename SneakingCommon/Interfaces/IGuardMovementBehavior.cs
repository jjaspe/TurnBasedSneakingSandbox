using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface IGuardMovementBehavior
    {
        /// <summary>
        /// Returns true if IGuard moved, false otherwise
        /// </summary>
        /// <param name="g"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        bool move(IGuard g,pointObj dest);
        /// <summary>
        /// Returns true if IGuard turned, false otherwise
        /// </summary>
        /// <param name="g"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        bool turn(IGuard g,pointObj dest);
    }
}
