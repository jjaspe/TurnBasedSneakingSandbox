using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakingCommon.MVC_Interfaces
{
    public interface IModelObserver
    {
        void update(string msg, IModel model);
    }
}
