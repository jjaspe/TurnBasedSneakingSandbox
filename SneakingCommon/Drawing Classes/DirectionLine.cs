using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;

namespace Sneaking_Classes.Drawing_Classes
{
    public class DirectionLine:IDrawable
    {
        pointObj begin, end;

        public DirectionLine(pointObj _begin, pointObj _end)
        {
            begin = _begin;
            end = _end;
        }
        public void draw()
        {
            Common.drawLine(begin,end);
        }

        public int getId()
        {
            return 0;
        }

        public int[] getPosition()
        {
            return begin.toIntArray();
        }

        public void setPosition(pointObj newPosition)
        {
            begin = newPosition;
        }
    }
}
