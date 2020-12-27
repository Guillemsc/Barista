using Barista.Shared.EntryPoints;
using Barista.Shared.Events;
using Juce.Core.Events;

namespace Barista.Shared.Actions
{
    public class LevelCompletedAction : ILevelCompletedAction
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly LevelState levelState;

        public LevelCompletedAction(
            IEventDispatcher eventDispatcher,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.levelState = levelState;
        }

        public void Invoke()
        {

            eventDispatcher.Dispatch(new LevelCompletedOutEvent());
        }
    }
}