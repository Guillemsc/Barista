using Barista.Client.View.Entities.Item;
using Barista.Shared.Logic.Items;
using Juce.Core.Sequencing;

namespace Barista.Client.Level.Instructions.Item
{
    public class SpawnItemEntityViewInstruction : InstantInstruction
    {
        private readonly ItemEntityViewRepository itemEntityViewRepository;
        private readonly ItemType typeId;
        private readonly int instanceId;

        public SpawnItemEntityViewInstruction(
            ItemEntityViewRepository itemEntityViewRepository,
            ItemType typeId,
            int instanceId
            )
        {
            this.itemEntityViewRepository = itemEntityViewRepository;
            this.typeId = typeId;
            this.instanceId = instanceId;
        }

        protected override void OnInstantStart()
        {
            itemEntityViewRepository.Spawn(typeId, instanceId);
        }
    }
}