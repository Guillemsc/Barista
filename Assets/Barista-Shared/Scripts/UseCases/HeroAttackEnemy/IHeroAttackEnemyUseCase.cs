using Barista.Shared.Entities.Enemy;

namespace Barista.Shared.Logic.UseCases
{
    public interface IHeroAttackEnemyUseCase
    {
        bool CanAttack(EnemyEntity enemyEntity);
        void Attack(EnemyEntity enemyEntity, int damage);
    }
}
