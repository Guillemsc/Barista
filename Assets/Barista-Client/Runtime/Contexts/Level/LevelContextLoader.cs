using Barista.Client.Constants;
using Juce.CoreUnity.Contexts;
using Juce.CoreUnity.Scenes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barista.Client.Contexts.Level
{
    public class LevelContextLoader : IContextLoader
    {
        private readonly ScenesLoader scenesLoader;

        public LevelContextLoader()
        {
            scenesLoader = new ScenesLoader(new List<string> { ScenesConstants.LevelScene });
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