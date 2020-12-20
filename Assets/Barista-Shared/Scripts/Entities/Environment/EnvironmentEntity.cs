using Juce.Core.Architecture;

namespace Barista.Shared.Entities.Environment
{
    public class EnvironmentEntity : IEntity<string>
    {
        public string TypeId { get; }
        public int InstanceId { get; }

        public EnvironmentEntity(string typeId, int instanceId)
        {
            TypeId = typeId;
            InstanceId = instanceId;
        }
    }
}