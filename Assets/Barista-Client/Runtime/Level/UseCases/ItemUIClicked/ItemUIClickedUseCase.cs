using Barista.Client.State;
using Barista.Shared.Events;
using Barista.Shared.Logic.Items;
using Juce.Core.Events;

namespace Barista.Client.Level.UseCases
{
    public class ItemUIClickedUseCase : IItemUIClickedUseCase
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly State<ExpectingHeroActionState> expectingHeroActionState;

        public ItemUIClickedUseCase(
            IEventDispatcher eventDispatcher,
            State<ExpectingHeroActionState> expectingHeroActionState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.expectingHeroActionState = expectingHeroActionState;
        }

        public void Invoke(ItemType itemType)
        {
            if (expectingHeroActionState.Value != ExpectingHeroActionState.Expecting)
            {
                return;
            }

            eventDispatcher.Dispatch(new UseItemInEvent(itemType));
        }
    }
}
