using Juce.Logic.Enums;
using Juce.Logic.Instructions;

namespace Juce.Logic.Nodes
{
    [CreateNodeMenu("Arithmetics/Int Operation")]
    [NodeWidth(240)]
    public class IntOperationFlowInstructionNode : FlowNode
    {
        [Input] public IntOperationType typeIn;
        [Input] public int value1In;
        [Input] public int value2In;
        [Output] public int resultOut;

        private IntOperationFlowScriptInstruction intOperationFlowScriptInstruction = new IntOperationFlowScriptInstruction();

        public override FlowScriptInstruction FlowScriptInstruction => intOperationFlowScriptInstruction;

        public override void LinkScriptPorts()
        {
            LinkInputScriptPort(nameof(typeIn), intOperationFlowScriptInstruction.OperationType, typeIn);
            LinkInputScriptPort(nameof(value1In), intOperationFlowScriptInstruction.Value1In, value1In);
            LinkInputScriptPort(nameof(value2In), intOperationFlowScriptInstruction.Value2In, value2In);
            LinkOutputScriptPort(nameof(resultOut), intOperationFlowScriptInstruction.ResultOut);
        }
    }
}
