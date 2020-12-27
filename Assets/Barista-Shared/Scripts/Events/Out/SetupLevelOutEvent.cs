using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;

namespace Barista.Shared.Events
{
    public class SetupLevelOutEvent
    {
        public EnvironmentEntity EnvironmentEntity { get; }
        public HeroEntity HeroEntity { get; }

        public SetupLevelOutEvent(
            EnvironmentEntity environmentEntity,
            HeroEntity heroEntity
            )
        {
            EnvironmentEntity = environmentEntity;
            HeroEntity = heroEntity;
        }
    }
}