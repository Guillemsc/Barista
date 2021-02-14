using Barista.Shared.Entities.Enemy;
using Barista.Shared.Logic.EnemyActions;

namespace Barista.Shared.Logic.UseCases
{
    public interface IGetEnemyNextActionUseCase
    {
        EnemyActionType Get(EnemyEntity enemyEntity);
    }
}
