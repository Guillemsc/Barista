using Barista.Client.Constants;
using Juce.CoreUnity.Contexts;
using Juce.CoreUnity.Scenes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barista.Client.Contexts.Cameras
{
    public class CamerasContextLoader : IContextLoader
    {
        private readonly ScenesLoader scenesLoader;

        public CamerasContextLoader()
        {
            scenesLoader = new ScenesLoader(new List<string> { ScenesConstants.CamerasScene });
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