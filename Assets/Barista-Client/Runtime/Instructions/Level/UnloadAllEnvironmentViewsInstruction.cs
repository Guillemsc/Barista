using Barista.Client.View.Entities.Environment;
using Juce.Core.Sequencing;

namespace Barista.Client.Instructions.Level
{
    public class UnloadAllEnvironmentViewsInstruction : InstantInstruction
    {
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly int instanceId;

        public UnloadAllEnvironmentViewsInstruction(EnvironmentEntityViewRepository environmentEntityViewRepository)
        {
            this.environmentEntityViewRepository = environmentEntityViewRepository;
        }

        protected override void OnInstantStart()
        {
            environmentEntityViewRepository.DespawnAll();
        }
    }
}