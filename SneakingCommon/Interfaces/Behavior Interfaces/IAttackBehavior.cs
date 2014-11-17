using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SneakingCommon.Interfaces.Model;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface IAttackBehavior
    {
        bool isAttackPossible(IAttack attack, IGuard target, IGuard attacker,IMap map);
        void beenAttacked(IGuard target, IGuard attacker, int baseDamage);
    }
}
