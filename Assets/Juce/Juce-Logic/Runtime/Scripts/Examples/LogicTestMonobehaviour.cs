using Juce.Logic.Compiler;
using Juce.Logic.Graphs;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace Asd
{
    public class LogicTestMonobehaviour : MonoBehaviour
    {
        [SerializeField] private LogicGraph logicGraph;

        private ScriptExecutor scriptExecutor;

        private void Awake()
        {
            LogicGraphCompiler logicGraphCompiler = new LogicGraphCompiler(logicGraph);

            scriptExecutor = logicGraphCompiler.Compile();

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            settings.TypeNameHandling = TypeNameHandling.All;
            settings.Formatting = Formatting.Indented;
            settings.ConstructorHandling = ConstructorHandling.Default;

            string jsonScript = JsonConvert.SerializeObject(scriptExecutor, settings);

            scriptExecutor = JsonConvert.DeserializeObject<ScriptExecutor>(jsonScript, settings);

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/script.json";
            File.WriteAllText(desktopPath, jsonScript);
        }

        private void Update()
        {
            if(Input.GetKeyDown("a"))
            {
                scriptExecutor.Execute();
            }
        }
    }
}
