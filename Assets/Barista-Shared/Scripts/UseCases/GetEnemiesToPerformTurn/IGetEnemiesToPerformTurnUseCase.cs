using Barista.Shared.Entities.Enemy;
using System.Collections.Generic;

namespace Barista.Shared.Logic.UseCases
{
    public interface IGetEnemiesToPerformTurnUseCase
    {
        List<EnemyEntity> Get();
    }
}
