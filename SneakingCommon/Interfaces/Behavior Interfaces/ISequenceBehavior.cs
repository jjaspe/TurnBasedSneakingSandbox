using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakingCommon.Interfaces.Behaviors
{
    public interface ISequenceBehavior
    {
        /// <summary>
        /// Return the guard that will be active next turn. If it 
        /// can't find one return null
        /// </summary>
        /// <param name="active"></param>
        /// <param name="agents"></param>
        /// <returns></returns>
        GuardAgent determineTurn(List<GuardAgent> agents);
        void guardsTurnOver();
    }
}
