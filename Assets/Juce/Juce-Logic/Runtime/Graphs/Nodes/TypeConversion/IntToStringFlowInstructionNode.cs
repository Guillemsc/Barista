using Juce.Logic.Instructions;

namespace Juce.Logic.Nodes
{
    [CreateNodeMenu("Type Conversion/Int To String")]
    public class IntToStringFlowInstructionNode : FlowNode
    {
        [Input] public int intIn;
        [Output] public string stringOut;

        private IntToStringFlowScriptInstruction intToStringFlowScriptInstruction = new IntToStringFlowScriptInstruction();

        public override FlowScriptInstruction FlowScriptInstruction => intToStringFlowScriptInstruction;

        public override void LinkScriptPorts()
        {
            LinkInputScriptPort(nameof(intIn), intToStringFlowScriptInstruction.IntIn, intIn);
            LinkOutputScriptPort(nameof(stringOut), intToStringFlowScriptInstruction.StringOut);
        }
    }
}
