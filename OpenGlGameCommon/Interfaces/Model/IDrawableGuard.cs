using System;
using System.Collections.Generic;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;         
using Canvas_Window_Template.Drawables;
using OpenGlGameCommon.Data_Classes;
using OpenGlGameCommon.Interfaces.View;
using OpenGlCommonGame.Interfaces.Behaviors;
using CharacterSystemLibrary.Classes;


namespace OpenGlGameCommon.Interfaces.Model
{
    public interface IDrawableGuard:IDrawable
    {
        IVisibilityBehavior VisibilityBehavior
        {
            get;
            set;
        }
        ITileBehavior OrientationBehavior
        {
            get;
            set;
        }
        IFoVBehavior FoVBehavior
        {
            get;
            set;
        }
        IGuardMovementBehavior MovementBehavior
        {
            get;
            set;
        }
        IPoint Position
        {
            get;
            set;
        }
        Character MyGuard
        {
            get;
            set;
        }

        List<KeyValuePair<string, int>> getAttacksInfo();
        IAttack getAttack(string name);
        List<IPoint> getFOV();
        void turnQ();
        void updateVisiblePoints(List<IPoint> availablePoints, int height);
        void updateFoV(List<IPoint> availablePoints, int height);

        #region Guard Stat Manipulation
        string getName();          
        void reset();   
        #endregion

    }
}
