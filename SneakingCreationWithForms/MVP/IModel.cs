﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.Sneaking_Drawables;
using SneakingCommon.System_Classes;

namespace SneakingCreationWithForms.MVP
{
    public interface IModel
    {
        SneakingMap Map
        {
            get;
            set;
        }
        List<SneakingGuard> Guards
        {
            get;
            set;
        }
        GameSystem System
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void modelChanged(string name);
    }
}