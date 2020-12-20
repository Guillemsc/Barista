﻿using Barista.Client.Libraries;
using UnityEngine;

namespace Barista.Client.References.Level
{
    [System.Serializable]
    public class LevelLibrariesReferences
    {
        [SerializeField] private EnvironmentsLibrary environmentsLibrary = default;
        [SerializeField] private HeroesLibrary heroesLibrary = default;

        public EnvironmentsLibrary EnvironmentsLibrary => environmentsLibrary;
        public HeroesLibrary HeroesLibrary => heroesLibrary;
    }
}