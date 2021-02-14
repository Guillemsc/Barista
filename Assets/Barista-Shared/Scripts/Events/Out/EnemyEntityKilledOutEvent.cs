using Barista.Shared.Entities.Enemy;

namespace Barista.Shared.Events
{
    public class EnemyEntityKilledOutEvent
    {
        public EnemyEntity EnemyEntity { get; }

        public EnemyEntityKilledOutEvent(EnemyEntity enemyEntity)
        {
            EnemyEntity = enemyEntity;
        }
    }
}