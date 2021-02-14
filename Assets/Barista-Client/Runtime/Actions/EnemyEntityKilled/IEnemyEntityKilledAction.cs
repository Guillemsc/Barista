using Barista.Shared.Entities.Enemy;

namespace Barista.Client.Actions
{
    public interface IEnemyEntityKilledAction : IAction
    {
        void Invoke(
            EnemyEntity enemyEntity
            );
    }
}