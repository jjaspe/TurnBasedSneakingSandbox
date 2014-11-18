using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.Drawables;
using OpenGlGameCommon.Classes;
using Canvas_Window_Template.Interfaces;
using SneakingCommon.Data_Classes;
using SneakingCommon.Interfaces.Model;


namespace Sneaking_Gameplay.Sneaking_Drawables
{
    public class SneakingPC:DrawablePC
    {
        IDrawableGuard myPC;
        public IDrawableGuard MyPC
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

        #region Stat Stuff
        /// <summary>
        /// Gets value of a stat called statName
        /// </summary>
        /// <param name="statName"></param>
        /// <returns></returns>
        public int getValue(string statName)
        {
            return myPC.getValue(statName);
        }
        /// <summary>
        /// Decreases value of stat given by statName
        /// </summary>
        /// <param name="statName"></param>
        /// <param name="value"></param>
        public void decreaseValue(string statName, int value)
        {
            MyPC.decreaseValue(statName, value);
        }
        /// <summary>
        /// Changes the value of a stat called statName
        /// </summary>
        /// <param name="statName"></param>
        /// <param name="value"></param>
        public void setValue(string statName, int value)
        {
            MyPC.setValue(statName, value);
        }
        #endregion

        #region IDRAWABLE
        /// <summary>
        /// Hides draw from DrawableGuard because it could draw small pc
        /// or big pc depending on "Is Sneaking" stat, and it only draws if 
        /// it has an image
        /// </summary>
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
