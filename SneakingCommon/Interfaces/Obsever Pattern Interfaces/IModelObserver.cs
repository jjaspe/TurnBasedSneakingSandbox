using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Observer_Pattern
{
    public interface IModelObserver
    {
        void update(string msg, ISneakingGameMaster gameMaster);
    }
}
