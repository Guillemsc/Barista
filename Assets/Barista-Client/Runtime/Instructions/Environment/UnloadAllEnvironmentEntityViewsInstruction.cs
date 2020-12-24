using Barista.Client.View.Entities.Environment;
using Juce.Core.Sequencing;

namespace Barista.Client.Instructions.Level
{
    public class UnloadAllEnvironmentEntityViewsInstruction : InstantInstruction
    {
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly int instanceId;

        public UnloadAllEnvironmentEntityViewsInstruction(EnvironmentEntityViewRepository environmentEntityViewRepository)
        {
            this.environmentEntityViewRepository = environmentEntityViewRepository;
        }

        protected override void OnInstantStart()
        {
            environmentEntityViewRepository.DespawnAll();
        }
    }
}