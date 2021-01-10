using Barista.Client.View.Entities.Item;
using Juce.Core.Sequencing;

namespace Barista.Client.Instructions.Item
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

        protected override void OnInstantStart()
        {
            itemEntityViewRepository.Despawn(instanceId);
        }
    }
}