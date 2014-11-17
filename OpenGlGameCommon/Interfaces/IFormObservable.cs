using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGlGameCommon.Interfaces
{
    public interface IFormObservable
    {
        void registerObserver(IFormObserver newObserver);
        void removeObserver(IFormObserver newObserver);
        void notifyObservers(string code = null);
    }
}
