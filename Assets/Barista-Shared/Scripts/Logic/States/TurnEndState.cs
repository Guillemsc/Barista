using Juce.Core.State;

namespace Barista.Shared.Logic.States
{
    public class TurnEndState : IStateMachineStateAction
    {
        private readonly LevelLogic levelLogic;

        public TurnEndState(LevelLogic levelLogic)
        {
            this.levelLogic = levelLogic;
        }

        public void OnEnter()
        {
            levelLogic.StateMachine.Next(LevelLogicState.WaitingForHeroAction);
        }

        public void OnExit()
        {

        }
    }
}
