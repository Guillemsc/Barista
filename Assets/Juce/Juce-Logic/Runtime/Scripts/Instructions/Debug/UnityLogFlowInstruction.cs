using Juce.Logic.Ports;

namespace Juce.Logic.Instructions
{
    public class UnityLogFlowInstruction : FlowScriptInstruction
    {
        public InScriptPort Message { get; }

        public UnityLogFlowInstruction()
        {
            Message = AddInPort();
        }

        public override void Execute()
        {
            string message = GetInputValue<string>(Message);

            UnityEngine.Debug.Log(message);
        }
    }
}
