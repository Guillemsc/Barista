using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Logic.Range
{
    public interface IRange
    {
        bool Used { get; }
        List<Int2> GetRangePositions(Int2 initialPosition);
        bool HasRange(Int2 initialPosition, Int2 finalPosition);
    }
}
