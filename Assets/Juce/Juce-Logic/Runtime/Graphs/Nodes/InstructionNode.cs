using Juce.Logic.Instructions;
using Juce.Logic.Ports;
using System.Collections.Generic;
using System.Linq;
using XNode;

namespace Juce.Logic.Nodes
{
    public abstract class InstructionNode : Node
    {
        private readonly Dictionary<NodePort, InScriptPort> inputScriptLinks = new Dictionary<NodePort, InScriptPort>();
        private readonly Dictionary<NodePort, OutScriptPort> outputScriptLinks = new Dictionary<NodePort, OutScriptPort>();

        public List<NodePort> InputScriptLinks => inputScriptLinks.Keys.ToList();

        public abstract ScriptInstruction ScriptInstruction { get; }

        public override object GetValue(NodePort port)
        {
            return null;
        }

        protected void LinkInputScriptPort(string id, InScriptPort port, object fallback)
        {
            if (port == null)
            {
                UnityEngine.Debug.LogError($"Tried to link input script port, with id '{id}' for a {nameof(InScriptPort)} that " +
                    $"was not created, at {GetType().Name}");

                return;
            }

            port.SetFallbackValue(fallback);

            inputScriptLinks.Add(GetInputPort(id), port);
        }

        protected void LinkOutputScriptPort(string id, OutScriptPort port)
        {
            if (port == null)
            {
                UnityEngine.Debug.LogError($"Tried to link output script port, with id '{id}' for a {nameof(OutScriptPort)} that " +
                    $"was not created, at {GetType().Name}");

                return;
            }

            outputScriptLinks.Add(GetOutputPort(id), port);
        }

        public InScriptPort GetInputScriptPort(NodePort nodePort)
        {
            bool found = inputScriptLinks.TryGetValue(nodePort, out InScriptPort port);

            if(!found)
            {
                return null;
            }

            return port;
        }

        public OutScriptPort GetOutputScriptPort(NodePort nodePort)
        {
            bool found = outputScriptLinks.TryGetValue(nodePort, out OutScriptPort port);

            if (!found)
            {
                return null;
            }

            return port;
        }

        public abstract void LinkScriptPorts();
    }
}
