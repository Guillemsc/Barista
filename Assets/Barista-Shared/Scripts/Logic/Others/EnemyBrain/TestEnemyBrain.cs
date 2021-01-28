using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Logic.EnemyActions;
using Barista.Shared.Logic.Pathfinding;
using Barista.Shared.State;

namespace Barista.Shared.Logic.EnemyBrain
{
    public class TestEnemyBrain : IEnemyBrain
    {
        private readonly EnvironmentEntityRepository environmentEntityRepository;
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly EnemyEntityRepository enemyEntityRepository;
        private readonly PathFactory pathfindingFactory;
        private readonly LevelState levelState;

        public TestEnemyBrain(
            EnvironmentEntityRepository environmentEntityRepository,
            HeroEntityRepository heroEntityRepository,
            EnemyEntityRepository enemyEntityRepository,
            PathFactory pathfindingFactory,
            LevelState levelState
            )
        {
            this.environmentEntityRepository = environmentEntityRepository;
            this.heroEntityRepository = heroEntityRepository;
            this.enemyEntityRepository = enemyEntityRepository;
            this.pathfindingFactory = pathfindingFactory;
            this.levelState = levelState;
        }

        public IEnemyAction GenerateNextEnemyAction(EnemyEntity enemyEntity)
        {
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            if(heroEntity.GridPosition.Distance(enemyEntity.GridPosition) > 1)
            {
                return new MoveTowardsHeroEnemyAction(heroEntity);
            }

            return new AttackEntityEnemyAction(heroEntity);
        }
    }
}
