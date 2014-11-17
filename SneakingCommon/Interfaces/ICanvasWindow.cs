using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Sneaking_Classes.View1;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface ICanvasWindow
    {
        simpleOpenGlView getView();
        void setMap(OpenGlMap newMap,int width, int height,int tileSize,pointObj origin);
        OpenGlMap getMap();
        void refresh();
    }
}
