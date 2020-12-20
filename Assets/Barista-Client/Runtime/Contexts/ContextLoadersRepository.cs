using Barista.Client.Contexts.Cameras;
using Barista.Client.Contexts.CoreServices;
using Barista.Client.Contexts.Level;
using Juce.CoreUnity.Contexts;

namespace Barista.Client.Contexts
{
    public class ContextLoadersRepository
    {
        public IContextLoader CoreServicesContextLoader { get; } = new ServicesContextLoader();
        public IContextLoader CamerasContextLoader { get; } = new CamerasContextLoader();
        public IContextLoader LevelContextLoader { get; } = new LevelContextLoader();
        public IContextLoader LevelUIContextLoader { get; } = new LevelUIContextLoader();
    }
}