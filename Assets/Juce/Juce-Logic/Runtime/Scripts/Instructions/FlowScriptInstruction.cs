using Juce.Logic.Ports;
using Juce.Logic.Flow;

namespace Juce.Logic.Instructions
{
    public abstract class FlowScriptInstruction : ScriptInstruction
    {
        public FlowScriptInstruction Flow { get; set; }
    }
}
