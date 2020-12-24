using Juce.Logic.Instructions;

namespace Juce.Logic.Ports
{
    [System.Serializable]
    public abstract class ScriptPort
    {
        public ScriptInstruction Instruction { get; }

        public ScriptPort(ScriptInstruction instruction)
        {
            Instruction = instruction;
        }
    }
}
