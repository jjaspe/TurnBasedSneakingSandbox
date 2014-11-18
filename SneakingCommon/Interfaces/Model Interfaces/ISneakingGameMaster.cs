using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;           
using Canvas_Window_Template.Drawables;
using SneakingCommon.System_Classes;
using SneakingCommon.Data_Classes;
using SneakingCommon.Interfaces.Model;
using SneakingCommon.Interfaces.Observer_Pattern;

namespace SneakingCommon.Interfaces.Model
{
    public interface ISneakingGameMaster:IModelSubject
    {
        NoiseMap MyNoiseMap
        {
            get;
            set;
        }
        ISneakingMap MyMap
        {
            get;
            set;
        }
       
    }
}
