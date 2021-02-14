using Juce.Core.State;

namespace Barista.Shared.Logic.States
{
    public class TurnStartState : IStateMachineStateAction
    {
        private readonly LevelLogic levelLogic;

        public TurnStartState(LevelLogic levelLogic)
        {
            this.levelLogic = levelLogic;
        }

        public void OnEnter()
        {
            levelLogic.StateMachine.Next(LevelLogicState.HeroTurn);
        }

        public void OnExit()
        {

        }
    }
}
