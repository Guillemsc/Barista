using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using System.Collections.Generic;

namespace Barista.Client.Actions
{
    public interface ISetupLevelAction
    {
        void Invoke(
            EnvironmentEntity environmentEntity,
            HeroEntity heroEntity,
            IReadOnlyList<EnemyEntity> enemyEntities
            );
    }
}