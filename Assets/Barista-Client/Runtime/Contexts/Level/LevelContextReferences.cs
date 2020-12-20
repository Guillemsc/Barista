using Barista.Client.Libraries;
using Barista.Client.References.Level;
using UnityEngine;

namespace Barista.Client.Contexts.Level
{
    [System.Serializable]
    public class LevelContextReferences
    {
        [SerializeField] private LevelLibrariesReferences levelLibrariesReferences = default;

        public LevelLibrariesReferences LevelLibrariesReferences => levelLibrariesReferences;
    }
}