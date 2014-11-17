using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGlGameCommon.Interfaces
{
    public interface ITemplateForm
    {
        void start();
        void end();
        string getName();
        void enabled(bool value);
    }
}
