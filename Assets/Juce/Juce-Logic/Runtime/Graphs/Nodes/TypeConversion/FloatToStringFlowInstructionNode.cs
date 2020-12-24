using Juce.Logic.Instructions;

namespace Juce.Logic.Nodes
{
    [CreateNodeMenu("Type Conversion/Float To String")]
    public class FloatToStringFlowInstructionNode : FlowNode
    {
        [Input] public float floatIn;
        [Output] public string stringOut;

        private FloatToStringFlowScriptInstruction floatToStringFlowScriptInstruction = new FloatToStringFlowScriptInstruction();

        public override FlowScriptInstruction FlowScriptInstruction => floatToStringFlowScriptInstruction;

        public override void LinkScriptPorts()
        {
            LinkInputScriptPort(nameof(floatIn), floatToStringFlowScriptInstruction.FloatIn, floatIn);
            LinkOutputScriptPort(nameof(stringOut), floatToStringFlowScriptInstruction.StringOut);
        }
    }
}
