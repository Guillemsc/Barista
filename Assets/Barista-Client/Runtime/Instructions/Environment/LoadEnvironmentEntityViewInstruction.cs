using Barista.Client.View.Entities.Environment;
using Juce.Core.Sequencing;

namespace Barista.Client.Instructions.Environment
{
    public class LoadEnvironmentEntityViewInstruction : InstantInstruction
    {
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly string typeId;
        private readonly int instanceId;

        public LoadEnvironmentEntityViewInstruction(
            EnvironmentEntityViewRepository environmentEntityViewRepository,
            string typeId, 
            int instanceId
            )
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