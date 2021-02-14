using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Logic.EnemyActions;
using Juce.Core.State;
using System.Collections.Generic;

namespace Barista.Shared.Logic.States
{
    public class HeroTurnState : IStateMachineStateAction
    {
        private readonly LevelLogic levelLogic;

        public HeroTurnState(LevelLogic levelLogic)
        {
            this.levelLogic = levelLogic;
        }

        public void OnEnter()
        {
            HeroEntity heroEntity = levelLogic.UseCases.GetCurrentHeroUseCase.Get();

            bool canGrab = levelLogic.UseCases.HeroGrabItemUseCase.CanGrab(heroEntity);

            if(canGrab)
            {
                levelLogic.UseCases.HeroGrabItemUseCase.Grab(heroEntity);
            }

            levelLogic.StateMachine.Next(LevelLogicState.EnemiesTurn);
        }

        public void OnExit()
        {

        }
    }
}
