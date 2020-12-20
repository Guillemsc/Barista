using System;

namespace Barista.Client.EntryPoints
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