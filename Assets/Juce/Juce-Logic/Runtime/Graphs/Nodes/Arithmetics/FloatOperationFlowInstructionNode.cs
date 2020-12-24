using Juce.Logic.Enums;
using Juce.Logic.Instructions;

namespace Juce.Logic.Nodes
{
    [CreateNodeMenu("Arithmetics/Float Operation")]
    [NodeWidth(240)]
    public class FloatOperationFlowInstructionNode : FlowNode
    {
        [Input] public FloatOperationType typeIn;
        [Input] public float value1In;
        [Input] public float value2In;
        [Output] public float resultOut;

        private FloatOperationFlowScriptInstruction floatOperationFlowScriptInstruction = new FloatOperationFlowScriptInstruction();

        public override FlowScriptInstruction FlowScriptInstruction => floatOperationFlowScriptInstruction;

        public override void LinkScriptPorts()
        {
            LinkInputScriptPort(nameof(typeIn), floatOperationFlowScriptInstruction.OperationType, typeIn);
            LinkInputScriptPort(nameof(value1In), floatOperationFlowScriptInstruction.Value1In, value1In);
            LinkInputScriptPort(nameof(value2In), floatOperationFlowScriptInstruction.Value2In, value2In);
            LinkOutputScriptPort(nameof(resultOut), floatOperationFlowScriptInstruction.ResultOut);
        }
    }
}
