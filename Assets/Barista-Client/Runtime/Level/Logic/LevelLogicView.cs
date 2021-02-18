using Barista.Client.Level.Input;
using Barista.Client.Level.Logic.Timelines;
using Barista.Client.Signals;
using Barista.Shared.Events;
using Barista.Shared.Logic.Items;
using Juce.Core.Direction;
using Juce.Core.Events;
using Juce.Core.Sequencing;
using Juce.Core.Tickable;
using System;

namespace Barista.Client.Level.Logic
{
    public class LevelLogicView : ITickable
    {
        public IEventReceiver EventReceiver { get; }
        public LevelLogicViewUseCasesRepository LevelLogicViewUseCasesRepository { get; }
        public LevelLogicViewTimelines LevelLogicViewTimelines { get; }
        public MovementInput MovementInput { get; }
        public ItemViewUIClickedSignal ItemViewUIClickedSignal { get; }

        private bool playing;

        public LevelLogicView(
            IEventReceiver eventReceiver,
            LevelLogicViewUseCasesRepository levelLogicViewUseCasesRepository,
            LevelLogicViewTimelines levelLogicViewTimelines,
            MovementInput movementInput,
            ItemViewUIClickedSignal itemViewUIClickedSignal
            )
        {
            EventReceiver = eventReceiver;
            LevelLogicViewUseCasesRepository = levelLogicViewUseCasesRepository;
            LevelLogicViewTimelines = levelLogicViewTimelines;
            MovementInput = movementInput;
            ItemViewUIClickedSignal = itemViewUIClickedSignal;
        }

        public void Start()
        {
            SetupEvents();
        }

        public void Tick()
        {
            TryDequeNext();
        }

        private void RunUseCase(Func<Instruction> context)
        {

        }

        private void TryDequeNext()
        {
            if(playing)
            {
                return;
            }

            EventReceiver.DequeNext();
        }

        private void SetupEvents()
        {
            MovementInput.OnPerformed += (Direction4Axis direction) =>
            {
                LevelLogicViewUseCasesRepository.MovementInputPerformedUseCase.Invoke(direction);
            };

            ItemViewUIClickedSignal.Register((ItemType itemType) =>
            {
                LevelLogicViewUseCasesRepository.ItemUIClickedUseCase.Invoke(itemType);
            });

            EventReceiver.Subscribe((SetupLevelOutEvent ev) =>
            {
                LevelLogicViewUseCasesRepository.SetupLevelUseCase.Invoke(
                    ev.EnvironmentEntity,
                    ev.HeroEntity,
                    ev.EnemyEntities,
                    ev.ItemEntities
                    );
            });

            EventReceiver.Subscribe((ExpectingHeroActionChangedOutEvent ev) =>
            {
                LevelLogicViewUseCasesRepository.ExpectingHeroActionChangedUseCase.Invoke(
                    ev.Expecting
                    );
            });

            EventReceiver.Subscribe((HeroMovedOutEvent ev) =>
            {
                LevelLogicViewUseCasesRepository.MoveHeroUseCase.Invoke(
                    ev.HeroEntityInstanceId,
                    ev.Path
                    );
            });

            EventReceiver.Subscribe((EnemyMovedOutEvent ev) =>
            {
                LevelLogicViewUseCasesRepository.MoveEnemyUseCase.Invoke(
                    ev.EnemyEntityInstanceId,
                    ev.Path
                    );
            });

            EventReceiver.Subscribe((HeroGrabbedItemOutEvent ev) =>
            {
                LevelLogicViewUseCasesRepository.HeroGrabbedItemUseCase.Invoke(
                    ev.HeroEntityInstanceId,
                    ev.ItemEntityInstanceId,
                    ev.ItemType,
                    ev.ItemTotalStacks
                    );
            });
        }
    }
}
