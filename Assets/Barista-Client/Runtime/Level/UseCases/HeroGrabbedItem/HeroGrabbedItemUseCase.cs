using Barista.Client.Level.Instructions.Item;
using Barista.Client.Level.Instructions.UI.Item;
using Barista.Client.UI.Items;
using Barista.Client.View.Entities.Item;
using Barista.Shared.Logic.Items;
using Juce.Core.Sequencing;

namespace Barista.Client.Level.UseCases
{
    public class HeroGrabbedItemUseCase : IHeroGrabbedItemUseCase
    {
        private readonly ItemEntityViewRepository itemEntityViewRepository;
        private readonly ItemsUIView itemsUIView;

        public HeroGrabbedItemUseCase(
            ItemEntityViewRepository itemEntityViewRepository,
            ItemsUIView itemsUIView
            )
        {
            this.itemEntityViewRepository = itemEntityViewRepository;
            this.itemsUIView = itemsUIView;
        }

        public Instruction Invoke(
            int heroEntityInstanceId, 
            int itemEntityInstanceId, 
            ItemType itemType,
            int itemTotalStacks
            )
        {
            InstructionsSequence sequence = new InstructionsSequence();

            sequence.Append(new DespawnItemEntityViewInstruction(
                itemEntityViewRepository,
                itemEntityInstanceId
                ));

            sequence.Append(new SetItemEntityUIStacksInstruction(
                itemsUIView,
                itemType,
                itemTotalStacks
                ));

            return sequence;
        }
    }
}
