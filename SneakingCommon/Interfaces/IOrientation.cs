using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.Model_Stuff;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface ICharacterOrientation
    {
        string getOrientation(Guard g);
    }
}
