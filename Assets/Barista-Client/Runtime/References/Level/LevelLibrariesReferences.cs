using Barista.Client.Libraries;
using UnityEngine;

namespace Barista.Client.References.Level
{
    [System.Serializable]
    public class LevelLibrariesReferences
    {
        [SerializeField] private EnvironmentsLibrary environmentsLibrary = default;
        [SerializeField] private HeroesLibrary heroesLibrary = default;
        [SerializeField] private EnemiesLibrary enemiesLibrary = default;
        [SerializeField] private ItemsLibrary itemsLibrary = default;
        [SerializeField] private EffectsLibrary effectsLibrary = default;

        public EnvironmentsLibrary EnvironmentsLibrary => environmentsLibrary;
        public HeroesLibrary HeroesLibrary => heroesLibrary;
        public EnemiesLibrary EnemiesLibrary => enemiesLibrary;
        public ItemsLibrary ItemsLibrary => itemsLibrary;
        public EffectsLibrary EffectsLibrary => effectsLibrary;
    }
}