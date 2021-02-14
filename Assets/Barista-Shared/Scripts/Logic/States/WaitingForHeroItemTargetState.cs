using Barista.Shared.Entities.Hero;
using Juce.Core.State;

namespace Barista.Shared.Logic.States
{
    public class WaitingForHeroItemTargetState : IStateMachineStateAction
    {
        private readonly LevelLogic levelLogic;

        public WaitingForHeroItemTargetState(
            LevelLogic levelLogic
            )
        {
            this.levelLogic = levelLogic;
        }

        public void OnEnter()
        {
            if(!levelLogic.ExpectingItemTargetBoard.ExpectingHeroItemTarget)
            {
                levelLogic.StateMachine.Next(LevelLogicState.WaitingForHeroAction);
            }

            HeroEntity heroEntity = levelLogic.UseCases.GetCurrentHeroUseCase.Get();

            levelLogic.UseCases.StartExpectingHeroItemTargetUseCase.Invoke(
                heroEntity, 
                levelLogic.ExpectingItemTargetBoard.ExpectingHeroItemTargetItemType
                );
        }

        public void OnExit()
        { 

        }
    }
}
