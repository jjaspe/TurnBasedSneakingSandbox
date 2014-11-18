using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGlGameCommon.Interfaces.Model
{
    public interface IAttack
    {
        string getName();
        int getAPCost();
        int getValue(string valueName);
    }
}
