using Barista.Shared.Entities.Environment;

namespace Barista.Shared.Events
{
    public class SetupLevelOutEvent
    {
        public EnvironmentEntity EnvironmentEntity { get; }

        public SetupLevelOutEvent(EnvironmentEntity environmentEntity)
        {
            EnvironmentEntity = environmentEntity;
        }
    }
}