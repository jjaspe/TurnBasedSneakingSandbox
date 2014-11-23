using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template;

namespace SneakingCreationWithForms.MVP
{
    public interface IView
    {
        Presenter MyPresenter
        {
            get;
            set;
        }
        simpleOpenGlView MyOpenGlView
        {
            get;
            set;
        }
        #region MAIN FORM STUFF
        void start();
        #endregion

        #region MAP CREATION STUFF
        void startMapCreation();
        #endregion

        #region GUARD CREATION STUFF
        void startGuardCreation();
        #endregion
    }
}
