using Barista.Client.Libraries;
using UnityEngine;

namespace Barista.Client.References.camera
{
    [System.Serializable]
    public class CameraReferences
    {
        [SerializeField] private Camera mainCamera = default;

        public Camera MainCamera => mainCamera;
    }
}