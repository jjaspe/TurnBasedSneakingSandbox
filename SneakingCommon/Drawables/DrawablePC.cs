using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Classes;
using SneakingCommon.Data_Classes;

namespace SneakingCommon.Drawables
{
    public class DrawablePC:OpenGlPC
    {
        NoiseMap foh;

        public NoiseMap FoH
        {
            get { return foh; }
            set { foh = value; }
        }
    }
}
