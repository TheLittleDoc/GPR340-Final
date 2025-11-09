using System.Collections.Generic;

namespace BehaviorTrees
{
    public class Selector : Node
    {
        List<Node> children;
        
        public Selector(List<Node> nodes) { 
            children = nodes; 
        }

        public override NodeState Evaluate()
        {
            if(nodeState == NodeState.Ready)
                nodeState = NodeState.Running;
            foreach (Node child in children)
            {
                
                    switch (child.Evaluate())
                    {
                        case NodeState.Failure:
                            continue;
                        case NodeState.Success:
                        {
                            nodeState = NodeState.Success;
                            return nodeState;
                        }
                        case NodeState.Running:
                        {
                            nodeState = NodeState.Running;
                            return nodeState;
                        }
                        default:
                            continue;
                    
                }
            }
            return nodeState;
        }

        public override bool Ready()
        {
            foreach (Node child in children)
            {
                if (!child.Ready())
                    return false;
                

            }
            nodeState = NodeState.Ready;
            return true;
        }
    }
}