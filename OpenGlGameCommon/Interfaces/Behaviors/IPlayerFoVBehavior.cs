﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenGlGameCommon.Interfaces.Model;



namespace OpenGlGameCommon.Interfaces.Behaviors
{
    public interface IPlayerFoVBehavior
    {
        void setPlayerFoV(IDrawableGuard myPc, IMap myMap);
    }
}
