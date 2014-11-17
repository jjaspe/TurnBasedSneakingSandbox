using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakingCommon.Interfaces.Observer_Pattern
{
    public interface IModelSubject
    {
        void registerObserver(IModelObserver obs);
        void removeObserver(IModelObserver obs);
        void notifyObservers(string msg, IModelSubject sub);
    }
}
