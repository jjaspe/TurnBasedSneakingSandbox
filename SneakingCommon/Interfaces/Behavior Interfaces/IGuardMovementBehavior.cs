using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Interfaces;                                                                                                                                                                                                                                                   using Canvas_Window_Template.Drawables;
using SneakingCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IGuardMovementBehavior
    {
        /// <summary>
        /// Returns true if IGuard moved, false otherwise
        /// </summary>
        /// <param name="g"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        bool move(IGuard g,IPoint dest,IMap map);
        /// <summary>
        /// Returns true if IGuard turned, false otherwise
        /// </summary>
        /// <param name="g"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        bool turn(IGuard g,IPoint dest,IMap map);
    }
}
