using Barista.Shared.Configuration;
using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.EntryPoints;
using Barista.Shared.Events;
using Juce.Core.Events;
using System.Collections.Generic;

namespace Barista.Shared.Actions
{
    public class SetupLevelAction : ISetupLevelAction
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly LevelSetup levelSetup;
        private readonly EnvironmentEntityRepository environmentEntityRepository;
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly EnemyEntityRepository enemyEntityRepository;
        private readonly LevelState levelState;

        public SetupLevelAction(
            IEventDispatcher eventDispatcher,
            LevelSetup levelConfiguration,
            EnvironmentEntityRepository environmentEntityRepository,
            HeroEntityRepository heroEntityRepository,
            EnemyEntityRepository enemyEntityRepository,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.levelSetup = levelConfiguration;
            this.environmentEntityRepository = environmentEntityRepository;
            this.heroEntityRepository = heroEntityRepository;
            this.enemyEntityRepository = enemyEntityRepository;
            this.levelState = levelState;
        }

        public void Invoke()
        {
            EnvironmentEntity loadedEnvironmentEntity = environmentEntityRepository.Spawn(levelSetup.EnvironmentSetup);
            levelState.LoadedEnvironmentId = loadedEnvironmentEntity.InstanceId;

            HeroEntity spawnedHeroEntity = heroEntityRepository.Spawn(levelSetup.HeroSetup);
            spawnedHeroEntity.SetGridPosition(levelSetup.HeroSetup.SpawnPosition);
            levelState.LoadedHeroId = spawnedHeroEntity.InstanceId;

            List<EnemyEntity> spawnedEnemyEntities = new List<EnemyEntity>();

            foreach (EnemySetup enemySetup in levelSetup.EnemySetups)
            {
                EnemyEntity enemyEntity = enemyEntityRepository.Spawn(enemySetup);
                enemyEntity.GridPosition = enemySetup.SpawnPosition;
                spawnedEnemyEntities.Add(enemyEntity);
            }

            eventDispatcher.Dispatch(new SetupLevelOutEvent(
                loadedEnvironmentEntity, 
                spawnedHeroEntity,
                spawnedEnemyEntities
                ));
        }
    }
}