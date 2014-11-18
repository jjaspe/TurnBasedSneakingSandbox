using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGlGameCommon.Interfaces.Model;

namespace OpenGlCommonGame.Interfaces.Behaviors
{
    public interface IAttackBehavior
    {
        bool isAttackPossible(IAttack attack, IDrawableGuard target, IDrawableGuard attacker,IMap map);
        void beenAttacked(IDrawableGuard target, IDrawableGuard attacker, int baseDamage);
    }
}
