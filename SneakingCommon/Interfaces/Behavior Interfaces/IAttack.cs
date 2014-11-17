using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IAttack
    {
        string getName();
        int getAPCost();
        int getValue(string valueName);
    }
}
