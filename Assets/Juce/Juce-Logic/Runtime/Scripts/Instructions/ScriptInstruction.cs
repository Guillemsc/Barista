using Juce.Logic.Ports;
using System.Collections.Generic;

namespace Juce.Logic.Instructions
{
    [System.Serializable]
    public abstract class ScriptInstruction
    {
        public InScriptPort AddInPort()
        {
            return new InScriptPort(this);
        }

        public OutScriptPort AddOutPort()
        {
            return new OutScriptPort(this);
        }

        public void SetOutputValue(OutScriptPort outPort, object value)
        {
            if(outPort == null)
            {
                UnityEngine.Debug.LogError($"Tried to set output value for a {nameof(OutScriptPort)} that " +
                    $"was not created, at {GetType().Name}");

                return;
            }

            outPort.Value = value;
        }

        public T GetInputValue<T>(InScriptPort inPort)
        {
            if(inPort == null)
            {
                UnityEngine.Debug.LogError($"Tried to get input value for a {nameof(InScriptPort)} that " +
                    $"was not created, at {GetType().Name}");

                return default;
            }

            if(inPort.Connection == null)
            {
                return (T)inPort.FallbackValue;
            }

            inPort.Connection.Instruction.Execute();

            bool isWrongType = !(inPort.Connection.Value is T);

            if (isWrongType)
            {
                return default;
            }

            return (T)inPort.Connection.Value;
        }

        public abstract void Execute();
    }
}
