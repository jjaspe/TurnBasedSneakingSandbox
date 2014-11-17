using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakingCommon.System_Classes
{
    public enum Orientation { none, up, right, down, left };
    public enum GoalName {ReachTile,
                           ReachTileAndAction,
                            ReachTileAndActionAndTile,
                            Guards,
                           SpecialGuard};
}
