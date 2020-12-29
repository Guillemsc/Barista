using Barista.Shared.Entities;
using System;

namespace Barista.Shared.Logic.EnemyActions
{
    public class MoveTowardsEntityEnemyAction : IEnemyAction
    {
        public IMapEntity EntityToReach { get; }

        public MoveTowardsEntityEnemyAction(IMapEntity entityToReach)
        {
            EntityToReach = entityToReach;
        }
    }
}
