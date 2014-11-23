using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.Sneaking_Drawables;

namespace SneakingCreationWithForms.MVP
{
    public interface IModel
    {
        SneakingMap Map
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
