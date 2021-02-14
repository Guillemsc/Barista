using Barista.Shared.Entities.Hero;
using Barista.Shared.Logic.Items;
using Juce.Core.Events;

namespace Barista.Shared.Logic.UseCases
{
    public class StartExpectingHeroItemTargetUseCase : IStartExpectingHeroItemTargetUseCase
    {
        private readonly IEventDispatcher eventDispatcher;

        public StartExpectingHeroItemTargetUseCase(IEventDispatcher eventDispatcher)
        {
            this.eventDispatcher = eventDispatcher;
        }

        public void Invoke(HeroEntity heroEntity, ItemType itemType)
        {

        }
    }
}
