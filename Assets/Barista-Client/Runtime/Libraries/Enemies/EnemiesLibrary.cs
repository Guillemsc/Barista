using Barista.Client.View.Entities.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Client.Libraries
{
    [CreateAssetMenu(fileName = nameof(EnemiesLibrary), menuName = "Barista/Libraries/" + nameof(EnemiesLibrary))]
    public class EnemiesLibrary : ScriptableObject
    {
        [SerializeField] private List<EnemiesLibraryItem> items = new List<EnemiesLibraryItem>();

        public bool TryGetItem(string typeId, out EnemyEntityView enemyEntityView)
        {
            foreach (EnemiesLibraryItem item in items)
            {
                if (string.Equals(item.TypeId, typeId))
                {
                    enemyEntityView = item.Prefab;
                    return true;
                }
            }

            enemyEntityView = null;
            return false;
        }
    }
}