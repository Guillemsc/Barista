using Barista.Client.View.Entities.Item;
using Juce.Core.Sequencing;

namespace Barista.Client.Level.Instructions.Item
{
    public class DespawnItemEntityViewInstruction : InstantInstruction
    {
        private readonly ItemEntityViewRepository itemEntityViewRepository;
        private readonly int instanceId;

        public DespawnItemEntityViewInstruction(
            ItemEntityViewRepository itemEntityViewRepository,
            int instanceId
            )
        {
            this.itemEntityViewRepository = itemEntityViewRepository;
            this.instanceId = instanceId;
        }

        protected override void OnInstantExecute()
        {
            itemEntityViewRepository.Despawn(instanceId);
        }
    }
}