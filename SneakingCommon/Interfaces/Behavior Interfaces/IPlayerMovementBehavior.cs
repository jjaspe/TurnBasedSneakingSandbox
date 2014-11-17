using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.System_Classes;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using SneakingCommon.Interfaces.Controller;
using SneakingCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IPlayerMovementBehavior
    {
        bool isMovePossible(IGuard pc, IPoint dest, IGameMaster gameMaster, IMap map, IPlayerAI pAI);
        bool isTurnPossible(IGuard pc, IPoint dest, IGameMaster gameMaster, IMap map, IPlayerAI pAI);
    }
}
