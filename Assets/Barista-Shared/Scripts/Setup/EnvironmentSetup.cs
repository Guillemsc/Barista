using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Configuration
{
    public class EnvironmentSetup
    {
        public string TypeId { get; }
        public IReadOnlyList<Int2> WalkabilityGrid { get; }

        public EnvironmentSetup(string typeId, IReadOnlyList<Int2> walkabilityGrid)
        {
            TypeId = typeId;
            WalkabilityGrid = walkabilityGrid;
        }
    }
}