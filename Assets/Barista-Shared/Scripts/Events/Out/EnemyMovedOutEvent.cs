using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Events
{
    public class EnemyMovedOutEvent
    {
        public int EnemyEntityInstanceId { get; }
        public IReadOnlyList<Int2> Path { get; }

        public EnemyMovedOutEvent(int enemyEntityInstanceId, IReadOnlyList<Int2> path)
        {
            EnemyEntityInstanceId = enemyEntityInstanceId;
            Path = path;
        }
    }
}