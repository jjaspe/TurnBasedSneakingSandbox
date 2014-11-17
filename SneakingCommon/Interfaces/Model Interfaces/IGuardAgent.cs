using System;
using OpenGlGameCommon.Classes;
using System.Collections.Generic;
namespace SneakingCommon.Interfaces.Model
{
    public interface IGuardAgent
    {
        void go(WorldMessage message, List<ObjectArg> args);
        void start(WorldMessage message, List<object> args);
        void update(WorldMessage message, List<OpenGlGameCommon.Classes.ObjectArg> args);
    }
}
