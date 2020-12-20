using Barista.Client.References.camera;
using UnityEngine;

namespace Barista.Client.Contexts.Cameras
{
    [System.Serializable]
    public class CameraContextReferences
    {
        [SerializeField] private CameraReferences cameraReferences = default;

        public CameraReferences CameraReferences => cameraReferences;
    }
}