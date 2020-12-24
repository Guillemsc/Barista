using Juce.Logic.Flow;
using Juce.Logic.Ports;
using System;

namespace Juce.Logic.Instructions
{
    public class StartFlowScriptInstruction : FlowScriptInstruction
    {
        public override void Execute()
        {
            FlowScriptInstruction currFlow = Flow;

            while(currFlow != null)
            {
                currFlow.Execute();

                currFlow = currFlow.Flow;
            }
        }
    }
}
