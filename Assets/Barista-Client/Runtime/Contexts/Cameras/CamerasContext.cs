using Juce.CoreUnity.Contexts;
using UnityEngine;

namespace Barista.Client.Contexts.Cameras
{
    public class CamerasContext : Context
    {
        [SerializeField] private CameraContextReferences cameraContextReferences = default;

        public CameraContextReferences CameraContextReferences => cameraContextReferences;

        protected override void Init()
        {
            ContextsProvider.Register(this);
        }

        protected override void CleanUp()
        {
            ContextsProvider.Unregister(this);
        }
    }
}