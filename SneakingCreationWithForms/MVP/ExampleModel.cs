using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.Sneaking_Drawables;
using Canvas_Window_Template.Basic_Drawing_Functions;
using SneakingCommon.System_Classes;
using Canvas_Window_Template.Interfaces;
using OpenGlGameCommon.Data_Classes;

namespace SneakingCreationWithForms.MVP
{
    public class ExampleModel:IModel
    {
        SneakingMap map;
        GameSystem system;
        List<SneakingGuard> guards;
        List<PatrolPath> paths;
        SneakingPC pc;

        public SneakingPC PC
        {
            get { return pc; }
            set { pc = value; }
        }

        public List<PatrolPath> Paths
        {
          get { return paths; }
          set { paths = value; }
        }

        public SneakingMap Map
        {
            get { return map; }
            set { map = value; }
        }
        public List<SneakingGuard> Guards
        {
            get { return guards; }
            set { guards = value; }
        }
        
        public GameSystem System
        {
            get { return system; }
            set { system = value; }
        }

        public ExampleModel()
        {
            guards = new List<SneakingGuard>();
            paths = new List<PatrolPath>();

        }

        public void modelChanged(string name)
        {
            throw new NotImplementedException();
        }
    }
}
