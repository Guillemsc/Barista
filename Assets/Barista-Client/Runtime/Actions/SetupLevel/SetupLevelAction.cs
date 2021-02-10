using Barista.Client.ActionsSatates;
using Barista.Client.Instructions.Enemy;
using Barista.Client.Instructions.Entity;
using Barista.Client.Instructions.Environment;
using Barista.Client.Instructions.Hero;
using Barista.Client.Instructions.Item;
using Barista.Client.Timelines;
using Barista.Client.View.Entities.Enemy;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Barista.Client.View.Entities.Item;
using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Juce.Core.Sequencing;
using System.Collections.Generic;

namespace Barista.Client.Actions
{
    public class SetupLevelAction : ISetupLevelAction
    {
        private readonly LevelTimelines levelTimelines;
        private readonly IActionsState turnActionsState;
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly HeroEntityViewRepository heroEntityViewRepository;
        private readonly EnemyEntityViewRepository enemyEntityViewRepository;
        private readonly ItemEntityViewRepository itemEntityViewRepository;

        public SetupLevelAction(
            LevelTimelines levelTimelines,
            IActionsState turnActionsState,
            EnvironmentEntityViewRepository environmentEntityViewRepository,
            HeroEntityViewRepository heroEntityViewRepository,
            EnemyEntityViewRepository enemyEntityViewRepository,
            ItemEntityViewRepository itemEntityViewRepository
            )
        {
            this.levelTimelines = levelTimelines;
            this.turnActionsState = turnActionsState;
            this.environmentEntityViewRepository = environmentEntityViewRepository;
            this.heroEntityViewRepository = heroEntityViewRepository;
            this.enemyEntityViewRepository = enemyEntityViewRepository;
            this.itemEntityViewRepository = itemEntityViewRepository;
        }

        public void Invoke(
            EnvironmentEntity environmentEntity,
            HeroEntity heroEntity,
            IReadOnlyList<EnemyEntity> enemyEntities,
            IReadOnlyList<ItemEntity> itemEntities
            )
        {
            InstructionsSequence sequence = new InstructionsSequence();

            sequence.Append(new LoadEnvironmentEntityViewInstruction(
                environmentEntityViewRepository,
                environmentEntity.TypeId, 
                environmentEntity.InstanceId
                ));

            sequence.Append(new SpawnHeroEntityViewInstruction(
                heroEntityViewRepository,
                heroEntity.TypeId,
                heroEntity.InstanceId
                ));

            sequence.Append(new SetEntityViewGridPositionInstruction(
                environmentEntityViewRepository.LoadedEnvironmentLazy,
                heroEntityViewRepository.GetLazyAsMovable(heroEntity.InstanceId),
                heroEntity.GridPosition
                ));

            foreach(EnemyEntity enemyEntity in enemyEntities)
            {
                sequence.Append(new SpawnEnemyEntityViewInstruction(
                    enemyEntityViewRepository,
                    enemyEntity.TypeId,
                    enemyEntity.InstanceId
                    ));

                sequence.Append(new SetEntityViewGridPositionInstruction(
                    environmentEntityViewRepository.LoadedEnvironmentLazy,
                    enemyEntityViewRepository.GetLazyAsMovable(enemyEntity.InstanceId),
                    enemyEntity.GridPosition
                    ));
            }

            foreach(ItemEntity itemEntity in itemEntities)
            {
                sequence.Append(new SpawnItemEntityViewInstruction(
                    itemEntityViewRepository,
                    itemEntity.TypeId,
                    itemEntity.InstanceId
                    ));

                sequence.Append(new SetEntityViewGridPositionInstruction(
                    environmentEntityViewRepository.LoadedEnvironmentLazy,
                    itemEntityViewRepository.GetLazyAsMovable(itemEntity.InstanceId),
                    itemEntity.GridPosition
                    ));
            }

            levelTimelines.MainTimeline.Play(sequence);

            turnActionsState.Enable();
        }
    }
}