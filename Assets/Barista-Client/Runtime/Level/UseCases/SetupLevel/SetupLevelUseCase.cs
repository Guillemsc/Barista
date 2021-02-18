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
using System.Threading;
using System.Threading.Tasks;

namespace Barista.Client.Level.UseCases
{
    public class SetupLevelUseCase : ISetupLevelUseCase
    {
        private readonly Sequencer mainSequencer;
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly HeroEntityViewRepository heroEntityViewRepository;
        private readonly EnemyEntityViewRepository enemyEntityViewRepository;
        private readonly ItemEntityViewRepository itemEntityViewRepository;

        public SetupLevelUseCase(
            Sequencer mainSequencer,
            EnvironmentEntityViewRepository environmentEntityViewRepository,
            HeroEntityViewRepository heroEntityViewRepository,
            EnemyEntityViewRepository enemyEntityViewRepository,
            ItemEntityViewRepository itemEntityViewRepository
            )
        {
            this.mainSequencer = mainSequencer;
            this.environmentEntityViewRepository = environmentEntityViewRepository;
            this.heroEntityViewRepository = heroEntityViewRepository;
            this.enemyEntityViewRepository = enemyEntityViewRepository;
            this.itemEntityViewRepository = itemEntityViewRepository;
        }

        public void Invoke(
            EnvironmentEntityDto environmentEntity,
            HeroEntityDto heroEntity,
            IReadOnlyList<EnemyEntityDto> enemyEntities,
            IReadOnlyList<ItemEntityDto> itemEntities
            )
        {
            mainSequencer.Play(ct => Execute(
                environmentEntity,
                heroEntity,
                enemyEntities,
                itemEntities,
                ct
                ));
        }

        private async Task Execute(
            EnvironmentEntityDto environmentEntity,
            HeroEntityDto heroEntity,
            IReadOnlyList<EnemyEntityDto> enemyEntities,
            IReadOnlyList<ItemEntityDto> itemEntities,
            CancellationToken cancellationToken
            )
        {
            await new LoadEnvironmentEntityViewInstruction(
                environmentEntityViewRepository,
                environmentEntity.TypeId,
                environmentEntity.InstanceId
                ).Execute(cancellationToken);

            await new SpawnHeroEntityViewInstruction(
                heroEntityViewRepository,
                heroEntity.TypeId,
                heroEntity.InstanceId
                ).Execute(cancellationToken);

            await new SetEntityViewGridPositionInstruction(
                environmentEntityViewRepository.LoadedEnvironmentLazy,
                heroEntityViewRepository.GetLazyAsMovable(heroEntity.InstanceId),
                heroEntity.GridPosition
                ).Execute(cancellationToken);

            foreach (EnemyEntityDto enemyEntity in enemyEntities)
            {
                await new SpawnEnemyEntityViewInstruction(
                    enemyEntityViewRepository,
                    enemyEntity.TypeId,
                    enemyEntity.InstanceId
                    ).Execute(cancellationToken);

                await new SetEntityViewGridPositionInstruction(
                    environmentEntityViewRepository.LoadedEnvironmentLazy,
                    enemyEntityViewRepository.GetLazyAsMovable(enemyEntity.InstanceId),
                    enemyEntity.GridPosition
                    ).Execute(cancellationToken);
            }

            foreach (ItemEntityDto itemEntity in itemEntities)
            {
                await new SpawnItemEntityViewInstruction(
                    itemEntityViewRepository,
                    itemEntity.TypeId,
                    itemEntity.InstanceId
                    ).Execute(cancellationToken);

                await new SetEntityViewGridPositionInstruction(
                    environmentEntityViewRepository.LoadedEnvironmentLazy,
                    itemEntityViewRepository.GetLazyAsMovable(itemEntity.InstanceId),
                    itemEntity.GridPosition
                    ).Execute(cancellationToken);
            }
        }
    }
}
