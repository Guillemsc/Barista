using Barista.Client.View.Entities.Hero;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Client.Libraries
{
    [CreateAssetMenu(fileName = nameof(HeroesLibrary), menuName = "Barista/Libraries/" + nameof(HeroesLibrary))]
    public class HeroesLibrary : ScriptableObject
    {
        [SerializeField] private List<HeroesLibraryItem> items = new List<HeroesLibraryItem>();

        public bool TryGetItem(string typeId, out HeroEntityView heroEntityView)
        {
            foreach (HeroesLibraryItem item in items)
            {
                if (string.Equals(item.TypeId, typeId))
                {
                    heroEntityView = item.Prefab;
                    return true;
                }
            }

            heroEntityView = null;
            return false;
        }
    }
}