using Barista.Shared.Entities.Enemy;
using System.Collections.Generic;

namespace Barista.Shared.Logic.UseCases
{
    public class GetEnemiesToPerformTurnUseCase : IGetEnemiesToPerformTurnUseCase
    {
        private readonly EnemyEntityRepository enemyEntityRepository;

        public GetEnemiesToPerformTurnUseCase(EnemyEntityRepository enemyEntityRepository)
        {
            this.enemyEntityRepository = enemyEntityRepository;
        }

        public List<EnemyEntity> Get()
        {
            List<EnemyEntity> ret = new List<EnemyEntity>(enemyEntityRepository.Elements);

            return ret;
        }
    }
}
