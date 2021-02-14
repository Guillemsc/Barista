using Barista.Shared.Events;
using Juce.Core.Events;

namespace Barista.Shared.Logic.UseCases
{
    public class StopExpectingHeroActionUseCase : IStopExpectingHeroActionUseCase
    {
        private readonly IEventDispatcher eventDispatcher;

        public StopExpectingHeroActionUseCase(IEventDispatcher eventDispatcher)
        {
            this.eventDispatcher = eventDispatcher;
        }

        public void Stop()
        {
            eventDispatcher.Dispatch(new ExpectingHeroActionChangedOutEvent(false));
        }
    }
}
