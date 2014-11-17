using System;
namespace SneakingCommon.Interfaces.Model
{
    public interface IGuardAgent
    {
        void go(WorldMessage message, System.Collections.Generic.List<OpenGlGameCommon.Classes.ObjectArg> args);
        void start(WorldMessage message, System.Collections.Generic.List<object> args);
        void update(WorldMessage message, System.Collections.Generic.List<OpenGlGameCommon.Classes.ObjectArg> args);
    }
}
