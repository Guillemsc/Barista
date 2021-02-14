using Barista.Shared.Dto.Entities;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Events;
using Barista.Shared.Logic.UseCases;
using Juce.Core.State;

namespace Barista.Shared.Logic.States
{
    public class SetupState : IStateMachineStateAction
    {
        private readonly LevelLogic levelLogic;

        public SetupState(LevelLogic levelLogic)
        {
            this.levelLogic = levelLogic;
        }

        public void OnEnter()
        {
            levelLogic.UseCases.SetupLevelUseCase.Setup();

            levelLogic.StateMachine.Next(LevelLogicState.Start);
        }

        public void OnExit()
        {
           
        }
    }
}
