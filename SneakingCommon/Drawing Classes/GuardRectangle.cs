using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;

namespace Sneaking_Classes.Drawing_Classes
{
    public class GuardRectangle:IDrawable
    {
        rectangleObj myRectangle;

        public rectangleObj MyRectangle
        {
            get { return myRectangle; }
            set { myRectangle = value; }
        }

        public void draw()
        {
            Common.drawRectangleAndOutline(myRectangle);
        }

        public int getId()
        {
            return 0;
        }

        public int[] getPosition()
        {
            return myRectangle.BottomLeft.toIntArray();
        }

        public void setPosition(pointObj newPosition)
        {
            MyRectangle.BottomLeft = newPosition;
        }
    }
}
