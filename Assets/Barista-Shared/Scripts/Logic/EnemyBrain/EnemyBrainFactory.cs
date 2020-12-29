using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.EntryPoints;
using Barista.Shared.Factories;
using System;

namespace Barista.Shared.Logic.EnemyBrain
{
    public class EnemyBrainFactory
    {
        private EnvironmentEntityRepository environmentEntityRepository;
        private HeroEntityRepository heroEntityRepository;
        private EnemyEntityRepository enemyEntityRepository;
        private PathfindingFactory pathfindingFactory;
        private LevelState levelState;

        public void Init(
            EnvironmentEntityRepository environmentEntityRepository,
            HeroEntityRepository heroEntityRepository,
            EnemyEntityRepository enemyEntityRepository,
            PathfindingFactory pathfindingFactory,
            LevelState levelState
            )
        {
            this.environmentEntityRepository = environmentEntityRepository;
            this.heroEntityRepository = heroEntityRepository;
            this.enemyEntityRepository = enemyEntityRepository;
            this.pathfindingFactory = pathfindingFactory;
            this.levelState = levelState;
        }

        public IEnemyBrain Create(EnemyBrainType type)
        {
            switch(type)
            {
                case EnemyBrainType.Test:
                    {
                        return new TestEnemyBrain(
                            environmentEntityRepository,
                            heroEntityRepository,
                            enemyEntityRepository,
                            pathfindingFactory,
                            levelState
                            );
                    }
            }

            throw new NotImplementedException();
        }
    }
}
