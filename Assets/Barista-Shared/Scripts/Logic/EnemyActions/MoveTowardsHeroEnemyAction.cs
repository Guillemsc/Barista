using Barista.Shared.Entities.Hero;
using System;

namespace Barista.Shared.Logic.EnemyActions
{
    public class MoveTowardsHeroEnemyAction : IEnemyAction
    {
        public HeroEntity HeroEntityToReach { get; }

        public MoveTowardsHeroEnemyAction(HeroEntity heroEntityToReach)
        {
            HeroEntityToReach = heroEntityToReach;
        }
    }
}
