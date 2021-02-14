using Barista.Shared.Entities.Hero;
using Barista.Shared.Events;
using Barista.Shared.Logic.UseCases;
using Juce.Core.Events;
using Juce.Core.State;

namespace Barista.Shared.Logic.States
{
    public class WaitingForHeroActionState : IStateMachineStateAction
    {
        private readonly LevelLogic levelLogic;

        private IEventReference moveHeroInEventReference;
        private IEventReference useItemInEventReference;

        public WaitingForHeroActionState(
            LevelLogic levelLogic
            )
        {
            this.levelLogic = levelLogic;
        }

        public void OnEnter()
        {
            levelLogic.UseCases.StartExpectingHeroActionUseCase.Start();

            moveHeroInEventReference = levelLogic.EventReceiver.Subscribe<MoveHeroActionInEvent>(MoveHeroActionInEvent);
            useItemInEventReference = levelLogic.EventReceiver.Subscribe<UseItemInEvent>(UseItemInEvent);
        }

        public void OnExit()
        {
            levelLogic.EventReceiver.Unsubscribe(moveHeroInEventReference);
            levelLogic.EventReceiver.Unsubscribe(useItemInEventReference);
        }

        private void MoveHeroActionInEvent(MoveHeroActionInEvent ev)
        {
            levelLogic.UseCases.StopExpectingHeroActionUseCase.Stop();

            HeroEntity heroEntity = levelLogic.UseCases.GetCurrentHeroUseCase.Get();

            levelLogic.UseCases.MoveHeroUseCase.Move(heroEntity, ev.Direction);

            levelLogic.StateMachine.Next(LevelLogicState.TurnStart);
        }

        private void UseItemInEvent(UseItemInEvent ev)
        {
            levelLogic.UseCases.StopExpectingHeroActionUseCase.Stop();

            HeroEntity heroEntity = levelLogic.UseCases.GetCurrentHeroUseCase.Get();

            bool canUse = levelLogic.UseCases.HeroUseItemUseCase.CanUse(heroEntity, ev.ItemType);

            if(!canUse)
            {
                return;
            }

            bool needsTarget = levelLogic.UseCases.HeroUseItemUseCase.NeedsTarget(heroEntity, ev.ItemType);

            if(needsTarget)
            {
                levelLogic.ExpectingItemTargetBoard.SetExpecting(ev.ItemType);

                levelLogic.StateMachine.Next(LevelLogicState.WaitingForHeroActionTarget);
            }

            //levelLogic.UseCases.HeroUseItemUseCase.Use(heroEntity, ev.ItemType);

            //levelLogic.StateMachine.Next(LevelLogicState.WaitingForHeroActionTarget);
        }
    }
}
