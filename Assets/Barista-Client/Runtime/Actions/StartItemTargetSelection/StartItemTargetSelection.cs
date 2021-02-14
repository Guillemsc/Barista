//using Barista.Client.ActionsSatates;
//using Barista.Client.Instructions.ActionsState;
//using Barista.Client.Level.Instructions.TargetSelector;
//using Barista.Client.Timelines;
//using Barista.Client.View.Effects.TargetSelector;
//using Barista.Client.View.Entities.Environment;
//using Juce.Core.Containers;
//using Juce.Core.Sequencing;
//using System.Collections.Generic;

//namespace Barista.Client.Actions
//{
//    public class StartItemTargetSelection : IStartItemTargetSelection
//    {
//        private readonly LevelTimelines levelTimelines;
//        private readonly IActionsState itemInputActionsState;
//        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
//        private readonly TargetSelectorViewRepository targetSelectorViewRepository;

//        public StartItemTargetSelection(
//            LevelTimelines levelTimelines,
//            IActionsState itemInputActionsState,
//            EnvironmentEntityViewRepository environmentEntityViewRepository,
//            TargetSelectorViewRepository targetSelectorViewRepository
//            )
//        {
//            this.levelTimelines = levelTimelines;
//            this.itemInputActionsState = itemInputActionsState;
//            this.environmentEntityViewRepository = environmentEntityViewRepository;
//            this.targetSelectorViewRepository = targetSelectorViewRepository;
//        }

//        public void Invoke(
//            IReadOnlyList<Int2> avaliableTargetPositions
//            )
//        {
//            InstructionsSequence sequence = new InstructionsSequence();

//            sequence.Append(new EnableActionStateInstruction(itemInputActionsState));

//            sequence.Append(new ShowTargetSelectorsInstruction(
//                targetSelectorViewRepository,
//                environmentEntityViewRepository.LoadedEnvironmentLazy,
//                avaliableTargetPositions
//                ));

//            levelTimelines.MainTimeline.Play(sequence);
//        }
//    }
//}