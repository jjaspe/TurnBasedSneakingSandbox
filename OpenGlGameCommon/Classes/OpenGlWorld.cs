using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;

namespace OpenGlGameCommon.Classes
{
    public class OpenGlWorld:IWorld
    {
        List<IDrawable> drawables = new List<IDrawable>();
        OpenGlMap map;

        public OpenGlMap Map
        {
            get { return map; }
            set { map = value; }
        }
        List<IDrawable> IWorld.getEntities()
        {
            return drawables;
        }

        void IWorld.add(IDrawable d)
        {
            drawables.Add(d);
        }

        IDrawable IWorld.remove(IDrawable d)
        {
            return drawables.Remove(d)?d:null;
        }
    }
}
