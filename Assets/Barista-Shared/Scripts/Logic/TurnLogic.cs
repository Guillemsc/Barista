using Barista.Shared.Events;
using Juce.Core.Events;

namespace Barista.Shared.Logic
{
    public static class TurnLogic
    {
        public static void StartTurn(IEventDispatcher eventDispatcher)
        {
            eventDispatcher.Dispatch(new StartTurnOutEvent());
        }

        public static void EndTurn(IEventDispatcher eventDispatcher)
        {
            eventDispatcher.Dispatch(new StartTurnOutEvent());
        }
    }
}
