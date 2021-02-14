using Barista.Shared.Dto.Entities;
using Juce.Core.Sequencing;
using System.Collections.Generic;

namespace Barista.Client.Level.UseCases
{
    public interface ISetupLevelUseCase
    {
        Instruction Setup(
            EnvironmentEntityDto environmentEntity,
            HeroEntityDto heroEntity,
            IReadOnlyList<EnemyEntityDto> enemyEntities,
            IReadOnlyList<ItemEntityDto> itemEntities
            );
    }
}
