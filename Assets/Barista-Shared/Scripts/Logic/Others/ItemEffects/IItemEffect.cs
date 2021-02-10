using Barista.Shared.Entities.Enemy;

namespace Barista.Shared.Logic.Items
{
    public abstract class EnemyItemEffect
    {
        public abstract void Execute(EnemyEntity enemyEntity);
    }
}
