using System;

namespace Barista.Client.Level.EntryPoints
{
    public class LevelViewEntryPointSettings
    {
        public bool IsVisualizer { get; }

        public LevelViewEntryPointSettings(bool isVisualizer)
        {
            IsVisualizer = isVisualizer;
        }
    }
}