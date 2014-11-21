﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces; 
using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Classes;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IObservableWorld
    {
        void registerObserver(IWorldObserver obs);
        void removeObserver(IWorldObserver obs);
        void notifyObservers(WorldMessage message, List<ObjectArg> args);
    }
}