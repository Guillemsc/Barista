using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Hero;

namespace Barista.Shared.Logic.UseCases
{
    public interface IMoveEnemyTowardsHeroUseCase
    {
        bool CanMove(EnemyEntity enemyEntity, HeroEntity heroEntity, int range);
        void Move(EnemyEntity enemyEntity, HeroEntity heroEntity, int range);
    }
}
