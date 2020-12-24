using Juce.Logic.Instructions;
using XNode;

namespace Juce.Logic.Nodes
{
    public abstract class FlowNode : InstructionNode
    {
        [Input(connectionType = ConnectionType.Override)] public Flow.FlowContext FlowIn;
        [Output(connectionType = ConnectionType.Override)] public Flow.FlowContext FlowOut;

        public override ScriptInstruction ScriptInstruction => FlowScriptInstruction;
        public abstract FlowScriptInstruction FlowScriptInstruction { get; }
    }
}
