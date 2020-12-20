using Juce.CoreUnity.Contexts;
using Juce.CoreUnity.Service;
using Juce.CoreUnity.Services;

namespace Barista.Client.Contexts.CoreServices
{
    public class ServicesContext : Context
    {
        protected override void Init()
        {
            TickablesService tickableService = new TickablesService();
            ServicesProvider.Register(tickableService);
        }

        protected override void CleanUp()
        {
        }
    }
}