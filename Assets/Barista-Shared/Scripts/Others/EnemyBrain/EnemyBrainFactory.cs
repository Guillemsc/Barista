using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Logic.Pathfinding;
using Barista.Shared.State;
using System;

namespace Barista.Shared.Logic.EnemyBrain
{
    public class EnemyBrainFactory
    {
        private EnvironmentEntityRepository environmentEntityRepository;
        private HeroEntityRepository heroEntityRepository;
        private LevelState levelState;

        public void Init(
            EnvironmentEntityRepository environmentEntityRepository,
            HeroEntityRepository heroEntityRepository,
            LevelState levelState
            )
        {
            this.environmentEntityRepository = environmentEntityRepository;
            this.heroEntityRepository = heroEntityRepository;
            this.levelState = levelState;
        }

        public IEnemyBrain Create(EnemyBrainType type)
        {
            return null;
            //switch(type)
            //{
            //    case EnemyBrainType.Test:
            //        {
            //            return new TestEnemyBrain(
            //                environmentEntityRepository,
            //                heroEntityRepository,
            //                levelState
            //                );
            //        }
            //}

            //throw new NotImplementedException();
        }
    }
}
