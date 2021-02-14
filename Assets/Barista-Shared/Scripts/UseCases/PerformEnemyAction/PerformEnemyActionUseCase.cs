using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Logic.EnemyActions;
using System.Collections.Generic;

namespace Barista.Shared.Logic.UseCases
{
    public class PerformEnemyActionUseCase : IPerformEnemyActionUseCase
    {
        private readonly IGetCurrentHeroUseCase getCurrentHeroUseCase;
        private readonly IMoveEnemyTowardsHeroUseCase moveEnemyTowardsHeroUseCase;

        public PerformEnemyActionUseCase(
            IGetCurrentHeroUseCase getCurrentHeroUseCase,
            IMoveEnemyTowardsHeroUseCase moveEnemyTowardsHeroUseCase
            )
        {
            this.getCurrentHeroUseCase = getCurrentHeroUseCase;
            this.moveEnemyTowardsHeroUseCase = moveEnemyTowardsHeroUseCase;
        }

        public void Perform(EnemyEntity enemyEntity, EnemyActionType enemyActionType)
        {
            switch (enemyActionType)
            {
                case EnemyActionType.AttackHero:
                    {

                    }
                    break;

                case EnemyActionType.MoveTowardsHero:
                    {
                        HeroEntity heroEntity = getCurrentHeroUseCase.Get();

                        moveEnemyTowardsHeroUseCase.Move(enemyEntity, heroEntity, 1);
                    }
                    break;
            }
        }
    }
}
