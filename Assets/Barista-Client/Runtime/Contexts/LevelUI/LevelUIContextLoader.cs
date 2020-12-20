using Barista.Client.Constants;
using Juce.CoreUnity.Contexts;
using Juce.CoreUnity.Scenes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barista.Client.Contexts.Level
{
    public class LevelUIContextLoader : IContextLoader
    {
        private readonly ScenesLoader scenesLoader;

        public LevelUIContextLoader()
        {
            scenesLoader = new ScenesLoader(new List<string> { ScenesConstants.LevelUIScene });
        }

        public Task Load()
        {
            return scenesLoader.Load();
        }

        public Task Unload()
        {
            return scenesLoader.Unload();
        }
    }
}