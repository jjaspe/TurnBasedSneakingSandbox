using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.Interfaces.Controller;
using SneakingCommon.Interfaces.Observer_Pattern;
using SneakingCommon.Interfaces.Model;

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
