using Juce.Core.State;

namespace Barista.Shared.Logic.States
{
    public class StartState : IStateMachineStateAction
    {
        private readonly LevelLogic levelLogic;

        public StartState(LevelLogic levelLogic)
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
