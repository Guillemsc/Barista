using Barista.Client.Level.Instructions.Enemy;
using Barista.Client.Level.Instructions.Entity;
using Barista.Client.Level.Instructions.Environment;
using Barista.Client.Level.Instructions.Hero;
using Barista.Client.Level.Instructions.Item;
using Barista.Client.View.Entities.Enemy;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Barista.Client.View.Entities.Item;
using Barista.Shared.Dto.Entities;
using Juce.Core.Sequencing;
using System.Collections.Generic;

namespace Barista.Client.Level.UseCases
{
    public class SetupLevelUseCase : ISetupLevelUseCase
    {
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly HeroEntityViewRepository heroEntityViewRepository;
        private readonly EnemyEntityViewRepository enemyEntityViewRepository;
        private readonly ItemEntityViewRepository itemEntityViewRepository;

        public SetupLevelUseCase(
            EnvironmentEntityViewRepository environmentEntityViewRepository,
            HeroEntityViewRepository heroEntityViewRepository,
            EnemyEntityViewRepository enemyEntityViewRepository,
            ItemEntityViewRepository itemEntityViewRepository
            )
        {
            this.environmentEntityViewRepository = environmentEntityViewRepository;
            this.heroEntityViewRepository = heroEntityViewRepository;
            this.enemyEntityViewRepository = enemyEntityViewRepository;
            this.itemEntityViewRepository = itemEntityViewRepository;
        }

        public Instruction Setup(
            EnvironmentEntityDto environmentEntity,
            HeroEntityDto heroEntity,
            IReadOnlyList<EnemyEntityDto> enemyEntities,
            IReadOnlyList<ItemEntityDto> itemEntities
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

            foreach (EnemyEntityDto enemyEntity in enemyEntities)
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

            foreach (ItemEntityDto itemEntity in itemEntities)
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

            return sequence;
        }
    }
}
