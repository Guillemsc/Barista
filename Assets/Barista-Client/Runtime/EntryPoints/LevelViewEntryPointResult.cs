using System;

namespace Barista.Client.EntryPoints
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