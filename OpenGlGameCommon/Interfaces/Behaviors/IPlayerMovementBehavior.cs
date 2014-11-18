using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Interfaces.Controller;
using OpenGlGameCommon.Interfaces.Model;

namespace OpenGlCommonGame.Interfaces.Behaviors
{
    public interface IPlayerMovementBehavior
    {
        bool isMovePossible(IDrawableGuard pc, IPoint dest, IGameMaster gameMaster, IMap map, IPlayerAI pAI);
        bool isTurnPossible(IDrawableGuard pc, IPoint dest, IGameMaster gameMaster, IMap map, IPlayerAI pAI);
    }
}
