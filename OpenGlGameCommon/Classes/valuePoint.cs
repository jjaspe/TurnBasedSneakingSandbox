using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;

namespace OpenGlGameCommon.Classes
{
    public class valuePoint
    {
        public valuePoint() { }
        public valuePoint(IPoint _p, double _d)
        {
            p = _p;
            value = _d;
        }
        public IPoint p;
        public double value;
    }
}
