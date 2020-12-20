using Barista.Shared.EntryPoints;
using Barista.Shared.Events;
using Juce.Core.Events;

namespace Barista.Shared.Actions
{
    public class LevelLostAction : ILevelLostAction
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly LevelState levelState;

        public LevelLostAction(
            IEventDispatcher eventDispatcher,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.levelState = levelState;
        }

        public void Invoke()
        {
            levelState.Playing = false;
        }
    }
}