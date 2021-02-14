using Barista.Shared.Entities.Enemy;
using Barista.Shared.Logic.EnemyActions;
using System.Collections.Generic;

namespace Barista.Shared.Logic.UseCases
{
    public interface IPerformEnemyActionUseCase
    {
        void Perform(EnemyEntity enemyEntity, EnemyActionType enemyActionType);
    }
}
