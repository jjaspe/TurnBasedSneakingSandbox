using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;                
using Canvas_Window_Template.Drawables;
using SneakingCommon.Interfaces.Behaviors;
using SneakingCommon.Interfaces.View;
using SneakingCommon.Data_Classes;
using OpenGlGameCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Model
{
    public interface ISneakingMap:IMap
    {
        INoiseCreationBehavior NoiseCreationBehavior
        {
            get;
            set;
        }
    }
}
