using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using Canvas_Window_Template.Interfaces;

namespace OpenGlGameCommon.Entities
{
    public class PC:Character
    {
        IPoint myPosition;
        List<IPoint> fov;
        
        public IPoint MyPosition
        {
            get { return myPosition; }
            set { myPosition = value; }
        }
        public List<IPoint> FoV
        {
            get { return fov; }
            set { fov = value; }
        }

        public int getValue(string name)
        {
            if (this.isStat(name))
                return (int)this.getStat(name).Value;
            else
                throw new Exception("Stat not found");
        }
    }
}
