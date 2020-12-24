using Juce.Logic.Graphs;
using Juce.Logic.Instructions;
using Juce.Logic.Nodes;
using Juce.Logic.Ports;
using System;
using System.Collections.Generic;
using XNode;

namespace Juce.Logic.Compiler
{
    public class LogicGraphCompiler
    {
        private readonly LogicGraph logicGraph;

        public LogicGraphCompiler(LogicGraph logicGraph)
        {
            this.logicGraph = logicGraph;
        }

        public ScriptExecutor Compile()
        {
            LinkAllScriptPorts();

            StartFlowNode startFlowNode = GetNode<StartFlowNode>();

            FlowNode currentFlowNode = startFlowNode;

            while (currentFlowNode != null)
            {
                FlowNode lastFlowNode = currentFlowNode;

                bool found = TryGetNextFlowNode(lastFlowNode, out currentFlowNode);

                if (!found)
                {
                    break;
                }

                LinkFlowNodes(lastFlowNode, currentFlowNode);

                LinkInstructionNode(currentFlowNode);
            }

            IReadOnlyList<ScriptInstruction> scriptInstructions = GetAllScriptInstructions();

            return new ScriptExecutor(
                startFlowNode.FlowScriptInstruction,
                scriptInstructions
                );
        }

        public bool TryGetNextFlowNode(FlowNode flowNode, out FlowNode nextFlowNode)
        {
            NodePort nodePort = flowNode.GetOutputPort(nameof(flowNode.FlowOut));

            if(nodePort.Connection == null)
            {
                nextFlowNode = null;
                return false;
            }

            nextFlowNode = nodePort.Connection.node as FlowNode;

            return true;
        }

        public bool TryGetInstructionNodeConnection(NodePort port, out InstructionNode instructionNode)
        {
            if(port.Connection == null)
            {
                instructionNode = null;
                return false;
            }

            instructionNode = port.Connection.node as InstructionNode;

            if(instructionNode == null)
            {
                return false;
            }

            return true;
        }

        public void LinkAllScriptPorts()
        {
            foreach (Node node in logicGraph.nodes)
            {
                InstructionNode instructionNode = node as InstructionNode;

                if (instructionNode == null)
                {
                    continue;
                }

                instructionNode.LinkScriptPorts();
            }
        }

        public void LinkFlowNodes(FlowNode flowNode1, FlowNode flowNode2)
        {
            flowNode1.FlowScriptInstruction.Flow = flowNode2.FlowScriptInstruction;
        }

        public void LinkInstructionNode(InstructionNode instructionNode)
        {
            foreach (NodePort inputPort in instructionNode.InputScriptLinks)
            {
                bool found = TryGetInstructionNodeConnection(inputPort, out InstructionNode connectedNode);

                if(!found)
                {
                    continue;
                }

                LinkInstructionNode(connectedNode);

                LinkPort(instructionNode, connectedNode, inputPort);
            }
        }

        public void LinkPort(InstructionNode instructionNode, InstructionNode connectedNode, NodePort inputPort)
        {
            InScriptPort inPort = instructionNode.GetInputScriptPort(inputPort);

            if(inPort == null)
            {
                return;
            }

            OutScriptPort outPort = connectedNode.GetOutputScriptPort(inputPort.Connection);

            if(outPort == null)
            {
                return;
            }

            inPort.Connect(outPort);
        }

        public T GetNode<T>() where T : Node
        {
            Type lookingType = typeof(T);

            foreach(Node node in logicGraph.nodes)
            {
                if(node.GetType() == lookingType)
                {
                    return node as T;
                }
            }

            return null;
        }

        public List<ScriptInstruction> GetAllScriptInstructions()
        {
            List<ScriptInstruction> ret = new List<ScriptInstruction>();

            foreach (Node node in logicGraph.nodes)
            {
                InstructionNode instructionNode = node as InstructionNode;

                if (instructionNode == null)
                {
                    continue;
                }

                ret.Add(instructionNode.ScriptInstruction);
            }

            return ret;
        }
    }
}
