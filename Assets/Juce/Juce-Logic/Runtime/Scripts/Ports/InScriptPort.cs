using Juce.Logic.Instructions;
using System;

namespace Juce.Logic.Ports
{
    [System.Serializable]
    public class InScriptPort : ScriptPort
    { 
        public OutScriptPort Connection { get; set ; }
        public object FallbackValue { get; private set; }

        public InScriptPort(ScriptInstruction instruction) : base(instruction)
        {

        }

        public void Connect(OutScriptPort outPort)
        {
            Connection = outPort;
            outPort.Connection = this;
        }

        public void SetFallbackValue(object value)
        {
            FallbackValue = value;
        }
    }
}
