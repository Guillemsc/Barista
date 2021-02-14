//using Barista.Client.Level.Instructions.State;
//using Barista.Client.State;
//using Barista.Client.Timelines;
//using Juce.Core.Sequencing;

//namespace Barista.Client.Actions
//{
//    public class StartTurnAction : IStartTurnAction
//    {
//        private readonly LevelTimelines levelTimelines;
//        private readonly State<bool> levelState;

//        public StartTurnAction(
//            LevelTimelines levelTimelines,
//            State<bool> levelState
//            )
//        {
//            this.levelTimelines = levelTimelines;
//            this.levelState = levelState;
//        }

//        public void Invoke()
//        {
//            InstructionsSequence sequence = new InstructionsSequence();

//            sequence.Append(new SetBoolStateInstruction(levelState, true));

//            levelTimelines.MainTimeline.Play(sequence);
//        }
//    }
//}