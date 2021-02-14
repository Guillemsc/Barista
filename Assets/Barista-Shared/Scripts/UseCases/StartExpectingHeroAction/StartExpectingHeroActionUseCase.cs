using Barista.Shared.Events;
using Juce.Core.Events;

namespace Barista.Shared.Logic.UseCases
{
    public class StartExpectingHeroActionUseCase : IStartExpectingHeroActionUseCase
    {
        private readonly IEventDispatcher eventDispatcher;

        public StartExpectingHeroActionUseCase(IEventDispatcher eventDispatcher)
        {
            this.eventDispatcher = eventDispatcher;
        }

        public void Start()
        {
            eventDispatcher.Dispatch(new ExpectingHeroActionChangedOutEvent(true));
        }
    }
}
