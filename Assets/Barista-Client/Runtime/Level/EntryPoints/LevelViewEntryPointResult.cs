using System;

namespace Barista.Client.Level.EntryPoints
{
    public class LevelViewEntryPointResult
    {
        public bool PlayAgain { get; }

        public LevelViewEntryPointResult(bool playAgain)
        {
            PlayAgain = playAgain;
        }
    }
}