using Juce.Core.Sequencing;

namespace Barista.Client.Level.UseCases
{
    public interface IExpectingHeroActionChangedUseCase
    {
        Instruction Invoke(bool expecting);
    }
}
