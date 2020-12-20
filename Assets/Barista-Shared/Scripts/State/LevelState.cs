using System;

namespace Barista.Shared.EntryPoints
{
    public class LevelState
    {
        public int loadedEnvironmentId { get; set; }
        public bool Playing { get; set; }
        public bool Completed { get; set; }
    }
}