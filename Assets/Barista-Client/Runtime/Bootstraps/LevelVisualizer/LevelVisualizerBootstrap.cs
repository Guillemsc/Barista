using Barista.Client.Contexts;
using Barista.Client.Contexts.Level;
using Juce.CoreUnity.Contexts;
using System.Threading.Tasks;
using UnityEngine;

namespace Barista.Client.Bootstraps
{
    public class LevelVisualizerBootstrap : MonoBehaviour
    {
        [SerializeField] private LevelVisualizerBootstrapSettings settings = default;

        private readonly ContextLoadersRepository contextLoadersRepository = new ContextLoadersRepository();

        private void Awake()
        {
            Execute().RunAsync();
        }

        private async Task Execute()
        {
            await contextLoadersRepository.CoreServicesContextLoader.Load();
            await contextLoadersRepository.CamerasContextLoader.Load();
            await contextLoadersRepository.LevelUIContextLoader.Load();
            await contextLoadersRepository.LevelContextLoader.Load();

            LevelContext levelContext = ContextsProvider.GetContext<LevelContext>();

            levelContext.StartLevel(settings.LevelAsset);
        }
    }
}