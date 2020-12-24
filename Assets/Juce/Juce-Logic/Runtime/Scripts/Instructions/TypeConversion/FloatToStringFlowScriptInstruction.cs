using Juce.Logic.Ports;
using System;

namespace Juce.Logic.Instructions
{
    [Serializable]
    public class FloatToStringFlowScriptInstruction : FlowScriptInstruction
    {
        public InScriptPort FloatIn { get; }
        public OutScriptPort StringOut { get; }

        public FloatToStringFlowScriptInstruction()
        {
            FloatIn = AddInPort();
            StringOut = AddOutPort();
        }

        public override void Execute()
        {
            float floatInValue = GetInputValue<float>(FloatIn);

            string stringOutValue = floatInValue.ToString();

            SetOutputValue(StringOut, stringOutValue);
        }
    }
}
