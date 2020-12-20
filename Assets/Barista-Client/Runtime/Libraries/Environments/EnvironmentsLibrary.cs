using Barista.Client.View.Entities.Environment;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Client.Libraries
{
    [CreateAssetMenu(fileName = nameof(EnvironmentsLibrary), menuName = "Barista/Libraries/" + nameof(EnvironmentsLibrary))]
    public class EnvironmentsLibrary : ScriptableObject
    {
        [SerializeField] private List<EnvironmentsLibraryItem> items = new List<EnvironmentsLibraryItem>();

        public bool TryGetItem(string typeId, out EnvironmentEntityView environmentEntityView)
        {
            foreach (EnvironmentsLibraryItem item in items)
            {
                if (string.Equals(item.TypeId, typeId))
                {
                    environmentEntityView = item.Prefab;
                    return true;
                }
            }

            environmentEntityView = null;
            return false;
        }
    }
}