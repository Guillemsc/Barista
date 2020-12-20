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
        [SerializeField] private Tilemap walkabilityTilemap = default;

        [Header("Spawns")]
        [SerializeField] private HeroEntitySpawnView heroEntitySpawnView = default;

        [Header("General")]
        [SerializeField] private List<GameObject> toDisable = default;

        public string TypeId { get; private set; }
        public int InstanceId { get; private set; }

        public Tilemap WalkabilityTilemap => walkabilityTilemap;
        public HeroEntitySpawnView HeroEntitySpawnView => heroEntitySpawnView;

        private void Awake()
        {
            DisableGameObjects();
        }

        public void Construct(string typeId, int instanceId)
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
            foreach(GameObject gameObject in toDisable)
            {
                gameObject.SetActive(false);
            }
        }
    }
}