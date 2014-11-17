using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sneaking_Gameplay.Game_Components;
using SneakingClasses.System_Classes;
using Sneaking_Gameplay.MVC_Interfaces.Model_Interfaces;
using Sneaking_Gameplay.Game_Components.Interfaces.Behaviors;

namespace Sneaking_Gameplay.MVC_Interfaces
{
    public interface IController
    {
        void messageSent(WorldMessage msg, List<ObjectArg> args);
        void initialize();
        IGuard getActiveGuard();

        void setNoiseCreationBehavior(INoiseCreationBehavior nB);

    }
}
