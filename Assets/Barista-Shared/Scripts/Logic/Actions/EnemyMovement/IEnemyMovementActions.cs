using Barista.Shared.Entities.Enemy;

namespace Barista.Shared.Logic
{
    public interface IEnemyMovementActions
    {
        void MoveEnemyTowardsHero(EnemyEntity enemyEntity, int range);
    }
}
