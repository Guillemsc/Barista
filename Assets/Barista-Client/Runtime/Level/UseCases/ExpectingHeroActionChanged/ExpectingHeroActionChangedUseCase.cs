using Barista.Client.State;
using Juce.Core.Sequencing;

namespace Barista.Client.Level.UseCases
{
    public class ExpectingHeroActionChangedUseCase : IExpectingHeroActionChangedUseCase
    {
        private readonly State<ExpectingHeroActionState> state;

        public ExpectingHeroActionChangedUseCase(State<ExpectingHeroActionState> state)
        {
            this.state = state;
        }

        public Instruction Invoke(bool expecting)
        {
            state.Value = expecting ? ExpectingHeroActionState.Expecting : ExpectingHeroActionState.NotExpecting;

            return null;
        }
    }
}
