using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakingCommon.Interfaces.View
{
    public interface IView:IModelObserver
    {
        void update();
        void start();
        void setController(IController myController);
        void setLoader(IModelXmlLoader myLoader);
    }
}
