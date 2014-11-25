using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.Sneaking_Drawables;
using Canvas_Window_Template.Basic_Drawing_Functions;
using SneakingCommon.System_Classes;

namespace SneakingCreationWithForms.MVP
{
    public class ExampleModel:IModel
    {
        SneakingMap map;

        public SneakingMap Map
        {
            get { return map; }
            set { map = value; }
        }
        List<SneakingGuard> guards;

        public List<SneakingGuard> Guards
        {
            get { return guards; }
            set { guards = value; }
        }

        GameSystem system;

        public GameSystem System
        {
            get { return system; }
            set { system = value; }
        }


        public void modelChanged(string name)
        {
            throw new NotImplementedException();
        }
    }
}
