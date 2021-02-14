using Barista.Client.View.Entities.Enemy;
using Barista.Shared.Entities.Enemy;

namespace Barista.Client.Actions
{
    public class EnemyEntityKilledAction : IEnemyEntityKilledAction 
    {
        private readonly EnemyEntityViewRepository enemyEntityViewRepository;

        public EnemyEntityKilledAction(EnemyEntityViewRepository enemyEntityViewRepository)
        {
            this.enemyEntityViewRepository = enemyEntityViewRepository;
        }

        public void Invoke(
            EnemyEntity enemyEntity
            )
        {

        }
    }
}