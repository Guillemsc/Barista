
using Barista.Client.Level.Instructions.State;
using Barista.Client.State;
using Juce.Core.Sequencing;

namespace Barista.Client.Actions
{
    public class EndTurnAction 
    {
        //private readonly LevelTimelines levelTimelines;
        //private readonly State<bool> levelState;

        //public EndTurnAction(
        //    LevelTimelines levelTimelines,
        //    State<bool> levelState
        //    )
        //{
        //    this.levelTimelines = levelTimelines;
        //    this.levelState = levelState;
        //}

        //public void Invoke()
        //{
        //    InstructionsSequence sequence = new InstructionsSequence();

        //    sequence.Append(new SetBoolStateInstruction(levelState, false));

        //    levelTimelines.MainTimeline.Play(sequence);
        //}
    }
}