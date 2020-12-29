using System;

namespace Barista.Shared.Stats
{
    public interface IStat
    {
        StatType StatType { get; }
        int Value { get; }
    }
}
