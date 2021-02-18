using Barista.Client.Level.Instructions.Item;
using Barista.Client.Level.Instructions.UI.Item;
using Barista.Client.UI.Items;
using Barista.Client.View.Entities.Item;
using Barista.Shared.Logic.Items;
using Juce.Core.Sequencing;
using System.Threading;
using System.Threading.Tasks;

namespace Barista.Client.Level.UseCases
{
    public class HeroGrabbedItemUseCase : IHeroGrabbedItemUseCase
    {
        private readonly Sequencer mainSequencer;
        private readonly ItemEntityViewRepository itemEntityViewRepository;
        private readonly ItemsUIView itemsUIView;

        public HeroGrabbedItemUseCase(
            Sequencer mainSequencer,
            ItemEntityViewRepository itemEntityViewRepository,
            ItemsUIView itemsUIView
            )
        {
            this.mainSequencer = mainSequencer;
            this.itemEntityViewRepository = itemEntityViewRepository;
            this.itemsUIView = itemsUIView;
        }

        public void Invoke(
            int heroEntityInstanceId,
            int itemEntityInstanceId, 
            ItemType itemType,
            int itemTotalStacks
            )
        {
            mainSequencer.Play(ct => Execute(
                itemEntityInstanceId,
                itemType,
                itemTotalStacks,
                ct
                ));
        }

        private async Task Execute(
            int itemEntityInstanceId,
            ItemType itemType,
            int itemTotalStacks,
            CancellationToken cancellationToken
            )
        {
            await new DespawnItemEntityViewInstruction(
                itemEntityViewRepository,
                itemEntityInstanceId
                ).Execute(cancellationToken);

            await new SetItemEntityUIStacksInstruction(
                itemsUIView,
                itemType,
                itemTotalStacks
                ).Execute(cancellationToken);
        }
    }
}
