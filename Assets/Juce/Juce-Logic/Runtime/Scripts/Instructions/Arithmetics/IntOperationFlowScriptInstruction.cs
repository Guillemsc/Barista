using Juce.Logic.Enums;
using Juce.Logic.Ports;
using System;

namespace Juce.Logic.Instructions
{
    [Serializable]
    public class IntOperationFlowScriptInstruction : FlowScriptInstruction
    {
        public InScriptPort OperationType { get; }
        public InScriptPort Value1In { get; }
        public InScriptPort Value2In { get; }
        public OutScriptPort ResultOut { get; }

        public IntOperationFlowScriptInstruction()
        {
            OperationType = AddInPort();
            Value1In = AddInPort();
            Value2In = AddInPort();
            ResultOut = AddOutPort();
        }

        public override void Execute()
        {
            IntOperationType operationType = GetInputValue<IntOperationType>(OperationType);

            int value1 = GetInputValue<int>(Value1In);
            int value2 = GetInputValue<int>(Value2In);

            int resultValue = PerformOperation(operationType, value1, value2);

            SetOutputValue(ResultOut, resultValue);
        }

        private int PerformOperation(IntOperationType operationType, int value1, int value2)
        {
            switch(operationType)
            {
                case IntOperationType.Add:
                    {
                        return value1 + value2;
                    }

                case IntOperationType.Substract:
                    {
                        return value1 - value2;
                    }

                case IntOperationType.Multiply:
                    {
                        return value1 * value2;
                    }

                case IntOperationType.Divide:
                    {
                        if(value2 == 0)
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
