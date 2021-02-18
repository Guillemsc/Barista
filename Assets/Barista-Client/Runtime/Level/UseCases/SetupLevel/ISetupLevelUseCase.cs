using Barista.Shared.Dto.Entities;
using System.Collections.Generic;

namespace Barista.Client.Level.UseCases
{
    public interface ISetupLevelUseCase
    {
        void Invoke(
            EnvironmentEntityDto environmentEntity,
            HeroEntityDto heroEntity,
            IReadOnlyList<EnemyEntityDto> enemyEntities,
            IReadOnlyList<ItemEntityDto> itemEntities
            );
    }
}
