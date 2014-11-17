using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.Drawables;
using OpenGlGameCommon.Classes;
using Canvas_Window_Template.Interfaces;
using SneakingCommon.Data_Classes;
using SneakingCommon.MVC_Interfaces;

namespace Sneaking_Gameplay.Sneaking_Drawables
{
    public class SneakingPC:OpenGlPC
    {
        IGuard myPC;
        public IGuard MyPC
        {
            get { return myPC; }
            set { myPC = value; }
        }

        public IPoint MyPosition
        {
            get { return MyPC.getPosition(); }
        }
        public NoiseMap FoH
        {
            get { return MyPC.getNoiseMap(); }
            set { MyPC.setNoiseMap(value); }
        }
        public List<IPoint> FOV
        {
            get { return MyPC.getFOV(); }
            //set { MyPC.setFOV(value); }
        }
        public string Name 
        { 
            get{return MyPC.getName();} 
        }

        public SneakingPC():base()
        {
           
        }

        #region Guard Stuff
        public int getValue(string statName)
        {
            return myPC.getValue(statName);
        }
        public void decreaseValue(string statName, int value)
        {
            MyPC.decreaseValue(statName, value);
        }
        public void setValue(string statName, int value)
        {
            MyPC.setValue(statName, value);
        }
        #endregion

        #region IDRAWABLE
        new public void draw()
        {
            if (this.getValue("Is Sneaking") == 1)
                createLowImage();
            else
                createHighImage();
            if(myImage!=null)
                myImage.draw();
        }
        #endregion
    }
}
