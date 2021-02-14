using Barista.Client.State;
using Barista.Shared.Events;
using Juce.Core.Direction;
using Juce.Core.Events;

namespace Barista.Client.Level.UseCases
{
    public class MovementInputPerformedUseCase : IMovementInputPerformedUseCase
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly State<ExpectingHeroActionState> expectingHeroActionState;

        public MovementInputPerformedUseCase(
            IEventDispatcher eventDispatcher,
            State<ExpectingHeroActionState> expectingHeroActionState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.expectingHeroActionState = expectingHeroActionState;
        }

        public void Invoke(Direction4Axis direction)
        {
            if(expectingHeroActionState.Value != ExpectingHeroActionState.Expecting)
            {
                return;
            }

            eventDispatcher.Dispatch(new MoveHeroActionInEvent(direction));
        }
    }
}
