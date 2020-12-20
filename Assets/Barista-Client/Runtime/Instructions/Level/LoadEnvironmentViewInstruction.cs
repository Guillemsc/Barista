using Barista.Client.View.Entities.Environment;
using Juce.Core.Sequencing;

namespace Barista.Client.Instructions.Level
{
    public class LoadEnvironmentViewInstruction : InstantInstruction
    {
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly string typeId;
        private readonly int instanceId;

        public LoadEnvironmentViewInstruction(EnvironmentEntityViewRepository environmentEntityViewRepository,
            string typeId, int instanceId)
        {
            this.environmentEntityViewRepository = environmentEntityViewRepository;
            this.typeId = typeId;
            this.instanceId = instanceId;
        }

        protected override void OnInstantStart()
        {
            environmentEntityViewRepository.Spawn(typeId, instanceId);
        }
    }
}