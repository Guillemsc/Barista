using Juce.Logic.Instructions;
using XNode;

namespace Juce.Logic.Nodes
{
    public class StartFlowNode : FlowNode
    {
        private readonly StartFlowScriptInstruction startFlowInstruction = new StartFlowScriptInstruction();

        public override FlowScriptInstruction FlowScriptInstruction => startFlowInstruction;

        public override void LinkScriptPorts()
        {
           
        }
    }
}
