using System.Collections.Generic;
using BehaviorTrees;

namespace BehaviorTrees
{
    public class Sequence : Node
    {
        
        List<Node> children;

        public Sequence(List<Node> children)
        {
            this.children = children;
        }

        public override NodeState Evaluate()
        {
            foreach (Node child in children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Failure:
                    {
                        nodeState = NodeState.Failure;
                        return NodeState.Failure;
                    }
                    case NodeState.Success:
                    {
                        continue;
                    }
                    case NodeState.Running:
                    {
                        nodeState = NodeState.Running;
                        return NodeState.Running;
                    }
                }
            }
            return nodeState;
        }
        public override bool Ready()
        {
            foreach (Node child in children)
            {
                if (!child.Ready())
                {
                    nodeState = NodeState.NotReady;
                    return false;
                }
            }

            nodeState = NodeState.Ready;
            return true;

        }
    }
}