using Barista.Client.View.Spawns;
using Juce.Core.Contracts;
using Juce.CoreUnity.Architecture;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Barista.Client.View.Entities.Environment
{
    public class EnvironmentEntityView : MonoBehaviour, IEntityView<string>
    {
        [Header("Setup")]
        [SerializeField] private GameObject spawnsParent = default;
        [SerializeField] private GridLayout gridLayout = default;
        [SerializeField] private Tilemap walkabilityTilemap = default;
        [SerializeField] private TilemapRenderer walkabilityTilemapRenderer = default;

        [Header("Spawns")]
        [SerializeField] private HeroEntitySpawnView heroEntitySpawnView = default;
        [SerializeField] private List<EnemyEntitySpawnView> enemyEntitySpawnViews = default;
        [SerializeField] private List<ItemEntitySpawnView> itemEntitySpawnViews = default;

        public string TypeId { get; private set; }
        public int InstanceId { get; private set; }

        public GridLayout GridLayout => gridLayout;
        public Tilemap WalkabilityTilemap => walkabilityTilemap;
        public HeroEntitySpawnView HeroEntitySpawnView => heroEntitySpawnView;
        public IReadOnlyList<EnemyEntitySpawnView> EnemyEntitySpawnViews => enemyEntitySpawnViews;
        public IReadOnlyList<ItemEntitySpawnView> ItemEntitySpawnViews => itemEntitySpawnViews;

        private void Awake()
        {
            DisableGameObjects();
        }

        public void Init(string typeId, int instanceId)
        {
            Contract.IsNotNull(heroEntitySpawnView);

            TypeId = typeId;
            InstanceId = instanceId;
        }

        public void CleanUp()
        {
        }

        private void DisableGameObjects()
        {
            walkabilityTilemapRenderer.enabled = false;

            spawnsParent.gameObject.SetActive(false);
        }
    }
}