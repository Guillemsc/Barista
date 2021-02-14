using Barista.Shared.Entities.Enemy;
using Barista.Shared.Logic.EnemyActions;

namespace Barista.Shared.Logic.UseCases
{
    public class GetEnemyNextActionUseCase : IGetEnemyNextActionUseCase
    {
        public EnemyActionType Get(EnemyEntity enemyEntity)
        {
            return EnemyActionType.MoveTowardsHero;
        }
    }
}
