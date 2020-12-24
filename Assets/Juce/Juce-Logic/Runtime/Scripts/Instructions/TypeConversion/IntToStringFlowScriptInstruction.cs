using Juce.Logic.Ports;
using System;

namespace Juce.Logic.Instructions
{
    public class IntToStringFlowScriptInstruction : FlowScriptInstruction
    {
        public InScriptPort IntIn { get; }
        public OutScriptPort StringOut { get; }

        public IntToStringFlowScriptInstruction()
        {
            IntIn = AddInPort();
            StringOut = AddOutPort();
        }

        public override void Execute()
        {
            int intInValue = GetInputValue<int>(IntIn);

            string stringOutValue = intInValue.ToString();

            SetOutputValue(StringOut, stringOutValue);
        }
    }
}
