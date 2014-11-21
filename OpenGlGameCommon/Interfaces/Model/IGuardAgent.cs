using System;
using OpenGlGameCommon.Classes;
using System.Collections.Generic;

namespace OpenGlGameCommon.Interfaces.Model
{
    public interface IDrawableGuardAgent
    {
        void go(int message, List<ObjectArg> args);
        void start(int message, List<object> args);
        void update(int message, List<OpenGlGameCommon.Classes.ObjectArg> args);
    }
}
