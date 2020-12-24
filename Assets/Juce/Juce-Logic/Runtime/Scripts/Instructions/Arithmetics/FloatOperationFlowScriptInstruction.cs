using Juce.Logic.Enums;
using Juce.Logic.Ports;
using System;

namespace Juce.Logic.Instructions
{
    [Serializable]
    public class FloatOperationFlowScriptInstruction : FlowScriptInstruction
    {
        public InScriptPort OperationType { get; }
        public InScriptPort Value1In { get; }
        public InScriptPort Value2In { get; }
        public OutScriptPort ResultOut { get; }

        public FloatOperationFlowScriptInstruction()
        {
            OperationType = AddInPort();
            Value1In = AddInPort();
            Value2In = AddInPort();
            ResultOut = AddOutPort();
        }

        public override void Execute()
        {
            FloatOperationType operationType = GetInputValue<FloatOperationType>(OperationType);

            float value1 = GetInputValue<float>(Value1In);
            float value2 = GetInputValue<float>(Value2In);

            float resultValue = PerformOperation(operationType, value1, value2);

            SetOutputValue(ResultOut, resultValue);
        }

        private float PerformOperation(FloatOperationType operationType, float value1, float value2)
        {
            switch (operationType)
            {
                case FloatOperationType.Add:
                    {
                        return value1 + value2;
                    }

                case FloatOperationType.Substract:
                    {
                        return value1 - value2;
                    }

                case FloatOperationType.Multiply:
                    {
                        return value1 * value2;
                    }

                case FloatOperationType.Divide:
                    {
                        if (value1 == 0)
                        {
                            return 0;
                        }

                        return value1 / value2;
                    }

                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}
