using UnityEngine;

namespace Barista.Client.References.LevelUI
{
    [System.Serializable]
    public class LevelUICanvases
    {
        [SerializeField] private Canvas mainCanvas = default;

        public Canvas MainCanvas => mainCanvas;
    }
}