using Barista.Client.View.Entities.Environment;
using UnityEngine;

namespace Barista.Client.Libraries
{
    [System.Serializable]
    public class EnvironmentsLibraryItem
    {
        [SerializeField] private string typeId = default;
        [SerializeField] private EnvironmentEntityView prefab = default;

        public string TypeId => typeId;
        public EnvironmentEntityView Prefab => prefab;
    }
}