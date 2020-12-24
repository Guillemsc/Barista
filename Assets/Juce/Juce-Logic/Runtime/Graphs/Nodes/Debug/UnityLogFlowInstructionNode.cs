using Juce.Logic.Instructions;

namespace Juce.Logic.Nodes
{
    [CreateNodeMenu("Debug/Unity Log")]
    public class UnityLogFlowInstructionNode : FlowNode
    {
        [Input] public string message;

        private UnityLogFlowInstruction unityLogFlowInstruction = new UnityLogFlowInstruction();

        public override FlowScriptInstruction FlowScriptInstruction => unityLogFlowInstruction;

        public override void LinkScriptPorts()
        {
            LinkInputScriptPort(nameof(message), unityLogFlowInstruction.Message, message);
        }
    }
}
