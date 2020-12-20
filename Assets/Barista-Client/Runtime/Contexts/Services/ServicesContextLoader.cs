using Barista.Client.Constants;
using Juce.CoreUnity.Contexts;
using Juce.CoreUnity.Scenes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barista.Client.Contexts.CoreServices
{
    public class ServicesContextLoader : IContextLoader
    {
        private readonly ScenesLoader scenesLoader;

        public ServicesContextLoader()
        {
            scenesLoader = new ScenesLoader(new List<string> { ScenesConstants.ServicesScene });
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