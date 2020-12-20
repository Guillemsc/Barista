using Barista.Client.View.Entities.Hero;
using UnityEngine;

namespace Barista.Client.Libraries
{
    [System.Serializable]
    public class HeroesLibraryItem
    {
        [SerializeField] private string typeId = default;
        [SerializeField] private HeroEntityView prefab = default;

        public string TypeId => typeId;
        public HeroEntityView Prefab => prefab;
    }
}