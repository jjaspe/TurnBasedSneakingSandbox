using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.Sneaking_Drawables;
using Canvas_Window_Template.Basic_Drawing_Functions;

namespace SneakingCreationWithForms.MVP
{
    public class ExampleModel:IModel
    {
        SneakingMap map;

        public SneakingMap Map
        {
            get
            {
               return map;
            }
            set
            {
                map=value;
            }
        }

        public void modelChanged(string name)
        {
            throw new NotImplementedException();
        }
    }
}
