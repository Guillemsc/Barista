using Juce.Logic.Instructions;
using System.Collections.Generic;

namespace Juce.Logic.Compiler
{
    public class ScriptExecutor
    {
        public FlowScriptInstruction StartFlowScriptInstruction { get; }
        public IReadOnlyList<ScriptInstruction> ScriptInstructions { get; }

        public ScriptExecutor(
            FlowScriptInstruction startFlowScriptInstruction,
            IReadOnlyList<ScriptInstruction> scriptInstructions
            )
        {
            StartFlowScriptInstruction = startFlowScriptInstruction;
            ScriptInstructions = scriptInstructions;
        }

        public void Execute()
        {
            StartFlowScriptInstruction.Execute();
        }
    }
}
