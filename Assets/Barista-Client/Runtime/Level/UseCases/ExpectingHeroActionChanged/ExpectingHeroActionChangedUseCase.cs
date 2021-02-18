using Barista.Client.State;
using Juce.Core.Sequencing;
using System.Threading.Tasks;

namespace Barista.Client.Level.UseCases
{
    public class ExpectingHeroActionChangedUseCase : IExpectingHeroActionChangedUseCase
    {
        private readonly Sequencer mainSequencer;
        private readonly State<ExpectingHeroActionState> state;

        public ExpectingHeroActionChangedUseCase(
            Sequencer mainSequencer,
            State<ExpectingHeroActionState> state
            )
        {
            this.mainSequencer = mainSequencer;
            this.state = state;
        }

        public void Invoke(bool expecting)
        {
            mainSequencer.Play(() => Execute(
                expecting
                ));
        }

        private void Execute(bool expecting)
        {
            state.Value = expecting ? ExpectingHeroActionState.Expecting : ExpectingHeroActionState.NotExpecting;
        }
    }
}
