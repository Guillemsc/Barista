using Barista.Client.View.Entities.Enemy;
using UnityEngine;

namespace Barista.Client.Libraries
{
    [System.Serializable]
    public class EnemiesLibraryItem
    {
        [SerializeField] private string typeId = default;
        [SerializeField] private EnemyEntityView prefab = default;

        public string TypeId => typeId;
        public EnemyEntityView Prefab => prefab;
    }
}