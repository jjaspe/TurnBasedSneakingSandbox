using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.Drawables;
using OpenGlGameCommon.Classes;
using Canvas_Window_Template.Interfaces;
using SneakingCommon.Data_Classes;
using SneakingCommon.Interfaces.Model;
using OpenGlGameCommon.Interfaces.Model;


namespace Sneaking_Gameplay.Sneaking_Drawables
{
    public class SneakingPC:DrawablePC
    {
        public string Name 
        { 
            get{return MyCharacter.Name;} 
        }

        public SneakingPC():base()
        {
           
        }

        #region Stat Stuff        
        /// <summary>
        /// Decreases value of stat given by statName
        /// </summary>
        /// <param name="statName"></param>
        /// <param name="value"></param>
        public void decreaseValue(string statName, int value)
        {
            MyCharacter.decreaseValue(statName, value);
        }
        /// <summary>
        /// Changes the value of a stat called statName
        /// </summary>
        /// <param name="statName"></param>
        /// <param name="value"></param>
        public void setValue(string statName, int value)
        {
            MyCharacter.setValue(statName, value);
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
            if (this.MyCharacter.getValue("Is Sneaking") == 1)
                createLowImage();
            else
                createHighImage();
            if(myImage!=null)
                myImage.draw();
        }
        #endregion
    }
}
