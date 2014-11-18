using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;

namespace OpenGlGameCommon.Interfaces.Model
{
    public interface IGameMaster
    {

        List<IDrawableGuard> Guards
        {
            get;
            set;
        }
        IDrawableGuard MyPC
        {
            get;
            set;
        }
        IDrawableGuard ActiveGuard
        {
            get;
        }
        void sortGuards(string statName);
        List<IDrawableGuard> getSortedGuards();
        void commitChanges();
        bool tileFreeFromGuards(IPoint tileOrigin);
        bool GameStarted { get; set; }
    }
}
