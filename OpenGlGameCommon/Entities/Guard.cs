using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using Canvas_Window_Template.Interfaces;

namespace OpenGlGameCommon.Entities
{
    public class Guard:Character
    {        
        IPoint myPosition;
        public IPoint MyPosition
        {
            get { return myPosition; }
            set { myPosition = value; }
        }
    }
}
