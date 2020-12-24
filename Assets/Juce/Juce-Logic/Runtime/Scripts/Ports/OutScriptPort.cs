using Juce.Logic.Instructions;
using System;

namespace Juce.Logic.Ports
{
    [System.Serializable]
    public class OutScriptPort : ScriptPort
    {
        public InScriptPort Connection { get; set; }
        public object Value { get; set; }

        public OutScriptPort(ScriptInstruction instruction) : base(instruction)
        {

        }

        public void Connect(InScriptPort inPort)
        {
            Connection = inPort;
            inPort.Connection = this;
        }
    }
}
