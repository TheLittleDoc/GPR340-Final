namespace BehaviorTrees
{
    public class ActionNode : Node
    {
        /* Method signature for the action. */ 
        public delegate NodeState ActionNodeDelegate(); 
 
        /* The delegate that is called to evaluate this node */ 
        private ActionNodeDelegate action;

        public ActionNode(ActionNodeDelegate action)
        {
            this.action = action;
        }

        public override NodeState Evaluate()
        {
            if(nodeState == NodeState.Ready)
                nodeState = NodeState.Running;
            switch (action())
            {
                case NodeState.Failure:
                {
                    nodeState = NodeState.Failure;
                    break;
                }
                case NodeState.Success:
                {
                    nodeState = NodeState.Success;
                    break;
                }
                case NodeState.Running:
                {
                    nodeState = NodeState.Running;
                    break;
                }
                default:
                {
                    nodeState = NodeState.Failure;
                    break;
                }
                    
            }
            return nodeState;
        
        }

        public override bool Ready()
        {
            nodeState = NodeState.Ready;
            return true;
        }
    }
}