using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using Canvas_Window_Template.Interfaces;
using OpenGlCommonGame.Interfaces.Behaviors;
using OpenGlGameCommon.Interfaces.Model;

namespace OpenGlGameCommon.Entities
{
    public class Guard:Character,IDrawableGuard
    {        
        IPoint myPosition;
        string name;
        List<IPoint> fov;
        IVisibilityBehavior myVisibilityBehavior;
        ITileBehavior myOrientationBehavior;
        IFoVBehavior myFoVBehavior;
        IGuardMovementBehavior myMovementBehavior;
        public IPoint MyPosition
        {
            get { return myPosition; }
            set { myPosition = value; }
        }
        public string MyName
        {
            get { return name; }
            set { name = value; }
        }

        public IGuardMovementBehavior MovementBehavior
        {
            get { return myMovementBehavior; }
            set { myMovementBehavior = value; }
        }

        public ITileBehavior OrientationBehavior
        {
            get { return myOrientationBehavior; }
            set { myOrientationBehavior = value; }
        }
        public IVisibilityBehavior VisibilityBehavior
        {
            get { return myVisibilityBehavior; }
            set { myVisibilityBehavior = value; }
        }
        public IFoVBehavior FoVBehavior
        {
            get { return myFoVBehavior; }
            set { myFoVBehavior = value; }
        }

        public List<IPoint> FOV
        {
            get { return fov; }
            set { fov = value; }
        } 

        public IPoint Position
        {
            get { return myPosition; }
            set { myPosition = value; }
        }

        public List<KeyValuePair<string, int>> getAttacksInfo()
        {
            throw new NotImplementedException();
        }

        public IAttack getAttack(string name)
        {
            throw new NotImplementedException();
        }

        public List<IPoint> getFOV()
        {
            return FOV;
        }

        public void decreaseValue(string statName, double value)
        {
            throw new NotImplementedException();
        }

        public void increaseValue(string statName, double value)
        {
            throw new NotImplementedException();
        }

        public string getName()
        {
            throw new NotImplementedException();
        }

        public int getValue(string statName)
        {
            throw new NotImplementedException();
        }

        public void reset()
        {
            throw new NotImplementedException();
        }

        public void setValue(string statName, int value)
        {
            throw new NotImplementedException();
        }

        public void turnQ()
        {
            throw new NotImplementedException();
        }

        public void updateVisiblePoints(List<IPoint> availablePoints, int height)
        {
            throw new NotImplementedException();
        }

        public void updateFoV(List<IPoint> availablePoints, int height)
        {
            throw new NotImplementedException();
        }

        public int getId()
        {
            throw new NotImplementedException();
        }
    }
}
