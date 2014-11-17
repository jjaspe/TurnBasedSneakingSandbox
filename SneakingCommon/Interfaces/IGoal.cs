using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.Controller_Stuff.Controller1;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface IGoal
    {
        bool goalReached(ArgOwner argOwner);
    }
}
